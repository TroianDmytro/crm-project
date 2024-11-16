using AutoMapper;
using CRM_Business_Layer.DTO;
using CRM_Business_Layer.DTO.AuthDTO;
using CRM_DAL.Entitys;
using CRM_DAL.Entitys.Auth;
using CRM_Server_API.Models.Request;

namespace CRM_Server_API.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Client, ClientDTO>().ReverseMap();
            CreateMap<ClientRequest, ClientDTO>();

            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<ProductRequest, ProductDTO>();

            CreateMap<Deal, DealDTO>().ReverseMap();
            CreateMap<Client, ClientDTO>()
            .ForMember(
                dest => dest.DealDTOs,        // Указываем свойство в DTO
                opt => opt.MapFrom(src => src.Deals) // Связываем его с оригинальным свойством
            )
            .ReverseMap()
            .ForMember(
                dest => dest.Deals,          // Указываем свойство в оригинальной модели
                opt => opt.MapFrom(src => src.DealDTOs) // Связываем его с DTO
            );
            CreateMap<DealRequest, DealDTO>();


            CreateMap<DealProduct, DealProductDTO>().ReverseMap();

            CreateMap<RegisterModelDTO, EmployeeRegisterModel>();
            CreateMap<DealProductDTO, DealProduct>()
                .ForMember(dest => dest.DealId, opt => opt.MapFrom(src => src.DealId))
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.QuantityTransaction, opt => opt.MapFrom(src => src.QuantityTransaction));

            CreateMap<DealProduct, DealProductDTO>()
                .ForMember(dest => dest.DealId, opt => opt.MapFrom(src => src.DealId))
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.QuantityTransaction, opt => opt.MapFrom(src => src.QuantityTransaction));

        }
    }
}
