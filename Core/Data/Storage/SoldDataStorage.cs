namespace Core.Data.Storage
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Data.Objects;
    using System.Globalization;
    using System.Collections.Generic;

    public class SoldDataStorage : ModelDataStorage<Sold, solds>, ISoldDataStorage
    {
        public SoldDataStorage(string connectionString)
            : base(connectionString)
        {
        }

        //public IEnumerable<Coin> GetList(DateTime date) {
        //    using (var context = new VendingEntities())
        //    {
        //        return this.GetAllQuery(context).Where(m => m.name == name).Select(this.CreateModelFromEntityExpression()).SingleOrDefault();
        //    }
        //}

        protected override IQueryable<solds> GetAllQuery(Entities context)
        {
            return base.GetAllQuery(context);
        }

        protected override DbSet<solds> GetDBSet(Entities context)
        {
            return context.sold;
        }

        protected override Expression<Func<solds, bool>> GetModelByIDExpression(int id)
        {
            return (m) => m.id == id;
        }

        protected override int GetModelID(Sold model)
        {
            return model.id;
        }

        protected override Expression<Func<solds, Sold>> CreateModelFromEntityExpression()
        {
            return (entity) => new Sold
            {
                id = entity.id,
                drink_id = entity.drink_id,
                dt = entity.dt
            };
        }

        protected override void CopyModelToEntity(Sold model, solds entity)
        {
            entity.id = model.id;
            entity.drink_id = model.drink_id;
            entity.dt = model.dt;
        }

        protected override void CopyEntityToModel(Sold model, solds entity)
        {
            model.id = entity.id;
        }
    }
}
