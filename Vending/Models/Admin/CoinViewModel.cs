namespace Vending.Models.Admin
{
    using System.ComponentModel.DataAnnotations;

    public class CoinViewModel
    {
        public int id { get; set; }

        [Display(Name = "Цена")]
        public int price { get; set; }

        [Display(Name = "Количество")]
        public int qty { get; set; }

        [Display(Name = "Прием заблокирован")]
        public bool blocked { get; set; }

        public CoinViewModel(Core.Coin c)
        {
            this.id = c.id;
            this.price = c.price;
            this.qty = c.Available;
            this.blocked = c.blocked;
        }

    }
}