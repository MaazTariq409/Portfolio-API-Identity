using Portfolio_API.DTOs;
using Portfolio_API.Models;

namespace Portfolio_API.Repository.Repository_Interface
{
	public interface IProducts
    {
		public IEnumerable<UserProducts> GetProducts();
		public IEnumerable<UserProducts> GetProductsByUserID(int id);
		public void AddProductsByUserID(int id, UserProducts userProducts);
		public void updateProductsByUserID(int id, int productId, UserProducts userProducts);
		public void removeProductsByUserID(int id, int productId);

        public void removeProductsRequest(int userId, int productId);
        public void updateProductsRequest(int id, int productId, UserProducts userProducts);

    }
}
