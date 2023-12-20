using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.CustomExceptions
{
    public class DataConstraintException : Exception
    {
        public DataConstraintException(string message) : base(message) { }
    }

    public class LoginUniqueException : DataConstraintException
    {
        public LoginUniqueException(string message = "Ce login est déjà utilisé") : base(message) { }
    }
}
