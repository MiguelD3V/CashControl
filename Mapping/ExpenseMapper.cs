using AutoMapper;
using Cashcontrol.API.Models.Bussines;
using Cashcontrol.API.Models.Dtos.Expense;

namespace Cashcontrol.API.Mapping
{
    public class ExpenseMapper : Profile
    {
        public ExpenseMapper()
        {
            CreateMap<Expense, ExpenseRequestDto>();
            CreateMap<ExpenseRequestDto, Expense>();
            CreateMap<Expense, ExpenseResponseDto>();
            CreateMap<ExpenseResponseDto, Expense>();
            CreateMap<ExpenseRequestDto, ExpenseResponseDto>();
            CreateMap<ExpenseResponseDto, ExpenseRequestDto>();
        }
    }
}
