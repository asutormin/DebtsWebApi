using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DebtsWebApi.Entities
{
    public class AuthResult
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string PositionName { get; set; }
        public long StateId { get; set; }
        public int BusinessUnitId { get; set; }
        public long DepartmentId { get; set; }
        public string UserLogin { get; set; }
        public string LdapLogin { get; set; }
        public string Token { get; set; }
    }
}
