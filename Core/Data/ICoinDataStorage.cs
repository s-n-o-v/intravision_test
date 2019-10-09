namespace Core.Data
{
    using System;
    using System.Collections.Generic;

    public interface ICoinDataStorage : IModelDataStorage<Coin>
    {
        Coin GetByValue(int value);

        IEnumerable<Coin> GetAvailableCoins();
    }
}
