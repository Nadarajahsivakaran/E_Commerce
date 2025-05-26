using System.ComponentModel.DataAnnotations;

namespace Product.Models.DTO
{
    public class CreatedCategoryDTO
    {
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; } = string.Empty;

        [Display(Name = "Category Description")]
        public string CategoryDescription { get; set; } = string.Empty;
    }
}
