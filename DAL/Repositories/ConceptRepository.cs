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
                .Include(ci => ci.Idea.Thinker)
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
                .Include(ci => ci.Idea.Thinker)
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
        public IEnumerable<ConceptIdeaEntity> GetByTitle(string title)
        {
            return _MMContext.ConceptIdeas
                .Include(ci => ci.Concept)
                .Include(ci => ci.Idea)
                .Include(ci => ci.Idea.Thinker)
                .Where(ci => ci.Concept.Title.ToLower().Contains(title.ToLower()));
        }

        public int InsertIdea(int conceptId, int ideaId, uint index)
        {
            ConceptIdeaEntity cie = new ConceptIdeaEntity
            {
                ConceptId = conceptId,
                IdeaId = ideaId,
                Order = index
            };
            cie = MoveIdea(cie);

            _MMContext.ConceptIdeas.Add(cie);
            SaveChanges();
            return cie.ConceptId;            
        }
        private ConceptIdeaEntity MoveIdea(ConceptIdeaEntity conceptIdea)
        {

            IEnumerable<ConceptIdeaEntity> cies = GetIdea(conceptIdea.ConceptId);

            uint max = cies.Select(cie => cie.Order).Max();

            if (conceptIdea.Order > max + 1) conceptIdea.Order = max + 1;
            else if(cies.Where(cie => cie.Order == conceptIdea.Order).FirstOrDefault() is not null)
            {
                IEnumerable<ConceptIdeaEntity> ciesToMove = cies.Where(cie => cie.Order >= conceptIdea.Order);
                foreach(ConceptIdeaEntity cie in ciesToMove)
                {
                    cie.Order = cie.Order + 1;
                    _MMContext.Update(cie);
                }
                SaveChanges();
            }

            return conceptIdea;
        }

        public bool RemoveIdea(int conceptId, int ideaId)
        {
            ConceptIdeaEntity? cie = _MMContext.ConceptIdeas.Where(ci => ci.ConceptId == conceptId && ci.IdeaId == ideaId).FirstOrDefault();
            
            if (cie is null) return false;

            _MMContext.ConceptIdeas.Remove(cie);
            SaveChanges();

            return true;
        }

        public bool UpdateIdea(int conceptId, int ideaId, uint index)
        {
            ConceptIdeaEntity? cie = _MMContext.ConceptIdeas.Where(ci => ci.ConceptId == conceptId && ci.IdeaId == ideaId).FirstOrDefault();

            if (cie is null) return false;

            cie.Order = index;
            cie = MoveIdea(cie);

            _MMContext.ConceptIdeas.Update(cie);
            SaveChanges();
            return true;
        }

        public IEnumerable<ConceptIdeaEntity> GetManyIdea(IEnumerable<int> index)
        {
            return _MMContext.ConceptIdeas
                .Include(ci => ci.Concept)
                .Include(ci => ci.Idea)
                .Include(ci => ci.Idea.Thinker)
                .Where(ci => index.Contains(ci.ConceptId));
        }
    }
}
