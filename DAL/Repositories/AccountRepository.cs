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
    public class AccountRepository : IRepository<int,AccountEntity>
    {
        private MindMasterContext _MMContext;

        public AccountRepository(MindMasterContext mMContext)
        {
            _MMContext = mMContext;
        }


        // Crud TODO check for unicity
        public AccountEntity? Create(AccountEntity entity)
        {
            if (isLoginAlredayUsed(entity.Login)) return null;

            _MMContext.Accounts.Add(entity);
            _MMContext.SaveChanges();
            return entity;
        }

        // cRud
        public IEnumerable<AccountEntity> GetAll()
        {
            return _MMContext.Accounts;
        }

        public AccountEntity? GetOneById(int id)
        {
            return _MMContext.Accounts.Where(account => account.Id == id).FirstOrDefault();
        }

        // crUd TODO
        public bool Update(int id, AccountEntity entity)
        {

            AccountEntity? accountExist = GetOneById(id);

            if (accountExist is null) return false;

            accountExist.Login = entity.Login;
            accountExist.Role = entity.Role;

            _MMContext.Accounts.Update(accountExist);
            _MMContext.SaveChanges();
            return true;
        }

        // cruD TODO
        public bool Delete(int id)
        {
            AccountEntity? entity = GetOneById(id);

            if (entity is null) return false;

            _MMContext.Accounts.Remove(entity);
            _MMContext.SaveChanges();

            return true;
        }
        public AccountEntity? FindByLogin(string login)
        {
            return _MMContext.Accounts.Where(account => account.Login == login).FirstOrDefault();
        }
        //Contraintes
        public bool isLoginAlredayUsed(string login)
        {
            int count = _MMContext.Accounts.Count(account => account.Login == login);

            if (count > 0) return true;
            return false;
        }
    }
}
