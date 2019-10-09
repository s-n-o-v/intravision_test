namespace Vending.Settings
{
    using System.Collections.Generic;
    using System.Web;
    using System.Web.Mvc;

    public class AppSettings
    {
        public static int Cash
        {
            get
            {
                int cash = 0;
                if (HttpContext.Current.Session["CASH"] != null)
                {
                    cash = (int)HttpContext.Current.Session["CASH"];
                }

                return cash;
            }
            set { HttpContext.Current.Session["CASH"] = value; }
        }

        public static string SecretKey
        {
            get
            {
                string key = null;
                if (HttpContext.Current.Session["SKEY"] != null)
                {
                    key = (string)HttpContext.Current.Session["SKEY"];
                }

                return key;
            }
            set { HttpContext.Current.Session["SKEY"] = value; }
        }

    }
}