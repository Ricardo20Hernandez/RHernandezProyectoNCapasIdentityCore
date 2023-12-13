using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Rol
    {
        public Guid RoleId { get; set; }
        public string Name { get; set; }
        public List<object> Roles { get; set; }
    }
}
