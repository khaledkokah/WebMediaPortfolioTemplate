using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

//JSON/XML or any data parsers here, for 
namespace WebMediaPortfolioTemplate.Classes
{
    public class Parsers
    {
        public static string JsonEncode(object obj)
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();
            return ser.Serialize(obj);
        }

        public static T JsonDecode<T>(string str)
        {
            return JsonConvert.DeserializeObject<T>(str);
        }
    }
}