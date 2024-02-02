using System.ComponentModel.DataAnnotations;

namespace MyProjectBE.Model
{
    public class ProductModel
    {
        [Key]
        public long ProductID {get; set; }
        public string ProductName { get; set; }
        public float ProductPrice { get; set; }
        public long CategoryID{get;set; }
        public int Stock {get;set; }
        public string ProductImage{get; set; }
    }
}
