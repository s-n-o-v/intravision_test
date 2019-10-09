namespace Core
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Монеты
    /// </summary>
    public class Coin
    {
        public int id { get; set; }
        public int price { get; set; }
        public bool blocked { get; set; }

        public int Available
        {
            get
            {
                return Bank.CoinCount(id);
            }
        }

        public void Increase(int size)
        {
            Bank bnk = new Bank();
            bnk.coin_id = this.id;
            bnk.qty = size;
            bnk.Save();
        }

        public void Decrease(int size)
        {
            this.Increase(-1 * size);
        }

        public static IEnumerable<Coin> getList()
        {
            return Data.Data.Instance.CoinDataStorage.GetList();
        }

        public static IEnumerable<Coin> availableList()
        {
            return Data.Data.Instance.CoinDataStorage.GetAvailableCoins();
        }

        public static Coin GetEntityById(int id)
        {
            return Data.Data.Instance.CoinDataStorage.GetByID(id);
        }

        public static Coin GetEntityByValue(int value)
        {
            return Data.Data.Instance.CoinDataStorage.GetByValue(value);
        }

        public void Save()
        {
            Data.Data.Instance.CoinDataStorage.Save(this);
        }

    }
}
