namespace Core.Data.Storage
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Data.Objects;
    using System.Globalization;
    using System.Collections.Generic;

    public class CoinDataStorage : ModelDataStorage<Coin, coins>, ICoinDataStorage
    {
        public CoinDataStorage(string connectionString)
            : base(connectionString)
        {
        }

        public Coin GetByValue(int value) {
            using (var context = new Entities())
            {
                return this.GetAllQuery(context).Where(m => m.price == value).Select(this.CreateModelFromEntityExpression()).SingleOrDefault();
            }
        }

        public IEnumerable<Coin> GetAvailableCoins() {
            using (var context = new Entities())
            {
                return this.GetAllQuery(context).Where(m => m.blocked == false).Select(this.CreateModelFromEntityExpression()).ToList();
            }
        }

        protected override IQueryable<coins> GetAllQuery(Entities context)
        {
            return base.GetAllQuery(context);
        }

        protected override DbSet<coins> GetDBSet(Entities context)
        {
            return context.coin;
        }

        protected override Expression<Func<coins, bool>> GetModelByIDExpression(int id)
        {
            return (m) => m.id == id;
        }

        protected override int GetModelID(Coin model)
        {
            return model.id;
        }

        protected override Expression<Func<coins, Coin>> CreateModelFromEntityExpression()
        {
            return (entity) => new Coin
            {
                id = entity.id,
                price = entity.price,
                blocked = entity.blocked.HasValue ? entity.blocked.Value : false
            };
        }

        protected override void CopyModelToEntity(Coin model, coins entity)
        {
            entity.id = model.id;
            entity.price = model.price;
            entity.blocked = model.blocked;
        }

        protected override void CopyEntityToModel(Coin model, coins entity)
        {
            model.id = entity.id;
        }
    }
}
