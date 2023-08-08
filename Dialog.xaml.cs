using FindDetails.models;
using ORM_1_21_;
using System;
using System.Diagnostics;
using System.Windows;
using FindDetails.utils;
using Path = System.IO.Path;

namespace FindDetails
{
    /// <summary>
    /// Логика взаимодействия для Dialog.xaml
    /// </summary>
    public partial class Dialog : Window
    {
        private readonly MDetails _details;

        public Dialog(MDetails details)
        {
            _details = details;
            InitializeComponent();
            TextBoxDescription.Text = details.Description;
            if (details.IsShow == 1)
            {
                CheckBoxShow.IsChecked = true;
            }

            if (details.Verdict == 1)
            {
                CheckBoxVerdict.IsChecked = true;
            }

            TextBlockNumber.Text = details.Number;
        }

        private void ShowPdf_OnClick(object sender, RoutedEventArgs e)
        {
            UtilsCore.FindProc(_details.Number.Trim());
        }

       

        private void ButtonSave_OnClick(object sender, RoutedEventArgs e)
        {
            if (CheckBoxShow.IsChecked == true)
            {
                _details.IsShow = 1;
            }
            else
            {
                _details.IsShow = 0;
            }

            if (CheckBoxVerdict.IsChecked == true)
            {
                _details.Verdict = 1;
            }
            else
            {
                _details.Verdict = 0;
            }

            _details.Description = TextBoxDescription.Text.Trim();
            Configure.Session.Update(_details);
            this.DialogResult = true;
            this.Close();



        }

        private void ButtonCancel_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
