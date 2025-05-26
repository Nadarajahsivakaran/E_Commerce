using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product.Models
{
    [Table("Categories")]
    public class Category : BaseEntity
    {
        [Required,StringLength(30)]
        public string CategoryName { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string CategoryDescription { get; set; } = string.Empty;
    }
}
