namespace Vending.Models
{
    using System.Collections.Generic;

    public class ViewModel
    {
        public int Cash { get; set; }
        public IEnumerable<Vending.Models.Coin> Coins { get; set; }

        public ViewModel() { }

        public ViewModel(int cash, IEnumerable<Vending.Models.Coin> coins) {
            this.Cash = cash;
            this.Coins = coins;
        }
    }
}