using CreditService.Domain.Entites;
using System;
using System.Collections.Generic;

namespace CreditService.Domain.Entities.Models;

public partial class loan
{
    public long id { get; set; }

    public long credit_account_id { get; set; }

    public string loan_name { get; set; } = null!;

    public decimal principal_amount { get; set; }

    public decimal outstanding_amount { get; set; }

    public int tenure_months { get; set; }

    public DateOnly start_date { get; set; }

    public DateOnly maturity_date { get; set; }

    public DateTime created_at { get; set; }

    public DateTime updated_at { get; set; }

    public virtual credit_account credit_account { get; set; } = null!;
}
