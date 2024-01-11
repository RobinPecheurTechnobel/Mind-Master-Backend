using BLL.Models.Relations;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Mind_Master_Backend.DTOs;
using Newtonsoft.Json.Serialization;

namespace Mind_Master_Backend.Mappers
{
    public static class GroupThinkerMapperDTO
    {
        public static GroupThinkerInThinkerDTO ToInternalGroupDTO(this GroupThinkerModel model)
        {
            return new GroupThinkerInThinkerDTO
            {
                Group = model.Group.ToDTO(),
                isOwner = model.isOwner
            };
        }
        public static GroupThinkerInGroupDTO ToInternalThinkerDTO(this GroupThinkerModel model)
        {
            return new GroupThinkerInGroupDTO
            {
                Thinker = model.Thinker.ToDTO(),
                isOwner = model.isOwner
            };
        }
        public static JsonPatchDocument<GroupThinkerModel> ToJsonPatchDocumentModel(this JsonPatchDocument<GroupThinkerInGroupDTO>  jpd)
        {
            if (jpd.Operations is null || jpd.Operations.Where(op => op is not null).Count() < 1) return null;
            return new JsonPatchDocument<GroupThinkerModel>(
                new List<Operation<GroupThinkerModel>> (jpd.Operations.Select(op => op.ToOperationModel())), 
                jpd.ContractResolver);
        }
        public static Operation<GroupThinkerModel> ToOperationModel(this Operation<GroupThinkerInGroupDTO> o)
        {
            List<string> pathAutorized = new List<string> { "isOwner" };
            if (pathAutorized.Where(path => o.path.Contains(path)).Count() < 1) return null;
            return new Operation<GroupThinkerModel>
            {
                from = o.from,
                op =o.op,
                path = o.path,
                value = o.value
            };
        }
    }
}
