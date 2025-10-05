using DDD.Application.Repos.Interfaces;
using DDD.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Attribute = DDD.Domain.Models.Attribute;

namespace DDD.Application.Repos.Classes;

class AttributeRepository : Repository<Attribute>, IAttributeRepository
{
    private readonly EcommerceDbContext _context;
    public AttributeRepository(EcommerceDbContext context) : base(context)
    {
        _context = context;
    }
    public async Task<Attribute>? FindByIdAsync(int id)
    {
        return await _context.Attributes.FindAsync(id);
    }

    public Task Update(Attribute category)
    {
        throw new NotImplementedException();
    }
}