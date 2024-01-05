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
        public AssemblyService(AssemblyRepository assemblyRepository, ConceptRepository conceptRepository, ConceptService conceptService)
        {
            _repository = assemblyRepository;
            _ConceptRepository = conceptRepository;
            _ConceptService = conceptService;
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
            IEnumerable<ConceptAssemblyEntity> conceptAssemblies = ((AssemblyRepository)_repository).GetConcepts(id);
            
            if (conceptAssemblies is null || conceptAssemblies.Count() < 1) throw new AssemblyNotFoundException();

            AssemblyModel model = conceptAssemblies.First().Assembly.ToModel();
            
            model.Concepts = conceptAssemblies.Select(ca =>ca.ToConceptAssmblyModel());

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
    }
}
