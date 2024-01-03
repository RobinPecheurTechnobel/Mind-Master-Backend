using DAL.Entities.Enums;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class IdeaModel
    {
    }
    public class IdeaSimpleModel
    {
        public int Id { get; set; }
        public DateTime creationDate { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public string Format
        {
            get
            {
                return format.ToString();
            }
            set
            {
                if (!Enum.TryParse<FormatEntity>(value, out FormatEntity result))
                {
                    format = 0;
                }
                format = result;
            }
        }
        [NotMapped]
        public FormatEntity format { get; set; }

        public string? Content { get; set; }
        public string? Source { get; set; }
        [ForeignKey("Thinker")]
        public int ThinkerId { get; set; }

        public virtual ThinkerEntity Thinker { get; set; }
    }
}
