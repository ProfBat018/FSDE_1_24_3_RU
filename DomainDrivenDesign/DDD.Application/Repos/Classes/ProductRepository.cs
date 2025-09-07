using DDD.Application.Repos.Interfaces;
using DDD.Domain.Models;
using DDD.Infrastructure.Contexts;

namespace DDD.Application.Repos.Classes;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(EcommerceDbContext context) : base(context) { }
}
