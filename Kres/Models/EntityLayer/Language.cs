using Kres.Models.DataLayer;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace Kres.Models.EntityLayer
{
    public class Language : DataAccess
    {

        #region Properties
        public int Id { get; set; }
        public string Name { get; set; }
        public string LanguageCulture { get; set; }
        public string UniqueSeoCode { get; set; }
        public string FlagImageFileName { get; set; }
        public bool Published { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsSelected { get; set; }

        #endregion

        public Language()
        {
            DisplayOrder = 999;
        }
        #region Methods

        public static List<Language> GetList()
        {
            List<Language> list = new List<Language>();
            DataTable dt = DAL.GetListLanguage();
            foreach (DataRow row in dt.Rows)
            {
                Language language = new Language();
                language.Id = row.Field<int>("Id");
                language.DisplayOrder = row.Field<int>("DisplayOrder");
                language.FlagImageFileName = row.Field<string>("FlagImageFileName");
                language.LanguageCulture = row.Field<string>("LanguageCulture");
                language.Name = row.Field<string>("Name");
                language.UniqueSeoCode = row.Field<string>("UniqueSeoCode");
                list.Add(language);
            }
            return list;
        }

        #endregion
    }

    public partial class DataAccessLayer
    {
        public DataTable GetListLanguage()
        {
            return DatabaseContext.ExecuteReader(CommandType.StoredProcedure, "_GetList_Language", MethodBase.GetCurrentMethod().GetParameters(), new object[] { });

        }
    }
}