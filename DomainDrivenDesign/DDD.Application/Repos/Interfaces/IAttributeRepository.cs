using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Attribute = DDD.Domain.Models.Attribute;
using System.Threading.Tasks;
using DDD.Application.Services.Interfaces;

namespace DDD.Application.Repos.Interfaces;

public interface IAttributeRepository : IRepository<Attribute>
{
    Task Update(Attribute category);
    Task<Attribute>? FindByIdAsync(int id);
    public  Task<Attribute?> GetByNameAsync(string name);
}
