using BLL.Models;
using Mind_Master_Backend.DTOs;

namespace Mind_Master_Backend.Mappers
{
    /// <summary>
    ///     Classe d'extension permettant la conversion des Comptes de leur forme DTO à celle des model (utilisé dans la couche logique).
    ///     Et vice et versa.
    /// </summary>
    public static class AccountMapperDTO
    {
        /// <summary>
        ///     Convertit un Model en DTO (exploitable dans la couche de présentation)
        /// </summary>
        /// <param name="model">Le compte d'origine sous sa forme de model</param>
        /// <returns>Le compte en DTO</returns>
        public static AccountDTO ToDTO(this AccountModel model)
        {
            return new AccountDTO
            {
                Id = model.Id,
                Login = model.Login,
                Role = new EnumDTO(model.Role.ToDTO())
            };
        }

        /// <summary>
        ///     Convertit un DTO en Model (exploitable dans la couche logique)
        /// </summary>
        /// <param name="dto">Le compte d'origine sous sa forme de DTO</param>
        /// <returns>Le compte en Model</returns>
        public static AccountModel ToModel(this AccountDataTO dto)
        {
            return new AccountModel
            {
                Login = dto.Login,
                Role = dto.Role.ToModel()
            };
        }

        /// <summary>
        ///     Convertit en DTO (créé lors de l'enregistrement d'un nouveau compte) en Model
        /// </summary>
        /// <param name="newDto">Le compte en DTO</param>
        /// <returns>Le compte en Model</returns>
        public static AccountModel ToNewModel(this NewAccountDataTO newDto)
        {
            return new AccountModel
            {
                Id = 0,
                Login = newDto.Login,
                HashPassword = newDto.Password,
                Role = RoleDTO.User.ToModel()
            };
        }
    }
}
