using BLL.Models.Enums;
using DAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mappers.Enums
{
    public static class FormatMapper
    {
        public static FormatEntity ToEntity(this FormatModel model)
        {
            return (FormatEntity)model;
        }
        public static FormatModel ToModel(this FormatEntity entity)
        {
            return (FormatModel)entity;
        }
    }
}
