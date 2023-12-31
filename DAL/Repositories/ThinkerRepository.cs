using DAL.Data;
using DAL.Entities;
using DAL.Entities.Relations;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ThinkerRepository : AbstractRepository<int, ThinkerEntity>
    {
        private MindMasterContext _MMContext;

        public ThinkerRepository(MindMasterContext mMContext)
        {
            _MMContext = mMContext;
            _dbSet = _MMContext.Thinkers;
        }


        // Crud
        public override ThinkerEntity? Create(ThinkerEntity entity)
        {
            if (isLoginAlredayUsed(entity.Login)) return null;

            entity.Pseudo = entity.Login;

            _MMContext.Thinkers.Add(entity);
            _MMContext.SaveChanges();




            return entity;
        }

        // cRud
        public IEnumerable<ThinkerEntity> GetAll()
        {
            return _MMContext.Thinkers;
        }

        public ThinkerEntity? FindByLogin(string login)
        {
            return _MMContext.Thinkers.Where(account => account.Login == login).FirstOrDefault();
        }
        //Contraintes
        public bool isLoginAlredayUsed(string login)
        {
            int count = _MMContext.Thinkers.Count(account => account.Login == login);

            if (count > 0) return true;
            return false;
        }


        protected override Func<ThinkerEntity, bool> PredicateIdentifier(int id)
        {
            return t => t.Id == id;
        }

        public override ThinkerEntity MapperEntity(ThinkerEntity oldOne, ThinkerEntity entity)
        {
            ThinkerEntity fusion = oldOne;

            oldOne.Login = entity.Login;
            oldOne.Pseudo = entity.Pseudo;
            oldOne.Role = entity.Role;
            oldOne.Email = entity.Email ?? oldOne.Email;

            return fusion;
        }
        public override void SaveChanges()
        {
            _MMContext.SaveChanges();
        }

        public IEnumerable<GroupThinkerEntity> getGroups(int id)
        {
            IEnumerable < GroupThinkerEntity > groups =_MMContext.GroupThinkers
                .Include(gt => gt.Group)
                .Where(gt => gt.ThinkerId == id);
            return groups;
        }
    }
}
