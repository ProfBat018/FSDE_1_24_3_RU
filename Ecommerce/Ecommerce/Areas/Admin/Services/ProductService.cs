using AutoMapper;
using Bogus.Extensions.UnitedKingdom;
using Ecommerce.Shared.DTOs.Request;
using Ecommerce.Shared.DTOs.Response;
using Microsoft.EntityFrameworkCore;
using ProductRepository.Contexts;
using ProductRepository.Models;

namespace Ecommerce.Areas.Admin.Services;

public class ProductService
{
    private readonly ProductsContext _context;
    private readonly IMapper _mapper;

    public ProductService(ProductsContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedResult<Product>> GetAllProductsAsync(int page, int pageSize)
    {
        var products = _context.Products.Skip((page - 1) * pageSize).Take(pageSize);
        var totalItems = await _context.Products.AsNoTracking().CountAsync();
        
        return PaginatedResult<Product>.Success(await products.ToListAsync(), totalItems, page, pageSize);
    }

    public async Task<TypedResult<Product>> GetProductAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> CreateProductAsync(CreateProductDto request)
    {
        var product = _mapper.Map<CreateProductDto, Product>(request);
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
        return Result.Success("Product created");
    }

    public async Task<Result> UpdateProductAsync(UpdateProductDto request)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> DeleteProductAsync(string id)
    {
        throw new NotImplementedException();
    }
}