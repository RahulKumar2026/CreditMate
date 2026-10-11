using CreditService.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace CreditService.Domain.Interfaces
{
    public interface ICreditAccount
    {
        Task<credit_account?> GetByIdAsync( long id,CancellationToken cancellationToken);
        Task<IReadOnlyList<credit_account>> GetByUserIdAsync(long userId,CancellationToken cancellationToken);
        Task<credit_account> AddAsync(credit_account account,CancellationToken cancellationToken);
        Task<credit_account> UpdateAsync(credit_account account, CancellationToken cancellationToken);
        Task <bool> CloseAsync(long id, CancellationToken cancellationToken);
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
