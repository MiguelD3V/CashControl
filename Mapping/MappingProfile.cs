using AutoMapper;
using Cashcontrol.API.Models.Bussines;
using Cashcontrol.API.Models.Dtos;


namespace Cashcontrol.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Expense, ExpenseRequestDto>();
            CreateMap<ExpenseRequestDto, Expense>();
            CreateMap<Expense, ExpenseRequestDto>();
        }
    }
}
