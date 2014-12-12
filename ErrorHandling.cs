using System;
using System.Globalization;
using System.IO;


namespace BarDataAccessLayer
{
    public class ErrorHandling
    {
        private readonly string _sLogFormat;
        private readonly string _sErrorTime;

        public ErrorHandling()
        {
            //sLogFormat used to create log files format :
            // dd/mm/yyyy hh:mm:ss AM/PM ==> Log Message
            _sLogFormat = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " ==> ";

            //this variable used to create log filename format "
            //for example filename : ErrorLogYYYYMMDD
            string sYear = DateTime.Now.Year.ToString(CultureInfo.InvariantCulture);
            string sMonth = DateTime.Now.Month.ToString(CultureInfo.InvariantCulture);
            string sDay = DateTime.Now.Day.ToString(CultureInfo.InvariantCulture);
            _sErrorTime = sYear + sMonth + sDay;
        }

        public void ErrorLog(string sPathName, string sErrMsg)
        {
            var sw = new StreamWriter(sPathName + _sErrorTime, true);
            sw.WriteLine(sErrMsg);
            sw.Flush();
            sw.Close();
        }

        public string GetLogMessage()
        {
            return _sLogFormat;
        }
    }
}