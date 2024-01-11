using BLL.Models.Relations;
using DAL.Entities;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;

namespace BLL.Mappers.Relations
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

        public static JsonPatchDocument<GroupThinkerEntity> ToJsonPatchDocumentEntity(this JsonPatchDocument<GroupThinkerModel> jpd)
        {
            return new JsonPatchDocument<GroupThinkerEntity>(
                new List<Operation<GroupThinkerEntity>>(jpd.Operations.Select(op => op.ToOperationEntity())),
                jpd.ContractResolver);
        }
        public static Operation<GroupThinkerEntity> ToOperationEntity(this Operation<GroupThinkerModel> o)
        {
            return new Operation<GroupThinkerEntity>
            {
                from = o.from,
                op = o.op,
                path = o.path,
                value = o.value
            };
        }
    }
}
