namespace Core.Data
{
    using System.Collections.Generic;

    public interface IModelDataStorage<Model>
    {
        IEnumerable<Model> GetList();

        long Count();

        Model GetByID(int id);

        void Save(Model model);

        void Delete(int id);
    }
}
