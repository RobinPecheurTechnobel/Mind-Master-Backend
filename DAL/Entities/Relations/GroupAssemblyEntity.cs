using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities.Relations
{
    [Table("GroupAssembly", Schema = "Idea")]
    [PrimaryKey(nameof(AssemblyId), nameof(GroupId))]
    public class GroupAssemblyEntity
    {
        [ForeignKey("Assembly")]
        public int AssemblyId { get; set; }
        [ForeignKey("Group")]
        public int GroupId { get; set; }

        public AssemblyEntity Assembly { get; set; }
        public GroupEntity Group { get; set; }
    }
}
