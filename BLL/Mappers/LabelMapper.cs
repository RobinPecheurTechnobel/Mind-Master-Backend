using BLL.Models;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mappers
{
    public static class LabelMapper
    {
        public static LabelModel ToModel(this LabelEntity entity)
        {
            return new LabelModel
            {
                Id = entity.Id,
                Title = entity.Title
            };
        }
        public static LabelEntity ToEntity(this LabelModel model)
        {
            return new LabelEntity
            {
                Id = model.Id,
                Title = model.Title
            };
        }
    }
}
