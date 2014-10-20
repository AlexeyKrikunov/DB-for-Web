using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;

namespace DBLab
{
    public sealed class ApplicationCore
    {
        private static readonly ApplicationCore mInstance = new ApplicationCore();
        private static ISessionFactory mISessionFactory;

        public static ApplicationCore Instance
        {
            get { return mInstance; }
        }

        public ISessionFactory SessionFactory
        {
            get { return mISessionFactory; }
            set { mISessionFactory = value; }
        }
    }
}