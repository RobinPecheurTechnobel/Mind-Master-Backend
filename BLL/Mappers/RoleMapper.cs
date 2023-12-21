using BLL.Models;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mappers
{
    public static class RoleMapper
    {
        public static RoleEntity ToEntity(this RoleModel model)
        {
            return (RoleEntity)model;
        }
        public static RoleModel ToModel(this RoleEntity entity)
        {
            return (RoleModel)entity;
        }
    }
}
