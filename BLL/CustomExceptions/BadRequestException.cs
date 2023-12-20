using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.CustomExceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string Message) : base(Message) { }
    }

    public class InvalidCredentialException : BadRequestException
    {
        public InvalidCredentialException(string Message = "Le login ou mot de passe est incorrect") : base(Message) { }
    }
}
