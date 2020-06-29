using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DebtsWebApi.DAL.Models
{
    [Table("t_debt_types")]
    public class DebtType
    {
        [Key]
        [Column("debt_type_id", TypeName = "int")]
        public int Id { get; set; }

        [Column("name", TypeName = "varchar(50)")]
        public string Name { get; set; }

        [Column("description", TypeName = "varchar(300)")]
        public string Description { get; set; }

        [Column("is_debt", TypeName = "bit")]
        public bool IsDebt { get; set; }
    }
}
