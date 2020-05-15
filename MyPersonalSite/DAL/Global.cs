using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace MyPersonalSite.DAL
{
    public class Global
    {

        public static string result = String.Empty;
        public static int size;
        public static string WebsiteUrl()
        {
            switch (ConfigurationManager.AppSettings["Environment"].ToString().ToLower())
            {

                case "local":
                    result = "http://localhost:56347/";
                    break;
                case "development":
                    result = "http://vijaydwivedi.somee.com/";
                    break;
                case "production":
                    result = "http://smsweb.com/";
                    break;
                default:
                    result = "http://www.smsweb.com/";
                    break;
            }

            return result;
        }
    }
}