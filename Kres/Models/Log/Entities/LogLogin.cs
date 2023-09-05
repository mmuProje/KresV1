namespace Kres.Models.EntityLayer
{
    public class LogLogin
    {
        #region Properties
        public string Type { get; set; }
        public string B2bCode { get; set; }
        public string UserCode { get; set; }
        public string Pass { get; set; }
        #endregion

        #region Constructors
        public LogLogin()
        {
            Type = string.Empty;
            B2bCode = string.Empty;
            UserCode = string.Empty;
            Pass = string.Empty;
        } 
        #endregion
    }
}