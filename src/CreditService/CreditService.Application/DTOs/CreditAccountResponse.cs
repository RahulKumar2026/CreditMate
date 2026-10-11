using System;
using System.Collections.Generic;
using System.Text;

namespace CreditService.Application.DTOs
{
    public class CreditAccountResponse
    {
        public long id { get; set; }
        public long user_id { get; set; }
        public string account_name { get; set; } = string.Empty;
        public decimal credit_limit { get; set; }
        public decimal current_balance { get; set; }
        public decimal available_credit { get; set; }
        public decimal? utilization_percentage { get; set; }
        public string currency_code { get; set; } = "INR";
        public string status { get; set; } = string.Empty;
    }
}
