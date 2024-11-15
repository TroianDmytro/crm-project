using AutoMapper;
using CRM_Business_Layer.DTO;
using CRM_DAL.Entitys;
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

        }
    }
}
