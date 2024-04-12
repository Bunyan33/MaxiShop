using AutoMapper;
using MaxiShop.Application.DTO.Product;
using MaxiShop.Application.DTO.Category;
using MaxiShop.Application.DTO.Product;
using MaxiShop.Domine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaxiShop.Application.DTO.Brand;

namespace MaxiShop.Application.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category,CreateCategoryDto>().ReverseMap();
            CreateMap<Category,UpdateCategoryDto>().ReverseMap();
            CreateMap<Category,CategoryDto>().ReverseMap();

            CreateMap<Product, CreateProductDto>().ReverseMap();
            CreateMap<Product, UpdateProductDto>().ReverseMap();
            CreateMap<Product, ProductDto>()
                .ForMember(x => x.Category, opt => opt.MapFrom(source => source.Category.Name))
                .ForMember(x => x.Brand, opt => opt.MapFrom(source => source.Brand.Name));


            CreateMap<Brand, CreateBrandDto>().ReverseMap();
            CreateMap<Brand, UpdateBrandDto>().ReverseMap();
            CreateMap<Brand, BrandDto>().ReverseMap();

        }
    }
}
