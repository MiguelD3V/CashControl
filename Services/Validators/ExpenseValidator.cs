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
                response.Errors.Add("A despesa não pode ser nula");
                return response; 
            }

            if (string.IsNullOrWhiteSpace(expense.Name))
            {
                response.Success = false;
                response.Errors.Add("o Nome da despesa não pode estar vazio");
            }
            if (expense.Amount <= 0)
            {
                response.Success = false;
                response.Errors.Add("O valor da despesa não pode ser negativo");
            }
            if (expense.Date == default)
            {
                response.Success = false;
                response.Errors.Add("A data da despesa não pode ser nula");
            }
            if (expense.AccountId == Guid.Empty)
            {
                response.Success = false;
                response.Errors.Add("A despesa precisa ser associada a uma conta");
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
                response.Errors.Add("A despesa não pode ser nula");
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
                response.Errors.Add("O ID não pode estar vazio");
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
                response.Errors.Add("A Despesa não pode ser nula");
                return response; 
            }
            if (expense.Id == Guid.Empty)
            {
                response.Success = false;
                response.Errors.Add("o ID da despesa não pode ser nulo");
            }
            if (string.IsNullOrWhiteSpace(expense.Name))
            {
                response.Success = false;
                response.Errors.Add("A despesa não pode estar vazia");
            }
            if (expense.Amount <= 0)
            {
                response.Success = false;
                response.Errors.Add("o Valor da despesa precisa ser maior que zero");
            }
            if (expense.Date == default)
            {
                response.Success = false;
                response.Errors.Add("A data da despesa é Obrigatória");
            }
            if (expense.AccountId == Guid.Empty)
            {
                response.Success = false;
                response.Errors.Add("A Depsea precisa estar associada a uma conta");
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
