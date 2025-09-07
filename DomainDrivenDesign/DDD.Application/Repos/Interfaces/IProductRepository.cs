using DDD.Application.Services.Interfaces;
using DDD.Domain.Models;

namespace DDD.Application.Repos.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    // Add custom methods for Product if needed
}
