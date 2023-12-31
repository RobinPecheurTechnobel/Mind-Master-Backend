using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class GroupThinkerModel
    {
        public virtual ThinkerSimpleModel Thinker { get; set; }
        public virtual GroupSimpleModel Group { get; set; }

        public bool isOwner { get; set; }
    }
}
