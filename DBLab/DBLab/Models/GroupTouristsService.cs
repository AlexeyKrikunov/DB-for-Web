using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBLab.Models
{
    public class GroupTouristsService : DBService
    {
        public void AddNewGroupTourists(GroupTourists group_tourists)
        {
            Add(group_tourists);
        }
    }
}