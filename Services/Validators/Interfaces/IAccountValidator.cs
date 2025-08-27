using Cashcontrol.API.Models.Bussines;
using Cashcontrol.API.Models.Dtos.Account;

namespace Cashcontrol.API.Services.Validators.Interfaces
{
    public interface IAccountValidator
    {
        public AccountResponseDto ValidateToCreate(AccountRequestDto account);
        public AccountResponseDto ValidateToUpdate(Account account);
        public AccountResponseDto ValidateToDelete(Account account);
        
    }
}
