namespace Core.Data
{
    using System;
    //using System.Web;

    public class Data
    {
        public static IDataStorage Instance
        {
            get {
                try
                {
                    var vv = Nested.Instance;
                    return vv;
                }
                catch(TypeInitializationException ee)
                {
                    throw ee;
                }
            }
        }

        private class Nested
        {
            internal static readonly IDataStorage Instance = 
                new Storage.DataStorage(System.Configuration.ConfigurationManager.ConnectionStrings["Entities"].ConnectionString);

            static Nested()
            {
                ////
            }
        }

    }
}
