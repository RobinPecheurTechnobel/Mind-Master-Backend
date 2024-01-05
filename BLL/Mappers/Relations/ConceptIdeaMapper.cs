using BLL.Models.Relations;
using DAL.Entities.Relations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mappers.Relations
{
    public static class ConceptIdeaMapper
    {
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
