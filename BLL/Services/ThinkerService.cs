using BLL.CustomExceptions;
using BLL.Interfaces;
using BLL.Mappers;
using BLL.Mappers.Relations;
using BLL.Models;
using BLL.Models.Enums;
using DAL.Entities;
using DAL.Repositories;
using Isopoh.Cryptography.Argon2;
using Isopoh.Cryptography.SecureArray;
using System.Security.Cryptography;
using System.Text;

namespace BLL.Services
{
    public class ThinkerService : AbstractService<int,ThinkerModel,ThinkerEntity>
    {
        private Argon2Service _ArgonService;
        private GroupRepository _groupRepository;

        public ThinkerService(ThinkerRepository thinkerRepository, Argon2Service argon2Service, GroupRepository groupRepository)
        {
            _repository = thinkerRepository;
            _ArgonService = argon2Service;
            _groupRepository = groupRepository;
        }

        // Crud
        // TODO ARGON2Service
        public override ThinkerModel Create(ThinkerModel model)
        {
            try
            {
                LoginAlreadyUsed(model);

                model.HashPassword = _ArgonService.Hash(model.HashPassword);

                ThinkerModel? result = _repository.Create(model.ToEntity())?.ToModel();

                if (result is null) throw new Exception();

                GroupSimpleModel privateGroup = new GroupSimpleModel {
                    Name = $"Espace de {result.Pseudo}",
                    Description = $"Espace personnel de {result.Pseudo}"
                };
                int privateId = _groupRepository.Create(privateGroup.ToEntiTy())!.Id;

                _groupRepository.AddThinkerToGroup(1, result.Id);
                _groupRepository.AddThinkerToGroup(privateId, result.Id);

                return result;
            }
            catch(Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }


        // crUd TODO
        public override bool Update(int id, ThinkerModel modelUpdated)
        {
            try
            {
                IdUsed(id);
                
                ThinkerModel previously = GetOneById(id);
                // La vérification du pseudo n'a de sens qu'en cas de changement
                if(modelUpdated.Login != previously.Login)LoginAlreadyUsed(modelUpdated);

                if (!Enum.IsDefined(typeof(RoleModel), modelUpdated.Role)) modelUpdated.Role = previously.Role;

                return ((ThinkerRepository)_repository).Update(id,modelUpdated.ToEntity());
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }


        // Login
        public ThinkerModel? Login(string login, string password)
        {
            ThinkerModel? account = ((ThinkerRepository)_repository).FindByLogin(login)?.ToModel();

            if (account is null) throw new ThinkerNotFoundException();

            if (!_ArgonService.Verify(password, account.HashPassword)) throw new InvalidCredentialException();

            return account;
        }

        // Check Exception TODO Create custom exception
        public void LoginAlreadyUsed(ThinkerModel model)
        {
            if (((ThinkerRepository)_repository).isLoginAlredayUsed(model.ToEntity().Login))
                throw new LoginUniqueException();
        }

        public override void IdUsed(int id)
        {
            if (_repository.GetOneById(id) is null)
                throw new ThinkerNotFoundException();
        }
        public override ThinkerModel GetOneById(int id)
        {
            ThinkerModel thinker = _repository.GetOneById(id).ToModel();
            thinker.GroupThinkers = ((ThinkerRepository)_repository).getGroups(id).Select(gt => gt.ToModel());
            return thinker;
        }

        public override ThinkerModel MapperModel(ThinkerEntity entity)
        {
            return entity.ToModel();
        }

        public override ThinkerEntity MapperEntity(ThinkerModel model)
        {
            return model.ToEntity();
        }

        public IEnumerable<GroupSimpleModel> GetGroup(int thinkerId)
        {
            try
            {
                IdUsed(thinkerId);

                IEnumerable<GroupSimpleModel> groups = ((ThinkerRepository)_repository).getGroups(thinkerId).Select(gt => gt.Group.ToModel());
                return groups;
            }
            catch(NotFoundException nFException)
            {
                throw new NotFoundException(nFException.Message);
            }
            catch(Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public IEnumerable<ThinkerModel> GetByInformation(string information)
        {
            return ((ThinkerRepository)_repository).GetByInformation(information).Select(t => t.ToModel());
        }
    }
}
