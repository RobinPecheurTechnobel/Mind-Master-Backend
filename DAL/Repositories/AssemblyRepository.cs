using DAL.Data;
using DAL.Entities;
using DAL.Entities.Relations;
using DAL.Interfaces;
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
            return _MMContext.ConceptAssemblies
                .Include(ca => ca.Assembly)
                .Include(ca => ca.ConceptIdeas)
                .Where(ca => ca.AssemblyId == AssemblyId);
        }
        //TODO a tester
        public IEnumerable<ConceptAssemblyEntity> GetAssemblyByLabel(int LabelId)
        {

            IEnumerable<int> AssembliesId = _MMContext.LabelAssemblies
                .Include(la => la.Assembly)
                .Include(la => la.Label)
                .Where(la => la.LabelId == LabelId)
                .Select(la => la.AssemblyId);
            return _MMContext.ConceptAssemblies
                .Include(ca => ca.Assembly)
                .Include(ca => ca.ConceptIdeas)
                .Where(ca => AssembliesId.Contains(ca.AssemblyId));
        }
    }
}
