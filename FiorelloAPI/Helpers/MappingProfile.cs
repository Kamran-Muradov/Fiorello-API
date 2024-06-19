using AutoMapper;
using FiorelloAPI.DTOs.Blogs;
using FiorelloAPI.DTOs.Categories;
using FiorelloAPI.DTOs.Experts;
using FiorelloAPI.DTOs.Products;
using FiorelloAPI.DTOs.Settings;
using FiorelloAPI.DTOs.Sliders;
using FiorelloAPI.DTOs.Socials;
using FiorelloAPI.Models;

namespace FiorelloAPI.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Slider
            CreateMap<Slider, SliderDto>();
            CreateMap<SliderInfo, SliderInfoDto>();
            CreateMap<SliderCreateDto, Slider>();
            CreateMap<SliderEditDto, Slider>();

            //Category
            CreateMap<Category, CategoryDto>();
            CreateMap<Category, CategoryArchiveDto>()
                .ForMember(d => d.CreatedDate, opt => opt.MapFrom(s => s.CreatedDate.ToString("MM.dd.yyyy")));
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<Category, CategoryProductDto>()
                .ForMember(d => d.ProductCount, opt => opt.MapFrom(s => s.Products.Count))
                .ForMember(d => d.CreatedDate, opt => opt.MapFrom(s => s.CreatedDate.ToString("MM.dd.yyyy")));
            CreateMap<Category, CategoryDetailDTO>()
                .ForMember(d => d.Products, opt => opt.MapFrom(s => s.Products.Select(p => p.Name)))
                .ForMember(d => d.ProductCount, opt => opt.MapFrom(s => s.Products.Count))
                .ForMember(d => d.CreatedDate, opt => opt.MapFrom(s => s.CreatedDate.ToString("MM.dd.yyyy")));
            CreateMap<CategoryEditDto, Category>();

            //Product
            CreateMap<Product, ProductMainImageDto>()
                .ForMember(d => d.MainImage, opt => opt.MapFrom(s => s.ProductImages.FirstOrDefault(i => i.IsMain).Name));
            CreateMap<Product, ProductDetailDto>()
                .ForMember(d => d.Category, opt => opt.MapFrom(s => s.Category.Name))
                .ForMember(d => d.Images, opt => opt.MapFrom(s => s.ProductImages.Select(i => new ProductImageDto
                {
                    Name = i.Name,
                    IsMain = i.IsMain
                }).ToList()));
            CreateMap<ProductCreateDto, Product>();
            CreateMap<ProductEditDto, Product>();

            //Blog
            CreateMap<Blog, BlogDto>()
                .ForMember(d => d.CreatedDate, opt => opt.MapFrom(s => s.CreatedDate.ToString("MM.dd.yyyy")));
            CreateMap<Blog, BlogDetailDto>()
                .ForMember(d => d.CreatedDate, opt => opt.MapFrom(s => s.CreatedDate.ToString("MM.dd.yyyy")));
            CreateMap<BlogCreateDto, Blog>();
            CreateMap<BlogEditDto, Blog>();

            //Expert
            CreateMap<Expert, ExpertDto>();
            CreateMap<Expert, ExpertDetailDto>()
                .ForMember(d => d.CreatedDate, opt => opt.MapFrom(s => s.CreatedDate.ToString("MM.dd.yyyy")));
            CreateMap<ExpertCreateDto, Expert>();
            CreateMap<ExpertEditDto, Expert>();

            //Setting
            CreateMap<SettingEditDto, Setting>();

            //Social
            CreateMap<Social, SocialDto>();
        }
    }
}
