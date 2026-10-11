using CreditService.Domain.Entities.Models;
using System;
using System.Collections.Generic;

namespace CreditService.Domain.Entites;

public partial class credit_account
{
    public long id { get; set; }

    public long user_id { get; set; }

    public string account_name { get; set; } = null!;

    public string account_type { get; set; } = null!;

    public decimal credit_limit { get; set; }

    public decimal current_balance { get; set; }

    public string currency_code { get; set; } = null!;

    public string status { get; set; } = null!;

    public DateOnly opened_on { get; set; }

    public DateOnly? closed_on { get; set; }

    public long version { get; set; }

    public DateTime created_at { get; set; }

    public DateTime updated_at { get; set; }

    public virtual credit_card? credit_card { get; set; }

    public virtual ICollection<interest_rate> interest_rates { get; set; } = new List<interest_rate>();

    public virtual ICollection<loan> loans { get; set; } = new List<loan>();
}
