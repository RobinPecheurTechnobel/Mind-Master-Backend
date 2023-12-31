using BLL.Models;
using Mind_Master_Backend.DTOs;

namespace Mind_Master_Backend.Mappers
{
    /// <summary>
    ///     Classe d'extension permettant la conversion des Comptes de leur forme DTO à celle des model (utilisé dans la couche logique).
    ///     Et vice et versa.
    /// </summary>
    public static class ThinkerMapperDTO
    {
        /// <summary>
        ///     Convertit un Model en DTO (exploitable dans la couche de présentation)
        /// </summary>
        /// <param name="model">Le compte d'origine sous sa forme de model</param>
        /// <returns>Le compte en DTO</returns>
        public static ThinkerDTO ToDTO(this ThinkerSimpleModel model)
        {
            return new ThinkerDTO
            {
                Id = model.Id,
                Login = model.Login,
                Role = new EnumDTO(model.Role.ToDTO()),
                Pseudo = model.Pseudo,
                Email = model.Email
            };
        }

        /// <summary>
        ///     Convertit un DTO en Model (exploitable dans la couche logique)
        /// </summary>
        /// <param name="dto">Le compte d'origine sous sa forme de DTO</param>
        /// <returns>Le compte en Model</returns>
        public static ThinkerModel ToModel(this ThinkerDataTO dto)
        {
            return new ThinkerModel
            {
                Login = dto.Login,
                Role = dto.Role.ToModel(),
                Pseudo = dto.Pseudo,
                Email = dto.Email
            };
        }

        /// <summary>
        ///     Convertit en DTO (créé lors de l'enregistrement d'un nouveau compte) en Model
        /// </summary>
        /// <param name="newDto">Le compte en DTO</param>
        /// <returns>Le compte en Model</returns>
        public static ThinkerModel ToNewModel(this NewAccountDataTO newDto)
        {
            return new ThinkerModel
            {
                Id = 0,
                Login = newDto.Login,
                HashPassword = newDto.Password,
                Role = RoleDTO.User.ToModel(),
                Pseudo = newDto.Login,
                Email = null
            };
        }

        public static ThinkerFullDTO ToFullDTO(this ThinkerModel model)
        {
            return new ThinkerFullDTO
            {
                Id = model.Id,
                Email = model.Email,
                Login = model.Login,
                Pseudo = model.Pseudo,
                Role = new EnumDTO(model.Role.ToDTO()),
                Groups = model.GroupThinkers.Select(gt => gt.ToInternalGroupDTO())
            };
        }
    }
}
