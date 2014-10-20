using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBLab.Models
{
    public class SeatsTransportService : DBService
    {
        public void AddNewSeatsTransport(SeatsTransport seats_transport)
        {
            Add(seats_transport);
        }

        public List<String> GetCitiesOrigin(String Country, String City, String Type)
        {
            List<String> res = (List<String>)CitiesOrigin(Country, City, Type);
            return res;
        }
    }
}