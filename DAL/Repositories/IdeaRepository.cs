using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class IdeaRepository : AbstractRepository<int, IdeaEntity>
    {
        public override IdeaEntity MapperEntity(IdeaEntity oldOne, IdeaEntity entity)
        {
            oldOne.Format = entity.Format;
            oldOne.LastUpdateDate = DateTime.Now;
            oldOne.Content = oldOne.Content;


            throw new NotImplementedException();
        }

        protected override Func<IdeaEntity, bool> PredicateIdentifier(int id)
        {
            return (i => i.Id == id);
        }

        public override void SaveChanges()
        {
            _MMContext.SaveChanges();
        }
    }
}
