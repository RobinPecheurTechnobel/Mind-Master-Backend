using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class AccountModel
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Role { get; set; }
        public string? HashPassword { get; set; }
    }
}
