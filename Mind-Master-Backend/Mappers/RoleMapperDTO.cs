using BLL.Models.Enums;
using Mind_Master_Backend.DTOs;

namespace Mind_Master_Backend.Mappers
{
    /// <summary>Convert de l'enumeration des roles</summary>
    public static class RoleMapperDTO
    {
        /// <summary>Convertion de role en dto (exploitation dans la couche présentation)</summary>
        /// <param name="model">Role d'origine sous forme de Model</param>
        /// <returns>Role sous sa forme de DTO</returns>
        public static RoleDTO ToDTO(this RoleModel model){ return (RoleDTO)model;}
        
        /// <summary>Convertion de role en model (exploitation par la couche logique</summary>
        /// <param name="dto">Role d'origine sous forme de DTO</param>
        /// <returns>Role sous sa forme de Model</returns>
        public static RoleModel ToModel(this RoleDTO dto){ return (RoleModel)dto;}
    }
}
