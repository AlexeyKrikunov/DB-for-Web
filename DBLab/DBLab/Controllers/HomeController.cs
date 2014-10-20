using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using NHibernate;
using Npgsql;
using DBLab.Models;

namespace DBLab.Controllers
{
   // static List<String> temp2;
    /*
     * Акканунт
     * Логин: Менеджер
     * Пароль: 12345678
     */

    /*
     * Расшифровка массива temp
     * temp[0] - страна
     * temp[1] - город направления
     * temp[2] - тип тура
     * temp[3] - город отправления
     * temp[4] - тип тарифа
     * temp[5] - продолжительность от
     * temp[6] - продолжительность до
     * temp[7] - тип питания
     * temp[8] - тип комнаты
     * temp[9] - категория отеля
     * temp[10] - название отеля
     * temp[11] - дата начала тура c (число)
     * temp[12] - дата начала тура c (месяц)
     * temp[13] - дата начала тура с (год)
     * temp[14] - дата начала тура по (число)
     * temp[15] - дата начала тура по (месяц)
     * temp[16] - дата начала тура по (год)
     * temp[17] - количество туристов
     */

    public class HomeController : Controller
    {
        static String CountryName;
        static String CityName;
        static String TypeName;

        static int idCountry;
        static int idCity;
        static int idType;
        static int idCityOrigin;
        static int idTariff;
        static int idDurationS;
        static int idDurationE;
        static int idFood;
        static int idRoom;
        static int idCategory;
        static int idHotel;
        static List<String> temp;
        static List<String> tempHotel;
        static bool ch = false;

        public ActionResult Index(int Country = -1, int City = -1, int Type = -1, int CityOrigin = -1, int Tariff = -1, 
                                    int DurationS = -1, int DurationE = -1, int Food = -1, int Room = -1, int Category = -1, 
                                    int hotel = -1, String CostS = "-1", String CostE = "-1", int Day1 = -1, int Month1 = -1, int Year1 = -1,
                                                                             int Day2 = -1, int Month2 = -1, int Year2 = -1, int Tourist = -1)
        {
            Session["CheckApplication"] = false;

            String temporary;
            List<String> temp2 = new List<String>();
            List<SelectListItem> app_temp;
               //     List<String> temp = new List<String>();
            temp = new List<String>();
            TouristService tService = getTouristService();
            CountryService cService = getCountryService();

            if (Request.IsAuthenticated)
            {
                ApplicationService aService = getApplicationService();
                ViewBag.MaxIdApplication = aService.MaxId();
            }

            List<SelectListItem> defaultCont = new List<SelectListItem>();
            List<SelectListItem> days = new List<SelectListItem>();
            List<SelectListItem> monthes = new List<SelectListItem>();
            List<SelectListItem> years = new List<SelectListItem>();
            List<SelectListItem> tourists = new List<SelectListItem>();
            defaultCont.Add(new SelectListItem { Text = "-", Value = "-1" });

            List<Tourist> tourist = new List<Tourist>();

            for(int i = 1; i <= 5; i++)
                tourist.Add(tService.GetTourist("Tourist", i));

            for (int i = 0; i < 31; i++)
            {
                days.Add(new SelectListItem{Text = (i + 1).ToString(), Value = i.ToString()});
            }

            for (int i = 0; i < 12; i++)
            {
                monthes.Add(new SelectListItem{Text = (i + 1).ToString(), Value = i.ToString()});
            }

            int t = 2014;
            for (int i = 0; i < 3; i++)
            {
                years.Add(new SelectListItem{Text = t.ToString(), Value = i.ToString()});
                t += 1;
            }

            for (int i = 0; i < 10; i++)
            {
                tourists.Add(new SelectListItem { Text = (i + 1).ToString(), Value = i.ToString() });
            }

            ViewBag.Tourists = tourists;
            ViewBag.Days = days;
            ViewBag.Monthes = monthes;
            ViewBag.Years = years;
            ViewBag.Result = Country;
            ViewBag.Tourist = tourist;
            ViewBag.Countries = getContentCountries();
            ViewBag.Cities = defaultCont;
            ViewBag.Types = defaultCont;

            if (ch == true)
            {
            }

            
            if (Country == 1 && Type == 1)
            {
                ch = true;
            }

      /*      if (Session["CheckApplication"] != null && (bool) Session["CheckApplication"])
            {
                temp2 = (List<String>)Session["ApplicationTemporary"];
                app_temp = getContentCountries();

                for (int i = 0; i < app_temp.Count; i++)
                {
                    if (app_temp[i].Text.Trim() == temp2[0])
                    {
                        idCountry = Convert.ToInt32(app_temp[i].Value);
                    }
                }

            }*/

           if (Country != -1)
            {
                temporary = ViewBag.Countries[Country + 1].Text.Trim();
                temp.Add(ViewBag.Countries[Country + 1].Text.Trim());
                //temp = temp.Trim();
                ViewBag.Text = ViewBag.Countries[Country + 1].Text.Trim();
                ViewBag.Cities = getContentCities(ViewBag.Text);
                if (ViewBag.Cities.Count != 0 && idCountry != Country)
                {
                    City = 0;
                    idCountry = Country;
                    idCity = -1;
                }
                else if(ViewBag.Cities.Count == 0)
                {
                    City = -1;
                    idCity = -1;
                    idCountry = Country;
                    //idCountry = -1;
                    //Type = -1;
                }

            /*    if (ViewBag.Cities.Count != 0)
                {
                    ViewBag.Types = getContentTypes(ViewBag.Text, ViewBag.Cities[0].Text.Trim());
                }
                else
                    ViewBag.Cities = defaultCont;

                if (ViewBag.Types.Count != 0)
                {
                }
                else
                    ViewBag.Types = defaultCont;*/
                //ViewBag.Types = defaultCont;

            }
            else if (Country == -1)
            {
                ViewBag.Text = "test";
                //    defaultCont.Clear();
                //    defaultCont.Add(new SelectListItem { Text = "-", Value = "0" });
              //  ViewBag.Country = null;
              //  ViewBag.Cities = defaultCont;
              //  ViewBag.Types = defaultCont;
               // CountryName = null;
                idCountry = -1;
                City = -1;
            }

            if (City != -1)
            {
                temp.Add(ViewBag.Cities[City].Text.Trim());
                //temp[1] = temp[1].Trim();
                ViewBag.Types = getContentTypes(temp[0], temp[1]);
                if (ViewBag.Types.Count != 0 && idCity != City)
                {
                    Type = 0;
                    idCity = City;
                    idType = -1;
                }
                else if(ViewBag.Types.Count == 0)
                {
                    Type = -1;
                    idType = -1;
                    idCity = City;
                }

            }
            else if (City == -1)
            {
                //ViewBag.Types = defaultCont;
                ViewBag.Cities = defaultCont;
                //CityName = null;
                idCity = -1;
                Type = -1;
            }
 
            if (Type != -1)
            {
                temp.Add(ViewBag.Types[Type].Text.Trim());
                ViewBag.CitiesOrigin = getContentCitiesOrigin(temp[0], temp[1], temp[2]);
                if (ViewBag.CitiesOrigin.Count != 0 && idType != Type)
                {
                    CityOrigin = 0;
                    idType = Type;
                    idCityOrigin = -1;
                }
                else if (ViewBag.CitiesOrigin.Count == 0)
                {
                    CityOrigin = -1;
                    idCityOrigin = -1;
                    idType = Type;
                }

            }
            else if (Type == -1)
            {
                ViewBag.Types = defaultCont;
                //TypeName = null;
                idType = -1;
                CityOrigin = -1;
            }


            if (CityOrigin != -1)
            {
                temp.Add(ViewBag.CitiesOrigin[CityOrigin].Text.Trim());
                ViewBag.Tariff = getContentTariffs(temp[0], temp[1], temp[2], temp[3]);
                if (ViewBag.Tariff.Count != 0 && idCityOrigin != CityOrigin)
                {
                    Tariff = 0;
                    idCityOrigin = CityOrigin;
                    idTariff = -1;
                }
                else if (ViewBag.Tariff.Count == 0)
                {
                    Tariff = -1;
                    idTariff = -1;
                    idCityOrigin = CityOrigin;
                }

            }
            else if (CityOrigin == -1)
            {
                ViewBag.CitiesOrigin = defaultCont;
                idCityOrigin = -1;
                Tariff = -1;
            }

            if (Tariff != -1)
            {
                temp.Add(ViewBag.Tariff[Tariff].Text.Trim());
                ViewBag.Duration = getContentDuration(temp[0], temp[1], temp[2], temp[3], temp[4]);
                if (ViewBag.Duration.Count != 0 && idTariff != Tariff)
                {
                    DurationS = 0;
                    DurationE = 0;
                    idTariff = Tariff;
                    idDurationS = -1;
                    idDurationE = -1;
                }
                else if (ViewBag.Duration.Count == 0)
                {
                    DurationS = -1;
                    DurationE = -1;
                    idDurationS = -1;
                    idDurationE = -1;
                    idTariff = Tariff;
                }
            }
            else if (Tariff == -1)
            {
                ViewBag.Tariff = defaultCont;
                idTariff = -1;
                DurationS = -1;
                DurationE = -1;
            }

            if (DurationS != -1)
            {
                temp.Add(ViewBag.Duration[DurationS].Text.Trim());
                temp.Add(ViewBag.Duration[DurationE].Text.Trim());
                ViewBag.Food = getContentFoods(temp[0], temp[1], temp[2], temp[3], temp[4]);
                if (ViewBag.Food.Count != 0 && idDurationS != DurationS)
                {
                    Food = 0;
                    idDurationS = DurationS;
                    idDurationE = DurationE;
                    idFood = -1;
                }
                else if (ViewBag.Food.Count == 0)
                {
                    Food = -1;
                    idFood = -1;
                    idDurationS = DurationS;
                    idDurationE = DurationE;
                }
            }
            else if (DurationS == -1)
            {
                ViewBag.Duration = defaultCont;
                idDurationS = -1;
                idDurationE = -1;
                Food = -1;
            }

            if (Food != -1)
            {
                temp.Add(ViewBag.Food[Food].Text.Trim());
                ViewBag.Room = getContentRooms(temp[0], temp[1], temp[2], temp[3], temp[4], temp[7]);
                if (ViewBag.Room.Count != 0 && idFood != Food)
                {
                    Room = 0;
                    idFood = Food;
                    idRoom = -1;
                }
                else if (ViewBag.Room.Count == 0)
                {
                    Room = -1;
                    idRoom = -1;
                    idFood = Food;
                }
            }
            else if (Food == -1)
            {
                ViewBag.Food = defaultCont;
                idFood = -1;
                Room = -1;
            }

            if (Room != -1)
            {
                temp.Add(ViewBag.Room[Room].Text.Trim());
                ViewBag.Category = getContentCategories(temp[0], temp[1], temp[2], temp[3], temp[4], temp[7], temp[8]);
                if (ViewBag.Category.Count != 0 && idRoom != Room)
                {
                    Category = 0;
                    idRoom = Room;
                    idCategory = -1;
                }
                else if (ViewBag.Category.Count == 0)
                {
                    Category = -1;
                    idCategory = -1;
                    idRoom = Room;
                }
            }
            else if (Room == -1)
            {
                ViewBag.Room = defaultCont;
                idRoom = -1;
                Category = -1;
            }

            if (Category != -1)
            {
                temp.Add(ViewBag.Category[Category].Text.Trim());
              //  if(Category == 0)
                //    ViewBag.Hotel = getContentHotels(temp[0], temp[1], temp[2], temp[3], temp[4], temp[7], temp[8]);
                //else
                    ViewBag.Hotel = getContentHotels(temp[0], temp[1], temp[2], temp[3], temp[4], temp[7], temp[8], temp[9]);

                if (ViewBag.Hotel.Count != 0 && idCategory != Category)
                {
                    hotel = 0;
                    idCategory = Category;
                    idHotel = -1;
                }
                else if (ViewBag.Hotel.Count == 0)
                {
                    hotel = -1;
                    idHotel = -1;
                    idCategory = Category;
                }
            }
            else if (Category == -1)
            {
                ViewBag.Category = defaultCont;
                idCategory = -1;
                hotel = -1;
            }

            if (hotel != -1)
            {
                temp.Add(ViewBag.Hotel[hotel].Text.Trim());
                if (ViewBag.Hotel[hotel].Text.Trim() == "Любой")
                {
                    tempHotel = new List<String>();
                    for (int i = 0; i < ViewBag.Hotel.Count; i++)
                        if(ViewBag.Hotel[i].Text.Trim() != "Любой") 
                            tempHotel.Add(ViewBag.Hotel[i].Text.Trim());
                }
            }
            else if (hotel == -1)
            {
                ViewBag.Hotel = defaultCont;
                idHotel = -1;
            }

            if (Day1 != -1)
            {
                temp.Add(ViewBag.Days[Day1].Text.Trim());
            }

            if (Month1 != -1)
            {
                temp.Add(ViewBag.Monthes[Month1].Text.Trim());
            }

            if (Year1 != -1)
            {
                temp.Add(ViewBag.Years[Year1].Text.Trim());
            }

            if (Day2 != -1)
            {
                temp.Add(ViewBag.Days[Day2].Text.Trim());
            }

            if (Month2 != -1)
            {
                temp.Add(ViewBag.Monthes[Month2].Text.Trim());
            }

            if (Year2 != -1)
            {
                temp.Add(ViewBag.Years[Year2].Text.Trim());
            }

            if (Tourist != -1)
            {
                temp.Add(ViewBag.Tourists[Tourist].Text.Trim());
            }

        //    if (submit == true)
        //    {
       //     }


           /* else
            {
                ViewBag.Text = "test";
            //    defaultCont.Clear();
            //    defaultCont.Add(new SelectListItem { Text = "-", Value = "0" });
                ViewBag.Cities = defaultCont;
                ViewBag.Types = defaultCont;
            }*/


            DropDownList DropList = new DropDownList();
            DropList.Items.Clear();
            for (int i = 0; i < 5; i++)
            {
                DropList.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }


          //  if (Session["CheckApplication"] != null && (bool) Session["CheckApplication"])
               // return RedirectToAction("Search2", "Home");
           // else
          //  {
               // Session["CheckApplication"] = false;
                return View();
          //  }
        }

        public ActionResult Country(int country = -1)
        {
            CountryService cService = getCountryService();
            ViewBag.Countries2 = getContentCountries();
            return RedirectToAction("Index", "Home");
        }

       // [AcceptVerbs(HttpVerbs.Post)]
        public void CategoryChosen(int Country)
        {
            ViewBag.Result = Country;
           // return View("Index");
            //return RedirectToAction("Index", "Home");
            
        }


        public ActionResult Search2(int CostS = -1, int CostE = -1)
        {
            if (Request.IsAuthenticated)
            {
                ApplicationService aService = getApplicationService();
                ViewBag.MaxIdApplication = aService.MaxId();
            }
            if ((bool) Session["CheckApplication"])
            {
                temp = (List<String>) Session["ApplicationTemporary"];
                if (temp[10] == "Любой")
                {
                    tempHotel = new List<String>();
                    List<SelectListItem> h = getContentHotels(temp[0], temp[1], temp[2], temp[3], temp[4], temp[7],
                        temp[8], temp[9]);
                    for (int i = 0; i < h.Count; i++)
                    {
                        if(h[i].Text.Trim() != "Любой")
                            tempHotel.Add(h[i].Text.Trim());
                    }
                }
            }
            List<Hotel> hotels = getResultSearch(temp[10]);
         //   List<String> name = new List<String>();
       //     List<int> category = new List<int>();
       ////     List<String> typeR = new List<String>();
      //      List<String> typeE = new List<String>();
       //     List<String> RateT = new List<String>();
      //      List<int> cost = new List<int>();
            List<Search> result = new List<Search>();
            Search temporary;
         //   int num = 0;
            DateTime dateS = new DateTime(Convert.ToInt32(temp[13]), Convert.ToInt32(temp[12]), Convert.ToInt32(temp[11]));
            DateTime dateE = new DateTime(Convert.ToInt32(temp[16]), Convert.ToInt32(temp[15]), Convert.ToInt32(temp[14]));

            for (int i = 0; i < hotels.Count; i++)
            {

                if ((temp[9] != "Любая" && hotels[i].category == Convert.ToInt32(temp[9])) || temp[9] == "Любая")
                {
                    for (int j = 0; j < hotels[i].places.Count; j++)
                    {
                        if (hotels[i].places[j].typeRoom.Trim() == temp[8] &&
                            hotels[i].places[j].typeEat.Trim() == temp[7] &&
                            hotels[i].places[j].places >= Convert.ToInt32(temp[17]) &&
                            hotels[i].places[j].date >= dateS &&
                            hotels[i].places[j].date <= dateE)
                        {
                            if (hotels[i].places[j].tour.rateType.Trim() == temp[4] &&
                                hotels[i].places[j].tour.type.Trim() == temp[2])
                            {
                                for (int k = 0; k < hotels[i].places[j].tour.transports.Count; k++)
                                {
                                    if (hotels[i].places[j].tour.transports[k].cityOrigin.Trim() == temp[3] && 
                                                  hotels[i].places[j].tour.transports[k].seats >= Convert.ToInt32(temp[17]))
                                    {
                                        if (hotels[i].places[j].tour.country.region.Trim() == temp[1] &&
                                            hotels[i].places[j].tour.country.nameCountry.Trim() == temp[0])
                                        {
                                            for (int p = Convert.ToInt32(temp[5]); p <= (Convert.ToInt32(temp[6]) - Convert.ToInt32(temp[5]) + 1); p++)
                                            {
                                                if (hotels[i].places[j].tour.numberNights >= p)
                                                {
                                                    temporary = new Search();
                                                    temporary.name = hotels[i].name;
                                                    temporary.category = hotels[i].category;
                                                    temporary.eat = hotels[i].places[j].typeEat;
                                                    temporary.room = hotels[i].places[j].typeRoom;
                                                    temporary.type = hotels[i].places[j].tour.type;
                                                    temporary.tariff = hotels[i].places[j].tour.rateType;
                                                    temporary.HealthInsurance = hotels[i].places[j].tour.healthInsurance;
                                                    temporary.country = hotels[i].places[j].tour.country.nameCountry;
                                                    temporary.city = hotels[i].places[j].tour.country.region;
                                                    temporary.Class = hotels[i].places[j].tour.transports[k].Class;
                                                    temporary.NameTransportCompany = hotels[i].places[j].tour.transports[k].transport.name;
                                                    temporary.TypeTransportCompany = hotels[i].places[j].tour.transports[k].transport.typeTransport;
                                                    temporary.start = hotels[i].places[j].date;
                                                    temporary.end = hotels[i].places[j].date.AddDays(p);
                                                    temporary.numberNights = p;
                                                    temporary.numberTourists = Convert.ToInt32(temp[17]);
                                                    temporary.tour = hotels[i].places[j].tour;
                                                    temporary.nameGuide = hotels[i].places[j].tour.groupTourists[0].nameGuide;
                                                    temporary.surnameGuide = hotels[i].places[j].tour.groupTourists[0].surnameGuide;
                                                    temporary.patronymicGuide = hotels[i].places[j].tour.groupTourists[0].patronymicGuide;
                                                    temporary.placesHotel = hotels[i].places[j];
                                                    temporary.seatsTransport = hotels[i].places[j].tour.transports[k];
                                                    temporary.cost = p*hotels[i].places[j].roomPrice + hotels[i].places[j].tour.tourPrice + hotels[i].places[j].tour.transports[k].tiketPrice;

                                                    //result.Add(temporary);
                                                    if (CostS != -1 && temporary.cost >= CostS)
                                                    {
                                                        if (CostE != -1 && temporary.cost <= CostE)
                                                        {
                                                            result.Add(temporary);
                                                        }
                                                        else if(CostE == -1)
                                                        {
                                                            result.Add(temporary);
                                                        }
                                                    }
                                                    else if (CostE != -1 && temporary.cost <= CostE)
                                                    {
                                                        result.Add(temporary);
                                                    }
                                                    else if (CostS == -1 && CostE == -1)
                                                    {
                                                        result.Add(temporary);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                  //  name.Add(hotels[i].name);
                  //  category.Add(hotels[i].category);
                }
     //           else if (temp[9] == "любая")
       //         {
     ///               name.Add(hotels[i].name);
        //            category.Add(hotels[i].category);
         //       }


            }

            Session["result"] = result;
            Session["app"] = false;
            Session["temporary"] = temp;
            if (CostS != -1)
            {
                if (CostE != -1)
                {
                }
                else
                {
                }
            }
            else if (CostE != -1)
            {
            }
           // DBService dbs = new DBService();
          //  List<String> ans = (List<String>)dbs.Search(temp[0], temp[1], temp[2], temp[3], temp[4], temp[7], temp[8], -1, "-1");
            ViewBag.Result = result;


            if ((bool) Session["CheckApplication"])
                return RedirectToAction("Index", "Tour", new{ button = (int)Session["idSearch"] });

            else
                return View();
            //return About();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Страница описания приложения.";
           // ViewBag.Countries = getContentCountries();
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Страница контактов.";

            return View();
        }


        private CountryService getCountryService()
        {
            CountryService cService = new CountryService();
            cService.SetSession(ApplicationCore.Instance.SessionFactory.OpenSession());

            return cService;
        }

        private TouristService getTouristService()
        {
            TouristService tService = new TouristService();
            tService.SetSession(ApplicationCore.Instance.SessionFactory.OpenSession());

            return tService;
        }

        private TourService getTourService()
        {
            TourService trService = new TourService();
            trService.SetSession(ApplicationCore.Instance.SessionFactory.OpenSession());

            return trService;
        }

        private SeatsTransportService getSeatsTransportService()
        {
            SeatsTransportService stService = new SeatsTransportService();
            stService.SetSession(ApplicationCore.Instance.SessionFactory.OpenSession());

            return stService;
        }

        private PlacesHotelService getPlacesHotelService()
        {
            PlacesHotelService phService = new PlacesHotelService();
            phService.SetSession(ApplicationCore.Instance.SessionFactory.OpenSession());

            return phService;
        }

        private HotelService getHotelService()
        {
            HotelService hService = new HotelService();
            hService.SetSession(ApplicationCore.Instance.SessionFactory.OpenSession());

            return hService;
        }

        private ApplicationService getApplicationService()
        {
            ApplicationService aService = new ApplicationService();
            aService.SetSession(ApplicationCore.Instance.SessionFactory.OpenSession());

            return aService;
        }




        private List<SelectListItem> getContentCountries()
        {
            CountryService cService = getCountryService();
            List<SelectListItem> countries = new List<SelectListItem>();
            List<String> country = cService.GetCountries();

            for (int i = 0; i < country.Count; i++)
            {
                if (i == 0)
                    countries.Add(new SelectListItem { Text = "-", Value = "-1" });
                countries.Add(new SelectListItem { Text = country[i], Value = i.ToString() });
            }

            return countries;
        }

        private List<SelectListItem> getContentCities(String country)
        {
            CountryService cService = getCountryService();
            List<SelectListItem> cities = new List<SelectListItem>();
            List<String> city = cService.GetCities(country);

            for (int i = 0; i < city.Count; i++)
                cities.Add(new SelectListItem { Text = city[i], Value = i.ToString() });

            return cities;
        }

        private List<SelectListItem> getContentTypes(String country, String city)
        {
            TourService trService = getTourService();
            List<SelectListItem> types = new List<SelectListItem>();
            List<String> type = trService.GetTypes(country, city);

            for (int i = 0; i < type.Count; i++)
                types.Add(new SelectListItem { Text = type[i], Value = i.ToString() });

            return types;
        }

        private List<SelectListItem> getContentCitiesOrigin(String country, String city, String type)
        {
            SeatsTransportService stService = getSeatsTransportService();
            List<SelectListItem> citiesOrigin = new List<SelectListItem>();
            List<String> cityOrigin = stService.GetCitiesOrigin(country, city, type);

            for (int i = 0; i < cityOrigin.Count; i++)
                citiesOrigin.Add(new SelectListItem { Text = cityOrigin[i], Value = i.ToString() });

            return citiesOrigin;
        }

        private List<SelectListItem> getContentTariffs(String country, String city, String type, String cityOrigin)
        {
            TourService trService = getTourService();
            List<SelectListItem> tariffs = new List<SelectListItem>();
            List<String> tariff = trService.GetTariffs(country, city, type, cityOrigin);

            for (int i = 0; i < tariff.Count; i++)
                tariffs.Add(new SelectListItem { Text = tariff[i], Value = i.ToString() });

            return tariffs;
        }

        private List<SelectListItem> getContentDuration(String country, String city, String type, String cityOrigin, String tariff)
        {
            TourService trService = getTourService();
            List<SelectListItem> duration = new List<SelectListItem>();
            int number = trService.GetDuration(country, city, type, cityOrigin, tariff);

            for (int i = 0; i < number; i++)
            {
                if(i == number - 1)
                    duration.Add(new SelectListItem { Text = number.ToString(), Value = i.ToString() });
                else
                    duration.Add(new SelectListItem { Text = (i + 1).ToString(), Value = i.ToString() });
            }

            return duration;
        }

        private List<SelectListItem> getContentFoods(String country, String city, String type, String cityOrigin, String tariff)
        {
            PlacesHotelService phService = getPlacesHotelService();
            List<SelectListItem> foods = new List<SelectListItem>();
            List<String> food = phService.GetFoods(country, city, type, cityOrigin, tariff);

            for (int i = 0; i < food.Count; i++)
                foods.Add(new SelectListItem { Text = food[i], Value = i.ToString() });

            return foods;
        }

        private List<SelectListItem> getContentRooms(String country, String city, String type, String cityOrigin, String tariff, String food)
        {
            PlacesHotelService phService = getPlacesHotelService();
            List<SelectListItem> rooms = new List<SelectListItem>();
            List<String> room = phService.GetRooms(country, city, type, cityOrigin, tariff, food);

            for (int i = 0; i < room.Count; i++)
                rooms.Add(new SelectListItem { Text = room[i], Value = i.ToString() });

            return rooms;
        }

        private List<SelectListItem> getContentCategories(String country, String city, String type, 
                                                            String cityOrigin, String tariff, String food, String room)
        {
            HotelService hService = getHotelService();
            List<SelectListItem> categories = new List<SelectListItem>();
            List<int> category = hService.GetCategories(country, city, type, cityOrigin, tariff, food, room);

            for (int i = 0; i < category.Count + 1; i++)
            {
                if(i == 0)
                    categories.Add(new SelectListItem { Text = "Любая", Value = i.ToString() });
                else
                    categories.Add(new SelectListItem { Text = category[i - 1].ToString(), Value = i.ToString() });
            }

            return categories;
        }

        private List<SelectListItem> getContentHotels(String country, String city, String type,
                                                           String cityOrigin, String tariff, String food, String room, String category)
        {
            HotelService hService = getHotelService();
            List<SelectListItem> hotels = new List<SelectListItem>();
            List<String> hotel;
            if(category  == "Любая")
                hotel = hService.GetHotels(country, city, type, cityOrigin, tariff, food, room);
            else
                hotel = hService.GetHotels(country, city, type, cityOrigin, tariff, food, room, Convert.ToInt32(category));

            for (int i = 0; i < hotel.Count + 1; i++)
            {
                if (i == 0)
                    hotels.Add(new SelectListItem { Text = "Любой", Value = i.ToString() });
                else
                    hotels.Add(new SelectListItem { Text = hotel[i - 1], Value = i.ToString() });
            }

            return hotels;
        }

        private List<Hotel> getResultSearch(String hotel)
        {
            HotelService hService = getHotelService();
            //List<SelectListItem> hotels = new List<SelectListItem>();
            List<Hotel> hotels = new List<Hotel>();
            if (hotel == "Любой")
                for (int i = 0; i < tempHotel.Count; i++)
                    hotels.Add(hService.ResultSearch(tempHotel[i]));
            else
                hotels.Add(hService.ResultSearch(hotel));

            return hotels;
        }

    }
}
