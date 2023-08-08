using System;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace FindDetails.utils
{
    public static class UtilsCore
    {
        
        public static void FindProc(string detail)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(SettingsCore.Instance.PathPdfFile))
                {
                  
                    MessageBox.Show("Файл PDF в котом осуществляется поис не выбран.");
                    return;
              
                }

                if (SettingsCore.Instance.IsAddNullFromNumber)
                {
                    int d = SettingsCore.Instance.LengthNumber - detail.Length;
                    if (d > 0)
                    {
                        for (int i = 0; i < d; i++)
                        {
                            detail = "0" + detail;
                        }
                    }
                }
                //
                //string anyCommand = $"  /A search=\"{detail.Trim()}\" \"{Path.Combine(Environment.CurrentDirectory, "itogo2.pdf")}\" ";
                string anyCommand = $"  /A search=\"{detail.Trim()}\" \"{SettingsCore.Instance.PathPdfFile}\" ";
                var proc = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = SettingsCore.Instance.PathAdobeExecute,
                        Arguments = anyCommand,
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true,
                        WorkingDirectory = Environment.CurrentDirectory
                    }
                };
                proc.Start();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Старт процесса поиска ошибка: " + exception.Message);
            }
        }
    }
}
