namespace Mind_Master_Backend.DTOs.Relations
{
    public class ConceptAssemblyDTO
    {
        public int Id { get; set; }
        public uint Order { get; set; }
        public ConceptDTO Concept { get; set; }
    }
}
