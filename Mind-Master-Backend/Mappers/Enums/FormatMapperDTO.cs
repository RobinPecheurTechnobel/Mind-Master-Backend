using BLL.Models.Enums;
using Mind_Master_Backend.DTOs.Enums;

namespace Mind_Master_Backend.Mappers.Enums
{
    public static class FormatMapperDTO
    {
        public static FormatDTO ToDTO(this FormatModel model) { return (FormatDTO)model; }

        public static FormatModel ToModel(this FormatDTO dto) { return (FormatModel)dto; }
    }
}
