using CreditService.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace CreditService.Application.Service.Interface
{
    public interface ICreditAccountService
    {
        Task<CreditAccountResponse> CreateAsync(CreateCreditAccountRequest request,CancellationToken cancellationToken);
        Task<CreditAccountResponse?> GetByIdAsync(long id, CancellationToken cancellationToken);
        Task<IReadOnlyList<CreditAccountResponse>> GetByUserIdAsync(long userId,CancellationToken cancellationToken);
        Task<CreditAccountResponse?> UpdateAsync(long id,UpdateCreditAccountRequest request, CancellationToken cancellationToken);
        Task<bool> CloseAsync(long id,CancellationToken cancellationToken);
    }
}
