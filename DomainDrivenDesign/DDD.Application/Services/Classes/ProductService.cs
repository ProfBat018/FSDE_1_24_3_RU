using DDD.Application.Repos.Interfaces;
using DDD.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Application.Services.Classes;

class ProductService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public async Task AddProductAsync(Product product)
    {
        await _unitOfWork.ProductRepository.AddAsync(product);
    }
}
