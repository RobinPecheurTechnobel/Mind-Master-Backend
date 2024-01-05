using BLL.Models.Relations;
using Mind_Master_Backend.DTOs.Relations;

namespace Mind_Master_Backend.Mappers.Relations
{
    public static class ConceptAssemblyMapperDTO
    {
        public static ConceptAssemblyDTO ToDTO(this ConceptAssemblyModel model)
        {
            return new ConceptAssemblyDTO
            {
                Id = model.Id,
                Order = model.Order,
                Concept = model.Concept.ToDTO()
            };
        }
    }
}
