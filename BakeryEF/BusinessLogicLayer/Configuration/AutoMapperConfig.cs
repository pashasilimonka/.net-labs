using AutoMapper;
using BusinessLogicLayer.DTO.Request;
using BusinessLogicLayer.DTO.Responce;
using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Configuration
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Client, ClientResponce>();
            CreateMap<Client, ClientOrders>()
                .ForMember(dest => dest.orders,
                opt => opt.MapFrom(src => src.orders));
            CreateMap<ClientRequest, Client>();
            CreateMap<Order, OrderResponce>();

            CreateMap<Order, OrderProductResponce>()
                .ForMember(dest => dest.products,
                opt => opt.MapFrom(src => src.products));
            CreateMap<Order, OrderRequest>();
            CreateMap<Product, ProductResponce>();
            CreateMap<Product, ProductRequest>();
            CreateMap<ProductRequest,Product>();

        }
    }
}
