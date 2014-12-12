
using System.Configuration;

namespace BarDataAccessLayer
{
    class DbHelper
    {
        public static string ConnString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;   
            }
        }
    }
}
