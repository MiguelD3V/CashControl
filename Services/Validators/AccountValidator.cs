using Cashcontrol.API.Models.Bussines;
using Cashcontrol.API.Models.Dtos;
using Cashcontrol.API.Services.Validators.Interfaces;

namespace Cashcontrol.API.Services.Validators
{
    public class AccountValidator : IAccountValidator
    {
        public AccountResponseDto ValidateToCreate(AccountRequestDto account)
        {
            var response = new AccountResponseDto
            {
                Errors = []
            };
            if (account == null)
            {
                response.Success = false;
                response.Errors.Add("Account cannot be null");
                return response;
            }
            if(account.Name == null)
            {
                response.Success = false;
                response.Errors.Add("Account name cannot be null");
                return response;
            }
           
            if (!Enum.IsDefined(typeof(AccountType), account.Type))
            {
                response.Success = false;
                response.Errors.Add("Invalid account type");
                return response;
            }
            if (account.Balance < 0)
            {
                response.Success = false;
                response.Errors.Add("Account balance cannot be negative");
            }
            if (response.Errors.Count == 0)
            {
                response.Success = true;
                response.Data = account;
            }
            else
            {
                response.Success = false;
            }

            return response;
        }

        public AccountResponseDto ValidateToDelete(Account account)
        {
            var response = new AccountResponseDto
            {
                Errors = []
            };
            if (account == null)
            {
                response.Success = false;
                response.Errors.Add("Account cannot be null");

            }
            if (account.Id == Guid.Empty)
            {
                response.Success = false;
                response.Errors.Add("Account ID cannot be empty");
            }
            if(response.Errors.Count == 0)
            {
                response.Success = true;
                response.Data = account;
            }
            else
            {
                response.Success = false;

            }
                return response;
        }

        public AccountResponseDto ValidateToUpdate(Account account)
        {
            var response = new AccountResponseDto
            {
                Errors = []
            };
            if (account == null)
            {
                response.Success = false;
                response.Errors.Add("Account cannot be null");
                return response;
            }
            if (account.Name == null)
            {
                response.Success = false;
                response.Errors.Add("Account name cannot be null");
                return response;
            }

            if (!Enum.IsDefined(typeof(AccountType), account.Type))
            {
                response.Success = false;
                response.Errors.Add("Invalid account type");
                return response;
            }
            if (account.Balance < 0)
            {
                response.Success = false;
                response.Errors.Add("Account balance cannot be negative");
            }
            if (account.CreatedAt == default)
            {
                response.Success = false;
                response.Errors.Add("Account creation date is required");
            }
            if (response.Errors.Count == 0)
            {
                response.Success = true;
                response.Data = account;
            }
            else
            {
                response.Success = false;
            }

            return response;
        }
    }
}
