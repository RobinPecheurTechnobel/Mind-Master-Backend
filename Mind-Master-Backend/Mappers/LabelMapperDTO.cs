using BLL.Models;
using Mind_Master_Backend.DTOs;

namespace Mind_Master_Backend.Mappers
{
    public static class LabelMapperDTO
    {
        public static LabelDTO ToDTO(this LabelModel model)
        {
            return new LabelDTO
            {
                Id = model.Id,
                Title = model.Title
            };
        }
        public static LabelModel ToModel(this LabelDataTO dto)
        {
            return new LabelModel
            {
                Id = 0,
                Title = dto.Title
            };
        }
    }
}
