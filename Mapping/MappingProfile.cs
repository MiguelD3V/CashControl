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
            CreateMap<AccountRequestDto, Account>();

            CreateMap<Account, AccountRequestDto>();
            CreateMap<AccountResponseDto, Account>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src =>src.Name))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.Balance, opt => opt.MapFrom(src => src.Balance))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt));

            CreateMap<AccountResponseDto, AccountRequestDto>();
            


        }
    }
}
