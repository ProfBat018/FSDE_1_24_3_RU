using DDD.Application.Services.Interfaces;
using DDD.Domain.Models;

namespace DDD.Application.Repos.Interfaces;

public interface IOrderRepository : IRepository<Order>
{
    // Add custom methods for Order if needed
}
