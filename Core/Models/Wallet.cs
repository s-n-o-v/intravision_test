namespace Core.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Wallet
    {
        public IList<Bank> coins { get; set; }

        public void Refresh()
        {
            coins = Data.Data.Instance.BankDataStorage.getWallet();
        }

        public int AvailableCoins(int coin_id)
        {
            return coins.Where(c => c.id == coin_id).Select(x => x.qty).Single();
        }
    }
}
