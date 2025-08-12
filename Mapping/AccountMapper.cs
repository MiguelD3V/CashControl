using AutoMapper;
using Cashcontrol.API.Models.Bussines;
using Cashcontrol.API.Models.Dtos;

namespace Cashcontrol.API.Mapping
{
    public class AccountMapper : Profile
    {
        public AccountMapper()
        {
            CreateMap<Account, AccountRequestDto>();
            CreateMap<AccountRequestDto, Account>();
            CreateMap<Account, AccountResponseDto>();
            CreateMap<AccountResponseDto, Account>();
            CreateMap<AccountRequestDto, AccountResponseDto>();
            CreateMap<AccountResponseDto, AccountRequestDto>();
        }
    }
}
