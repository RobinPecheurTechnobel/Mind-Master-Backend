using System.ComponentModel.DataAnnotations;

namespace Mind_Master_Backend.DTOs
{
    public class LabelDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
    public class LabelDataTO
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Le pseudo ne peut dépasser 100 caractères")]
        public string Title { get; set; }
    }
}
