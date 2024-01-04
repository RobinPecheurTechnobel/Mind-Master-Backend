using BLL.Models.Relations;
using Mind_Master_Backend.DTOs.Relations;

namespace Mind_Master_Backend.Mappers.Relations
{
    public static class ConceptIdeaMapperDTO
    {
        public static ConceptIdeaDTO ToDTO(this ConceptIdeaModel model)
        {
            return new ConceptIdeaDTO
            {
                Id = model.Id,
                Idea = model.Idea.ToDTO(),
                Order = model.Order
            };
        }
    }
}
