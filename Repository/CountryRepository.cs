using AutoMapper;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Portfolio_API.Data;
using Portfolio_API.DTOs;
using Portfolio_API.Models;
using Portfolio_API.Repository.Repository_Interface;

namespace Portfolio_API.Repository
{
	public class CountryRepository : ICountry
	{
        private readonly PorfolioContext _context;
        public CountryRepository(PorfolioContext context)
        {
            _context = context;

        }

        public IEnumerable<UserCountry> GetCountries()
        {
            return _context.userCountry.ToList();
        }

        public void AddCountry(UserCountry country)
        {
            _context.userCountry.Add(country);
            _context.SaveChanges();
        }

        public void updateCountry(int countryId, UserCountry countryData)
        {
            var country = _context.userCountry.ToList();
            var countryUpdate = country[countryId];

            if (countryUpdate != null)
            {
                countryUpdate.Name= countryData.Name;
                countryUpdate.Alpha3 = countryData.Alpha3;
                countryUpdate.Code = countryData.Code;

                _context.SaveChanges();
            }
        }

        public void removeCountry(int countryId)
        {
            var country = _context.userCountry.ToList();
            var countryDel = country[countryId];

            if (countryDel != null)
            {
                _context.Remove(countryDel);
                _context.SaveChanges();
            }
        }
    }
}
