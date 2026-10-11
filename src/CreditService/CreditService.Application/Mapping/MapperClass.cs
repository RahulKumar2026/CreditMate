using AutoMapper;
using CreditService.Domain.Entites;
using CreditService.Application.DTOs;
using System.Text;

namespace CreditService.Application.Mapper
{
    public class MapperClass : Profile
    {
        public MapperClass() 
        {
            CreateMap<credit_account,CreditAccountResponse>();
            CreateMap<CreateCreditAccountRequest, credit_account>();
            CreateMap<UpdateCreditAccountRequest, credit_account>();

        }
    }
}
