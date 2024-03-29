﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Models.Relations;

namespace BLL.Models
{
    public class GroupModel : GroupSimpleModel
    {
        public IEnumerable<GroupThinkerModel> GroupThinkers { get; set; }
    }
    public class GroupSimpleModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string? Description { get; set; }
    }
}
