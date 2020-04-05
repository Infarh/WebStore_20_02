using System.Collections.Generic;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using WebStore.Clients.Base;
using WebStore.Domain;
using WebStore.Domain.DTO.Products;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;

namespace WebStore.Clients.Products
{
    public class ProductsClient : BaseClient, IProductData
    {
        public ProductsClient(IConfiguration config) : base(config, WebAPI.Products) { }

        public IEnumerable<SectionDTO> GetSections() => Get<List<SectionDTO>>("sections");
        
        public SectionDTO GetSectionById(int id) => Get<SectionDTO>($"sections/{id}");

        public IEnumerable<BrandDTO> GetBrands() => Get<List<BrandDTO>>("brands");

        public BrandDTO GetBrandById(int id) => Get<BrandDTO>($"brands/{id}");

        public PagedProductsDTO GetProducts(ProductFilter Filter = null) => 
            Post(Filter)
               .Content
               .ReadAsAsync<PagedProductsDTO>()
               .Result;

        public ProductDTO GetProductById(int id) => GetById<ProductDTO>(id);
    }
}
