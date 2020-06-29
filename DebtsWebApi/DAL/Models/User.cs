using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DebtsWebApi.DAL.Models
{
    [Table("vw_users")]
    public class User
    {
        [Key]
        [Column("user_id", TypeName = "bigint")]
        public long Id { get; set; }

        [Column("user_name", TypeName = "varchar(152)")]
        public string Name { get; set; }

        [Column("office", TypeName = "varchar(250)")]
        public string PositionName { get; set; }

        [Column("state_id", TypeName = "bigint")]
        public long StateId { get; set; }

        [Column("business_unit_id", TypeName = "int")]
        public int BusinessUnitKey { get; set; }

        [ForeignKey("BusinessUnitKey")]
        public BusinessUnit BusinessUnit {get; set;}

        [Column("department_id", TypeName = "bigint")]
        public long DepartmentKey { get; set; }
        
        [ForeignKey("DepartmentKey")]
        public Department Department { get; set; }
        
        [Column("user_login", TypeName = "varchar(50)")]
        public string UserLogin { get; set; }

        [Column("user_password", TypeName = "varchar(50)")]
        public string UserPassword { get; set; }

        [Column("ldap_login", TypeName = "varchar(50)")]
        public string LdapLogin { get; set; }

        [NotMapped]
        public string Token { get; set; }
    }
}
