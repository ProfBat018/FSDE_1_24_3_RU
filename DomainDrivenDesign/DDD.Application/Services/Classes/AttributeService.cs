using DDD.Application.Repos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Attribute = DDD.Domain.Models.Attribute;

namespace DDD.Application.Services.Classes;


public class AttributeService
{
    private readonly IUnitOfWork _unitOfWork;

    public AttributeService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> CreateAttributeAsync(string name, List<string> values)
    {
        var existing = await _unitOfWork.AttributeRepository.GetByNameAsync(name);
        if (existing != null)
            return false; 

        var attribute = new Attribute(name);

        foreach (var value in values)
        {
            try
            {
                attribute.AddValue(value);
            }
            catch
            {
                continue;
            }
        }

        await _unitOfWork.AttributeRepository.AddAsync(attribute);

        await _unitOfWork.SaveAsync();

        return true;
    }

    public async Task<IEnumerable<string>> GetAllAttributeNamesAsync()
    {
        return await _unitOfWork.AttributeRepository.GetAll(
            selectFilter: a => a.AttributeName
        );
    }

    public async Task<Attribute?> GetByNameAsync(string name)
    {
        return await _unitOfWork.AttributeRepository.GetByNameAsync(name);
    }
}