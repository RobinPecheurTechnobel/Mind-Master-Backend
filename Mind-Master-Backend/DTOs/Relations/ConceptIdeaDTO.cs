using BLL.Models;

namespace Mind_Master_Backend.DTOs.Relations
{
    public class ConceptIdeaDTO
    {
        public int Id { get; set; }
        public uint Order { get; set; }
        public IdeaDTO Idea { get; set; }
    }
}
