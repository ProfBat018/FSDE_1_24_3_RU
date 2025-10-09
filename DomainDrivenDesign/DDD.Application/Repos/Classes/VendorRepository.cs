using DDD.Application.Repos.Interfaces;
using DDD.Domain.Models;
using DDD.Infrastructure.Contexts;

namespace DDD.Application.Repos.Classes;

public class VendorRepository : Repository<Vendor>, IVendorRepository
{
    public VendorRepository(EcommerceDbContext context) : base(context) { }
}
