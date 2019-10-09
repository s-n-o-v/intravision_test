namespace Core.Data.Storage
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Data.Objects;
    using System.Globalization;
    using System.Collections.Generic;

    public class StoreDataStorage : ModelDataStorage<Store, stores>, IStoreDataStorage
    {
        public StoreDataStorage(string connectionString)
            : base(connectionString)
        {
        }

        public Store getByDrinkId(int drink_id)
        {
            using (var context = new Entities())
            {
                return this.GetAllQuery(context).Where(m => m.drink_id == drink_id).Select(this.CreateModelFromEntityExpression()).SingleOrDefault();
            }
        }

        protected override IQueryable<stores> GetAllQuery(Entities context)
        {
            return base.GetAllQuery(context);
        }

        protected override DbSet<stores> GetDBSet(Entities context)
        {
            return context.store;
        }

        protected override Expression<Func<stores, bool>> GetModelByIDExpression(int id)
        {
            return (m) => m.id == id;
        }

        protected override int GetModelID(Store model)
        {
            return model.id;
        }

        protected override Expression<Func<stores, Store>> CreateModelFromEntityExpression()
        {
            return (entity) => new Store
            {
                id = entity.id,
                drink_id = entity.drink_id,
                qty = entity.qty
            };
        }

        protected override void CopyModelToEntity(Store model, stores entity)
        {
            entity.id = model.id;
            entity.drink_id = model.drink_id;
            entity.qty = model.qty;
        }

        protected override void CopyEntityToModel(Store model, stores entity)
        {
            model.id = entity.id;
        }
    }
}
