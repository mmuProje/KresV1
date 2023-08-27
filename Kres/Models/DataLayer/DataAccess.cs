
using System;

namespace Kres.Models.EntityLayer
{
    public class DataAccess
    {

        #region Properties
        public DateTime CreateDate { get; set; }
        public string CreateDateStr { get { return CreateDate.ToShortDateString(); } }
        public int CreateId { get; set; }
        public DateTime? EditDate { get; set; }
        public string EditDateStr { get; set; }
        public int EditId { get; set; }
        public bool Deleted { get; set; }
        public string CreateName { get; set; }
        public string LastUpdateName { get; set; }
        #endregion

        public static string NullControl(string value)
        {
            return String.IsNullOrEmpty(value) ? "-" : value;
        }

        public static string[] GenerateT9Search(string t9Text)
        {
            string[] t9Array = new string[10];
            for (int i = 0; i < 10; i++)
                t9Array[i] = "*";

            if (t9Text != "*")
            {
                string[] tmpArray = t9Text.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                int j = 0;
                for (int i = 0; i < tmpArray.Length; i++)
                {
                    string t = tmpArray[i];
                    if (t.Length > 1)
                    {
                        if (j < t9Array.Length)
                        {
                            t9Array[j] = t.ToUpper().Replace('Ü', 'U').Replace('Ğ', 'G').Replace('İ', 'I').Replace('Ş', 'S').Replace('Ç', 'C').Replace('Ö', 'O');
                            j++;
                        }
                    }

                    if (j == 10)
                        break;
                }
            }

            return t9Array;
        }


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