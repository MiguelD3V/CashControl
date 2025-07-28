using Cashcontrol.API.Banco.Interfaces;
using Cashcontrol.API.Models.Bussines;
using Cashcontrol.API.Models.Dtos;
using Cashcontrol.API.Services.Validators.Interfaces;

namespace Cashcontrol.API.Services.Validators
{
    public class ExpenseValidator : IExpenseValidator
    {
        public ExpenseResponseDto ValidateToCreate(Expense expense)
        {
            var response = new ExpenseResponseDto
            {
                Errors = new List<string>() 
            };

            if (expense == null)
            {
                response.Success = false;
                response.Errors.Add("Expense cannot be null");
                return response; 
            }

            if (string.IsNullOrWhiteSpace(expense.Name))
            {
                response.Success = false;
                response.Errors.Add("Expense name cannot be empty");
            }
            if (expense.Amount <= 0)
            {
                response.Success = false;
                response.Errors.Add("Expense amount must be greater than zero");
            }
            if (expense.Date == default)
            {
                response.Success = false;
                response.Errors.Add("Expense date is required");
            }
            if (expense.AccountId == Guid.Empty)
            {
                response.Success = false;
                response.Errors.Add("Expense must be associated with an account");
            }
            if (response.Errors.Count == 0)
            {
                response.Success = true;
                response.Data = expense;
            }
            else
            {
                response.Success = false;
            }

            return response;
        }

        public ExpenseResponseDto ValidateToDelete(Guid id)
        {
            var response = new ExpenseResponseDto
            {
                Errors = new List<string>()
            };

            if (id == Guid.Empty)
            {
                response.Success = false;
                response.Errors.Add("Expense ID cannot be empty");
            }
            else
            {
                response.Success = true;
                response.Data = new Expense { Id = id };
            }
            return response;
        }

        public ExpenseResponseDto ValidateToGet(Guid id)
        {
            var response = new ExpenseResponseDto
            {
                Errors = new List<string>()
            };
            if (id == Guid.Empty)
            {
                response.Success = false;
                response.Errors.Add("Expense ID cannot be empty");
            }
            else
            {
                response.Success = true;
                response.Data = new Expense { Id = id };
            }
            return response;
        }

        public ExpenseResponseDto ValidateToUpdate(Expense expense)
        {
           var response = new ExpenseResponseDto
            {
                Errors = new List<string>()
            };
            if (expense == null)
            {
                response.Success = false;
                response.Errors.Add("Expense cannot be null");
                return response; 
            }
            if (expense.Id == Guid.Empty)
            {
                response.Success = false;
                response.Errors.Add("Expense ID cannot be empty");
            }
            if (string.IsNullOrWhiteSpace(expense.Name))
            {
                response.Success = false;
                response.Errors.Add("Expense name cannot be empty");
            }
            if (expense.Amount <= 0)
            {
                response.Success = false;
                response.Errors.Add("Expense amount must be greater than zero");
            }
            if (expense.Date == default)
            {
                response.Success = false;
                response.Errors.Add("Expense date is required");
            }
            if (expense.AccountId == Guid.Empty)
            {
                response.Success = false;
                response.Errors.Add("Expense must be associated with an account");
            }
            
            if (response.Errors.Count == 0)
            {
                response.Success = true;
                response.Data = expense;
            }
            else
            {
                response.Success = false;
            }
            return response;
        }
    }
}
