using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DebtsWebApi.DAL.Models
{
    [Table("t_user_debt_ids")]
    public class DebtId
    {
        [Key]
        [Column("user_debt_id", TypeName = "int")]
        public int Id { get; set; }

        [Column("is_actual", TypeName = "bit")]
        public bool IsActual { get; set; }
    }
}
