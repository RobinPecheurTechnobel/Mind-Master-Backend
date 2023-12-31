using BLL.Models;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mappers
{
    public static class GroupMapper
    {
        public static GroupSimpleModel ToSimpleModel(this GroupEntity entity)
        {
            return new GroupSimpleModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description
            };
        }
        public static GroupModel ToModel(this GroupEntity entity)
        {
            return new GroupModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                GroupThinkers = null
            };
        }
        public static GroupEntity ToEntiTy(this GroupSimpleModel model)
        {
            return new GroupEntity
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description
            };
        }
    }
}
