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
    public class TourController : Controller
    {
        //
        // GET: /Tour/
        static Search card;

        public ActionResult Index(int button)
        {
            if (Request.IsAuthenticated)
            {
                ApplicationService aService = getApplicationService();
                ViewBag.MaxIdApplication = aService.MaxId();
            }

            List<Search> result = (List<Search>)Session["result"];

            if (result.Count > 0)
            {
                ViewBag.EmptyTour = false;
                ViewBag.Card = result[button];
                ViewBag.Check = 0;
                card = result[button];
                ViewBag.App = Session["app"];
                ViewBag.ChApp = Session["CheckApplication"];
                Session["id"] = button;
                //  Session["card"] = card;

                if ((bool) Session["CheckApplication"])
                {
                    ViewBag.appData = Session["appData"];
                }
            }
            else ViewBag.EmptyTour = true;

            return View();
        }

        public ActionResult AddTourists(String name, String surname, String patronymic, 
            String phoneNumber, String city, String passportData, String address, int button)
        {

            if (Request.IsAuthenticated)
            {
                ApplicationService aService = getApplicationService();
                ViewBag.MaxIdApplication = aService.MaxId();
            }


            ViewBag.EmptyTour = false;
            ViewBag.ChApp = Session["CheckApplication"];
            ViewBag.App = Session["app"];

            if ((bool)Session["CheckApplication"])
            {
                ViewBag.appData = Session["appData"];
            }

            TouristService tService = getTouristService();
            //TourService tService2 = getTourService();
            Tourist tourist = new Tourist();
           // PlacesHotel placesHotel;
            GroupTourists groupTourists = new GroupTourists();

            tourist.name = name;
            tourist.surname = surname;
            tourist.patronymic = patronymic;
            tourist.phoneNumber = phoneNumber;
            tourist.city = city;
            tourist.passportData = passportData;
            tourist.address = address;

            groupTourists.nameGuide = card.nameGuide;
            groupTourists.surnameGuide = card.surnameGuide;
            groupTourists.patronymicGuide = card.patronymicGuide;
            groupTourists.tour = card.tour;
           // placesHotel = card.placesHotel;
            
            tService.AddNewTourist(tourist, groupTourists, card.placesHotel, card.seatsTransport);
           // groupTourists.tour.numberNights = groupTourists.tour.numberNights - 1;
            //tService2.UpdateTour(groupTourists.tour);

            ViewBag.Card = card;
            ViewBag.Check = button;
           // return RedirectToAction("Index", "Tour");
          //  if ((bool) Session["CheckApplication"])
        //    {
               // Session["DeleteApplication"] = true;
      //          int id = (int)Session["idApp"];
    //            ApplicationService aService = getApplicationService();
  //              List<Application> app = (List<Application>)Session["Application"];

//                aService.DeliteApplication(app[id]);

             //   return RedirectToAction("Index", "Application");
           // }
         //   else
                return View("Index");
        }

        public ActionResult DeleteCard()
        {
            int id = (int) Session["idApp"];
            ApplicationService aService = getApplicationService();
            List<Application> app = (List<Application>) Session["Application"];

            aService.DeliteApplication(app[id]);

            return RedirectToAction("Index", "Application");
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

        private ApplicationService getApplicationService()
        {
            ApplicationService aService = new ApplicationService();
            aService.SetSession(ApplicationCore.Instance.SessionFactory.OpenSession());

            return aService;
        }
    }
}
