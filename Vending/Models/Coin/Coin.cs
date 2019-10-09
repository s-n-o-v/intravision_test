namespace Vending.Models
{
    public class Coin
    {
        public Coin(Core.Coin monet)
        {
            this.coin = monet;
            this.image_url = "/Content/images/" + monet.price.ToString() + "rub.jpg";
        }

        public Core.Coin coin { get; private set; }
        public string image_url { get; private set; } 
    }
}