using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBLab.Models
{
    public class TransportService : DBService
    {
        public void AddNewTransport(Transport transport)
        {
            Add(transport);
        }
    }
}