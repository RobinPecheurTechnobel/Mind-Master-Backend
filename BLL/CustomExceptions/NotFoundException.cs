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

    public class AccountNotFoundException : NotFoundException
    {
        public AccountNotFoundException(string message = "Ce compte n'existe pas") : base(message) { }
    }
}
