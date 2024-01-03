using BLL.Models.Relations;
using Mind_Master_Backend.DTOs;

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
    }
}
