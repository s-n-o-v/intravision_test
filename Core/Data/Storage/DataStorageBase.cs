namespace Core.Data.Storage
{
    public abstract class DataStorageBase
    {
        public DataStorageBase(string connectionString)
        {
            this.ConnectionString = connectionString;
        }
        public string ConnectionString { get; private set; }
    }
}
