using BLL.CustomExceptions;
using BLL.Interfaces;
using BLL.Mappers;
using BLL.Models;
using DAL.Entities;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class LabelService : AbstractService<int, LabelModel, LabelEntity>
    {
        private ConceptService _ConceptService;
        private AssemblyService _AssemblyService;

        public LabelService(LabelRepository labelRepository, AssemblyService assemblyService, ConceptService conceptService)
        {
            _repository = labelRepository;
            _AssemblyService = assemblyService;
            _ConceptService = conceptService;
        }

        public bool DetachLabelFromAssembly(int labelId, int assemblyId)
        {
            _AssemblyService.IdUsed(assemblyId);
            IdUsed(labelId);

            bool result = ((LabelRepository)_repository).DetachLabelAssembly(labelId, assemblyId);
            if (!result) throw new LabelAssemblyNotLinkedException();

            return result;
        }

        public bool DetachLabelFromConcept(int labelId, int conceptId)
        {
            _ConceptService.IdUsed(conceptId);
            IdUsed(labelId);

            bool result = ((LabelRepository)_repository).DetachLabelConcept(labelId, conceptId);
            if (!result) throw new LabelConceptNotLinkedException();

            return result;
        }

        public override void IdUsed(int id)
        {
            if (_repository.GetOneById(id) is null)
                throw new LabelNotFoundException();
        }

        public bool LinkLabelToAssembly(int labelId, int assemblyId)
        {
            _AssemblyService.IdUsed(assemblyId);
            IdUsed(labelId);

            bool result = ((LabelRepository)_repository).AssociateLabelAssembly(labelId, assemblyId);
            if (!result) throw new LabelAssemblyAlredayLinkedException();

            return result;
        }

        public bool LinkLabelToConcept(int labelId, int conceptId)
        {
            _ConceptService.IdUsed(conceptId);
            IdUsed(labelId);

            bool result = ((LabelRepository)_repository).AssociateLabelConcept(labelId, conceptId);
            if (!result) throw new LabelConceptAlredayLinkedException();

            return result;                
        }

        public override LabelEntity MapperEntity(LabelModel model)
        {
            return model.ToEntity();
        }

        public override LabelModel MapperModel(LabelEntity entity)
        {
            return entity.ToModel();
        }
    }
}
