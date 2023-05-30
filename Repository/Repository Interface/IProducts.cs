using Portfolio_API.DTOs;
using Portfolio_API.Models;

namespace Portfolio_API.Repository.Repository_Interface
{
	public interface IProducts
    {
		public IEnumerable<UserProducts> GetProducts();
		public IEnumerable<UserProducts> GetProductsByUserID(string id);
		public void AddProductsByUserID(string id, UserProducts userProducts);
		public void updateProductsByUserID(string id, int productId, UserProducts userProducts);
		public void removeProductsByUserID(string id, int productId);

        public void removeProductsRequest(string userId, int productId);
        public void updateProductsRequest(string id, int productId, UserProducts userProducts);

    }
}
