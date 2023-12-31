using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public ThinkerModel? Thinker { get; set; }

        public string? Email { get; set; }

        public string Pseudo { get; set; }
    }
}
