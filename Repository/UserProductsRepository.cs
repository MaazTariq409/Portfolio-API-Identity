using Microsoft.EntityFrameworkCore;
using Portfolio_API.Data;
using Portfolio_API.Models;
using Portfolio_API.Repository.Repository_Interface;

namespace Portfolio_API.Repository
{
    public class UserProductsRepository : IProducts
    {
        private readonly PorfolioContext _context;
        public UserProductsRepository(PorfolioContext context)
        {
            _context = context;
        }

        public void AddProductsByUserID(int id, UserProducts userProducts)
        {
            var user = _context.user.Include(x => x.UserProducts).FirstOrDefault(x => x.Id == id);

            if (user != null)
            {
                user.UserProducts.Add(userProducts);
                _context.SaveChanges();
            }
        }

        public IEnumerable<UserProducts> GetProducts()
        {
            var allProducts = _context.userProducts.Include(x => x.user.About).ToList();
            return allProducts;
        }

        public IEnumerable<UserProducts> GetProductsByUserID(int id)
        {
            var allProducts = _context.userProducts.Where(x => x.Id == id).Include(x => x.user.About).ToList();
            return allProducts;
        }

        public void removeProductsByUserID(int id, int productId)
        {

            var user = _context.user.Include(x => x.UserProducts).FirstOrDefault(y => y.Id == id);

            if(user != null)
            {
                var product = user.UserProducts[productId];

                if(product != null)
                {
                    _context.Remove(product);
                    _context.SaveChanges();
                }
            }
        }

        public void removeProductsRequest(int userId, int productId)
        {
            var user = _context.user.Include(x => x.UserProducts).FirstOrDefault(y => y.Id == userId);

            if (user != null)
            {
                var product = user.UserProducts.FirstOrDefault(x => x.Id == productId);

                if (product != null)
                {
                    _context.Remove(product);
                    _context.SaveChanges();
                }
            }
        }

        public void updateProductsByUserID(int id, int productId, UserProducts userProducts)
        {
            var user = _context.user.Include(x => x.UserProducts).FirstOrDefault(y => y.Id == id);

            if (user != null)
            {
                var product = user.UserProducts[productId];

                if (product != null)
                {
                    product.Title = userProducts.Title;
                    product.Description = userProducts.Description;
                    product.DateCreated = userProducts.DateCreated;
                    product.PermaLink = userProducts.PermaLink;
                    product.VideoUrl = userProducts.VideoUrl;
                    product.Image = userProducts.Image;
                    product.Type = userProducts.Type;
                    product.Status = "pending";

                    _context.SaveChanges();
                }
            }

        }

        public void updateProductsRequest(int id, int productId, UserProducts userProducts)
        {
            var user = _context.user.Include(x => x.UserProducts).FirstOrDefault(y => y.Id == id);

            if (user != null)
            {
                var product = user.UserProducts.FirstOrDefault(x => x.Id == productId);

                if (product != null)
                {
                    product.Title = userProducts.Title;
                    product.Description = userProducts.Description;
                    product.DateCreated = userProducts.DateCreated;
                    product.PermaLink = userProducts.PermaLink;
                    product.VideoUrl = userProducts.VideoUrl;
                    product.Image = userProducts.Image;
                    product.Type = userProducts.Type;
                    product.Status = userProducts.Status;

                    _context.SaveChanges();
                }
            }
        }
    }
}
