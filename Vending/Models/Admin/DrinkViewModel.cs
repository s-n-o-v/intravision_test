namespace Vending.Models.Admin
{
    using System.ComponentModel.DataAnnotations;

    public class DrinkViewModel
    {
        public int id { get; set; }

        [Display(Name = "Наименование напитка")]
        public string name { get; set; }

        [Display(Name = "Цена")]
        public int price { get; set; }

        [Display(Name = "Количество")]
        public int qty { get; set; }

        [Display(Name = "Изображение")]
        public string pic_url { get; set; }

        public DrinkViewModel()
        {
        }

        public DrinkViewModel(Core.Drink drink)
        {
            this.id = drink.id;
            this.name = drink.name;
            this.price = drink.price;
            this.qty = drink.Available;
            this.pic_url = drink.picture;
        }

        public void CopyTo(Core.Drink drink)
        {
            drink.id = this.id;
            drink.name = this.name;
            drink.price = this.price;
            if (this.pic_url != null)
                drink.picture = this.pic_url;
        }
    }
}