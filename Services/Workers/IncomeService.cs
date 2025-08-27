using AutoMapper;
using Cashcontrol.API.Data.Interfaces;
using Cashcontrol.API.Data.Repositories;
using Cashcontrol.API.Mapping;
using Cashcontrol.API.Models.Bussines;
using Cashcontrol.API.Models.Dtos.Income;
using Cashcontrol.API.Services.Validators.Interfaces;
using Cashcontrol.API.Services.Workers.Interfaces;
using System.Collections.Immutable;

namespace Cashcontrol.API.Services.Workers
{
    public class IncomeService : IIncomeService
    {
        private readonly IIncomeRepository _incomeRepository;
        private readonly IIncomeValidator _incomeValidator;
        private readonly IMapper _mapper;
        private readonly IAccountRepository _accountRepository;
        public IncomeService(IIncomeRepository incomeRepository, IIncomeValidator incomeValidator, IMapper mapper, IAccountRepository accountRepository)
        {
            _incomeRepository = incomeRepository;
            _incomeValidator = incomeValidator;
            _mapper = mapper;
            _accountRepository = accountRepository;
        }
        public async Task<IncomeResponseDto> CreateAsync(IncomeRequestDto income)
        {

            var validation = _incomeValidator.ValidateToCreateAsync(income);
            if (!validation.Success)
            {
                return new IncomeResponseDto
                {
                    Success = false,
                    Errors = validation.Errors
                };
            }

            var incomeDto = _mapper.Map<Income>(income);

            var account = _accountRepository.GetByIdAsync(income.AccountId);
            account.Result.Balance += income.Amount;

            incomeDto.Date = DateTime.UtcNow;

            await _accountRepository.UpdateAsync(account.Result);
            await _incomeRepository.CreateAsync(incomeDto);
            return new IncomeResponseDto
            {
                Success = true,
                Data = incomeDto
            };
        }

        public async Task<IncomeResponseDto> DeleteAsync(Guid id)
        {
            var incomeFind = await _incomeRepository.GetByIdAsync(id);
            if (incomeFind == null)
            {
                return new IncomeResponseDto
                {
                    Success = false,
                    Errors = new List<string> { "Receita não encontrada" }
                };
            }
            var incomeModel =  _mapper.Map<Income>(incomeFind);

            var account = await _accountRepository.GetByIdAsync(incomeModel.AccountId);
            if (account == null)
            {
                return new IncomeResponseDto
                {
                    Success = false,
                    Errors = new List<string> { "Conta não encontrada" }
                };
            }
            account.Balance -= incomeModel.Amount;

            await _accountRepository.UpdateAsync(account);
            await _incomeRepository.DeleteAsync(incomeModel);
            return new IncomeResponseDto
            {
                Success = true,
                Data = incomeModel
            };

        }

        public async Task<IImmutableList<IncomeResponseDto>> GetAllAsync()
        {
            var incomes = await _incomeRepository.GetAllAsync();
            var incomeDtos = _mapper.Map<List<IncomeResponseDto>>(incomes);
            return incomeDtos.ToImmutableList();
        }

        public async Task<IncomeResponseDto> GetByIdAsync(Guid id)
        {
            var income = await _incomeRepository.GetByIdAsync(id);
            if (income == null)
            {
                return new IncomeResponseDto
                { 
                     Success = false,
                     Errors = new List<string> { "Receita não encontrada" }
                };
            }
            var incomeDto = _mapper.Map<IncomeResponseDto>(income);
            return  new IncomeResponseDto
            {
                Success = true,
                Data = incomeDto
            };
        }

        public async Task<IncomeResponseDto> UpdateAsync(IncomeRequestDto income, Guid id)
        {
            var incomeFind = _incomeRepository.GetByIdAsync(id);
            if (incomeFind == null)
            {
                return (new IncomeResponseDto
                {
                    Success = false,
                    Errors = new List<string> { "Receita não encotrada" }
                });
            }
            var validation = _incomeValidator.ValidateToUpdateAsync(income, id);
            if (!validation.Success)
            {
                return new IncomeResponseDto
                {
                    Success = false,
                    Errors = validation.Errors
                };
            }

            var diference = incomeFind.Result.Amount - income.Amount;
            var account = await _accountRepository.GetByIdAsync(income.AccountId);
            account.Balance -= diference;

            var incomeDto = _mapper.Map<Income>(income);
            incomeDto.Id = id;

            await _incomeRepository.UpdateAsync(incomeDto);
            await _accountRepository.UpdateAsync(account);

            return new IncomeResponseDto
            {
                Success = true,
                Data = incomeDto
            };
        }
    }
}
