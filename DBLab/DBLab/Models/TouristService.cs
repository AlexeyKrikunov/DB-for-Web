using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBLab.Models
{
    public class TouristService : DBService
    {
        public void AddNewTourist(Tourist tourist, GroupTourists groupTourists, PlacesHotel placesHotel, SeatsTransport seatsTransport)
        {
            int maxIdT = MaxIdTourist();
            int maxIdGT = MaxIdGroupTourists();
            tourist.id = maxIdT + 1;
            groupTourists.id = maxIdGT + 1;
            groupTourists.tourist = tourist;

            placesHotel.places = placesHotel.places - 1;
            seatsTransport.seats = seatsTransport.seats - 1;

            Add(tourist);
            Add(groupTourists);
            Update(placesHotel);
            Update(seatsTransport);
            //Delete(tourist);
            //Delete(groupTourists);
        }

        public Tourist GetTourist(String tourist, int i)
        {
            return (Tourist)Get(tourist, i);
        }
    }
}