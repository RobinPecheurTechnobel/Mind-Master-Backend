using DAL.Data;
using DAL.Entities;
using DAL.Entities.Relations;
using DAL.Interfaces;
using DAL.Migrations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class AssemblyRepository : AbstractRepository<int, AssemblyEntity>
    {
        public AssemblyRepository(MindMasterContext context)
        {
            _MMContext = context;
            _dbSet = _MMContext.Assemblies;
        }
        public override void SaveChanges()
        {
            _MMContext.SaveChanges();
        }
        // Update sera override
        public override AssemblyEntity MapperEntity(AssemblyEntity oldOne, AssemblyEntity entity)
        {
            throw new NotImplementedException();
        }

        protected override Func<AssemblyEntity, bool> PredicateIdentifier(int id)
        {
            return a => a.Id == id;
        }
        public IEnumerable<ConceptAssemblyEntity> GetConcepts(int AssemblyId)
        {
            IEnumerable<ConceptAssemblyEntity>  all =
                _MMContext.ConceptAssemblies
                .Include(ca => ca.Assembly)
                .Include(ca => ca.Concept)
                .GroupJoin(_MMContext.ConceptIdeas.Include(ci => ci.Concept).Include(ci => ci.Idea),
                    ca => ca.ConceptId,
                    ci => ci.ConceptId,
                    (ca, ci) => new
                    {
                        ca.Id,
                        ca.ConceptId,
                        ca.Concept,
                        ca.Order,
                        ca.Assembly,
                        ca.AssemblyId,
                        ci
                    })
                .Select(c => new ConceptAssemblyEntity
                {
                    Id = c.Id,
                    ConceptId = c.ConceptId,
                    Concept = c.Concept,
                    Assembly = c.Assembly,
                    AssemblyId = c.AssemblyId,
                    Order = c.Order,
                    ConceptIdeas = c.ci
                })
                .Where(ca => ca.AssemblyId == AssemblyId);
            return all;
        }
        //TODO a tester
        public IEnumerable<AssemblyEntity> GetAssemblyByLabel(int LabelId)
        {
            IEnumerable<int> AssembliesId = _MMContext.LabelAssemblies
                .Include(la => la.Assembly)
                .Include(la => la.Label)
                .Where(la => la.LabelId == LabelId)
                .Select(la => la.AssemblyId);
            return _MMContext.Assemblies
                .Where(a => AssembliesId.Contains(a.Id));
        }
        public IEnumerable<AssemblyEntity> GetByTitle(string title)
        {
            return _MMContext.Assemblies
                .Where(a => a.Title.ToLower().Contains(title.ToLower()));
        }

        public int InsertConcept(int assemblyId, int conceptId, uint index)
        {
            ConceptAssemblyEntity cae = new ConceptAssemblyEntity
            {
                AssemblyId = assemblyId,
                ConceptId = conceptId,
                Order = index
            };
            cae = MoveIdea(cae);

            _MMContext.ConceptAssemblies.Add(cae);
            SaveChanges();
            return cae.ConceptId;
        }

        public bool RemoveConcept(int assemblyId, int conceptId)
        {
            ConceptAssemblyEntity? cae = _MMContext.ConceptAssemblies.Where(ca => ca.ConceptId == conceptId && ca.AssemblyId == assemblyId).FirstOrDefault();

            if (cae is null) return false;

            _MMContext.ConceptAssemblies.Remove(cae);
            SaveChanges();

            return true;
        }

        public bool UpdateConcept(int assemblyId, int conceptId, uint index)
        {
            ConceptAssemblyEntity? cae = _MMContext.ConceptAssemblies.Where(ca => ca.ConceptId == conceptId && ca.AssemblyId == assemblyId).FirstOrDefault();

            if (cae is null) return false;

            cae.Order = index;
            cae = MoveIdea(cae);

            _MMContext.ConceptAssemblies.Update(cae);
            SaveChanges();
            return true;
        }
        private ConceptAssemblyEntity MoveIdea(ConceptAssemblyEntity conceptAssembly)
        {

            IEnumerable<ConceptAssemblyEntity> caes = GetConcepts(conceptAssembly.ConceptId);

            uint max = caes.Select(cae => cae.Order).Max();

            if (conceptAssembly.Order > max + 1) conceptAssembly.Order = max + 1;
            else if (caes.Where(cie => cie.Order == conceptAssembly.Order).FirstOrDefault() is not null)
            {
                IEnumerable<ConceptAssemblyEntity> caesToMove = caes.Where(cae => cae.Order >= conceptAssembly.Order);
                foreach (ConceptAssemblyEntity cae in caesToMove)
                {
                    cae.Order = cae.Order + 1;
                    _MMContext.Update(cae);
                }
                SaveChanges();
            }

            return conceptAssembly;
        }

        public IEnumerable<AssemblyEntity> GetAssemblyForThisGroup(int groupId)
        {
            return _MMContext.GroupAssemblies
                .Include(ga => ga.Group)
                .Include(a => a.Assembly)
                .Where(ga => ga.GroupId == groupId)
                .Select(ga => ga.Assembly);
        }

        public IEnumerable<AssemblyEntity> GetAssemblyForThisGroupWithCriteria(int groupId, string withThis)
        {
            return _MMContext.GroupAssemblies
                .Include(ga => ga.Group)
                .Include(a => a.Assembly)
                .GroupJoin(_MMContext.LabelAssemblies
                    .Include(la => la.Assembly)
                    .Include(la => la.Label),
                    ga => ga.Assembly.Id,
                    la => la.Assembly.Id,
                    (ga, la) => new { 
                        assembly = ga.Assembly,
                        group = ga.Group,
                        labels = la.Select(la => la.Label)
                    }
                )
                .Where(
                    gla => gla.group.Id == groupId && (
                        gla.assembly.Title.ToLower().Contains(withThis.ToLower()) ||
                        gla.labels.Select(l => l.Title).Contains(withThis)
                    )
                )
                .Select(ga => ga.assembly);
        }
    }
}
