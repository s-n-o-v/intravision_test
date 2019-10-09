namespace Core
{
    using System;
    using System.Collections.Generic;

    public class Sold
    {
        public int id { get; set; }
        public int drink_id { get; set; }
        public DateTime? dt { get; set; }

        private Drink p_drink;

        public Drink Drink
        {
            get
            {
                if (drink_id == 0) return null;
                if (p_drink != null) return p_drink;
                p_drink = Drink.GetEntityById(drink_id);
                return p_drink;
            }
        }

        public static Sold GetEntityById(int id)
        {
            return Data.Data.Instance.SoldDataStorage.GetByID(id);
        }

        public static long Count()
        {
            return Data.Data.Instance.SoldDataStorage.Count();
        }

        public void Save()
        {
            Data.Data.Instance.SoldDataStorage.Save(this);
        }

    }
}
