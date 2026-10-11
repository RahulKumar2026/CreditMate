using System;
using System.Collections.Generic;

namespace CreditService.Domain.Entites;

public partial class credit_card
{
    public long id { get; set; }

    public long credit_account_id { get; set; }

    public string card_name { get; set; } = null!;

    public string issuer_name { get; set; } = null!;

    public string last_four_digits { get; set; } = null!;

    public string card_network { get; set; } = null!;

    public short billing_cycle_day { get; set; }

    public short due_day { get; set; }

    public DateTime created_at { get; set; }

    public DateTime updated_at { get; set; }

    public virtual credit_account credit_account { get; set; } = null!;
}
