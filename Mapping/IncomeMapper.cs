using AutoMapper;
using Cashcontrol.API.Models.Bussines;
using Cashcontrol.API.Models.Dtos;

namespace Cashcontrol.API.Mapping
{
    public class IncomeMapper : Profile
    {
        public IncomeMapper()
        {
            CreateMap<Income, IncomeRequestDto>();
            CreateMap<IncomeRequestDto, Income>();
            CreateMap<Income, IncomeResponseDto>();
            CreateMap<IncomeResponseDto, Income>();
            CreateMap<IncomeRequestDto, IncomeResponseDto>();
            CreateMap<IncomeResponseDto, IncomeRequestDto>();
        }
    }
}
