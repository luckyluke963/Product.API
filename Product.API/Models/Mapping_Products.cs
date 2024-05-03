using AutoMapper;
using Product.API.MyHelper;
using Product.Core.Dto;
using Product.Core.Entities;


namespace Product.API.Models
{
    public class Mapping_Products : Profile
    { 
        public Mapping_Products() 
        {
            //CreateMap<ProductDto,Products>().ReverseMap();
            CreateMap<Products, ProductDto>()
                .ForMember(d => d.CategoryName, o => o.MapFrom(s => s.Category.Name))
                .ForMember(d => d.ProductPicture, o => o.MapFrom<ProductUrlResolver>())
                .ReverseMap();

            CreateMap<CreateProductDto, Products>().ReverseMap();
            CreateMap<Products, UploadProductDto>().ReverseMap();

        }
    }
}
