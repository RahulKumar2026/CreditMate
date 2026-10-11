using System;
using System.Collections.Generic;

namespace CreditService.Domain.Entites;

public partial class interest_rate
{
    public long id { get; set; }

    public long credit_account_id { get; set; }

    public string rate_type { get; set; } = null!;

    public decimal annual_rate { get; set; }

    public DateOnly effective_from { get; set; }

    public DateOnly? effective_to { get; set; }

    public DateTime created_at { get; set; }

    public DateTime updated_at { get; set; }

    public virtual credit_account credit_account { get; set; } = null!;
}
