using System.ComponentModel.DataAnnotations;

namespace Product.Models
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public int? IsDelete { get; set; } = 0;

        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [DataType(DataType.Date)]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
