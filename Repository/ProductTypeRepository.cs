using AutoMapper;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Portfolio_API.Data;
using Portfolio_API.DTOs;
using Portfolio_API.Models;
using Portfolio_API.Repository.Repository_Interface;

namespace Portfolio_API.Repository
{
    public class ProductTypeRepository : IProductType
    {
        private readonly PorfolioContext _context;
        public ProductTypeRepository(PorfolioContext context)
        {
            _context = context;

        }

        IEnumerable<UserProductType> IProductType.GetUserProductTypes()
        {
            return _context.productTypes.ToList();
        }

        void IProductType.AddProductType(Portfolio_API.Models.UserProductType productType)
        {
            _context.productTypes.Add(productType);
            _context.SaveChanges();
        }

        void IProductType.UpdateProductType(int productTypeId, UserProductType productType)
        {
            var productype = _context.productTypes.ToList();
            var productypeUpdate = productype[productTypeId];

            if (productypeUpdate != null)
            {
                productypeUpdate.ProductName = productType.ProductName;

                _context.SaveChanges();
            }
        }

        void IProductType.RemoveProductType(int productTypeId)
        {
            var producttype = _context.productTypes.ToList();
            var delproduct = producttype[productTypeId];

            if (delproduct != null)
            {
                _context.Remove(delproduct);
                _context.SaveChanges();
            }
        }
    }

}

