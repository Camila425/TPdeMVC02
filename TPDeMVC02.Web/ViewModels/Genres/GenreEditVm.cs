using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TPDeMVC02.Web.ViewModels.Genres
{
	public class GenreEditVm
	{
		public int GenreId { get; set; }
		[Required(ErrorMessage = "{0} is required")]
		[StringLength(10, ErrorMessage = "{0} must have between {2} and {1} characters", MinimumLength = 3)]
		[DisplayName("Genre Name")]
		public string GenreName { get; set; } = null!;
	}
}
