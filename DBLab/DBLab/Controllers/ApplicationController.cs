using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHibernate;
using  Npgsql;
using DBLab.Models;

namespace DBLab.Controllers
{
    public class ApplicationController : Controller
    {
        //
        // GET: /Application/

        public ActionResult Index()
        {
            ApplicationService aService = getApplicationService();

            ViewBag.App = Session["App"];
            ViewBag.id = Session["id"];
            ViewBag.MaxIdApplication = aService.MaxId();

            if (ViewBag.MaxIdApplication > 0)
            {
                ViewBag.Application = aService.GetApp();
                Session["Application"] = ViewBag.Application;
            }

            return View();
        }

        public ActionResult AddApplication(String name, String surname, String patronymic,
            String phoneNumber, String address)
        {
           // List<String> temp = Session["temporary"];
            ApplicationService aService = getApplicationService();

            ViewBag.MaxIdApplication = aService.MaxId();

            Application application = new Application();
         //   Search card = (Search)Session["card"];
            List<String> temp = (List<String>) Session["temporary"];
            int id = (int)Session["id"];

           // application.id = 2;
            application.idCountry = temp[0];
            application.idCityArrive = temp[1];
            application.idTourType = temp[2];
            application.idCityDeparture = temp[3];
            application.idTypeTariff = temp[4];
            application.idFrom = temp[5];
            application.idTo = temp[6];
            application.idTypeFood = temp[7];
            application.idTypeRoom = temp[8];
            application.idTypeHotel = temp[9];
            application.idHotel = temp[10];
            application.idStartDay = temp[11];
            application.idStartMonth = temp[12];
            application.idStartYear = temp[13];
            application.idStopDay = temp[14];
            application.idStopMonth = temp[15];
            application.idStopYear = temp[16];
            application.idNumberTourists = temp[17];
            application.Name = name;
            application.Surname = surname;
            application.Patronymic = patronymic;
            application.PhoneNumber = phoneNumber;
            application.Email = address;
            application.idSearch = id;

            aService.AddApplication(application);
            ViewBag.App = true;
            Session["App"] = true;
            ViewBag.id = Session["id"];
            return View("Index");
            //return RedirectToAction("Index", "Tour");
        }

        public ActionResult ViewApplication(int button)
        {
            List<String> temp = new List<String>();
            List<String> appData = new List<String>();
            List<Application> application;

            application = (List<Application>) Session["Application"];

            temp.Add(application[button].idCountry);
            temp.Add(application[button].idCityArrive);
            temp.Add(application[button].idTourType);
            temp.Add(application[button].idCityDeparture);
            temp.Add(application[button].idTypeTariff);
            temp.Add(application[button].idFrom);
            temp.Add(application[button].idTo);
            temp.Add(application[button].idTypeFood);
            temp.Add(application[button].idTypeRoom);
            temp.Add(application[button].idTypeHotel);
            temp.Add(application[button].idHotel);
            temp.Add(application[button].idStartDay);
            temp.Add(application[button].idStartMonth);
            temp.Add(application[button].idStartYear);
            temp.Add(application[button].idStopDay);
            temp.Add(application[button].idStopMonth);
            temp.Add(application[button].idStopYear);
            temp.Add(application[button].idNumberTourists);

            appData.Add(application[button].Name);
            appData.Add(application[button].Surname);
            appData.Add(application[button].Patronymic);
            appData.Add(application[button].PhoneNumber);
            appData.Add(application[button].Email);

            Session["ApplicationTemporary"] = temp;
            Session["idSearch"] = application[button].idSearch;
            Session["CheckApplication"] = true;
            Session["appData"] = appData;
            Session["idApp"] = button;

            //return View("Index");
            return RedirectToAction("Search2", "Home");
        }

        private ApplicationService getApplicationService()
        {
            ApplicationService aService = new ApplicationService();
            aService.SetSession(ApplicationCore.Instance.SessionFactory.OpenSession());

            return aService;
        }

    }
}
