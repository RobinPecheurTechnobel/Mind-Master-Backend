using DAL.Data;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ThinkerRepository : IRepository<int, ThinkerEntity>
    {
        private MindMasterContext _MMContext;

        public ThinkerRepository(MindMasterContext mMContext)
        {
            _MMContext = mMContext;
        }

        public ThinkerEntity? Create(ThinkerEntity entity)
        {
            _MMContext.Thinkers.Add(entity);
            _MMContext.SaveChanges();
            return entity;
        }

        public bool Delete(int id)
        {
            ThinkerEntity? entity = GetOneById(id);

            if (entity is null) return false;

            _MMContext.Thinkers.Remove(entity);
            _MMContext.SaveChanges();

            return true;
        }

        public IEnumerable<ThinkerEntity> GetAll()
        {
            return _MMContext.Thinkers;
        }

        public ThinkerEntity? GetOneById(int id)
        {
            return _MMContext.Thinkers.Where(thinker => thinker.Id == id).FirstOrDefault();
        }

        public bool Update(int id, ThinkerEntity entity)
        {
            ThinkerEntity? thinkerExist = GetOneById(id);

            if (thinkerExist is null) return false;

            thinkerExist.FirstName = entity.FirstName;
            thinkerExist.LastName = entity.LastName;
            thinkerExist.Pseudo = entity.Pseudo;
            thinkerExist.Email = entity.Email;

            _MMContext.Thinkers.Update(thinkerExist);
            _MMContext.SaveChanges();
            return true;
        }
    }
}
