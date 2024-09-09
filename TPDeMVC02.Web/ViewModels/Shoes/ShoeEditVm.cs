using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TPDeMVC02.Web.ViewModels.Shoes
{
    public class ShoeEditVm
    {
        public int ShoeId { get; set; }


        [Required(ErrorMessage = "{0} is required")]
        [StringLength(200, ErrorMessage = "{0} must have between {2} and {1} characters", MinimumLength = 3)]
        [DisplayName("Description")]
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }


        [Required(ErrorMessage = "Brand is required")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a Brand")]
        [DisplayName("Brand")]
        public int BrandId { get; set; }

        [Required(ErrorMessage = "Sport is required")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a Sport")]
        [DisplayName("Sport")]
        public int SportId { get; set; }

        [Required(ErrorMessage = "Genre is required")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a Genre")]
        [DisplayName("Genre")]
        public int GenreId { get; set; }

        [Required(ErrorMessage = "Color is required")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a Color")]
        [DisplayName("Color")]
        public int ColorId { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> Brands { get; set; } = null!;

        [ValidateNever]
        public IEnumerable<SelectListItem> Sports { get; set; } = null!;

        [ValidateNever]
        public IEnumerable<SelectListItem> Genres { get; set; } = null!;
        public IEnumerable<SelectListItem> Colors { get; set; } = null!;
    }
}
