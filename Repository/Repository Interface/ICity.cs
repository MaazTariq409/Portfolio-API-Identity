using Portfolio_API.DTOs;
using Portfolio_API.Models;

namespace Portfolio_API.Repository.Repository_Interface
{
	public interface ICity
    {
		public IEnumerable<UserCity> GetCities();
		public void AddCity(UserCity city);
		public void updateCity(int cityId, UserCity city);
		public void removeCity(int cityId);


	}
}
