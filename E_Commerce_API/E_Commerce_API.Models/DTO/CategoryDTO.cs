using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_API.Models.DTO
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
