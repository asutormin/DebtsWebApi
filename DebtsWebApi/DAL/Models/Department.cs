using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DebtsWebApi.DAL.Models
{
    [Table("vw_departments")]
    public class Department
    {
        [Key]
        [Column("department_id", TypeName = "bigint")]
        public long Id { get; set; }

        [Column("department_pid", TypeName = "bigint")]
        public long? ParentId { get; set; }

        [Column("department_name", TypeName = "varchar(250)")]
        public string Name { get; set; }
    }
}
