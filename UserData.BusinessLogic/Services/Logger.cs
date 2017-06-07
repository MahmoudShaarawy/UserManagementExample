using System;
using System.Web;
using Elmah;

namespace UserData.BusinessLogic.Services
{
    public interface ILogger
    {
        void Log(Exception ex);
    }


    public class Logger : ILogger
    {
        public void Log(Exception ex)
        {
            Elmah.ErrorLog.GetDefault(HttpContext.Current).Log(new Error(ex));
           // throw new System.NotImplementedException();
        }
    }
}