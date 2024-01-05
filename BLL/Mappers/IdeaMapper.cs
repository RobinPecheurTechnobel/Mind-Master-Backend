using BLL.Mappers.Enums;
using BLL.Models;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mappers
{
    public static class IdeaMapper
    {
        public static IdeaModel ToModel(this IdeaEntity entity)
        {
            return new IdeaModel
            {
                Id = entity.Id,
                Content = entity.Content,
                CreationDate = entity.CreationDate,
                format = entity.format.ToModel(),
                LastUpdateDate = entity.LastUpdateDate,
                Source = entity.Source,
                Thinker = entity.Thinker is null ? null : entity.Thinker.ToModel(),
                ThinkerId = entity.ThinkerId
            };
        }
        public static IdeaEntity ToEntity(this IdeaModel model)
        {
            IdeaEntity idea = new IdeaEntity
            {
                Id = model.Id,
                Content = model.Content,
                CreationDate = model.CreationDate,
                LastUpdateDate = model.LastUpdateDate,
                format = model.format.ToEntity(),
                Source = model.Source,
                ThinkerId = model.ThinkerId==0? model.Thinker.Id: model.ThinkerId
            };
            if (model.Thinker is not null) idea.Thinker = model.Thinker.ToEntity();
            return idea;
        }
    }
}
