using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using FindDetails.utils;

namespace FindDetails
{
    /// <summary>
    /// Логика взаимодействия для FreeSearching.xaml
    /// </summary>
    public partial class FreeSearching : Window
    {
        public FreeSearching()
        {
            InitializeComponent();
        }

        private void ShowPdf_OnClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TextBoxSearchData.Text))
            {
                MessageBox.Show("Данные номера детали отсутствуют");
                return;
            }
            UtilsCore.FindProc(TextBoxSearchData.Text.Trim());
        }

        private void ButtonCancel_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
