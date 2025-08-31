namespace Ecommerce;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using Microsoft.EntityFrameworkCore;
using ProductRepository.Contexts;
using ProductRepository.Models;

public static class ProductsContextSeeder
{
    public static async Task SeedAsync(ProductsContext ctx,
        int vendorsCount = 8,
        int categoriesCount = 12,
        int productsCount = 120,
        int warehousesCount = 4,
        int ordersCount = 60)
    {
        // Делаем сид детерминированным
        Randomizer.Seed = new Random(12345);

        // Ничего не делаем, если уже сидировано (по любому из крупных наборов)
        if (await ctx.Products.AnyAsync() || await ctx.Vendors.AnyAsync())
            return;

        // ---- Vendors
        var vendorFaker = new Faker<Vendor>("en")
            .RuleFor(v => v.Id, f => Guid.NewGuid().ToString())
            .RuleFor(v => v.VendorName, f => f.Company.CompanyName());

        var vendors = vendorFaker.Generate(vendorsCount);
        await ctx.Vendors.AddRangeAsync(vendors);

        // ---- Categories (Name = PK/ID)
        // Сделаем немного иерархии (часть с ParentCategoryName = null)
        var rootCategoryNames = new[] { "Electronics", "Appliances", "Fashion", "Sports", "Home", "Toys" };
        var categories = new List<Category>();

        // Корневые
        foreach (var name in rootCategoryNames.Take(Math.Max(3, rootCategoryNames.Length / 2)))
        {
            categories.Add(new Category
            {
                CategoryName = name,
                ParentCategoryName = null
            });
        }

        // Дочерние + случайные
        var childNameFaker = new Faker().Commerce.Categories(1);
        while (categories.Count < categoriesCount)
        {
            var parent = categories[Random.Shared.Next(Math.Min(3, categories.Count))]; // чаще к корневым
            var childName = new Faker().Commerce.Categories(1).First();
            if (categories.Any(c => c.CategoryName == childName)) continue;

            categories.Add(new Category
            {
                CategoryName = childName,
                ParentCategoryName = parent.CategoryName
            });
        }
        await ctx.Categories.AddRangeAsync(categories);

        // ---- Attributes
        // Базовые атрибуты (не уникальные по имени, но мы сделаем уникальные для удобства)
        var attributeNames = new[]
        {
            "Color","Size","Weight","Brand","Material","Capacity","Speed","Power","Length","Width"
        };

        var attributes = attributeNames.Select(n => new ProductRepository.Models.Attribute
        {
            Id = Guid.NewGuid().ToString(),
            AttributeName = n
        }).ToList();
        await ctx.Attributes.AddRangeAsync(attributes);

        // ---- AttributeValues (Value UNIQUE)
        // Подготовим пул значений для разных атрибутов
        var colorValues = new[] { "Black","White","Silver","Red","Blue","Green","Yellow","Pink","Gray" };
        var sizeValues = new[] { "XS","S","M","L","XL","XXL" };
        var weightValues = new[] { "100g","250g","500g","1kg","2kg","5kg" };
        var brandValues = vendors.Select(v => v.VendorName).Distinct().Take(12).ToList();
        var materialValues = new[] { "Plastic","Metal","Wood","Glass","Leather","Textile" };
        var capacityValues = new[] { "32GB","64GB","128GB","256GB","512GB","1TB","2TB" };
        var speedValues = new[] { "100Mbps","300Mbps","1Gbps","2.5Gbps","5Gbps","10Gbps" };
        var powerValues = new[] { "5W","10W","25W","60W","90W","120W","500W","750W" };
        var lengthValues = new[] { "10cm","25cm","50cm","1m","2m","3m" };
        var widthValues = new[] { "5cm","10cm","20cm","30cm","40cm","50cm" };

        var valuesByAttr = new Dictionary<string, IEnumerable<string>>(StringComparer.OrdinalIgnoreCase)
        {
            ["Color"] = colorValues,
            ["Size"] = sizeValues,
            ["Weight"] = weightValues,
            ["Brand"] = brandValues,
            ["Material"] = materialValues,
            ["Capacity"] = capacityValues,
            ["Speed"] = speedValues,
            ["Power"] = powerValues,
            ["Length"] = lengthValues,
            ["Width"] = widthValues
        };

        var attributeValues = new List<AttributeValue>();
        var usedValues = new HashSet<string>(StringComparer.OrdinalIgnoreCase); // соблюдаем уникальность Value

        foreach (var attr in attributes)
        {
            var pool = valuesByAttr.TryGetValue(attr.AttributeName, out var list)
                ? list
                : new Faker().Make(10, () => new Faker().Commerce.ProductAdjective());

            foreach (var val in pool)
            {
                if (usedValues.Add(val))
                {
                    attributeValues.Add(new AttributeValue
                    {
                        AttributeId = attr.Id,
                        Value = val
                    });
                }
            }
        }
        await ctx.AttributeValues.AddRangeAsync(attributeValues);

        // ---- CategoryAttributes (свяжем категории с частью атрибутов)
        var catAttrs = new List<CategoryAttributes>();
        var rnd = new Random();
        foreach (var cat in categories)
        {
            var take = rnd.Next(3, Math.Min(6, attributes.Count));
            foreach (var attr in attributes.OrderBy(_ => rnd.Next()).Take(take))
            {
                catAttrs.Add(new CategoryAttributes
                {
                    CategoryId = cat.CategoryName,   // CategoryId -> CategoryName
                    AttributeId = attr.Id
                });
            }
        }
        await ctx.AddRangeAsync(catAttrs);

        // ---- Warehouses
        var warehouseFaker = new Faker<Warehouse>("en")
            .RuleFor(w => w.Id, _ => Guid.NewGuid().ToString())
            .RuleFor(w => w.Address, f => $"{f.Address.City()}, {f.Address.StreetAddress()}");

        var warehouses = warehouseFaker.Generate(warehousesCount);
        await ctx.Warehouses.AddRangeAsync(warehouses);

        // ---- Products
        var productFaker = new Faker<Product>("en")
            .RuleFor(p => p.Id, _ => Guid.NewGuid().ToString())
            .RuleFor(p => p.ProductName, f => f.Commerce.ProductName())
            .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
            .RuleFor(p => p.VendorId, f => f.PickRandom(vendors).Id);

        var products = productFaker.Generate(productsCount);
        await ctx.Products.AddRangeAsync(products);

        // ---- ProductImages (1–4 на продукт, одна — IsMain)
        var productImages = new List<ProductImage>();
        foreach (var p in products)
        {
            var count = rnd.Next(1, 5);
            var mainIndex = rnd.Next(count);

            for (int i = 0; i < count; i++)
            {
                var fileName = $"{p.Id}_{i + 1:D2}.jpg"; // ImageName = PK (уникальный)
                productImages.Add(new ProductImage
                {
                    ImageName = fileName,
                    ProductId = p.Id,
                    IsMain = (i == mainIndex),
                    ImagePath = $"/images/products/{fileName}"
                });
            }
        }
        await ctx.ProductImages.AddRangeAsync(productImages);

        // ---- ProductCategory (по 1–3 категории на товар)
        var productCategories = new List<ProductCategory>();
        foreach (var p in products)
        {
            var take = rnd.Next(1, Math.Min(4, categories.Count));
            foreach (var c in categories.OrderBy(_ => rnd.Next()).Take(take))
            {
                productCategories.Add(new ProductCategory
                {
                    ProductCategoryId = Guid.NewGuid().ToString(),
                    ProductId = p.Id,
                    CategoryId = c.CategoryName // CategoryId -> CategoryName
                });
            }
        }
        await ctx.ProductCategories.AddRangeAsync(productCategories);

        // ---- ProductWarehouse (каждый продукт не обязательно в каждом складе)
        var productWarehouses = new List<ProductWarehouse>();
        foreach (var p in products)
        {
            // В 1–3 склада
            var takeWh = rnd.Next(1, Math.Min(4, warehouses.Count));
            foreach (var wh in warehouses.OrderBy(_ => rnd.Next()).Take(takeWh))
            {
                productWarehouses.Add(new ProductWarehouse
                {
                    Id = Guid.NewGuid().ToString(),
                    ProductId = p.Id,
                    WarehouseId = wh.Id,
                    Count = rnd.Next(0, 150), // допускаем 0
                    Price = Math.Round(new Faker().Random.Double(5, 1500), 2)
                });
            }
        }
        await ctx.ProductWarehouses.AddRangeAsync(productWarehouses);

        // ---- HubObject (по 1–3 хаба на склад, линкуем к случайному ProductWarehouse)
        var hubs = new List<HubObject>();
        foreach (var wh in warehouses)
        {
            var relatedPWs = productWarehouses.Where(pw => pw.WarehouseId == wh.Id).ToList();
            if (relatedPWs.Count == 0) continue;

            var hubCount = rnd.Next(1, 4);
            for (int i = 0; i < hubCount; i++)
            {
                var pw = relatedPWs[rnd.Next(relatedPWs.Count)];
                hubs.Add(new HubObject
                {
                    Id = Guid.NewGuid().ToString(),
                    HubName = $"Hub-{wh.Address.Split(',')[0]}-{i + 1}",
                    ProductWarehouseId = pw.Id
                });
            }
        }
        await ctx.Hubs.AddRangeAsync(hubs);

        // ---- Orders & OrderProducts
        // Генерируем заказы по хабам, TotalPrice посчитаем от ProductWarehouse.Price * qty (берём любую запись для ProductId)
        var orders = new List<Order>();
        var orderProducts = new List<OrderProduct>();

        if (hubs.Count > 0)
        {
            var usersFaker = new Faker().Internet.UserName;
            for (int i = 0; i < ordersCount; i++)
            {
                var hub = hubs[rnd.Next(hubs.Count)];
                var order = new Order
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = usersFaker(), // просто фейковый юзернейм
                    HubId = hub.Id,
                    TotalPrice = 0 // заполним ниже
                };

                // 1–5 позиций в заказе
                var lineCount = rnd.Next(1, 6);
                var chosenProducts = products.OrderBy(_ => rnd.Next()).Take(lineCount).ToList();

                double sum = 0;
                foreach (var prod in chosenProducts)
                {
                    var qty = rnd.Next(1, 5);
                    // Цена — из любого ProductWarehouse для данного товара
                    var pw = productWarehouses.FirstOrDefault(x => x.ProductId == prod.Id);
                    var price = pw?.Price ?? Math.Round(new Faker().Random.Double(5, 1500), 2);

                    sum += price * qty;

                    orderProducts.Add(new OrderProduct
                    {
                        OrderId = order.Id,
                        ProductId = prod.Id,
                        ProductCount = qty
                    });
                }

                order.TotalPrice = Math.Round(sum, 2);
                orders.Add(order);
            }
        }

        await ctx.Orders.AddRangeAsync(orders);
        await ctx.OrderProducts.AddRangeAsync(orderProducts);

        await ctx.SaveChangesAsync();
    }
}
