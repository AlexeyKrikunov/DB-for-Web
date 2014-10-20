using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;

namespace DBLab.Models
{
    public class DBService
    {
        private ISession _mSession;

        public void SetSession(ISession session)
        {
            _mSession = session;
        }

        public void Add(Object obj)
        {
            using (var transaction = _mSession.BeginTransaction())
            {
                _mSession.Save(obj);
                transaction.Commit();
            }
        }

        public void Delete(Object obj)
        {
            using (var transaction = _mSession.BeginTransaction())
            {
                _mSession.Delete(obj);
                transaction.Commit();
            }
        }

        public void Update(Object obj)
        {
            using (var transaction = _mSession.BeginTransaction())
            {
                //_mSession.Clear();
                _mSession.Update(obj);             
                transaction.Commit();
            }
        }

        public Object Get(String obj, int i)
        {
            Object ans = null;

            _mSession.Clear();
            IList<String> c = _mSession.QueryOver<Tourist>().SelectList(t => t.SelectGroup(x => x.city)).OrderBy(y => y.city).Asc.List<String>();
            if(obj == "Tourist")
                ans = _mSession.Get<Tourist>(i);
            if (obj == "GroupTourists")
                ans = _mSession.Get<GroupTourists>(i);
            if (obj == "Tour")
                ans = _mSession.Get<Tour>(i);
            if (obj == "Country")
                ans = _mSession.Get<Country>(i);
            if (obj == "PlacesHotel")
                ans = _mSession.Get<PlacesHotel>(i);
            if (obj == "Hotel")
                ans = _mSession.Get<Hotel>(i);
            if (obj == "SeatsTransport")
                ans = _mSession.Get<SeatsTransport>(i);
            if (obj == "Transport")
                ans = _mSession.Get<Transport>(i);
            
            return ans;
        }

        public int MaxIdTourist()
        {
            _mSession.Clear();
            IList<int> ans = _mSession.QueryOver<Tourist>().SelectList(t => t.SelectMax(x => x.id)).List<int>();
            return ans[0];
        }

        public int MaxIdGroupTourists()
        {
            _mSession.Clear();
            IList<int> ans = _mSession.QueryOver<GroupTourists>().SelectList(t => t.SelectMax(x => x.id)).List<int>();
            return ans[0];
        }

        public int MaxIdApplication()
        {
            _mSession.Clear();
        //    IList<Application> ans2 = _mSession.QueryOver<Application>().List<Application>();
            IList<int> ans1 = _mSession.QueryOver<Application>().Select(t => t.id).List<int>();
            if (ans1.Count > 0)
            {
                IList<int> ans = _mSession.QueryOver<Application>().SelectList(t => t.SelectMax(x => x.id)).List<int>();
                return ans[0];
            }
            else return 0;
        }


        public IList<String> Countries()
        {
            _mSession.Clear();
            IList<String> ans = _mSession.QueryOver<Country>().SelectList(t => t.SelectGroup(x => x.nameCountry)).OrderBy(y => y.nameCountry).Asc.List<String>();

            return ans;
        }

        public IList<String> Cities(String country)
        {
            _mSession.Clear();
            //IList<String> ans = _mSession.QueryOver<Country>().SelectList(t => t.SelectGroup(x => x.region)).Where(y => y.nameCountry == country).OrderBy(z => z.region).Asc.List<String>();
            IList<String> ans = _mSession.QueryOver<Country>().Where(y => y.nameCountry == country).SelectList(z => z.SelectGroup(x => x.region)).OrderBy(z => z.region).Asc.List<String>();

            return ans;
        }
        
        public IList<String> Types(String country, String city)
        {
            _mSession.Clear();
            IList<String> ans = _mSession.QueryOver<Tour>().SelectList(t => t.SelectGroup(x => x.type)).OrderBy(t => t.type).Asc.
                JoinQueryOver<Country>(t => t.country).Where(x => x.nameCountry == country).And(x => x.region == city).List<String>();
            return ans;
        }

        public IList<String> CitiesOrigin(String country, String city, String type)
        {
            _mSession.Clear();
            IList<String> ans = _mSession.QueryOver<SeatsTransport>().SelectList(t => t.SelectGroup(x => x.cityOrigin)).OrderBy(t => t.cityOrigin).Asc.
                JoinQueryOver<Tour>(t => t.tour).Where(x => x.type == type).JoinQueryOver<Country>(t => t.country).Where(x => x.nameCountry == country).And(x => x.region == city).List<String>();
            return ans;
        }

        public IList<String> Tariffs(String country, String city, String type, String cityOrigin)
        {
            _mSession.Clear();
            IList<String> ans = _mSession.QueryOver<Tour>().SelectList(t => t.SelectGroup(x => x.rateType)).OrderBy(t => t.rateType).Asc.
                JoinQueryOver<Country>(t => t.country).Where(x => x.nameCountry == country).And(x => x.region == city).JoinQueryOver<Tour>(t => t.tours).JoinQueryOver<SeatsTransport>(t => t.transports).Where(x => x.cityOrigin == cityOrigin).List<String>();
            return ans;
        }

        public IList<int> Duration(String country, String city, String type, String cityOrigin, String tariff)
        {
            _mSession.Clear();
            IList<int> ans = _mSession.QueryOver<Tour>().SelectList(t => t.SelectMax(x => x.numberNights)).Where(x => x.rateType == tariff).
                JoinQueryOver<Country>(t => t.country).Where(x => x.nameCountry == country).And(x => x.region == city).JoinQueryOver<Tour>(t => t.tours).JoinQueryOver<SeatsTransport>(t => t.transports).Where(x => x.cityOrigin == cityOrigin).List<int>();
            return ans;
        }

        public IList<String> Foods(String country, String city, String type, String cityOrigin, String tariff)
        {
            _mSession.Clear();
            IList<String> ans = _mSession.QueryOver<PlacesHotel>().SelectList(t => t.SelectGroup(x => x.typeEat)).OrderBy(t => t.typeEat).Asc.
                JoinQueryOver<Tour>(t => t.tour).Where(x => x.type == type).And(x => x.rateType == tariff).JoinQueryOver<Country>(t => t.country)
                .Where(x => x.nameCountry == country).And(x => x.region == city).JoinQueryOver<Tour>(t => t.tours)
                .JoinQueryOver<SeatsTransport>(t => t.transports).Where(x => x.cityOrigin == cityOrigin).List<String>();
            return ans;
        }

        public IList<String> Rooms(String country, String city, String type, String cityOrigin, String tariff, String food)
        {
            _mSession.Clear();
            IList<String> ans = _mSession.QueryOver<PlacesHotel>().SelectList(t => t.SelectGroup(x => x.typeRoom)).Where(x => x.typeEat == food).OrderBy(t => t.typeRoom).Asc.
                JoinQueryOver<Tour>(t => t.tour).Where(x => x.type == type).And(x => x.rateType == tariff).JoinQueryOver<Country>(t => t.country)
                .Where(x => x.nameCountry == country).And(x => x.region == city).JoinQueryOver<Tour>(t => t.tours)
                .JoinQueryOver<SeatsTransport>(t => t.transports).Where(x => x.cityOrigin == cityOrigin).List<String>();
            return ans;
        }

        public IList<int> Categories(String country, String city, String type, String cityOrigin, String tariff, String food, String room)
        {
            _mSession.Clear();
          /*  IList<int> ans = _mSession.QueryOver<Hotel>().SelectList(t => t.SelectGroup(x => x.category)).OrderBy(t => t.category).Asc
                .JoinQueryOver<PlacesHotel>(t => t.places).Where(x => x.typeEat == food).And(x => x.typeRoom == room).
                JoinQueryOver<Tour>(t => t.tour).Where(x => x.type == type).And(x => x.rateType == tariff).JoinQueryOver<Country>(t => t.country)
                .Where(x => x.nameCountry == country).And(x => x.region == city).JoinQueryOver<Tour>(t => t.tours)
                .JoinQueryOver<SeatsTransport>(t => t.transports).Where(x => x.cityOrigin == cityOrigin).List<int>();*/

           IList<int> ans = _mSession.QueryOver<Hotel>().SelectList(t => t.SelectGroup(x => x.category)).OrderBy(t => t.category).Asc
                   .JoinQueryOver<PlacesHotel>(t => t.places).Where(x => x.typeEat == food).And(x => x.typeRoom == room).
                   JoinQueryOver<Tour>(t => t.tour).Where(x => x.type == type).And(x => x.rateType == tariff)
                   .JoinQueryOver<SeatsTransport>(t => t.transports).Where(x => x.cityOrigin == cityOrigin)
                   .JoinQueryOver<Tour>(t => t.tour).JoinQueryOver<Country>(t => t.country).Where(x => x.nameCountry == country).And(x => x.region == city)
                   .List<int>();

            return ans;
        }

        public IList<String> Hotels(String country, String city, String type, String cityOrigin, String tariff, String food, String room, int category)
        {
            _mSession.Clear();
            IList<String> ans;

//            if (cityOrigin == "Санкт-Петербург")
//            {

//            }
            //ans = _mSession.CreateCriteria(typeof(Hotel))
                //    .CreateCriteria("PlacesHotel")
                 //   .List<String>();

            if(category == -1)
            {
                /*ans = _mSession.QueryOver<Hotel>().SelectList(t => t.SelectGroup(x => x.name)).OrderBy(t => t.name).Asc
                    .JoinQueryOver<PlacesHotel>(t => t.places).Where(x => x.typeEat == food).And(x => x.typeRoom == room).
                    JoinQueryOver<Tour>(t => t.tour).Where(x => x.type == type).And(x => x.rateType == tariff).JoinQueryOver<Country>(t => t.country)
                    .Where(x => x.nameCountry == country).And(x => x.region == city).JoinQueryOver<Tour>(t => t.tours).Where(x => x.type == type)
                    .JoinQueryOver<SeatsTransport>(t => t.transports).Where(x => x.cityOrigin == cityOrigin).List<String>();*/


                ans = _mSession.QueryOver<Hotel>().SelectList(t => t.SelectGroup(x => x.name)).OrderBy(t => t.name).Asc
                    .JoinQueryOver<PlacesHotel>(t => t.places).Where(x => x.typeEat == food).And(x => x.typeRoom == room).
                    JoinQueryOver<Tour>(t => t.tour).Where(x => x.type == type).And(x => x.rateType == tariff)
                    .JoinQueryOver<SeatsTransport>(t => t.transports).Where(x => x.cityOrigin == cityOrigin)
                    .JoinQueryOver<Tour>(t => t.tour).JoinQueryOver<Country>(t => t.country).Where(x => x.nameCountry == country).And(x => x.region == city)
                    .List<String>();
            }

            else
            {
                /*ans = _mSession.QueryOver<Hotel>().SelectList(t => t.SelectGroup(x => x.name)).Where(x => x.category == category).OrderBy(t => t.name).Asc
                    .JoinQueryOver<PlacesHotel>(t => t.places).Where(x => x.typeEat == food).And(x => x.typeRoom == room).
                    JoinQueryOver<Tour>(t => t.tour).Where(x => x.type == type).And(x => x.rateType == tariff).JoinQueryOver<Country>(t => t.country)
                    .Where(x => x.nameCountry == country).And(x => x.region == city).JoinQueryOver<Tour>(t => t.tours)
                    .JoinQueryOver<SeatsTransport>(t => t.transports).Where(x => x.cityOrigin == cityOrigin).List<String>();*/


                ans = _mSession.QueryOver<Hotel>().SelectList(t => t.SelectGroup(x => x.name)).Where(x => x.category == category).OrderBy(t => t.name).Asc
                    .JoinQueryOver<PlacesHotel>(t => t.places).Where(x => x.typeEat == food).And(x => x.typeRoom == room).
                    JoinQueryOver<Tour>(t => t.tour).Where(x => x.type == type).And(x => x.rateType == tariff)
                    .JoinQueryOver<SeatsTransport>(t => t.transports).Where(x => x.cityOrigin == cityOrigin)
                    .JoinQueryOver<Tour>(t => t.tour).JoinQueryOver<Country>(t => t.country).Where(x => x.nameCountry == country).And(x => x.region == city)
                    .List<String>();
            }

            return ans;
        }


        public IList<Hotel> Searching(String hotel)
        {
            //_mSession.Clear();
            IList<Hotel> ans = _mSession.QueryOver<Hotel>().Where(x => x.name == hotel).List<Hotel>();

            //            if (cityOrigin == "Санкт-Петербург")
            //            {

            //            }

            
            return ans;
        }

        public IList<Application> GetApplications()
        {
            IList<Application> ans = _mSession.QueryOver<Application>().List<Application>();

            return ans;
        }

    }
}