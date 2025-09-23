using AutoMapper;
using Cashcontrol.API.Data.Interfaces;
using Cashcontrol.API.Models.Bussines;
using Cashcontrol.API.Models.Dtos.Account;
using Cashcontrol.API.Services.Validators.Interfaces;
using Cashcontrol.API.Services.Workers.Interfaces;
using System.Collections.Immutable;

namespace Cashcontrol.API.Services.Workers
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;
        private readonly IAccountValidator _validator;

        public AccountService(IAccountRepository accountRepository, IMapper mapper, IAccountValidator validator)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<AccountResponseDto> CreateAsync(AccountRequestDto accountDto)
        {
            var validation = _validator.ValidateToCreate(accountDto);

            if (!validation.Success)
            {
                return new AccountResponseDto
                {
                    Success = false,
                    Errors = validation.Errors
                };
            }

            var account = _mapper.Map<Account>(accountDto);
            account.CreatedAt = DateTime.UtcNow;

            await _accountRepository.CreateAsync(account);

            return new AccountResponseDto
            {
                Success = true,
                Data = account
            };
        }

        public async Task<AccountResponseDto> DeleteAsync(Guid id)
        {
            var account = await _accountRepository.GetByIdAsync(id);
            await _accountRepository.DeleteAsync(account);
            return new AccountResponseDto 
            {
                Success = true,
                Data = account
            };
        }

        public async Task<IImmutableList<AccountResponseDto>> GetAllAsync()
        {
            var accounts = await _accountRepository.GetAllAsync();

            return accounts
                .Select(account => new AccountResponseDto
                {
                    Success = true,
                    Data = account
                }).ToImmutableList();
        }

        public async Task<AccountResponseDto> GetByIdAsync(Guid id)
        {
            var account = await _accountRepository.GetByIdAsync(id);
            if (account == null)
            {
                throw new Exception($"Conta com ID {id} não foi encontrada.");
            }
            var accountDto = _mapper.Map<AccountResponseDto>(account);
            accountDto.Data = account;

            return accountDto;
        }


        public async Task<AccountResponseDto> UpdateAsync(Guid id, AccountUpdateRequestDto account)
        {
            var findAccount = await _accountRepository.GetByIdAsync(id);
            if (findAccount == null)
            {
                return new AccountResponseDto
                {
                    Success = false,
                    Errors = new List<string> { "Conta não encontrada." }
                };
            }

            findAccount.Name = account.Name;
            findAccount.Type = account.Type;

            var validation = _validator.ValidateToUpdate(findAccount);
            if (!validation.Success)
            {
                return new AccountResponseDto
                {
                    Success = false,
                    Errors = validation.Errors
                };
            }

            await _accountRepository.UpdateAsync(findAccount);

            return new AccountResponseDto
            {
                Success = true,
                Data = findAccount
            };
        }
    }
}
