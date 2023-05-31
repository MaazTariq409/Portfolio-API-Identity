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

        public void AddProductsByUserID(string id, UserProducts userProducts)
        {
            var user = _context.identityManuals.Include(x => x.UserProfile.UserProducts).FirstOrDefault(x => x.Id == id);

            if (user != null)
            {
                user.UserProfile.UserProducts.Add(userProducts);
                _context.SaveChanges();
            }
        }

        public IEnumerable<UserProducts> GetProducts()
        {
            var allProducts = _context.userProducts.Include(x => x.UserProfile).ToList();
            return allProducts;
        }

        public IEnumerable<UserProducts> GetProductsByUserID(string id)
        {
            var user = _context.identityManuals.Include(x => x.UserProfile.UserProducts).FirstOrDefault(x => x.Id == id);
            return user.UserProfile.UserProducts;
        }

        public void removeProductsByUserID(string id, int productId)
        {

            var user = _context.identityManuals.Include(x => x.UserProfile.UserProducts).FirstOrDefault(x => x.Id == id);

            if (user != null)
            {
                var product = user.UserProfile.UserProducts[productId];

                if(product != null)
                {
                    _context.Remove(product);
                    _context.SaveChanges();
                }
            }
        }

        public void removeProductsRequest(string userId, int productId)
        {
            var user = _context.identityManuals.Include(x => x.UserProfile.UserProducts).FirstOrDefault(x => x.Id == userId);

            if (user != null)
            {
                var product = user.UserProfile.UserProducts.FirstOrDefault(x => x.Id == productId);

                if (product != null)
                {
                    _context.Remove(product);
                    _context.SaveChanges();
                }
            }
        }

        public void updateProductsByUserID(string id, int productId, UserProducts userProducts)
        {
            var user = _context.identityManuals.Include(x => x.UserProfile.UserProducts).FirstOrDefault(y => y.Id == id);

            if (user != null)
            {
                var product = user.UserProfile.UserProducts[productId];

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

        public void updateProductsRequest(string id, int productId, UserProducts userProducts)
        {
            var user = _context.identityManuals.Include(x => x.UserProfile.UserProducts).FirstOrDefault(y => y.Id == id);

            if (user != null)
            {
                var product = user.UserProfile.UserProducts.FirstOrDefault(x => x.Id == productId);

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
