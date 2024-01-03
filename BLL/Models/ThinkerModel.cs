using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Models.Enums;
using BLL.Models.Relations;

namespace BLL.Models
{
    public class ThinkerModel :ThinkerSimpleModel
    {
        public IEnumerable<GroupThinkerModel> GroupThinkers { get; set; }
    }
    public class ThinkerSimpleModel
    {

        public int Id { get; set; }
        public string Login { get; set; }
        public RoleModel Role { get; set; }
        public string? HashPassword { get; set; }

        public string? Email { get; set; }

        public string Pseudo { get; set; }
    }
}
