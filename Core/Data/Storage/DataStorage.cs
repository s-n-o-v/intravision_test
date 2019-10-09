namespace Core.Data.Storage
{
    using System.Linq;

    public class DataStorage : IDataStorage
    {
        public DataStorage(string connectionString)
        {
            this.ConnectionString = connectionString;

            this.CoinDataStorage = new CoinDataStorage(this.ConnectionString);
            this.DrinkDataStorage = new DrinkDataStorage(this.ConnectionString);
            this.BankDataStorage = new BankDataStorage(this.ConnectionString);
            this.SoldDataStorage = new SoldDataStorage(this.ConnectionString);
            this.StoreDataStorage = new StoreDataStorage(this.ConnectionString);
        }

        public string ConnectionString { get; private set; }

        public ICoinDataStorage CoinDataStorage { get; private set; }

        public IDrinkDataStorage DrinkDataStorage { get; private set; }

        public IBankDataStorage BankDataStorage { get; private set; }

        public ISoldDataStorage SoldDataStorage { get; private set; }

        public IStoreDataStorage StoreDataStorage { get; private set; }
    }
}
