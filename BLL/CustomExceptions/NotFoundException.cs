using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.CustomExceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException (string message) : base(message) { }
    }

    public class ThinkerNotFoundException : NotFoundException
    {
        public ThinkerNotFoundException(string message = "Ce compte n'existe pas") : base(message) { }
    }
    public class GroupNotFoundException : NotFoundException
    {
        public GroupNotFoundException(string message = "Ce groupe n'existe pas") : base(message) { }
    }
    public class IdeaNotFoundException : NotFoundException
    {
        public IdeaNotFoundException(string message = "Cet idée n'existe pas") : base(message) { }
    }
}
