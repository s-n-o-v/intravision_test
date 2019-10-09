namespace Core.Data
{
    using System;
    using System.Collections.Generic;
    
    public interface IDrinkDataStorage : IModelDataStorage<Drink>
    {
        Drink GetByName(string name);
        IEnumerable<Drink> StoredDrinks();
        IEnumerable<Drink> AllDrinks();
    }
}
