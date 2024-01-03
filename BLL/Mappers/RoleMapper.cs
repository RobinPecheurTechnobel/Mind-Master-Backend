using BLL.Models.Enums;
using DAL.Entities.Enums;

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
