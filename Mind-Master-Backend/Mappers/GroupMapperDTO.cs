using BLL.Models;
using Mind_Master_Backend.DTOs;

namespace Mind_Master_Backend.Mappers
{
    public static class GroupMapperDTO
    {
        public static GroupDTO ToDTO(this GroupSimpleModel model)
        {
            return new GroupDTO
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description
            };
        }
        public static GroupModel ToModel(this GroupDTO dto)
        {
            return new GroupModel
            {
                Id = dto.Id,
                Name = dto.Name,
                Description =dto.Description
            };
        }
        public static GroupModel ToNewModel(this GroupDataToObject dto)
        {
            return new GroupModel
            {
                Id = 0,
                Name = dto.Name,
                Description = dto.Description
            };
        }
    }
}
