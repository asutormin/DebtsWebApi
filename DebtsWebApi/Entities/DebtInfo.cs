using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DebtsWebApi.Entities
{
    public class DebtInfo
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        public int DebtTypeId { get; set; }
        
        [Required]
        public long DebtorId { get; set; }
        
        [Required]
        public int Year { get; set; }
        
        [Required]
        public int Month { get; set; }
        
        [Required]
        public float Cost { get; set; }
        
        [Required]
        public int Count { get; set; }
        
        public string Description { get; set; }
        
        [Required]
        public int EditorId { get; set; }
    }
}
