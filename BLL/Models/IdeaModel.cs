using DAL.Entities.Enums;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Models.Enums;

namespace BLL.Models
{
    public class IdeaModel : IdeaSimpleModel
    {
        public ThinkerModel Thinker { get; set; }
    }
    public class IdeaSimpleModel
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public FormatModel format { get; set; }

        public string? Content { get; set; }
        public string? Source { get; set; }
        public int ThinkerId { get; set; }

    }
}
