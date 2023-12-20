using BLL.Models;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mappers
{
    public static class AccountMapper
    {
        public static AccountModel ToModel(this AccountEntity entity)
        {
            return new AccountModel
            {
                Id = entity.Id,
                Login = entity.Login,
                HashPassword = entity.HashPassword,
                Role = entity.Role
            };
        }
        public static AccountEntity ToEntity(this AccountModel model)
        {
            return new AccountEntity
            {
                Id = model.Id,
                Login = model.Login,
                HashPassword = model.HashPassword!,
                Role = model.Role ?? "User"
            };
        }
    }
}
