using DDD.Application.Repos.Interfaces;
using DDD.Domain.Models;
using DDD.Infrastructure.Contexts;

namespace DDD.Application.Repos.Classes;

public class OrderProductRepository : Repository<OrderProduct>, IOrderProductRepository
{
    public OrderProductRepository(EcommerceDbContext context) : base(context) { }
}
