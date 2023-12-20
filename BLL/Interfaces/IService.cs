using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IService<TKey,TModel>
    {
        // C
        TKey Create(TModel model);

        // R
        IEnumerable<TModel> GetAll();
        TModel GetOneById(TKey id);

        // U
        bool Update(TKey id, TModel modelUpdated);

        // D
        bool Delete(TKey id);
    }
}
