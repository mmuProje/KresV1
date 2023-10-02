using Kres.Models.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kres.Models.EntityLayer
{
    public class AdmimCurrentSessions
    {
        public AdmimCurrentSessions()
        {
            CompanyInformation = new CurrentSessionCompanyInformation();
            Language = new CurrentSessionLanguage();
            Teacher = new CurrentSessionTeacher();
            LanguageList = new List<CurrentSessionLanguage>();
            //CurrentAuthorityList = new List<MenuDTO>();
        }
        public CurrentSessionCompanyInformation CompanyInformation { get; set; }
        public CurrentSessionLanguage Language { get; set; }
        public CurrentSessionTeacher Teacher { get; set; }
        public List<CurrentSessionLanguage> LanguageList { get; set; }
        //public List<MenuDTO> CurrentAuthorityList { get; set; }
    }
    public class CurrentSessions
    {
        public CurrentSessions()
        {
            Student = new CurrentSessionStudent();
            CompanyInformation = new CurrentSessionCompanyInformation();
            Language = new CurrentSessionLanguage();
            TeacherOfStudent = new List<CurrentSessionTeacher>();
            Teacher = new CurrentSessionTeacher();
            LanguageList = new List<CurrentSessionLanguage>();
        }
        public CurrentSessionStudent Student { get; set; }
        public LoginType LoginType { get; set; }
        public CurrentSessionCompanyInformation CompanyInformation { get; set; }
        public CurrentSessionLanguage Language { get; set; }
        public List<CurrentSessionTeacher> TeacherOfStudent { get; set; }
        public CurrentSessionTeacher Teacher { get; set; }
        public List<CurrentSessionLanguage> LanguageList { get; set; }
    }
    public class CurrentSessionCompanyInformation
    {
        public string Title { get; set; }
        public string WebSite { get; set; }
    }
    public class CurrentSessionLanguage
    {
        public int Id { get; set; }
        public bool IsSelected { get; set; }
        public string Name { get; set; }
        public string FlagImageFileName { get; set; }
    }
    public class CurrentSessionTeacher
    {
        public CurrentSessionTeacher()
        {
            //AuthorityTeacher = new AuthorityTeacher();
        }
        public int Id { get; set; }
        public string PicturePath { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Tel1 { get; set; }
        public bool IsSystemUser { get; set; }
        //public AuthorityTeacher AuthorityTeacher { get; set; }
    }
    public class CurrentSessionStudent
    {
        public CurrentSessionStudent()
        {
            //AuthorityStudent = new AuthorityStudent();
            //Users = new CurrentSessionUser();
        }
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        //public AuthorityStudent AuthorityStudent { get; set; }
        public CurrentSessionUser Users { get; set; }
    }
    public class CurrentSessionUser
    {
        public CurrentSessionUser()
        {
            //AuthorityUser = new AuthorityUser();
        }
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string AvatarPath { get; set; }
        //public AuthorityUser AuthorityUser { get; set; }
    }

}