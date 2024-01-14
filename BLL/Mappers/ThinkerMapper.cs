using BLL.Mappers.Enums;
using BLL.Models;
using BLL.Models.Relations;
using DAL.Entities;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;

namespace BLL.Mappers
{
    public static class ThinkerMapper
    {
        public static ThinkerModel ToModel(this ThinkerEntity entity)
        {
            return new ThinkerModel
            {
                Id = entity.Id,
                Login = entity.Login,
                HashPassword = entity.HashPassword,
                Role = entity.role.ToModel(),
                Pseudo = entity.Pseudo,
                Email = entity.Email,
                GroupThinkers = null
            };
        }
        public static ThinkerSimpleModel ToSimpleModel(this ThinkerEntity entity)
        {
            return new ThinkerSimpleModel
            {
                Id = entity.Id,
                Login = entity.Login,
                HashPassword = entity.HashPassword,
                Role = entity.role.ToModel(),
                Pseudo = entity.Pseudo,
                Email = entity.Email
            };
        }
        public static ThinkerEntity ToEntity(this ThinkerSimpleModel model)
        {
            return new ThinkerEntity
            {
                Id = model.Id,
                Login = model.Login,
                HashPassword = model.HashPassword!,
                role = model.Role.ToEntity(),
                Pseudo = model.Pseudo,
                Email = model.Email
            };
        }
        public static JsonPatchDocument<ThinkerEntity> ToJsonPatchDocumentEntity(this JsonPatchDocument<ThinkerModel> jpd)
        {
            return new JsonPatchDocument<ThinkerEntity>(
                new List<Operation<ThinkerEntity>>(jpd.Operations.Select(op => op.ToOperationEntity())),
                jpd.ContractResolver);
        }
        public static Operation<ThinkerEntity> ToOperationEntity(this Operation<ThinkerModel> o)
        {
            return new Operation<ThinkerEntity>
            {
                from = o.from,
                op = o.op,
                path = o.path,
                value = o.value
            };
        }

    }
}
