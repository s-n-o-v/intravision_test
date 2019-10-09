namespace Vending.Models.Admin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class AdminViewModel
    {
        List<CoinViewModel> coins { get; set; }
        List<DrinkViewModel> drinks { get; set; }
    }
}