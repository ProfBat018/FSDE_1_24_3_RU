using DDD.Application.Repos.Interfaces;
using DDD.Domain.Models;
using DDD.Infrastructure.Contexts;

namespace DDD.Application.Repos.Classes;

public class ProductCategoryRepository : Repository<ProductCategory>, IProductCategoryRepository
{
    public ProductCategoryRepository(EcommerceDbContext context) : base(context) { }
}
