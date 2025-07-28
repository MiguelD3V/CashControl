using AutoMapper;
using Cashcontrol.API.Data.Interfaces;
using Cashcontrol.API.Models.Bussines;
using Cashcontrol.API.Models.Dtos;
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
            var accountDtos = accounts.Select(a => _mapper.Map<AccountResponseDto>(a)).ToImmutableList();
            return accountDtos;
        }

        public async Task<AccountResponseDto> GetByIdAsync(Guid id)
        {
            var account = await _accountRepository.GetByIdAsync(id);
            if (account == null)
            {
                throw new Exception($"Account with ID {id} not found.");
            }
            var accountDto = _mapper.Map<AccountResponseDto>(account);
            return accountDto;
        }

        public async Task<AccountResponseDto> UpdateAsync(AccountRequestDto account)
        {
            var accountModel = _mapper.Map<Account>(account);

            var validation = _validator.ValidateToUpdate(accountModel);
            if (!validation.Success)
            {
                return new AccountResponseDto
                {
                    Success = false,
                    Errors = validation.Errors
                };
            }

            await _accountRepository.UpdateAsync(accountModel);

            return new AccountResponseDto
            {
                Success = true,
                Data = accountModel
            };
        }
    }
}
