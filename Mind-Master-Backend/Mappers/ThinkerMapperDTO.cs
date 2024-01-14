using BLL.Models;
using BLL.Models.Relations;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Mind_Master_Backend.DTOs;
using Mind_Master_Backend.DTOs.Enums;
using Mind_Master_Backend.Mappers.Enums;

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

        public static JsonPatchDocument<ThinkerModel> ToJsonPatchDocumentModel(this JsonPatchDocument<ThinkerDTO> jpd)
        {
            if (jpd.Operations is null || jpd.Operations.Where(op => op is not null).Count() < 1) return null;
            return new JsonPatchDocument<ThinkerModel>(
                new List<Operation<ThinkerModel>>(jpd.Operations.Select(op => op.ToOperationModel())),
                jpd.ContractResolver);
        }
        public static Operation<ThinkerModel> ToOperationModel(this Operation<ThinkerDTO> o)
        {
            List<string> pathAutorized = new List<string> { "Pseudo", "Email" };
            if (pathAutorized.Where(path => o.path.Contains(path)).Count() < 1) return null;
            return new Operation<ThinkerModel>
            {
                from = o.from,
                op = o.op,
                path = o.path,
                value = o.value
            };
        }
    }
}
