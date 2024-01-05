using DAL.Data;
using DAL.Entities;
using DAL.Entities.Relations;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class LabelRepository : AbstractRepository<int, LabelEntity>
    {
        public LabelRepository(MindMasterContext mMContext)
        {
            _MMContext = mMContext;
            _dbSet = _MMContext.Labels;
        }
        public override LabelEntity MapperEntity(LabelEntity oldOne, LabelEntity entity)
        {
            oldOne.Title = entity.Title;

            return oldOne;

        }

        protected override Func<LabelEntity, bool> PredicateIdentifier(int id)
        {
            return l => l.Id == id;
        }
        public override void SaveChanges()
        {
            _MMContext.SaveChanges();
        }

        public bool AssociateLabelConcept(int labelId, int conceptId)
        {
            LabelConceptEntity? lce = _MMContext.LabelConcepts
                .Where(lc => lc.ConceptId == conceptId && lc.LabelId == labelId)
                .FirstOrDefault();

            if (lce is not null) return false;

            lce = new LabelConceptEntity
            {
                LabelId = labelId,
                ConceptId = conceptId
            };
            _MMContext.LabelConcepts.Add(lce);
            SaveChanges();

            return true;
        }

        public bool AssociateLabelAssembly(int labelId, int assemblyId)
        {
            LabelAssemblyEntity? lae = _MMContext.LabelAssemblies
                .Where(la => la.AssemblyId == assemblyId && la.LabelId == labelId)
                .FirstOrDefault();

            if (lae is not null) return false;

            lae = new LabelAssemblyEntity
            {
                LabelId = labelId,
                AssemblyId = assemblyId
            };
            _MMContext.LabelAssemblies.Add(lae);
            SaveChanges();

            return true;
        }

        public bool DetachLabelConcept(int labelId, int conceptId)
        {
            LabelConceptEntity? lce = _MMContext.LabelConcepts
                .Where(lc => lc.ConceptId == conceptId && lc.LabelId == labelId)
                .FirstOrDefault();

            if (lce is null) return false;

            _MMContext.LabelConcepts.Remove(lce);
            SaveChanges();

            return true;
        }

        public bool DetachLabelAssembly(int labelId, int assemblyId)
        {
            LabelAssemblyEntity? lae = _MMContext.LabelAssemblies
                .Where(la => la.AssemblyId == assemblyId && la.LabelId == labelId)
                .FirstOrDefault();

            if (lae is null) return false;

            _MMContext.LabelAssemblies.Remove(lae);
            SaveChanges();

            return true;
        }
    }
}
