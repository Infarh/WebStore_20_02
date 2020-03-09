using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.Domain.DTO.Products;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;
using WebStore.Services.Mapping;

namespace WebStore.Services.Product
{
    public class SqlProductData : IProductData
    {
        private readonly WebStoreContext _db;

        public SqlProductData(WebStoreContext db) => _db = db;

        public IEnumerable<SectionDTO> GetSections() => _db.Sections.ToDTO().AsEnumerable();

        public SectionDTO GetSectionById(int id) => _db.Sections.Find(id).ToDTO();

        public IEnumerable<BrandDTO> GetBrands() => _db.Brands.ToDTO().AsEnumerable();

        public BrandDTO GetBrandById(int id) => _db.Brands.Find(id).ToDTO();

        public IEnumerable<ProductDTO> GetProducts(ProductFilter Filter = null)
        {
            IQueryable<Domain.Entities.Product> query = _db.Products;

            if (Filter?.BrandId != null)
                query = query.Where(product => product.BrandId == Filter.BrandId);

            if (Filter?.SectionId != null)
                query = query.Where(product => product.SectionId == Filter.SectionId);

            if (Filter?.Ids?.Count > 0)
                query = query.Where(product => Filter.Ids.Contains(product.Id));

            return query.AsEnumerable().ToDTO();
        }

        public ProductDTO GetProductById(int id) =>
            _db.Products
               .Include(p => p.Brand)
               .Include(p => p.Section)
               .FirstOrDefault(p => p.Id == id).ToDTO();
    }
}
