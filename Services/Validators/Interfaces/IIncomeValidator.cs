using Cashcontrol.API.Models.Bussines;
using Cashcontrol.API.Models.Dtos;

namespace Cashcontrol.API.Services.Validators.Interfaces
{
    public interface IIncomeValidator
    {
        public IncomeResponseDto ValidateToCreateAsync(IncomeRequestDto income);
        public IncomeResponseDto ValidateToUpdateAsync(IncomeRequestDto income, Guid id);
     
    }
}
