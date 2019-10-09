namespace Core.Data
{
    using System;
    //using System.Collections.Generic;

    public interface IDataStorage
    {
        IDrinkDataStorage DrinkDataStorage { get; }
        ICoinDataStorage CoinDataStorage { get; }
        IBankDataStorage BankDataStorage { get; }
        ISoldDataStorage SoldDataStorage { get; }
        IStoreDataStorage StoreDataStorage { get; }
    }
}
