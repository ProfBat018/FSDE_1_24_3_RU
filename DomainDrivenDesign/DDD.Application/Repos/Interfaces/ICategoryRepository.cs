using DDD.Application.Services.Interfaces;
using DDD.Domain.Models;

namespace DDD.Application.Repos.Interfaces;

public interface ICategoryRepository : IRepository<Category>
{
    // Add custom methods for Category if needed
}
