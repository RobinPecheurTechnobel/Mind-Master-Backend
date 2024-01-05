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
    public class GroupRepository : AbstractRepository<int, GroupEntity>
    {
        public GroupRepository(MindMasterContext context)
        {
            _MMContext = context;
            _dbSet = _MMContext.Groups; 
        }
        public override GroupEntity MapperEntity(GroupEntity oldOne, GroupEntity entity)
        {
            oldOne.Name = entity.Name;
            oldOne.Description = entity.Description;

            return oldOne;
        }

        protected override Func<GroupEntity, bool> PredicateIdentifier(int id)
        {
            return (g => g.Id == id);
        }
        public override void SaveChanges()
        {
            _MMContext.SaveChanges();
        }

        public IEnumerable<GroupThinkerEntity> getMembers (int id)
        {
            return _MMContext.GroupThinkers
                .Include(gt => gt.Group)
                .Include(gt => gt.Thinker)
                .Where(gt => gt.GroupId == id);
        }

        public int AddThinkerToGroup(int groupId, int thinkerId)
        {
            GroupThinkerEntity gtentity = new GroupThinkerEntity
            {
                GroupId = groupId,
                ThinkerId = thinkerId
            };
            _MMContext.GroupThinkers.Add(gtentity);
            SaveChanges();

            return gtentity.GroupId;
        }

        public bool RemoveThinkerToGroup(int groupId, int thinkerId)
        {

            GroupThinkerEntity? entity = _MMContext.GroupThinkers
                .Where(gt => gt.GroupId == groupId && gt.ThinkerId == thinkerId)
                .FirstOrDefault();

            if (entity is null) return false;

            _MMContext.GroupThinkers.Remove(entity);
            SaveChanges();

            return true;
        }
        //TODO Tester
        public IEnumerable<ConceptIdeaEntity> GetConcepts(int groupId)
        {
            IEnumerable<int> ConceptsId = _MMContext.ConceptGroups
                .Include(lc => lc.Group)
            .Include(lc => lc.Concept)
                .Where(lc => lc.GroupId == groupId)
                .Select(lc => lc.ConceptId);
            return _MMContext.ConceptIdeas
                .Include(ci => ci.Concept)
                .Include(ci => ci.Idea)
                .Where(ci => ConceptsId.Contains(ci.ConceptId));
        }
        //TODO Tester
        public IEnumerable<AssemblyEntity> GetAssemblies(int groupId)
        {
            IEnumerable<int> AssmebliesId = _MMContext.GroupAssemblies
                .Include(ga => ga.Group)
                .Include(ga => ga.Assembly)
                .Where(ga => ga.GroupId == groupId)
                .Select(ga => ga.AssemblyId);
            return _MMContext.ConceptAssemblies
                .Include(ca => ca.Concept)
                .Include(ca => ca.Assembly)
                .Where(ca => AssmebliesId.Contains(ca.AssemblyId))
                .Select(ca => ca.Assembly);
        }
    }
}
