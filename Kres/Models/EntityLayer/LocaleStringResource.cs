using Kres.Models.DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace Kres.Models.EntityLayer
{
    public class LocaleStringResource : DataAccess
    {

        #region Properties
        public int Id { get; set; }
        public string ResourceName { get; set; }
        public string ResourceValue { get; set; }
        public string UniqueSeoCode { get; set; }
        public int LanguageId { get; set; }
        public int ResourceKeyId { get; set; }

        #endregion
        #region Methods

        public static Dictionary<string, string> GetListByLanguageId(int pLanguageId)
        {
            Dictionary<string, string> list = new Dictionary<string, string>();
            DataTable dt = DAL.GetListLocaleStringResourceByLangId(pLanguageId);
            foreach (DataRow row in dt.Rows)
            {
                LocaleStringResource localeStringResource = new LocaleStringResource
                {
                    Id = row.Field<int>("Id"),
                    ResourceName = row.Field<string>("ResourceName"),
                    ResourceValue = row.Field<string>("ResourceValue"),
                    LanguageId = row.Field<int>("LanguageId"),
                    CreateDate = row.Field<DateTime>("CreateDate"),
                    CreateId = row.Field<int>("CreateId"),
                    ResourceKeyId = Convert.ToInt32(row["ResourceKeyId"]),
                };
                list.Add(localeStringResource.ResourceName, localeStringResource.ResourceValue);
            }
            return list;
        }

        public static string GetItemByResourceNameDefault(string pResourceName)
        {
            string returnValue = pResourceName;
            DataTable dt = DAL.GetItemResourceNameDefault(pResourceName);
            if (dt.Rows.Count > 0 && !string.IsNullOrEmpty(dt.Rows[0][0].ToString()))
                returnValue = dt.Rows[0][0].ToString();
            return returnValue;
        }
        #endregion

    }

    public partial class DataAccessLayer
    {
        public DataTable GetListLocaleStringResourceByLangId(int pLanguageId)
        {
            return DatabaseContext.ExecuteReader(CommandType.StoredProcedure, "_GetList_LocaleStringResourceByLangId", MethodBase.GetCurrentMethod().GetParameters(), new object[] { pLanguageId });

        }

        public DataTable GetItemResourceNameDefault(string pResourceName)
        {
            return DatabaseContext.ExecuteReader(CommandType.StoredProcedure, "_GetItem_ResourceNameDefault", MethodBase.GetCurrentMethod().GetParameters(), new object[] { pResourceName });

        }
    }
}