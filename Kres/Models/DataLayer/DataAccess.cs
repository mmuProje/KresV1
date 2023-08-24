using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kres.Models.EntityLayer
{
    public class DataAccess
    {
        private static DataAccessLayer dal;
        protected static DataAccessLayer DAL
        {
            get
            {
                if (dal == null)
                    dal = new DataAccessLayer();
                return dal;
            }
        }
    }
}