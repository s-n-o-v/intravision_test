using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vending.Models
{
    public class DrinkList
    {
        public DrinkList(Core.Drink src)
        {
            this.id = src.id;
            this.pic_url = "/Content/drinks/" + src.picture;
            this.name = src.name;
            this.price = src.price;
            this.avail = false;
        }

        public int id { get; private set; }
        public int price { get; private set; }
        public string name { get; private set; }
        public string pic_url { get; private set; }

        /// <summary>
        /// Признак доступности для заказа
        /// </summary>
        public bool avail { get; set; }

        public void Assign(Core.Drink src)
        {
            this.id = src.id;
            this.pic_url = "/Content/drinks/" + src.picture;
            this.name = src.name;
            this.price = src.price;
            this.avail = false;
        }
    }
}