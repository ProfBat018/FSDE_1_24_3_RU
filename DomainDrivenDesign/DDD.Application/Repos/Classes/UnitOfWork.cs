using DDD.Application.Repos.Interfaces;
using DDD.Infrastructure.Contexts;

namespace DDD.Application.Repos.Classes;

public class UnitOfWork : IUnitOfWork
{
    private readonly EcommerceDbContext _context;

    public IAttributeRepository AttributeRepository { get; }
    public IProductRepository ProductRepository { get; }
    public IProductCategoryRepository ProductCategoryRepository { get; }
    public IProductImageRepository ProductImageRepository { get; }
    public IProductWarehouseRepository ProductWarehouseRepository { get; }
    public IVendorRepository VendorRepository { get; }
    public IWarehouseRepository WarehouseRepository { get; }
    public IOrderRepository OrderRepository { get; }
    public IOrderProductRepository OrderProductRepository { get; }
    public ICategoryRepository CategoryRepository { get; }
    public ICategoryAttributesRepository CategoryAttributesRepository { get; }

    public UnitOfWork(EcommerceDbContext context)
    {
        _context = context;
        AttributeRepository = new AttributeRepository(context);
        ProductRepository = new ProductRepository(context);
        ProductCategoryRepository = new ProductCategoryRepository(context);
        ProductImageRepository = new ProductImageRepository(context);
        ProductWarehouseRepository = new ProductWarehouseRepository(context);
        VendorRepository = new VendorRepository(context);
        WarehouseRepository = new WarehouseRepository(context);
        OrderRepository = new OrderRepository(context);
        OrderProductRepository = new OrderProductRepository(context);
        CategoryRepository = new CategoryRepository(context);
        CategoryAttributesRepository = new CategoryAttributesRepository(context);
    }

    public Task<int> SaveAsync() => _context.SaveChangesAsync();
}
