using BLL.CustomExceptions;
using BLL.Interfaces;
using BLL.Mappers;
using BLL.Mappers.Relations;
using BLL.Models;
using BLL.Models.Relations;
using DAL.Entities;
using DAL.Entities.Relations;
using DAL.Migrations;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AssemblyService : AbstractService<int, AssemblyModel, AssemblyEntity>
    {
        private ConceptRepository _ConceptRepository;
        private ConceptService _ConceptService;
        private GroupService _GroupService;
        public AssemblyService(AssemblyRepository assemblyRepository, 
            ConceptRepository conceptRepository, 
            ConceptService conceptService,
            GroupService groupService)
        {
            _repository = assemblyRepository;
            _ConceptRepository = conceptRepository;
            _ConceptService = conceptService;
            _GroupService = groupService;
        }

        public override void IdUsed(int id)
        {
            if (_repository.GetOneById(id) is null)
                throw new AssemblyNotFoundException();
        }

        public override AssemblyEntity MapperEntity(AssemblyModel model)
        {
            return model.ToEntity();
        }

        public override AssemblyModel MapperModel(AssemblyEntity entity)
        {
            return entity.ToModel();
        }

        public override AssemblyModel GetOneById(int id)
        {
            IdUsed(id);

            IEnumerable<ConceptAssemblyEntity> conceptAssemblies = ((AssemblyRepository)_repository).GetConcepts(id);
            
            AssemblyModel model = _repository.GetOneById(id).ToModel();

            if (conceptAssemblies is  not null && conceptAssemblies.Count() > 0)
                model.Concepts = conceptAssemblies.Select(ca => ca.ToConceptAssmblyModel());

            return model;
        }

        public IEnumerable<AssemblyModel> GetByTitle(string title)
        {
            return ((AssemblyRepository)_repository).GetByTitle(title).Select(a => a.ToModel());
        }

        public IEnumerable<AssemblyModel> GetByLabel(int labelId)
        {
            return ((AssemblyRepository)_repository).GetAssemblyByLabel(labelId).Select(a => a.ToModel());
        }

        public int InsertConcept(int assemblyId, int conceptId, int order)
        {
            IdUsed(assemblyId);
            _ConceptService.IdUsed(conceptId);

            uint index = (uint)Math.Abs(order);

            return ((AssemblyRepository)_repository).InsertConcept(assemblyId, conceptId, index);
        }

        public bool RemoveConcept(int assemblyId, int conceptId)
        {
            IdUsed(assemblyId);
            _ConceptService.IdUsed(conceptId);

            return ((AssemblyRepository)_repository).RemoveConcept(assemblyId, conceptId);
        }

        public bool MoveConcept(int assemblyId, int conceptId, int order)
        {
            IdUsed(assemblyId);
            _ConceptService.IdUsed(conceptId);

            uint index = (uint)Math.Abs(order);

            return ((AssemblyRepository)_repository).UpdateConcept(assemblyId, conceptId, index);
        }

        public IEnumerable<AssemblyModel> GetAllForThisGroup(int groupId)
        {
            _GroupService.IdUsed(groupId);

            return ((AssemblyRepository)_repository).GetAssemblyForThisGroup(groupId).Select(a => a.ToModel());
        }

        public IEnumerable<AssemblyModel> GetAllForThisGroupWithCriteria(int groupId, string withThis)
        {
            _GroupService.IdUsed(groupId);

            return ((AssemblyRepository)_repository).GetAssemblyForThisGroupWithCriteria(groupId, withThis).Select(a => a.ToModel());
        }

        public AssemblyModel CreateAssembly(AssemblyModel assemblyModel, int groupId = 1)
        {
            AssemblyModel model = Create(assemblyModel);

            ((AssemblyRepository)_repository).AssociateGroupAssembly(model.Id, groupId);

            return model;
        }
    }
}
