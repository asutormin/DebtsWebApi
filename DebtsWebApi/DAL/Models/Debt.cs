using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DebtsWebApi.DAL.Models
{
    [Table("t_user_debt")]
    public class Debt
    {
        [Key]
        [Column("user_debt_id", TypeName = "int")]
        public int Id { get; set; }

        [Column("user_id", TypeName = "bigint")]
        public long DebtorKey { get; set; }
        
        [ForeignKey("DebtorKey")]
        public User Debtor { get; set; }

        [Column("debt_type_id", TypeName = "int")]
        public int DebtTypeKey { get; set; }

        [ForeignKey("DebtTypeKey")]
        public DebtType DebtType { get; set; }

        [Column("year", TypeName = "int")]
        public int Year { get; set; }

        [Column("month", TypeName = "int")]
        public int Month { get; set; }
        
        [Column("count", TypeName = "int")]
        public int Count { get; set; }

        [Column("cost", TypeName = "money")]
        public float Cost { get; set; }

        [Column("description", TypeName = "varchar(300)")]
        public string Description { get; set; }

        [Key]
        [Column("begin_date", TypeName = "datetime")]
        public DateTime BeginDate { get; set; }

        [Column("end_date", TypeName = "datetime")]
        public DateTime EndDate { get; set; }

        [Column("edit_user", TypeName = "bigint")]
        public long EditorKey { get; set; }

        [ForeignKey("EditorKey")]
        public User Editor { get; set; }
    }
}
