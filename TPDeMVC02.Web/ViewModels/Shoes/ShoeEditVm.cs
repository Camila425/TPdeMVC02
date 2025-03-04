using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TPDeMVC02.Web.ViewModels.Shoes
{
    public class ShoeEditVm
    {
        public int ShoeId { get; set; }


        [Required(ErrorMessage = "{0} is required")]
        [StringLength(200, ErrorMessage = "{0} must have between {2} and {1} characters", MinimumLength = 3)]
        [DisplayName("Descripción")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(150, ErrorMessage = "{0} must have between {2} and {1} characters", MinimumLength = 3)]
        [DisplayName("Modelo")]
        public string Model { get; set; } = null!;

        [Required(ErrorMessage = "{0} is required")]
        [Range(1, int.MaxValue)]
        [DisplayName("Precio")]
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
        public bool Active { get; set; }
        public string? ReturnUrl { get; set; }
        public List<string> Images { get; set; } = new List<string>();
        public List<IFormFile>? ImageFiles { get; set; }


        [ValidateNever]
        public List<SelectListItem>? Brands { get; set; }

        [ValidateNever]
        public List<SelectListItem>? Sports { get; set; }

        [ValidateNever]
        public List<SelectListItem>? Genres { get; set; }
    }
}
