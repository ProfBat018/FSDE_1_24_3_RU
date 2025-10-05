using System.Threading.Tasks;
using DDD.Application.Repos.Interfaces;

namespace DDD.Application.Repos.Interfaces;

public interface IUnitOfWork
{
    IAttributeRepository AttributeRepository { get; }
    IProductRepository ProductRepository { get; }
    IProductCategoryRepository ProductCategoryRepository { get; }
    IProductImageRepository ProductImageRepository { get; }
    IProductWarehouseRepository ProductWarehouseRepository { get; }
    IVendorRepository VendorRepository { get; }
    IWarehouseRepository WarehouseRepository { get; }
    IOrderRepository OrderRepository { get; }
    IOrderProductRepository OrderProductRepository { get; }
    ICategoryRepository CategoryRepository { get; }
    ICategoryAttributesRepository CategoryAttributesRepository { get; }

    Task<int> SaveAsync();
}
