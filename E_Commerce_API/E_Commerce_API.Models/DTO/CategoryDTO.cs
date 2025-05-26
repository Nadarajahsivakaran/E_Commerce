
using System.ComponentModel.DataAnnotations;


namespace Product.Models.DTO
{
    public class CategoryDTO
    {
        [Display(Name = "Category Id")]
        public int CategoryId { get; set; }

        [Display(Name = "Category Name")]
        public string CategoryName { get; set; } = string.Empty;

        [Display(Name = "Category Description")]
        public string CategoryDescription { get; set; } = string.Empty;
    }
}
