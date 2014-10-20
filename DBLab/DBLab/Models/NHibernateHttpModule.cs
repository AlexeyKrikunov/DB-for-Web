using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using Npgsql;


namespace DBLab
{
    public class NHibernateHttpModule : IHttpModule
    {
        public void Dispose()
        {

        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(BeginEventhandler);
            context.EndRequest += new EventHandler(EndEventhandler);
        }

        private void BeginEventhandler(object o, EventArgs e)
        {
            var session = ApplicationCore.Instance.SessionFactory.OpenSession();
            CurrentSessionContext.Bind(session);
        }

        private void EndEventhandler(object o, EventArgs e)
        {
            CurrentSessionContext.Unbind(ApplicationCore.Instance.SessionFactory);
        }
    }
}
