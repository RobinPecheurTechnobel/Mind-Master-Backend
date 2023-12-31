using BLL.Models;
using DAL.Entities;

namespace BLL.Mappers
{
    public static class ThinkerMapper
    {
        public static ThinkerModel ToModel(this ThinkerEntity entity)
        {
            return new ThinkerModel
            {
                Id = entity.Id,
                Login = entity.Login,
                HashPassword = entity.HashPassword,
                Role = entity.role.ToModel(),
                Pseudo = entity.Pseudo,
                Email = entity.Email,
                GroupThinkers = null
            };
        }
        public static ThinkerSimpleModel ToSimpleModel(this ThinkerEntity entity)
        {
            return new ThinkerSimpleModel
            {
                Id = entity.Id,
                Login = entity.Login,
                HashPassword = entity.HashPassword,
                Role = entity.role.ToModel(),
                Pseudo = entity.Pseudo,
                Email = entity.Email
            };
        }
        public static ThinkerEntity ToEntity(this ThinkerSimpleModel model)
        {
            return new ThinkerEntity
            {
                Id = model.Id,
                Login = model.Login,
                HashPassword = model.HashPassword!,
                role = model.Role.ToEntity(),
                Pseudo = model.Pseudo,
                Email = model.Email
            };
        }

    }
}
