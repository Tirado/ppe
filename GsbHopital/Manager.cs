using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GsbHopital
{
    static class Manager
    {

        static public SqlConnection Connection { get; set; }

        /**
        * Constructeur de la class 
        */
        public static void Init()
        {
            String cString = ConfigurationManager.ConnectionStrings["GSBHOPITAL"].ConnectionString;
            Manager.Connection = new SqlConnection(cString);
        }

        /**
         * Méthode SqlCommand
         **/
        public static SqlCommand SqlCommand(string value)
        {
            return new SqlCommand(value, Manager.Connection);
        }
        
    
    }
}
