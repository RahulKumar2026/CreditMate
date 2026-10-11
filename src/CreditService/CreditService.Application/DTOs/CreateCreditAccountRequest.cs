using System;
using System.Collections.Generic;
using System.Text;

namespace CreditService.Application.DTOs
{
    public class CreateCreditAccountRequest
    {
        public long user_id { get; set; }
        public string account_name { get; set; } = string.Empty;
        public decimal credit_limit { get; set; }
        public string account_type { get; set; } = null!;
        public string currency_code { get; set; } = "INR";
    }
}
