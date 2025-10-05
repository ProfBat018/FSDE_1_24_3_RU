using DDD.Application.Repos.Interfaces;
using DDD.Domain.Models;
using DDD.Infrastructure.Contexts;

namespace DDD.Application.Repos.Classes;

public class ProductWarehouseRepository : Repository<ProductWarehouse>, IProductWarehouseRepository
{
    public ProductWarehouseRepository(EcommerceDbContext context) : base(context) { }
}
