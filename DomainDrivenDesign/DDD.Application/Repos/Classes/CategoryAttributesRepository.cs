using DDD.Application.Repos.Interfaces;
using DDD.Domain.Models;
using DDD.Infrastructure.Contexts;

namespace DDD.Application.Repos.Classes;

public class CategoryAttributesRepository : Repository<CategoryAttributes>, ICategoryAttributesRepository
{
    public CategoryAttributesRepository(EcommerceDbContext context) : base(context) { }
}
