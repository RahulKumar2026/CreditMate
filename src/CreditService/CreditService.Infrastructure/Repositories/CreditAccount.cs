using AutoMapper.Configuration.Annotations;
using CreditService.Domain.Entites;
using CreditService.Domain.Interfaces;
using CreditService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
namespace CreditService.Infrastructure.Repositories
{
    public class CreditAccount : ICreditAccount
    {
        private readonly CreditDbContext _dbcontext;
        public CreditAccount(CreditDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<credit_account?> GetByIdAsync(long id, CancellationToken cancellationToken) 
        {
            try 
            {
                var result = await _dbcontext.credit_accounts
                                   .AsNoTracking()
                                   .FirstOrDefaultAsync(c => c.id == id, cancellationToken);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving credit account with ID {id}: {ex.Message}", ex);
            }
        }
        public async Task<IReadOnlyList<credit_account>> GetByUserIdAsync(long userId, CancellationToken cancellationToken) 
        {
            try 
            {
                var result = await _dbcontext.credit_accounts
                                      .AsNoTracking()
                                      .Where(c => c.user_id == userId)
                                      .ToListAsync(cancellationToken);
                return result;
            }
            catch(Exception ex)
            {
                throw new Exception($"Error retrieving credit accounts for user ID {userId}: {ex.Message}", ex);
            }
        }
        public async Task<credit_account> AddAsync(credit_account account, CancellationToken cancellationToken) 
        {
            try
            {
                await _dbcontext.credit_accounts.AddAsync(account, cancellationToken);
                await _dbcontext.SaveChangesAsync(cancellationToken);
                return account;

            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding credit account with ID {account.id}: {ex.Message}", ex);
            }
        }
        public async Task<credit_account> UpdateAsync(credit_account account, CancellationToken cancellationToken)
        {
            try
            {
                _dbcontext.credit_accounts.Update(account);
                await _dbcontext.SaveChangesAsync(cancellationToken);
                return account;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating credit account with ID {account.id}: {ex.Message}", ex);
            }
        }
        public async Task<bool> CloseAsync(long id, CancellationToken cancellationToken)
        {
            try
            {
                var account = await _dbcontext.credit_accounts.FirstOrDefaultAsync(c => c.id == id);
                if (account == null)
                {
                    return false;
                }
                account.closed_on = DateOnly.FromDateTime(DateTime.UtcNow);
                account.status = "CLOSED";
                await _dbcontext.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error closing credit account with ID {id}: {ex.Message}", ex);
            }
        }
        public async Task SaveChangesAsync(CancellationToken cancellationToken) 
        {
            await _dbcontext.SaveChangesAsync(cancellationToken);   
        }
    }
}
