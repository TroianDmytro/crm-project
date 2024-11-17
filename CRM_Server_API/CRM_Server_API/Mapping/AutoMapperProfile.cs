using AutoMapper;
using CRM_Business_Layer.DTO;
using CRM_Business_Layer.DTO.AuthDTO;
using CRM_DAL.Entitys;
using CRM_DAL.Entitys.Auth;
using CRM_Server_API.Blobs;
using CRM_Server_API.Models.Request;
using CRM_Server_API.Models.Responce;

namespace CRM_Server_API.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Client, ClientDTO>().ReverseMap();

            CreateMap<ClientRequest, ClientDTO>();

            CreateMap<Deal, DealDTO>().ReverseMap();

            CreateMap<Client, ClientDTO>()
                .ForMember(
                    dest => dest.DealDTOs,
                    opt => opt.MapFrom(src => src.Deals)
                )
                .ReverseMap()
                .ForMember(
                    dest => dest.Deals,
                    opt => opt.MapFrom(src => src.DealDTOs)
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

            // Маппинг с резолвером для загрузки файлов
            CreateMap<ProductRequest, ProductDTO>()
                .ForMember(
                    dest => dest.PhotoBlob,
                    opt => opt.MapFrom<PhotoBlobUploadResolver>() // Конвертация IFormFile? -> string?
                );

            CreateMap<Product, ProductDTO>().ReverseMap();

            CreateMap<ProductDTO, ProductResponce>()
            .ForMember(
                dest => dest.PhotoBase64,
                opt => opt.MapFrom<PhotoBlobToBase64Resolver>() // Используем резолвер для преобразования
            );
        }
    }

}
