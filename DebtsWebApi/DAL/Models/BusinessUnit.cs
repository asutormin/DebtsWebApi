using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DebtsWebApi.DAL.Models
{
    [Table("vw_business_units")]
    public class BusinessUnit
    {
        [Key]
        [Column("business_unit_id", TypeName = "int")]
        public int Id { get; set; }

        [Column("business_unit_name", TypeName = "varchar(150)")]
        public string Name { get; set; }
    }
}
