using Portfolio_API.DTOs;
using Portfolio_API.Models;

namespace Portfolio_API.Repository.Repository_Interface
{
	public interface ICountry
    {
        public IEnumerable<UserCountry> GetCountries();
        public void AddCountry(UserCountry country);
        public void updateCountry(int countryId, UserCountry country);
        public void removeCountry(int countryId);
    }
}
