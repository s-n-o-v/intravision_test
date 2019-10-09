
namespace Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Напитки
    /// </summary>
    public class Drink
    {
        public int id { get; set; }
        public string name { get; set; }
        public int price { get; set; }
        public string picture { get; set; }

        private int? i_count = null;

        public int Available
        {
            get
            {
                if (i_count == null)
                {
                    var s = Data.Data.Instance.StoreDataStorage.getByDrinkId(this.id);
                    i_count = s== null ? 0 : s.qty;
                }
                return i_count.Value;
            }
            set {
                i_count = value;
                Store store = new Store();
                store.drink_id = this.id;
                store.qty = value;
                store.Save();
            }
        }

        /// <summary>
        /// Список всех напитков
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Drink> getList()
        {
            return Data.Data.Instance.DrinkDataStorage.GetList();
        }

        /// <summary>
        /// Список всех напитков доступных в хранилище
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Drink> getListStored()
        {
            return Data.Data.Instance.DrinkDataStorage.StoredDrinks();
        }

        /// <summary>
        /// Список всех напитков
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Drink> getAllList()
        {
            return Data.Data.Instance.DrinkDataStorage.AllDrinks();
        }

        public static Drink GetEntityById(int id)
        {
            return Data.Data.Instance.DrinkDataStorage.GetByID(id);
        }

        public void Save()
        {
            Data.Data.Instance.DrinkDataStorage.Save(this);
        }

        public void Delete()
        {
            Data.Data.Instance.DrinkDataStorage.Delete(this.id);
        }

    }
}
