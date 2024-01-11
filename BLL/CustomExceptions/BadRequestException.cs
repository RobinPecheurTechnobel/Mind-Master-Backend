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
    public class LabelConceptAlredayLinkedException : BadRequestException
    {
        public LabelConceptAlredayLinkedException(string Message = "Ce tag et ce concept sont déjà liés") : base(Message) { }
    }
    public class LabelConceptNotLinkedException : BadRequestException
    {
        public LabelConceptNotLinkedException(string Message = "Ce tag et ce concept ne sont pas associés") : base(Message) { }
    }
    public class LabelAssemblyAlredayLinkedException : BadRequestException
    {
        public LabelAssemblyAlredayLinkedException(string Message = "Ce tag et cet assemblage sont déjà liés") : base(Message) { }
    }
    public class LabelAssemblyNotLinkedException : BadRequestException
    {
        public LabelAssemblyNotLinkedException(string Message = "Ce tag et cet assemblage ne sont pas associés") : base(Message) { }
    }
    public class UnAuthorizedPatchOperation : BadRequestException
    {
        public UnAuthorizedPatchOperation(string Message = "Ce champs ne peut-être modifier de cette façon") : base(Message) { }
    }
}
