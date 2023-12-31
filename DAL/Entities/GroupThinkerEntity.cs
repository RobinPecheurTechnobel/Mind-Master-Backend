using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    [Table("GroupThinker", Schema = "User")]
    [PrimaryKey(nameof(ThinkerId),nameof(GroupId))]
    public class GroupThinkerEntity
    {
        [Required]
        [ForeignKey("Thinker")]
        public int ThinkerId { get; set; }
        [Required]
        [ForeignKey("Group")]
        public int GroupId { get; set; }
        [DefaultValue(false)]
        public bool isOwner { get; set; }

        public virtual ThinkerEntity Thinker { get; set; }
        public virtual GroupEntity Group { get; set; }
    }
}
