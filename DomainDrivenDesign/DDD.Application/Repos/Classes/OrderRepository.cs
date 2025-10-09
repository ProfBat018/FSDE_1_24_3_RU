using DDD.Application.Repos.Interfaces;
using DDD.Domain.Models;
using DDD.Infrastructure.Contexts;

namespace DDD.Application.Repos.Classes;

public class OrderRepository : Repository<Order>, IOrderRepository
{
    public OrderRepository(EcommerceDbContext context) : base(context) { }
}
