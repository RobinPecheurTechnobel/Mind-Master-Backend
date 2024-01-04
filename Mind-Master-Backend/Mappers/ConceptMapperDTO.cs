using BLL.Models;
using Mind_Master_Backend.DTOs;
using Mind_Master_Backend.Mappers.Relations;

namespace Mind_Master_Backend.Mappers
{
    public static class ConceptMapperDTO
    {
        public static ConceptDTO ToDTO(this ConceptModel model)
        {
            return new ConceptDTO
            {
                Id = model.Id,
                Title = model.Title,
                Ideas = model.Ideas is null ? null : model.Ideas.Select(i => i.ToDTO())
            };
        }
        public static ConceptModel ToModel(this ConceptDataTO data)
        {
            return new ConceptModel
            {
                Id = 0,
                Ideas = null,
                Title = data.Title
            };
        }
    }
}
