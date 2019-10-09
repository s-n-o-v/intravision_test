namespace Core
{
    using System;
    using System.Collections.Generic;

    public class Store
    {
        public int id { get; set; }
        public int drink_id { get; set; }
        public int qty { get; set; }

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

        public static Store GetEntityById(int id)
        {
            return Data.Data.Instance.StoreDataStorage.GetByID(id);
        }

        public static Store GetEntityByDrinkId(int drink_id)
        {
            return Data.Data.Instance.StoreDataStorage.getByDrinkId(drink_id);
        }

        public static long Count()
        {
            return Data.Data.Instance.StoreDataStorage.Count();
        }

        public void Save()
        {
            Data.Data.Instance.StoreDataStorage.Save(this);
        }

        public void Delete()
        {
            Data.Data.Instance.StoreDataStorage.Delete(this.id);
        }
    }
}
