using DAL.Entities.Enums;
using Mind_Master_Backend.DTOs.Enums;
using System.ComponentModel.DataAnnotations;

namespace Mind_Master_Backend.DTOs
{
    public class IdeaDTO
    {

        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public EnumDTO format { get; set; }

        public string? Content { get; set; }
        public string? Source { get; set; }

    }
    public class IdeaDataTO
    {
        [Required]
        public FormatDTO format { get; set; }

        public string? Content { get; set; }
        [Required]
        public int ThinkerId { get; set; }
    }
}
