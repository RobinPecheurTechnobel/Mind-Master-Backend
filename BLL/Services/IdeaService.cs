using BLL.CustomExceptions;
using BLL.Interfaces;
using BLL.Mappers;
using BLL.Models;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class IdeaService : AbstractService<int,IdeaModel,IdeaEntity>
    {
        public IdeaService(IdeaRepository ideaRepository)
        {
            _repository = ideaRepository;
        }

        public override void IdUsed(int id)
        {
            if (_repository.GetOneById(id) is null)
                throw new IdeaNotFoundException();
        }

        public override IdeaEntity MapperEntity(IdeaModel model)
        {
            return model.ToEntity();
        }

        public override IdeaModel MapperModel(IdeaEntity entity)
        {
            return entity.ToModel();
        }
        public IEnumerable<IdeaModel> GetByThinkerId(int thinkerId)
        {
            return ((IdeaRepository)_repository).GetByThinker(thinkerId)
                .Select(i => i.ToModel());
        }
    }
}
