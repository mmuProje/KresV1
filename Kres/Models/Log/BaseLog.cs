using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kres.Models.EntityLayer
{
    public class BaseLog
    {
    }
    public enum LogGeneralErrorType
    {
        Error,
        Information,
    }
    public enum ClientType
    {
        B2BWeb = 1,
        B2BWpf = 2,
        B2BAndroid = 3,
        B2BIos = 4,
        Admin = 5,
        None = 99,
    }

    public enum LogTransactionSource
    {
        Login,
        LoginFail,
        StudentSelect,
        Logout,
        PasswordChange,
        UnAuthorized
    }

    public enum Navigation
    {

    }

    public enum ProcessLogin
    {
        Success,
        Fail,
        Logout,
        SelectStudent
    }
}