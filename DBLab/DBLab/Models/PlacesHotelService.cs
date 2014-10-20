using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBLab.Models
{
    public class PlacesHotelService : DBService
    {
        public void AddNewPlacesHotel(PlacesHotel places_hotel)
        {
            Add(places_hotel);
        }

        public List<String> GetFoods(String Country, String City, String Type, String CityOrigin, String Tariff)
        {
            List<String> res = (List<String>)Foods(Country, City, Type, CityOrigin, Tariff);
            return res;
        }

        public List<String> GetRooms(String Country, String City, String Type, String CityOrigin, String Tariff, String Food)
        {
            List<String> res = (List<String>)Rooms(Country, City, Type, CityOrigin, Tariff, Food);
            return res;
        }
    }
}