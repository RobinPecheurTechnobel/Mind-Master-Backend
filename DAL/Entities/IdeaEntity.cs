using DAL.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    [Table("Idea", Schema = "Idea")]
    [PrimaryKey(nameof(Id))]
    public class IdeaEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime creationDate { get; set; }
        [Required]
        public DateTime LastUpdateDate { get; set; }

        [Required]
        [MaxLength(10)]
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

        public  virtual ThinkerEntity Thinker { get; set; }
    }
}
