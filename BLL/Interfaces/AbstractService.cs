using BLL.CustomExceptions;
using BLL.Models;
using BLL.Services;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public  abstract class AbstractService<Tkey,TModel,TEntity> :IService<Tkey,TModel>
    {
        protected IRepository<Tkey, TEntity> _repository;

        public virtual TModel GetOneById(Tkey id)
        {
            try
            {
                IdUsed(id);
                TModel result = MapperModel(_repository.GetOneById(id));
                return result;
            }
            catch(NotFoundException exception)
            {
                throw new NotFoundException(exception.Message);
            }
        }

        public virtual TModel Create(TModel model)
        {
            try
            {
                TModel? result = MapperModel(_repository.Create(MapperEntity(model)));

                if (result is null) throw new Exception();

                return result;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public virtual IEnumerable<TModel> GetAll()
        {
            return _repository.GetAll().Select(Te => MapperModel(Te));
        }
        public virtual bool Update(Tkey id, TModel modelUpdated)
        {
            try
            {
                IdUsed(id);

                TModel previously = GetOneById(id);
                return _repository.Update(id, MapperEntity(modelUpdated));
            }

            catch (NotFoundException exception)
            {
                throw new NotFoundException(exception.Message);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
        public virtual bool Delete(Tkey id) 
        {
            try
            {
                IdUsed(id);
                return _repository.Delete(id);
            }
            catch (NotFoundException exception)
            {
                throw new NotFoundException(exception.Message);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public abstract void IdUsed(Tkey id);
        public abstract TModel MapperModel(TEntity entity);
        public abstract TEntity MapperEntity(TModel model);

    }
}
