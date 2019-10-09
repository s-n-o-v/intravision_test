namespace Core.Data.Storage
{
    using System;
    using System.Data;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    //using System.Data.Objects;

    public abstract class ModelDataStorage<TModel, TEntity> : DataStorageBase, IModelDataStorage<TModel>
        where TModel : class
        where TEntity : class, new()
    {
        public ModelDataStorage(string connectionString)
            : base(connectionString)
        {
            this.CreateFromEntity = CreateModelFromEntityExpression().Compile();
        }

        public Func<TEntity, TModel> CreateFromEntity { get; private set; }

        public virtual System.Collections.Generic.IEnumerable<TModel> GetList()
        {
            using (var context = new Entities())
            {
                return this.GetAllQuery(context).Select(this.CreateModelFromEntityExpression()).ToList();
            }
        }

        public virtual TModel GetByID(int id)
        {
            using (var context = new Entities())
            {
                var resultEntity = this.GetEntityByID(id, context);
                var convertFunc = this.CreateModelFromEntityExpression().Compile();

                return convertFunc(resultEntity);
            }
        }

        public virtual long Count()
        {
            using (var context = new Entities())
            {
                return this.GetAllQuery(context).Count();
            }
        }

        public virtual void Save(TModel model)
        {
            if (this.GetModelID(model) != 0)
            {
                this.Update(model);
            }
            else
            {
                this.Create(model);
            }
        }

        public virtual void Delete(int id)
        {
            using (var context = new Entities())
            {
                TEntity model = this.GetEntityByID(id, context);
                this.GetDBSet(context).Remove(model);
                context.SaveChanges();
            }
        }

        protected abstract DbSet<TEntity> GetDBSet(Entities context);

        protected abstract Expression<Func<TEntity, bool>> GetModelByIDExpression(int id);

        protected abstract int GetModelID(TModel model);

        protected abstract Expression<Func<TEntity, TModel>> CreateModelFromEntityExpression();

        protected abstract void CopyModelToEntity(TModel model, TEntity entity);

        protected abstract void CopyEntityToModel(TModel model, TEntity entity);

        protected TEntity CreateEntityFromModel(TModel model)
        {
            var result = new TEntity();
            this.CopyModelToEntity(model, result);

            return result;
        }

        protected virtual IQueryable<TEntity> GetAllQuery(Entities context)
        {
            return this.GetDBSet(context);
        }

        protected virtual void UpdateModelID(TEntity entity, TModel model)
        {
            
        }

        protected virtual void Create(TModel model)
        {
            using (var context = new Entities())
            {
                var entity = CreateEntityFromModel(model);
                this.GetDBSet(context).Add(entity);
                context.SaveChanges();
                this.CopyEntityToModel(model, entity);
            }
        }

        protected virtual void Update(TModel model)
        {
            using (var context = new Entities())
            {
                var entity = GetEntityByID(this.GetModelID(model), context);
                this.CopyModelToEntity(model, entity);

                context.SaveChanges();
                //this.UpdateModelID(entity, model);
            }
        }

        private TEntity GetEntityByID(int id, Entities context)
        {
            var result = this.GetAllQuery(context).FirstOrDefault(this.GetModelByIDExpression(id));
            return result;
        }
    }
}
