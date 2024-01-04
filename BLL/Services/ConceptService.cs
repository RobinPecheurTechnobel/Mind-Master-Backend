using BLL.CustomExceptions;
using BLL.Interfaces;
using BLL.Mappers;
using BLL.Models;
using BLL.Models.Relations;
using DAL.Entities;
using DAL.Entities.Relations;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ConceptService : AbstractService<int, ConceptModel, ConceptEntity>
    {
        private LabelRepository _LabelRepository;
        private IdeaService _IdeaService;
        public ConceptService(ConceptRepository conceptRepository, LabelRepository labelRepository, IdeaService ideaService)
        {
            _repository = conceptRepository;
            _LabelRepository = labelRepository;
            _IdeaService = ideaService;
        }

        public override void IdUsed(int id)
        {
            if (_repository.GetOneById(id) is null)
                throw new ConceptNotFoundException();
        }

        public override ConceptEntity MapperEntity(ConceptModel model)
        {
            return model.ToEntity();
        }

        public override ConceptModel MapperModel(ConceptEntity entity)
        {
            ConceptModel model = entity.ToModel();
            return model;
        }
        public override ConceptModel GetOneById(int id)
        {
            IEnumerable<ConceptIdeaEntity> conceptIdeas = ((ConceptRepository)_repository).GetIdea(id);
            if (conceptIdeas is null || conceptIdeas.Count() < 1) throw new ConceptNotFoundException();
            ConceptModel model = conceptIdeas.First().Concept.ToModel();
            model.Ideas = conceptIdeas.Select(ci => ci.ToConceptIdeaModel());
            return model;
        }

        public IEnumerable<ConceptModel> GetByTitle(string title)
        {
            IEnumerable<ConceptIdeaEntity> conceptIdeas = ((ConceptRepository)_repository).GetByTitle(title);
            if (conceptIdeas is null || conceptIdeas.Count() < 1) throw new ConceptNotFoundException($"Aucun concept contenant \"{title}\" n'a été trouvé");
            IEnumerable<ConceptModel> models = conceptIdeas
                .Select(ci => ci.Concept.ToModel())
                .DistinctBy(c => c.Id);
            
            return models;
        }
        public IEnumerable<ConceptModel> GetByLabel(int labelId)
        {
            IEnumerable<ConceptIdeaEntity> conceptIdeas = ((ConceptRepository)_repository).GetConceptByLabel(labelId);
            if (conceptIdeas is null || conceptIdeas.Count() < 1) throw new ConceptNotFoundException($"Aucun concept n'est associé à ce tag");
            IEnumerable<ConceptModel> models = conceptIdeas
                .Select(ci => ci.Concept.ToModel())
                .DistinctBy(c => c.Id);

            return models;
        }

        public int InsertIdea(int conceptId, int ideaId, int order)
        {
            IdUsed(conceptId);
            _IdeaService.IdUsed(ideaId);

            uint index = (uint)Math.Abs(order);

            return ((ConceptRepository)_repository).InsertIdea(conceptId, ideaId, index);
            
        }

        public bool RemoveIdea(int conceptId, int ideadId)
        {
            IdUsed(conceptId);
            _IdeaService.IdUsed(ideadId);

            return ((ConceptRepository)_repository).RemoveIdea(conceptId, ideadId);
        }

        public bool MoveIdea(int conceptId, int ideadId, int order)
        {
            IdUsed(conceptId);
            _IdeaService.IdUsed(ideadId);

            uint index = (uint)Math.Abs(order);

            return ((ConceptRepository)_repository).UpdateIdea(conceptId, ideadId, index);
        }
    }
}
