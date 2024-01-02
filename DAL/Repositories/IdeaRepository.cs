using DAL.Data;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class IdeaRepository : AbstractRepository<int, IdeaEntity>
    {
        public IdeaRepository(MindMasterContext mMContext)
        {
            _MMContext = mMContext;
            _dbSet = _MMContext.Ideas;
        }
        public override IdeaEntity MapperEntity(IdeaEntity oldOne, IdeaEntity entity)
        {
            oldOne.Format = entity.Format;
            oldOne.LastUpdateDate = DateTime.Now;
            oldOne.Content = entity.Content;
            oldOne.Source = entity.Source;

            return oldOne;
        }

        protected override Func<IdeaEntity, bool> PredicateIdentifier(int id)
        {
            return (i => i.Id == id);
        }

        public override void SaveChanges()
        {
            _MMContext.SaveChanges();
        }

        public IEnumerable<IdeaEntity> GetByThinker(int thinkerId)
        {
            return _dbSet.Include(i => i.Thinker)
                .Where(i => i.ThinkerId == thinkerId);
        }
    }
}
