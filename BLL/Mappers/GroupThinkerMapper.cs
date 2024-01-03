using BLL.Models.Relations;
using DAL.Entities;

namespace BLL.Mappers
{
    public static class GroupThinkerMapper
    {
        public static GroupThinkerEntity ToEntity(this GroupThinkerModel model)
        {
            return new GroupThinkerEntity
            {
                Group = model.Group.ToEntiTy(),
                GroupId = model.Group.Id,
                Thinker = model.Thinker.ToEntity(),
                ThinkerId = model.Thinker.Id,
                isOwner = model.isOwner
            };
        }
        public static GroupThinkerModel ToModel(this GroupThinkerEntity entity)
        {
            return new GroupThinkerModel
            {
                Group = entity.Group.ToSimpleModel(),
                Thinker = entity.Thinker.ToSimpleModel(),
                isOwner = entity.isOwner
            };
        }
    }
}
