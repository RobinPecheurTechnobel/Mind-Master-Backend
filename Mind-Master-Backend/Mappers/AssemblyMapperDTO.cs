using BLL.Models;
using Mind_Master_Backend.DTOs;
using Mind_Master_Backend.Mappers.Relations;

namespace Mind_Master_Backend.Mappers
{
    public static class AssemblyMapperDTO
    {
        public static AssemblyDTO ToDTO(this AssemblyModel model)
        {
            return new AssemblyDTO
            {
                Id = model.Id,
                Title = model.Title,
                Concepts = model.Concepts is null ? null : model.Concepts.Select(c => c.ToDTO())
            };
        }
        public static AssemblyModel ToModel(this AssemblyDataTO data)
        {
            return new AssemblyModel
            {
                Id = 0,
                Concepts = null,
                Title = data.Title
            };
        }
    }
}
