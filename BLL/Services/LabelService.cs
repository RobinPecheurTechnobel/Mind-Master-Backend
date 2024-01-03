using BLL.CustomExceptions;
using BLL.Interfaces;
using BLL.Mappers;
using BLL.Models;
using DAL.Entities;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class LabelService : AbstractService<int, LabelModel, LabelEntity>
    {
        public LabelService(LabelRepository labelRepository)
        {
            _repository = labelRepository;
        }

        public override void IdUsed(int id)
        {
            if (_repository.GetOneById(id) is null)
                throw new LabelNotFoundException();
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
