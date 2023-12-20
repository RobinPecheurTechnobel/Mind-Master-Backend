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

        Tkey Create(TEntity entity);

        bool Update(Tkey key, TEntity entity);

        bool Delete(Tkey key);


    }
}
