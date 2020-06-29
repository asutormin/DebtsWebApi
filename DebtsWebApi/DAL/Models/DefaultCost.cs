using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DebtsWebApi.DAL.Models
{
    [Table("t_default_cost")]
    public class DefaultCost
    {
        [Key]
        [Column("default_cost_id", TypeName = "int")]
        public int Id { get; set; }

        [Column("debt_type_id", TypeName = "int")]
        public int DebtTypeKey { get; set; }

        [ForeignKey("DebtTypeKey")]
        public DebtType DebtType { get; set; }

        [Column("default_cost", TypeName = "money")]
        public float Value { get; set; }

        [Key]
        [Column("begin_date", TypeName = "datetime")]
        public DateTime BeginDate { get; set; }

        [Column("end_date", TypeName = "datetime")]
        public DateTime EndDate { get; set; }
    }
}
