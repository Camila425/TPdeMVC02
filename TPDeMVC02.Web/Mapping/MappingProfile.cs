using AutoMapper;
using TPdeEFCore01.Entidades;
using TPDeMVC02.Web.ViewModels.Brands;
using TPDeMVC02.Web.ViewModels.Colors;
using TPDeMVC02.Web.ViewModels.Genres;
using TPDeMVC02.Web.ViewModels.Shoes;
using TPDeMVC02.Web.ViewModels.Sizes;
using TPDeMVC02.Web.ViewModels.Sports;

namespace TPDeMVC02.Web.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            LoadBrandsMapping();
            LoadGenresMapping();
            LoadSportsMapping();
			LoadColorsMapping();
			LoadSizesMapping();
			LoadShoesMapping();
		}

		private void LoadShoesMapping()
		{
              CreateMap<Shoe, ShoeListVm>()
             .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand.BrandName))
             .ForMember(dest => dest.Sport, opt => opt.MapFrom(src => src.Sport.SportName))
             .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.GenreName))
             .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.Color.ColorName));
			  CreateMap<Shoe, ShoeEditVm>().ReverseMap();
		}

		private void LoadSizesMapping()
		{
			CreateMap<Size, SizeEditVm>().ReverseMap();
		}

		private void LoadColorsMapping()
		{
            CreateMap<Color, ColorListVm>();
			CreateMap<Color, ColorDetailsVm>();
			CreateMap<Color, ColorEditVm>().ReverseMap();
		}

		private void LoadSportsMapping()
        {
            CreateMap<Sport, SportListVm>();
			CreateMap<Sport, SportEditVm>().ReverseMap();
        }

        private void LoadGenresMapping()
        {
            CreateMap<Genre, GenreEditVm>().ReverseMap();
        }

        private void LoadBrandsMapping()
        {
            CreateMap<Brand, BrandListVm>();
			CreateMap<Brand, BrandDetailsVm>();
			CreateMap<Brand, BrandEditVm>().ReverseMap();

		}
	}
}
