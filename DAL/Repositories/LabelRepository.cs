using DAL.Data;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class LabelRepository : AbstractRepository<int, LabelEntity>
    {
        public LabelRepository(MindMasterContext mMContext)
        {
            _MMContext = mMContext;
            _dbSet = _MMContext.Labels;
        }
        public override LabelEntity MapperEntity(LabelEntity oldOne, LabelEntity entity)
        {
            oldOne.Title = entity.Title;

            return oldOne;

        }

        protected override Func<LabelEntity, bool> PredicateIdentifier(int id)
        {
            return l => l.Id == id;
        }
        public override void SaveChanges()
        {
            _MMContext.SaveChanges();
        }
    }
}
