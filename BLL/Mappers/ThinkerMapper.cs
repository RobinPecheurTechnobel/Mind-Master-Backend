using BLL.Models;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mappers
{
    public static class ThinkerMapper
    {
        public static ThinkerModel ToModel(this ThinkerEntity entity)
        {
            return new ThinkerModel
            {
                Id = entity.Id,
                LastName = entity.LastName,
                FirstName = entity.FirstName,
                Pseudo = entity.Pseudo,
                Email = entity.Email
            };
        }
        public static ThinkerEntity ToEntity(this ThinkerModel model)
        {
            return new ThinkerEntity
            {
                Id = model.Id,
                LastName = model.LastName,
                FirstName = model.FirstName,
                Pseudo = model.Pseudo,
                Email = model.Email
            };
        }
    }
}
