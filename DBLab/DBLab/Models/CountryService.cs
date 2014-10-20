using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DBLab.Models
{
    public class CountryService : DBService
    {
        public void AddNewCountry(Country country)
        {
            Add(country);
        }

        public List<String> GetCountries()
        {
            List<String> ans = (List<String>)Countries();
            return ans;
        }

        public List<String> GetCities(String country)
        {
            List<String> ans = (List<String>)Cities(country);
            return ans;
        }
    }
}