using BLL.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Mind_Master_Backend.DTOs;
using Mind_Master_Backend.DTOs.Enums;
using Mind_Master_Backend.Mappers.Enums;
using System.Data;
using System.Runtime.CompilerServices;

namespace Mind_Master_Backend.Mappers
{
    public static class IdeaMapperDTO
    {
        public static IdeaDTO ToDTO(this IdeaModel model)
        {
            return new IdeaDTO
            {
                Id = model.Id,
                Content = model.Content,
                CreationDate = model.CreationDate,
                LastUpdateDate = model.LastUpdateDate,
                Source = model.Source,
                format = new EnumDTO(model.format.ToDTO())
            };
        }
        public static IdeaModel ToModel(this IdeaDataTO dataTO)
        {
            return new IdeaModel
            {
                Id = 0,
                Content = dataTO.Content,
                format = dataTO.format.ToModel(),
                Source = null,
                CreationDate = DateTime.Now,
                LastUpdateDate = DateTime.Now,
                ThinkerId = dataTO.ThinkerId
            };
        }
        
    }
}
