using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Ecommerce.Shared.DTOs.Request;
using ProductRepository.Models;
using Attribute = ProductRepository.Models.Attribute;

namespace Ecommerce.Mapping;

public class DtoProfile : Profile
{
    public DtoProfile()
    {
         CreateMap<Product, ProductListItemDto>();

        CreateMap<Product, ProductDto>()
            .ForMember(d => d.CategoryIds, o => o.MapFrom(s =>
                (s.ProductCategories ?? Array.Empty<ProductCategory>()).Select(pc => pc.CategoryId).ToList()))
            .ForMember(d => d.Images, o => o.MapFrom(s => s.ProductImages ?? new List<ProductImage>()));

        CreateMap<CreateProductDto, Product>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.ProductCategories, o => o.MapFrom(s =>
                (s.CategoryIds ?? Array.Empty<string>()).Select(cid => new ProductCategory
                {
                    CategoryId = cid
                })))
            .ForMember(d => d.ProductImages, o => o.MapFrom(s =>
                (s.Images ?? Array.Empty<CreateProductImageDto>()).Select(img => new ProductImage
                {
                    ImageName = img.ImageName,
                    IsMain = img.IsMain,
                    ImagePath = img.ImagePath
                })));

        CreateMap<UpdateProductDto, Product>()
            .ForMember(d => d.ProductCategories, o => o.Ignore()) // обновление связей — вручную
            .ForMember(d => d.ProductImages, o => o.Ignore());     // обновление изображений — вручную

        // ---------- ProductImage ----------
        CreateMap<ProductImage, ProductImageDto>();
        CreateMap<CreateProductImageDto, ProductImage>()
            .ForMember(d => d.ProductId, o => o.Ignore());
        CreateMap<UpdateProductImageDto, ProductImage>()
            .ForMember(d => d.ProductId, o => o.Ignore());

        // ---------- Vendor ----------
        CreateMap<Vendor, VendorDto>();
        CreateMap<CreateVendorDto, Vendor>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.Products, o => o.Ignore());
        CreateMap<UpdateVendorDto, Vendor>()
            .ForMember(d => d.Products, o => o.Ignore());

        // ---------- Category ----------
        CreateMap<Category, CategoryDto>();
        CreateMap<CreateCategoryDto, Category>()
            .ForMember(d => d.ProductCategories, o => o.Ignore())
            .ForMember(d => d.CategoryAttributes, o => o.Ignore());
        CreateMap<UpdateCategoryDto, Category>()
            .ForMember(d => d.ProductCategories, o => o.Ignore())
            .ForMember(d => d.CategoryAttributes, o => o.Ignore());

        // ---------- ProductCategory ----------
        CreateMap<ProductCategory, ProductCategoryDto>();
        CreateMap<CreateProductCategoryDto, ProductCategory>()
            .ForMember(d => d.ProductCategoryId, o => o.Ignore());

        // ---------- Warehouse ----------
        CreateMap<Warehouse, WarehouseDto>();
        CreateMap<CreateWarehouseDto, Warehouse>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.Hubs, o => o.Ignore());
        CreateMap<UpdateWarehouseDto, Warehouse>()
            .ForMember(d => d.Hubs, o => o.Ignore());

        // ---------- ProductWarehouse ----------
        CreateMap<ProductWarehouse, ProductWarehouseDto>();
        CreateMap<CreateProductWarehouseDto, ProductWarehouse>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.Warehouse, o => o.Ignore())
            .ForMember(d => d.Product, o => o.Ignore());
        CreateMap<UpdateProductWarehouseDto, ProductWarehouse>()
            .ForMember(d => d.Warehouse, o => o.Ignore())
            .ForMember(d => d.Product, o => o.Ignore());

        // ---------- HubObject ----------
        CreateMap<HubObject, HubObjectDto>();
        CreateMap<CreateHubObjectDto, HubObject>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.ProductWarehouse, o => o.Ignore());
        CreateMap<UpdateHubObjectDto, HubObject>()
            .ForMember(d => d.ProductWarehouse, o => o.Ignore());

        // ---------- Order ----------
        CreateMap<Order, OrderDto>();
        CreateMap<CreateOrderDto, Order>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.Hub, o => o.Ignore());
        CreateMap<UpdateOrderDto, Order>()
            .ForMember(d => d.Hub, o => o.Ignore())
            .ForMember(d => d.UserId, o => o.Ignore())
            .ForMember(d => d.HubId, o => o.Ignore());

        // ---------- OrderProduct ----------
        CreateMap<OrderProduct, OrderProductDto>();
        CreateMap<CreateOrderProductDto, OrderProduct>();
        CreateMap<UpdateOrderProductDto, OrderProduct>();

        // ---------- Attribute ----------
        CreateMap<Attribute, AttributeDto>();
        CreateMap<CreateAttributeDto, Attribute>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.Categories, o => o.Ignore())
            .ForMember(d => d.AttributeValues, o => o.Ignore())
            .ForMember(d => d.CategoryAttributes, o => o.Ignore());
        CreateMap<UpdateAttributeDto, Attribute>()
            .ForMember(d => d.Categories, o => o.Ignore())
            .ForMember(d => d.AttributeValues, o => o.Ignore())
            .ForMember(d => d.CategoryAttributes, o => o.Ignore());

        // ---------- AttributeValue ----------
        CreateMap<AttributeValue, AttributeValueDto>();
        CreateMap<CreateAttributeValueDto, AttributeValue>();
        CreateMap<UpdateAttributeValueDto, AttributeValue>();

        // ---------- CategoryAttributes ----------
        CreateMap<CategoryAttributes, CategoryAttributesDto>();
        CreateMap<CreateCategoryAttributesDto, CategoryAttributes>();
    }
}
