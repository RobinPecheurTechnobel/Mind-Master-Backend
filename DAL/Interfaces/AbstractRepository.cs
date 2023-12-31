using DAL.Data;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public abstract class AbstractRepository<TKey, TEntity> : IRepository<TKey, TEntity> where TEntity : class
    {
        protected MindMasterContext _MMContext;
        protected DbSet<TEntity> _dbSet;

        public virtual TEntity? Create(TEntity entity)
        {
            _dbSet.Add(entity);
            SaveChanges();
            return entity;
        }

        public virtual bool Delete(TKey id)
        {
            TEntity? entity = GetOneById(id);

            if (entity is null) return false;

            _dbSet.Remove(entity);
            SaveChanges();

            return true;
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return _dbSet;
        }

        public virtual TEntity? GetOneById(TKey id)
        {
            return _dbSet.Where(PredicateIdentifier(id)).FirstOrDefault();
        }
        protected abstract Func<TEntity,bool> PredicateIdentifier(TKey id);

        public virtual bool Update(TKey id, TEntity entity)
        {
            TEntity? entityExist = GetOneById(id);

            if (entityExist is null) return false;

            entityExist = MapperEntity(entityExist,entity);

            _dbSet.Update(entityExist);
            SaveChanges();
            return true;
        }
        public abstract TEntity MapperEntity(TEntity oldOne,TEntity entity);
        public virtual void SaveChanges()
        {
            _MMContext.SaveChanges();
        }
    }
}
