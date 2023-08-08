using FindDetails.utils;
using Newtonsoft.Json;
using System;
using System.Windows;

namespace FindDetails
{
    /// <summary>
    /// Логика взаимодействия для SettingsDialog.xaml
    /// </summary>
    public partial class SettingsDialog : Window
    {
        public SettingsDialog()
        {
            InitializeComponent();
            var s = JsonConvert.SerializeObject(SettingsCore.Instance, Formatting.Indented);
            TextBoxJson.Text = s;
        }

        private void Save_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                SettingsCore core = JsonConvert.DeserializeObject<SettingsCore>(TextBoxJson.Text);

                if (core != null)
                {
                    SettingsCore.Instance = core;
                    SettingsCore.Instance.Save();
                }

                MessageBox.Show("Ok. Reload program");

            }
            catch (Exception exception)
            {
                MessageBox.Show("Сохранение настроек Error: " + exception.Message);
            }

        }

        private void ButtonCancel_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
