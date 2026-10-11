using System;
using System.Collections.Generic;

namespace CreditService.Domain.Entites;

public partial class vw_credit_account_summary
{
    public long? credit_account_id { get; set; }

    public long? user_id { get; set; }

    public string? account_name { get; set; }

    public string? account_type { get; set; }

    public decimal? credit_limit { get; set; }

    public decimal? current_balance { get; set; }

    public string? currency_code { get; set; }

    public string? status { get; set; }

    public decimal? utilization_percentage { get; set; }

    public DateOnly? opened_on { get; set; }

    public DateOnly? closed_on { get; set; }

    public DateTime? created_at { get; set; }

    public DateTime? updated_at { get; set; }
}
