using AutoMapper;
using Core.Entities;
using Microsoft.Extensions.Configuration;
using SkinetWebApi.Dtos;

namespace SkinetWebApi.Helpers
{
    public class ProductUrlResorver : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration _config;

        public ProductUrlResorver(IConfiguration config)
        {
            _config = config;
        }
        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                return _config["ApiUrl"] + source.PictureUrl;
            }

            return null;
        }
    }
}
