using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities.Relations
{
    public class GroupAssemblyEntity
    {
        [ForeignKey("Assembly")]
        public int AssemblyId { get; set; }
        [ForeignKey("Group")]
        public int GroupId { get; set; }
    }
}
