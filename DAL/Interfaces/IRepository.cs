using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IRepository<Tkey,TEntity>
    {
        IEnumerable<TEntity> GetAll();

        TEntity? GetOneById(Tkey id);

        TEntity? Create(TEntity entity);

        bool Update(Tkey id, TEntity entity);

        bool Delete(Tkey id);


    }
}
