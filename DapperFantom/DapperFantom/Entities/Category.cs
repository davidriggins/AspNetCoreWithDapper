using System.ComponentModel.DataAnnotations;

namespace DapperFantom.Entities
{
    public class Category
    {
        [Dapper.Contrib.Extensions.Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Please enter your category")]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }

        [Required(ErrorMessage = "Please enter your slug")]
        [Display(Name = "Slug Name")]
        public string Slug { get; set; }
        public int OrderBy { get; set; }
    }
}
