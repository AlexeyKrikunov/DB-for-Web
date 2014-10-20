using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBLab.Models
{
    public class HotelService : DBService
    {
        public void AddNewHotel(Hotel hotel)
        {
            Add(hotel);
        }

        public List<int> GetCategories(String Country, String City, String Type, String CityOrigin, 
                                                                    String Tariff, String Food, String Room)
        {
            List<int> res = (List<int>)Categories(Country, City, Type, CityOrigin, Tariff, Food, Room);
            return res;
        }

        public List<String> GetHotels(String Country, String City, String Type, String CityOrigin,
                                                            String Tariff, String Food, String Room, int Category = -1)
        {
            List<String> res = (List<String>)Hotels(Country, City, Type, CityOrigin, Tariff, Food, Room, Category);
            return res;
        }

        public Hotel ResultSearch(String Hotel)
        {
            List<Hotel> res = (List<Hotel>)Searching(Hotel);
            return res[0];
        }
    }
}