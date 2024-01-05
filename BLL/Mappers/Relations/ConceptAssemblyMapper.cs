using BLL.Models.Relations;
using DAL.Entities.Relations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mappers.Relations
{
    public static class ConceptAssemblyMapper
    {
        public static ConceptAssemblyModel ToConceptAssmblyModel(this ConceptAssemblyEntity entity)
        {
            ConceptAssemblyModel cam = new ConceptAssemblyModel
            {
                Id = entity.Id,
                Order = entity.Order,
                Concept = entity.Concept.ToModel()
            };
            if(entity.ConceptIdeas is not null && entity.ConceptIdeas.Count() > 1)
            {
                cam.Concept.Ideas = entity.ConceptIdeas.Select(ci => ci.ToConceptIdeaModel());
            }
            return cam;
        }
    }
}
