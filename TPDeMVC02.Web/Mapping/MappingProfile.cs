using AutoMapper;
using TPdeEFCore01.Entidades;
using TPdeEFCore01.Entidades.Dtos;
using TPDeMVC02.Web.ViewModels.ApplicationUsers;
using TPDeMVC02.Web.ViewModels.Brands;
using TPDeMVC02.Web.ViewModels.Colors;
using TPDeMVC02.Web.ViewModels.Genres;
using TPDeMVC02.Web.ViewModels.OrderHeaders;
using TPDeMVC02.Web.ViewModels.Shoes;
using TPDeMVC02.Web.ViewModels.ShoppingCarts;
using TPDeMVC02.Web.ViewModels.Sizes;
using TPDeMVC02.Web.ViewModels.Sports;

namespace TPDeMVC02.Web.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            LoadBrandsMapping();
            LoadGenresMapping();
            LoadSportsMapping();
            LoadColorsMapping();
            LoadSizesMapping();
            LoadShoesMapping();
            LoadApplicationUsersMapping();
            LoadShoppingCartsMapping();
            LoadOrderHeadersMapping();
        }
        private void LoadOrderHeadersMapping()
        {
            CreateMap<OrderHeaderEditVm, OrderHeader>()
             .ForMember(dest => dest.OrderDetails, opt => opt.MapFrom(src => src.OrderDetails));
        }
        private void LoadShoppingCartsMapping()
        {
            CreateMap<ShoppingCartDetailVm, ShoppingCart>()
            .ForMember(dest => dest.ShoeSize, opt => opt.Ignore())
            .ForMember(dest => dest.ShoeColor, opt => opt.Ignore())
            .ForMember(dest => dest.ApplicationUser, opt => opt.Ignore())
            .ForMember(dest => dest.ApplicationUserId, opt => opt.MapFrom(src => src.ApplicationUserId));

            CreateMap<ShoppingCart, OrderDetail>()
            .ForMember(dest => dest.OrderHeaderId, opt => opt.Ignore())
            .ForMember(dest => dest.shoeSize, opt => opt.Ignore())
            .ForMember(dest => dest.ShoeColor, opt => opt.Ignore())
            .ForMember(dest => dest.ShoeSizeId, opt => opt.MapFrom(src => src.ShoeSizeId))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => (src.Quantity == 1 
             ? src.ShoeSize.shoe.Price+src.ShoeColor.PriceAdjustment 
             : src.ShoeSize.shoe.Price+ src.ShoeColor.PriceAdjustment * 0.9M)));
        }
        private void LoadApplicationUsersMapping()
        {
            CreateMap<ApplicationUser, ApplicationUserListVm>()
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country.CountryName))
            .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State.StateName))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City.CityName));
        }
        private void LoadShoesMapping()
        {
            CreateMap<Shoe, ShoeListVm>()
            .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand.BrandName))
            .ForMember(dest => dest.Sport, opt => opt.MapFrom(src => src.Sport.SportName))
            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.GenreName));
            CreateMap<Shoe, ShoeEditVm>().ForMember(dest => dest.Images,opt => opt.MapFrom(src => src.Images != null
              ? src.Images.Select(img => img.ImageUrl).ToList()!: new List<string>())).ReverseMap()
            .ForMember(dest => dest.Images, opt => opt.Ignore());
            CreateMap<ShoeColor, ShoeListVm>()
            .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Shoe.Brand.BrandName))
            .ForMember(dest => dest.Sport, opt => opt.MapFrom(src => src.Shoe.Sport.SportName))
            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Shoe.Genre.GenreName))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Shoe.Description))
            .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Shoe.Model))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Shoe.Price));

            CreateMap<Shoe, ShoeAssignSizesVm>();
            CreateMap<ShoeAssignSizesVm, ShoeSizeDto>()
            .ForMember(dest => dest.ShoeId, opt => opt.MapFrom(src => src.ShoeId))
            .ForMember(dest => dest.SizeId, opt => opt.MapFrom(src => src.NewSizeId))
            .ForMember(dest => dest.Stock, opt => opt.MapFrom(src => src.NewStock));

            CreateMap<ShoeImage, ShoeHomeIndexVm>()
           .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
           .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.shoe.Model))
           .ForMember(dest => dest.ListPrice, opt => opt.MapFrom(src => src.shoe.Price))
           .ForMember(dest => dest.Active, opt => opt.MapFrom(src => src.shoe.Active))
           .ForMember(dest => dest.SizeId, opt => opt.MapFrom(src => src.shoe.shoeSizes.OrderBy(ss => ss.SizeId)
           .Select(ss => (int?)ss.SizeId).FirstOrDefault()))
           .ForMember(dest => dest.ShoeId, opt => opt.MapFrom(src => src.shoe.ShoeId))
           .ForMember(dest => dest.ColorId, opt => opt.MapFrom(src => src.shoe.ShoeColors.OrderBy(ss => ss.ColorId)
           .Select(ss => (int?)ss.ColorId).FirstOrDefault()));
            CreateMap<ShoeSize, ShoeHomeDetailsVm>()
           .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.shoe.Brand.BrandName))
           .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.shoe.Description))
           .ForMember(dest => dest.ListPrice, opt => opt.MapFrom(src => src.shoe.Price))
           .ForMember(dest => dest.CashPrice, opt => opt.MapFrom(src => src.shoe.Price * 0.9m))
           .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.shoe.Images))
           .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.shoe.Model))
           .ForMember(dest => dest.AvailableStock, opt => opt.
            MapFrom(src => src.shoe.shoeSizes.Sum(ss => ss.AvailableStock)))

           .ForMember(dest => dest.ShoeColorId, opt => opt.MapFrom(src =>src.shoe.ShoeColors.OrderBy(sc => sc.ColorId)
           .Where(sc => sc.ColorId != 0) .Select(sc => (int?)sc.ColorId).FirstOrDefault()));
            CreateMap<ShoeSize, EditStockShoeSize>()
           .ForMember(dest => dest.SizeId, opt => opt.MapFrom(src => src.SizeId))
           .ForMember(dest => dest.ShoeId, opt => opt.MapFrom(src => src.ShoeId))
           .ForMember(dest => dest.StockActual, opt => opt.MapFrom(src => src.QuantityInStock))
           .ForMember(dest => dest.StockNuevo, opt => opt.MapFrom(src => src.QuantityInStock)).ReverseMap();

            CreateMap<EditStockShoeSize, ShoeSize>()
           .ForMember(dest => dest.SizeId, opt => opt.MapFrom(src => src.SizeId))
           .ForMember(dest => dest.ShoeId, opt => opt.MapFrom(src => src.ShoeId))
           .ForMember(dest => dest.QuantityInStock, opt => opt.MapFrom(src => src.StockActual))
           .ForMember(dest => dest.QuantityInStock, opt => opt.MapFrom(src => src.StockNuevo))
           .ForMember(dest => dest.shoe, opt => opt.Ignore())
           .ForMember(dest => dest.shoe, opt => opt.Ignore());
            CreateMap<Shoe, ShoeAssignColorsVm>()
            .ForMember(dest => dest.BasePrice, opt => opt.MapFrom(src => src.Price));

            CreateMap<ShoeAssignColorsVm, ShoeColorDto>()
           .ForMember(dest => dest.ShoeId, opt => opt.MapFrom(src => src.ShoeId))
           .ForMember(dest => dest.ColorId, opt => opt.MapFrom(src => src.NewColorId))
           .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.NewColorPrice));
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
