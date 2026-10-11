using System;
using System.Collections.Generic;
using System.Text;

namespace CreditService.Application.DTOs
{
    public class UpdateCreditAccountRequest
    {
        public string account_name { get; init; } = string.Empty;
        public decimal credit_limit { get; init; }
    }
}
