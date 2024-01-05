using Mind_Master_Backend.DTOs.Relations;
using System.ComponentModel.DataAnnotations;

namespace Mind_Master_Backend.DTOs
{
    public class AssemblyDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public IEnumerable<ConceptAssemblyDTO> Concepts { get; set; }
    }
    public class AssemblyDataTO
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Le titre de cette assemblage ne peut dépasser 100 caractères")]
        public string Title { get; set; }
    }
}
