namespace Core.Data
{
    public interface IStoreDataStorage : IModelDataStorage<Store>
    {
        Store getByDrinkId(int drink_id);
    }
}
