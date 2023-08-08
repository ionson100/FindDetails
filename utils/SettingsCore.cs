using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;

namespace FindDetails.utils
{
    class SettingsCore
    {
        private static string _path = Path.Combine(Environment.CurrentDirectory, "settings.txt");
        public static SettingsCore Instance;
        static SettingsCore()
        {
            if (File.Exists(_path) == false)
            {
                try
                {
                    Instance = new SettingsCore();
                    string s = JsonConvert.SerializeObject(Instance, Formatting.Indented);
                    File.WriteAllText(_path, s);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Первичная сериализация настроек: Error -" + e.Message);
                }
               
            }
            else
            {
                try
                {
                    string s = File.ReadAllText(_path);
                    Instance = JsonConvert.DeserializeObject<SettingsCore>(s);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Статическая десериализация ошибка: " + e.Message);
                }
               
            }

        }

        public bool IsAddNullFromNumber { get; set; } = true;
        public string PathDetails { get; set; }
        public string PathPdfFile { get; set; }
        public int LengthNumber { get; set; } = 8;
        public string PathAdobeExecute { get; set; } = @"C:\Program Files\Adobe\Acrobat DC\Acrobat\Acrobat.exe";

        public string ConnectionString =
            "Server=192.168.70.119;Port=5432;Database=detail;User Id=postgres;Password=postgres;";

        public void Save()
        {
            try
            {
              
                string s = JsonConvert.SerializeObject(Instance, Formatting.Indented);
                File.WriteAllText(_path, s);
            }
            catch (Exception e)
            {
                MessageBox.Show("Сохраннеие настроек : Error -" + e.Message);
            }
        }
        
    }
}
