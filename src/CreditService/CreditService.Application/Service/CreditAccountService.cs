using AutoMapper;
using CreditService.Application.DTOs;
using CreditService.Application.Mapper;
using CreditService.Application.Service.Interface;
using CreditService.Domain.Entites;
using CreditService.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CreditService.Application.Service
{
    public class CreditAccountService : ICreditAccountService
    {
        private readonly ICreditAccount _creditAccount;
        private readonly IMapper _mapper;
        public CreditAccountService(ICreditAccount creditAccount, IMapper mapper)
        {
            _creditAccount = creditAccount;
            _mapper = mapper;
        }
        public async Task<CreditAccountResponse> CreateAsync(CreateCreditAccountRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var creditAccount = _mapper.Map<credit_account>(request);
                creditAccount.opened_on = DateOnly.FromDateTime(DateTime.UtcNow);
                var addedCreditAccount = await _creditAccount.AddAsync(creditAccount,cancellationToken);
                var response = _mapper.Map<CreditAccountResponse>(addedCreditAccount);
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database error: {ex.InnerException?.Message}");
                Console.WriteLine($"Entity error: {ex.Message}");
                throw;
            }
        }
        public async Task<CreditAccountResponse?> GetByIdAsync(long id, CancellationToken cancellationToken)
        {
            try
            {
                var creditAccount = await _creditAccount.GetByIdAsync(id, cancellationToken);
                if (creditAccount == null)
                {
                    return null;
                }

                var response = _mapper.Map<CreditAccountResponse>(creditAccount);
                response.available_credit = creditAccount.credit_limit - creditAccount.current_balance;
                response.utilization_percentage = creditAccount.credit_limit > 0 ? (creditAccount.current_balance / creditAccount.credit_limit) * 100 : 0;

                return response;
            }
            catch (Exception ex) 
            {
                throw;
            }
        }
        public async Task<IReadOnlyList<CreditAccountResponse>> GetByUserIdAsync(long userId, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _creditAccount.GetByUserIdAsync(userId, cancellationToken);
                var creditAccountResponses = _mapper.Map<IReadOnlyList<CreditAccountResponse>>(response);
                foreach (var Accountresponse in creditAccountResponses)
                {
                    Accountresponse.available_credit = Accountresponse.credit_limit - Accountresponse.current_balance;
                    Accountresponse.utilization_percentage = Accountresponse.credit_limit > 0 ? 
                                                            (Accountresponse.current_balance / Accountresponse.credit_limit) * 100 : 0;
                }
                return creditAccountResponses;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<CreditAccountResponse?> UpdateAsync(long id, UpdateCreditAccountRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var creditAccount = await _creditAccount.GetByIdAsync(id, cancellationToken);
                if (creditAccount == null)
                {
                    return null;
                }
                creditAccount.account_name = request.account_name;
                creditAccount.credit_limit = request.credit_limit;
                var updatedCreditAccount = await _creditAccount.UpdateAsync(creditAccount, cancellationToken);
                var response = _mapper.Map<CreditAccountResponse>(updatedCreditAccount);
                response.available_credit = updatedCreditAccount.credit_limit - updatedCreditAccount.current_balance;
                response.utilization_percentage = updatedCreditAccount.credit_limit > 0 ?
                                                  (updatedCreditAccount.current_balance / updatedCreditAccount.credit_limit) * 100 : 0;
                return response;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<bool> CloseAsync(long id, CancellationToken cancellationToken)
        {
            try 
            {
                var creditAccount = await _creditAccount.CloseAsync(id, cancellationToken);
                if (!creditAccount)
                {
                    return false;
                }
                return true;
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
