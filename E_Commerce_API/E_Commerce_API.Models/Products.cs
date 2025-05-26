using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product.Models
{
    [Table("Products")]
    public class Products : BaseEntity
    {
        [Required, StringLength(30)]
        public string ProductName { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string ProductDescription { get; set; } = string.Empty;

        [Required,ForeignKey("Category")]
        public int CategoryId { get; set; }

        public Category? Category { get; set; }  // Navigation property
    }
}
