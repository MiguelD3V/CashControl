using Cashcontrol.API.Models.Bussines;
using Cashcontrol.API.Models.Dtos.Account;
using Cashcontrol.API.Services.Validators.Interfaces;
using System.Text.RegularExpressions;

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
                response.Errors.Add("A conta não pode ser nula");
                return response;
            }
            if(account.Name == null)
            {
                response.Success = false;
                response.Errors.Add("O nome da conta não pode ser nulo");
                return response;
            }
            if(Regex.IsMatch(account.Email ?? string.Empty, @"^[^@\s]+@[^@\s]+\.[^@\s]+$") == false)
            {
                response.Success = false;
                response.Errors.Add("Formato de email invalido");
                return response;
            }

            if (!Enum.IsDefined(typeof(AccountType), account.Type))
            {
                response.Success = false;
                response.Errors.Add("Tipo de conta invalido");
                return response;
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
                response.Errors.Add("A conta não pode ser nula");

            }
            if (account.Id == Guid.Empty)
            {
                response.Success = false;
                response.Errors.Add("O Id da conta não pode ser nulo");
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
                response.Errors.Add("A conta não pode ser nula");
                return response;
            }
            if (account.Name == null)
            {
                response.Success = false;
                response.Errors.Add("o Nome da conta não pode ser nulo");
                return response;
            }

            if (!Enum.IsDefined(typeof(AccountType), account.Type))
            {
                response.Success = false;
                response.Errors.Add("Tipo de conta invalido");
                return response;
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
