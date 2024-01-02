using DAL.Data;
using DAL.Entities;
using DAL.Entities.Relations;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ConceptRepository : AbstractRepository<int, ConceptEntity>
    {
        public ConceptRepository(MindMasterContext mMContext)
        {
            _MMContext = mMContext;
            _dbSet = _MMContext.Concepts;
        }
        //l'update sera override
        public override ConceptEntity MapperEntity(ConceptEntity oldOne, ConceptEntity entity)
        {
            throw new NotImplementedException();
        }

        protected override Func<ConceptEntity, bool> PredicateIdentifier(int id)
        {
            return a => a.Id == id;
        }

        public override void SaveChanges()
        {
            _MMContext.SaveChanges();
        }

        public IEnumerable<ConceptIdeaEntity> GetIdea(int ConceptId)
        {
            return _MMContext.ConceptIdeas
                .Include(ci => ci.Concept)
                .Include(ci => ci.Idea)
                .Where(ci => ci.ConceptId == ConceptId);
        }
        //TODO Tester
        public IEnumerable<ConceptIdeaEntity> GetConceptByLabel(int LabelId)
        {
            IEnumerable<int> ConceptsId = _MMContext.LabelConcepts
                .Include(lc => lc.Label)
                .Include(lc => lc.Concept)
                .Where(lc => lc.LabelId == LabelId)
                .Select(lc => lc.ConceptId);
            return _MMContext.ConceptIdeas
                .Include(ci => ci.Concept)
                .Include(ci => ci.Idea)
                .Where(ci => ConceptsId.Contains(ci.ConceptId));
        }
        public GroupEntity getGroupOfThisOne(int conceptId)
        {
            return _MMContext.ConceptGroups
                .Include(cg => cg.Concept)
                .Include(cg => cg.Group)
                .Where(cg => cg.ConceptId == conceptId)
                .Select(lc => lc.Group).First();
        }
    }
}
