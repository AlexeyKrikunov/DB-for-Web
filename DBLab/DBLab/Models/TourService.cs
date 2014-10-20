using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBLab.Models
{
    public class TourService : DBService
    {
        public void AddNewTour(Tour tour)
        {
            Add(tour);
        }

        public void UpdateTour(Tour tour)
        {
            Update(tour);
        }

        public List<String> GetTypes(String Country, String City)
        {
            List<String> res = (List<String>)Types(Country, City);
            return res;
        }

        public List<String> GetTariffs(String Country, String City, String Type, String CityOrigin)
        {
            List<String> res = (List<String>)Tariffs(Country, City, Type, CityOrigin);
            return res;
        }

        public int GetDuration(String Country, String City, String Type, String CityOrigin, String Tariff)
        {
            List<int> temp = (List<int>)Duration(Country, City, Type, CityOrigin, Tariff);
            int res = temp[0];
            return res;
        }
    }
}