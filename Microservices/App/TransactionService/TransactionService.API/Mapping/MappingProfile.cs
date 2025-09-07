using AutoMapper;
using TransactionService.Contracts.DTOs;
using TransactionService.Data.Entities;

namespace TransactionService.API.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Transaction, TransactionDto>();
        CreateMap<CreateTransactionDto, Transaction>();
    }
}