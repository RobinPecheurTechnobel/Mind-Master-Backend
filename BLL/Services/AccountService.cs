using BLL.CustomExceptions;
using BLL.Interfaces;
using BLL.Mappers;
using BLL.Models;
using DAL.Repositories;
using Isopoh.Cryptography.Argon2;
using Isopoh.Cryptography.SecureArray;
using System.Security.Cryptography;
using System.Text;

namespace BLL.Services
{
    public class AccountService : IService<int, AccountModel>
    {
        private AccountRepository _AccountRepository;
        private Argon2Service _ArgonService;

        public AccountService(AccountRepository accountRepository, Argon2Service argon2Service)
        {
            _AccountRepository = accountRepository;
            _ArgonService = argon2Service;
        }

        // Crud
        // TODO ARGON2Service
        public int Create(AccountModel model)
        {
            try
            {
                LoginAlreadyUsed(model);

                model.HashPassword = _ArgonService.Hash(model.HashPassword);

                int idCreated = _AccountRepository.Create(model.ToEntity());
                return idCreated;
            }
            catch(Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        // cRud
        public IEnumerable<AccountModel> GetAll()
        {
            return _AccountRepository.GetAll().Select(a => a.ToModel());
        }

        public AccountModel GetOneById(int id)
        {
            try
            {
                IdUsed(id);
                AccountModel result = _AccountRepository.GetOneById(id)!.ToModel();
                return result;
            }
            catch(Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        // crUd TODO
        public bool Update(int id, AccountModel modelUpdated)
        {
            try
            {
                IdUsed(id);
                
                AccountModel previously = GetOneById(id);
                // La vérification du pseudo n'a de sens qu'en cas de changement
                if(modelUpdated.Login != previously.Login)LoginAlreadyUsed(modelUpdated);

                if (!Enum.IsDefined(typeof(RoleModel), modelUpdated.Role)) modelUpdated.Role = previously.Role;

                return _AccountRepository.Update(id,modelUpdated.ToEntity());
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        // cruD TODO
        public bool Delete(int id)
        {
            try
            {
                IdUsed(id);
                return _AccountRepository.Delete(id);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        // Login
        public AccountModel? Login(string login, string password)
        {
            AccountModel? account = _AccountRepository.FindByLogin(login)?.ToModel();

            if (account is null) throw new AccountNotFoundException();

            if (!_ArgonService.Verify(password, account.HashPassword)) throw new InvalidCredentialException();

            return account;
        }

        // Check Exception TODO Create custom exception
        public void LoginAlreadyUsed(AccountModel model)
        {
            if (_AccountRepository.isLoginAlredayUsed(model.ToEntity().Login))
                throw new LoginUniqueException();
        }
        public void IdUsed(int id)
        {
            if (_AccountRepository.GetOneById(id) is null)
                throw new AccountNotFoundException();
        }
    }
}
