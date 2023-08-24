using Kres.Models.DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Security;

namespace Kres.Models.EntityLayer
{
    public class Teacher : DataAccess
    {
        #region Constructors
        public Teacher()
        {


        }
        #endregion

        #region Properties

        #endregion

        #region Method
        public static List<Teacher> GetAvailableInBasket()
        {
            List<Teacher> list = new List<Teacher>();
            DataTable dt = DAL.GetBasketById();


            return list;
        }
        #endregion
    }

    public partial class DataAccessLayer
    {
        public DataTable GetBasketById()
        {
            return DatabaseContext.ExecuteReader("_GetList_BasketById", MethodBase.GetCurrentMethod().GetParameters(), new object[] {  });
        }
    }
}