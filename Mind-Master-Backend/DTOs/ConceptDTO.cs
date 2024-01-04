using Mind_Master_Backend.DTOs.Relations;
using System.ComponentModel.DataAnnotations;

namespace Mind_Master_Backend.DTOs
{
    public class ConceptDTO
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public IEnumerable<ConceptIdeaDTO> Ideas { get; set; }
    }
    public class ConceptDataTO
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Le pseudo ne peut dépasser 100 caractères")]
        public string Title { get; set; }
    }
}
