using BLL.CustomExceptions;
using BLL.Interfaces;
using BLL.Mappers;
using BLL.Mappers.Relations;
using BLL.Models;
using BLL.Models.Relations;
using DAL.Entities;
using DAL.Repositories;
using Microsoft.AspNetCore.JsonPatch;
using System.Globalization;

namespace BLL.Services
{
    public class GroupService : AbstractService<int, GroupModel, GroupEntity>
    {
        private ThinkerService _thinkerService;
        public GroupService(GroupRepository thinkerRepository, ThinkerService thinkerService)
        {
            _thinkerService = thinkerService;
            _repository = thinkerRepository;
        }

        public override void IdUsed(int id)
        {
            if (_repository.GetOneById(id) is null)
                throw new GroupNotFoundException();
        }

        public override GroupEntity MapperEntity(GroupModel model)
        {
            return model.ToEntiTy();
        }

        public override GroupModel MapperModel(GroupEntity entity)
        {
            GroupModel group = entity.ToModel();
            group.GroupThinkers = ((GroupRepository)_repository).getMembers(entity.Id).Select(gt => gt.ToModel());
            return group;
        }

        public GroupSimpleModel GetOneById(int id)
        {
            try
            {
                IdUsed(id);

                return _repository.GetOneById(id).ToSimpleModel();
            }
            catch(NotFoundException nFException)
            {
                throw new NotFoundException(nFException.Message);
            }
        }

        public IEnumerable<GroupThinkerModel> getThinkers(int groupId)
        {
            IEnumerable<GroupThinkerEntity> gts = ((GroupRepository)_repository).getMembers(groupId);
            IEnumerable < GroupThinkerModel > gt = gts.Select(gt => gt.ToModel());
            return gt;
        }

        public int AddThinkerToGroup(int groupId, int thinkerId)
        {
            try
            {
                return AddThinkerToGroup(groupId, thinkerId, false);
            }
            catch (NotFoundException nFException)
            {
                throw new Exception(nFException.Message);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }

        }
        public int AddThinkerToGroup(int groupId, int thinkerId, bool isowner)
        {
            try
            {
                IdUsed(groupId);
                _thinkerService.IdUsed(thinkerId);
                return ((GroupRepository)_repository).AddThinkerToGroup(groupId, thinkerId, isowner);
            }
            catch (NotFoundException nFException)
            {
                throw new Exception(nFException.Message);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public bool RemoveThinkerToGroup(int groupId, int thinkerId)
        {
            try
            {
                IdUsed(groupId);
                _thinkerService.IdUsed(thinkerId);

                IsThenGroupWithoutOwner(groupId, thinkerId);

                return ((GroupRepository)_repository).RemoveThinkerToGroup(groupId, thinkerId);
            }
            catch(NotFoundException nFException)
            {
                throw new Exception(nFException.Message);
            }
            catch(Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public bool UpdateThinkerToGroup(int groupId, int thinkerId, JsonPatchDocument<GroupThinkerModel> patch)
        {
            try
            {
                IdUsed(groupId);
                _thinkerService.IdUsed(thinkerId);

                if (patch is null) throw new UnAuthorizedPatchOperation();

                if(patch.Operations.Where(o => o.path == "/isOwner").Count() > 0)
                    IsThenGroupWithoutOwner(groupId, thinkerId);

                return ((GroupRepository)_repository).UpdateThinkerToGroup(groupId, thinkerId, patch.ToJsonPatchDocumentEntity());
            }
            catch (NotFoundException nFException)
            {
                throw new Exception(nFException.Message);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        private void IsThenGroupWithoutOwner(int groupId, int thinkerId)
        {
            if (getThinkers(groupId).Where(gt => gt.isOwner && gt.Thinker.Id != thinkerId).Count() < 1)
                throw new GroupWithoutOwnerException();
        }
    }
}
