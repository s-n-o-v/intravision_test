namespace Core.Data.Storage
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Data.Objects;
    using System.Globalization;

    public class BankDataStorage : ModelDataStorage<Bank, banks>, IBankDataStorage
    {
        public BankDataStorage(string connectionString)
            : base(connectionString)
        {
        }

        public System.Collections.Generic.List<Bank> getWallet()
        {
            using (var context = new Entities())
            {
                var items = context.Database.SqlQuery<Bank>("sp_wallet");
                return items.ToList();
            }
        }

        public int CoinCount(int coin_id)
        {
            using (var context = new Entities())
            {
                var items = context.Database.SqlQuery<Bank>("sp_wallet");
                return items.Where(c=>c.coin_id == coin_id).Select(s=>s.qty).Single();
            }
        }

        public System.Collections.Generic.List<Bank> Cashout(int size)
        {
            using (var context = new Entities())
            {
                var items = context.Database.SqlQuery<Bank>("exec sp_cashout @size", new System.Data.SqlClient.SqlParameter("@size", size));
                return items.ToList();
            }
        }

        protected override IQueryable<banks> GetAllQuery(Entities context)
        {
            return base.GetAllQuery(context);
        }

        protected override DbSet<banks> GetDBSet(Entities context)
        {
            return context.bank;
        }

        protected override Expression<Func<banks, bool>> GetModelByIDExpression(int id)
        {
            return (m) => m.id == id;
        }

        protected override int GetModelID(Bank model)
        {
            return model.id;
        }

        protected override Expression<Func<banks, Bank>> CreateModelFromEntityExpression()
        {
            return (entity) => new Bank
            {
                id = entity.id,
                coin_id = entity.coin_id,
                qty = entity.qty
            };
        }

        protected override void CopyModelToEntity(Bank model, banks entity)
        {
            entity.id = model.id;
            entity.coin_id = model.coin_id;
            entity.qty = model.qty;
        }

        protected override void CopyEntityToModel(Bank model, banks entity)
        {
            model.id = entity.id;
        }
    }
}
