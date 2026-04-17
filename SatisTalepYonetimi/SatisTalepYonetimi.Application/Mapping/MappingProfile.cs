using AutoMapper;
using SatisTalepYonetimi.Application.Features.Customers.CreateCustomer;
using SatisTalepYonetimi.Application.Features.Customers.UpdateCustomer;
using SatisTalepYonetimi.Application.Features.Products.CreateProduct;
using SatisTalepYonetimi.Application.Features.Products.UpdateProduct;
using SatisTalepYonetimi.Domain.Entities;

namespace SatisTalepYonetimi.Application.Mapping
{
    public sealed class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateCustomerCommand, Customer>();
            CreateMap<UpdateCustomerCommand, Customer>();

            CreateMap<CreateProductCommand, Product>();
            CreateMap<UpdateProductCommand, Product>();
        }
    }
}
