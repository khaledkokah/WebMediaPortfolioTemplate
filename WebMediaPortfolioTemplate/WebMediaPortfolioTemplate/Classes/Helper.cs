using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using WebMediaPortfolioTemplate.Classes;
using WebMediaPortfolioTemplate.Models;

//Helper class contains client side interactive methods
namespace WebMediaPortfolioTemplate.Classes
{
    public static class Helper
    {
        //Display javascript alerts for pages
        public static void DisplayAlert(Control ctrl, string message, string locationUrl = "", bool reloadPage = false, bool alertBox = false)
        {
            message = Regex.Replace(message, @"\t|\n|\r|'", "");
            if (locationUrl != "")
            {
                ScriptManager.RegisterStartupScript(ctrl, ctrl.GetType(), "alert",
                "alert('" + message + "');window.location ='" + locationUrl + "';", true);
            }
            else
            {
                if (reloadPage == false)
                {
                    ScriptManager.RegisterStartupScript(ctrl, ctrl.GetType(), "alert",
                        "alert('" + message + "');", true);
                }
                else
                    ScriptManager.RegisterStartupScript(ctrl, ctrl.GetType(), "alert",
                "alert('" + message + "');window.location.reload();", true);
            }
        }

    }
}