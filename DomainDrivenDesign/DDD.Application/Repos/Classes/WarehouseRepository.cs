using DDD.Application.Repos.Interfaces;
using DDD.Domain.Models;
using DDD.Infrastructure.Contexts;

namespace DDD.Application.Repos.Classes;

public class WarehouseRepository : Repository<Warehouse>, IWarehouseRepository
{
    public WarehouseRepository(EcommerceDbContext context) : base(context) { }
}
