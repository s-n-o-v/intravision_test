namespace Core.Data
{
    using System;

    public interface IBankDataStorage : IModelDataStorage<Bank>
    {
        System.Collections.Generic.List<Bank> getWallet();

        int CoinCount(int coin_id);

        System.Collections.Generic.List<Bank> Cashout(int size);
    }
}
