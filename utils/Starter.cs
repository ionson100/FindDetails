using System;
using System.Windows;
using FindDetails.models;
using ORM_1_21_;

namespace FindDetails.utils
{
    class Starter
    {
        public static void Run()
        {
            string p = null;
#if DEBUG
            p = "assa.txt";     
#else

#endif
            try
            {
                _ = new Configure(SettingsCore.Instance.ConnectionString, ProviderName.PostgreSql, p);
                ISession ses = Configure.Session;
                if (ses.TableExists<MDetails>() == false)
                {
                    ses.TableCreate<MDetails>();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Инициализация орм ошибка: " + e.ToString());
            }
        }
    }
}
