using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyProjectBE.Model
{
    public class TransactionModel
    {
        [Key]
        public long TransactID{get;set;}
        public long Qty {get;set;}
        public Guid UserID { get; set; }
        public DateTime CreatedDate{get;set;} 
        [NotMapped]
        public TransactionDetailModel Detail { get; set; }
    }
    public class TransactionDetailModel { 
        [Key]
        public long TransactDetailID{get;set;}
        public long CategoryID{get;set;}
        public long ProductID{get;set;}
    }
}
