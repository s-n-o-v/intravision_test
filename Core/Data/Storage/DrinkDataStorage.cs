namespace Core.Data.Storage
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Data.Objects;
    using System.Globalization;
    using System.Collections.Generic;

    public class DrinkDataStorage : ModelDataStorage<Drink, drinks>, IDrinkDataStorage
    {
        public DrinkDataStorage(string connectionString)
            : base(connectionString)
        {
        }

        public Drink GetByName(string name)
        {
            using (var context = new Entities())
            {
                return this.GetAllQuery(context).Where(m => m.name == name && m.id > 0).Select(this.CreateModelFromEntityExpression()).SingleOrDefault();
            }
        }

        /// <summary>
        /// Напитки, которые еще есть в хранилище
        /// </summary>
        public IEnumerable<Drink> StoredDrinks()
        {
            using (var context = new Entities())
            {
                var storeContext = context.store.Where(x => x.qty > 0);

                return this.GetAllQuery(context)
                    .Join(storeContext, a => a.id, b => b.drink_id, (a,b) => new { drinks = a, stores = b })
                    .Select(d => d.drinks)
                    .Select(this.CreateModelFromEntityExpression())
                    .ToList();
            }
        }

        /// <summary>
        /// Напитки, которые еще есть в хранилище
        /// </summary>
        public IEnumerable<Drink> AllDrinks()
        {
            using (var context = new Entities())
            {
                return this.GetAllQuery(context)
                    .Join(context.store, a => a.id, b => b.drink_id, (a, b) => new { drinks = a, stores = b })
                    .Select(d => d.drinks)
                    .Select(this.CreateModelFromEntityExpression())
                    .ToList();
            }
        }

        protected override IQueryable<drinks> GetAllQuery(Entities context)
        {
            return base.GetAllQuery(context).Where(x => x.id > 0);
        }

        protected override DbSet<drinks> GetDBSet(Entities context)
        {
            return context.drink;
        }

        protected override Expression<Func<drinks, bool>> GetModelByIDExpression(int id)
        {
            return (m) => m.id == id;
        }

        protected override int GetModelID(Drink model)
        {
            return model.id;
        }

        protected override Expression<Func<drinks, Drink>> CreateModelFromEntityExpression()
        {
            return (entity) => new Drink
            {
                id = entity.id,
                name = entity.name,
                price = entity.price,
                picture = entity.picture
            };
        }

        protected override void CopyModelToEntity(Drink model, drinks entity)
        {
            entity.id = model.id;
            entity.name = model.name;
            entity.picture = model.picture;
            entity.price = model.price;
        }

        protected override void CopyEntityToModel(Drink model, drinks entity)
        {
            model.id = entity.id;
        }
    }
}
