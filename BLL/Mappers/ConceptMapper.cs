using BLL.Models;
using BLL.Models.Relations;
using DAL.Entities;
using DAL.Entities.Relations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mappers
{
    public static class ConceptMapper
    {
        public static ConceptSimpleModel ToSimpleModel(this ConceptEntity entity)
        {
            return new ConceptSimpleModel
            {
                Id = entity.Id,
                Title = entity.Title
            };
        }
        public static ConceptEntity ToEntity(this ConceptSimpleModel model)
        {
            return new ConceptEntity
            {
                Id = model.Id,
                Title = model.Title
            };
        }
        public static ConceptModel ToModel(this ConceptEntity entity)
        {
            return new ConceptModel
            {
                Id = entity.Id,
                Title = entity.Title
            };
        }
        public static ConceptIdeaModel ToConceptIdeaModel(this ConceptIdeaEntity entity)
        {
            return new ConceptIdeaModel
            {
                Id = entity.Id,
                Idea = entity.Idea.ToModel(),
                Order = entity.Order
            };
        }
    }
}
