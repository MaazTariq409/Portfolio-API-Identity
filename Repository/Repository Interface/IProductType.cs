using Portfolio_API.DTOs;
using Portfolio_API.Models;

namespace Portfolio_API.Repository.Repository_Interface
{
	public interface IProductType
    {
		public IEnumerable<UserProductType> GetUserProductTypes();
		public void AddProductType(UserProductType productType);
		public void UpdateProductType(int productTypeId, UserProductType productType);
		public void RemoveProductType(int productTypeId);


	}
}
