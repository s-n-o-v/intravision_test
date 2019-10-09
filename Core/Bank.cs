namespace Core
{
    using System;
    using System.Collections.Generic;

    public class Bank
    {
        public int id { get; set; }
        public int coin_id { get; set; }
        public int qty { get; set; }

        private Coin p_coin;

        public Coin Coin
        {
            get
            {
                if (coin_id == 0) return null;
                if (p_coin != null) return p_coin;
                p_coin = Coin.GetEntityById(coin_id);
                return p_coin;
            }
        }

        public static Bank GetEntityById(int id)
        {
            return Data.Data.Instance.BankDataStorage.GetByID(id);
        }

        public static int getBank()
        {
            var coins = Data.Data.Instance.BankDataStorage.getWallet();
            int summ = 0;
            coins.ForEach(x => summ += x.Coin.price * x.qty);
            return summ;
        }

        public static int CoinCount(int coin_id)
        {
            return Data.Data.Instance.BankDataStorage.CoinCount(coin_id);
        }

        // метод исключительно для демонстрации выданных монет
        public static string Cashout(int size)
        {
            string text_out = "Выдано:\n";
            List<Bank> co = Data.Data.Instance.BankDataStorage.Cashout(size);
            int tmp_sum = 0;
            co.ForEach(x => tmp_sum += x.qty * x.Coin.price);
            if (tmp_sum != size)
                return "Извините недостаточно монент для выдачи сдачи\n";

            co.ForEach(x => text_out += x.qty.ToString() + " монет по " + x.Coin.price.ToString() + " руб.\n");
            return text_out;
        }

        public static long Count()
        {
            return Data.Data.Instance.BankDataStorage.Count();
        }

        public void Save()
        {
            Data.Data.Instance.BankDataStorage.Save(this);
        }

    }
}
