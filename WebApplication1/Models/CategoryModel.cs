using System.ComponentModel.DataAnnotations;

namespace MyProjectBE.Model
{
    public class CategoryModel
    {
        [Key]
        public long CategoryID { get; set; }
        [Required]
        public string CategoryName { get; set; }
    }
}
