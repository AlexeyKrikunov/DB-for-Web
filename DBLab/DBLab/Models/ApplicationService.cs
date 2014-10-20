using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DBLab.Models
{
    public class ApplicationService : DBService
    {
        public void AddApplication(Application app)
        {
            app.id = MaxIdApplication() + 1;
            Add(app);
        }

        public List<Application> GetApp()
        {
            List<Application> ans = (List<Application>) GetApplications();

            return ans;
        }

        public int MaxId()
        {
            return MaxIdApplication();
        }

        public void DeliteApplication(Application app)
        {
            Delete(app);
        }
    }
}