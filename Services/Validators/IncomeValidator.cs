using Cashcontrol.API.Models.Bussines;
using Cashcontrol.API.Models.Dtos;
using Cashcontrol.API.Services.Validators.Interfaces;

namespace Cashcontrol.API.Services.Validators
{
    public class IncomeValidator : IIncomeValidator
    {
        public IncomeResponseDto ValidateToCreateAsync(IncomeRequestDto income)
        {
            var response = new IncomeResponseDto
            {
                Errors = new List<string>()
            };
            if (income == null)
            {
                response.Success = false;
                response.Errors.Add("Income cannot be null");
                
            }
            if (string.IsNullOrWhiteSpace(income.Description))
            {
                response.Success = false;
                response.Errors.Add("Income description cannot be empty");
            }
            if (income.Amount <= 0)
            {
                response.Success = false;
                response.Errors.Add("Income amount must be greater than zero");
            }
            if (income.AccountId == Guid.Empty)
            {
                response.Success = false;
                response.Errors.Add("Income must be associated with an account");
            }
            if (response.Errors.Count == 0)
            {
                response.Success = true;
                response.Data = new IncomeResponseDto
                {
                    Description = income.Description,
                    Amount = income.Amount,
                    Source = income.Source
                };
            }
            else
            {
                response.Success = false;
            }
            return response;

        }

        public IncomeResponseDto ValidateToUpdateAsync(IncomeRequestDto income, Guid id)
        {
            var response = new IncomeResponseDto
            {
                Errors = new List<string>()
            };
            if (income == null)
            {
                response.Success = false;
                response.Errors.Add("Income cannot be null");
            }
            if (string.IsNullOrWhiteSpace(income.Description))
            {
                response.Success = false;
                response.Errors.Add("Income description cannot be empty");
            }
            if (income.Amount <= 0)
            {
                response.Success = false;
                response.Errors.Add("Income amount must be greater than zero");
            }
            if (income.AccountId == Guid.Empty)
            {
                response.Success = false;
                response.Errors.Add("Income must be associated with an account");
            }
            if (response.Errors.Count == 0)
            {
                response.Success = true;
                response.Data = new IncomeResponseDto
                {
                    Description = income.Description,
                    Amount = income.Amount,
                    Source = income.Source
                };
            }

            return response;
        }
    }
}
