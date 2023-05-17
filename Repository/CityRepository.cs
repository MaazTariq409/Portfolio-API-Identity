using AutoMapper;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Portfolio_API.Data;
using Portfolio_API.DTOs;
using Portfolio_API.Models;
using Portfolio_API.Repository.Repository_Interface;

namespace Portfolio_API.Repository
{
    public class CityRepository : ICity
    {
        private readonly PorfolioContext _context;
        public CityRepository(PorfolioContext context)
        {
            _context = context;

        }

        public IEnumerable<UserCity> GetCities()
        {
            return _context.userCity.ToList();
        }

        public void AddCity(UserCity city)
        {
            _context.userCity.Add(city);
            _context.SaveChanges();
        }

        public void updateCity(int cityId, UserCity cityData)
        {
            var city = _context.userCity.ToList();
            var cityUpdate = city[cityId];

            if (cityUpdate != null)
            {
                cityUpdate.Name = cityData.Name;
                cityUpdate.Region = cityData.Region;
                cityUpdate.ZipCode = cityData.ZipCode;

                _context.SaveChanges();
            }

        }
        public void removeCity(int cityId)
        {
            var city = _context.userCity.ToList();
            var delcity = city[cityId];

            if (delcity != null)
            {
                _context.Remove(delcity);
                _context.SaveChanges();
            }
        }
    }

}

