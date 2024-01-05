using BLL.Models;
using BLL.Models.Relations;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mappers
{
    public static class AssemblyMapper
    {
        public static AssemblySimpleModel ToSimpleModel(this AssemblyEntity entity)
        {
            return new AssemblySimpleModel
            {
                Id = entity.Id,
                Title = entity.Title
            };
        }
        public static AssemblyEntity ToEntity(this AssemblySimpleModel model)
        {
            return new AssemblyEntity
            {
                Id = model.Id,
                Title = model.Title
            };
        }
        public static AssemblyModel ToModel(this AssemblyEntity entity)
        {
            return new AssemblyModel
            {
                Id = entity.Id,
                Title = entity.Title
            };
        }
    }
}
