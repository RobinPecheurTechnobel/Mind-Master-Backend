using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.CustomExceptions
{
    public class TokenException : Exception
    {
        public TokenException(string message) : base(message) {}
    }
    public class ExpiredTokenException : TokenException
    {
        public ExpiredTokenException(string message = "Le token a expiré") : base (message){}
    }
    public class NotValidTokenException : TokenException
    {
        public NotValidTokenException(string message = "Le token n'est pas valide") : base(message) { }
    }
}
