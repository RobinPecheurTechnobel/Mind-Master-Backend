using BLL.Interfaces;
using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ThinkerService : IService<int, ThinkerModel>
    {
        //TODO HERE
        public ThinkerModel Create(ThinkerModel model)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ThinkerModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public ThinkerModel GetOneById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(int id, ThinkerModel modelUpdated)
        {
            throw new NotImplementedException();
        }
    }
}
