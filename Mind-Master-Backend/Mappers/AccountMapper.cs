using BLL.Models;
using Mind_Master_Backend.DTOs;

namespace Mind_Master_Backend.Mappers
{
    public static class AccountMapper
    {
        public static AccountDTO ToDTO(this AccountModel model)
        {
            return new AccountDTO
            {
                Id = model.Id,
                Login = model.Login,
                Role = model.Role
            };
        }
        public static AccountModel ToNewModel(this AccountDataTO newDto) 
        {
            return new AccountModel
            {
                Id = 0,
                Login = newDto.Login,
                HashPassword = newDto.Password
            };
        }
        public static AccountModel ToModel(this AccountDTO dto)
        {
            return new AccountModel
            {
                Id = dto.Id,
                Login = dto.Login,
                Role = dto.Role
            };
        }
    }
}
