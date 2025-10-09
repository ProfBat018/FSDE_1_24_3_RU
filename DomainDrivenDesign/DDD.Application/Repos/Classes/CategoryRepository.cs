using DDD.Application.Repos.Interfaces;
using DDD.Domain.Models;
using DDD.Infrastructure.Contexts;

namespace DDD.Application.Repos.Classes;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(EcommerceDbContext context) : base(context) { }
}
