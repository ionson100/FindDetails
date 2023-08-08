using FindDetails.models;
using FindDetails.utils;
using ORM_1_21_;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace FindDetails
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly SettingsCore _settingsCore = SettingsCore.Instance;

        public MainWindow()
        {
            InitializeComponent();
            Starter.Run();
            Init();

        }

        private void Init()
        {
            int i = 0;
            try
            {
                var r = Directory.GetFiles(_settingsCore.PathDetails, "*.txt");
                if (r.Length == 0) return;
                HashSet<string> set = new HashSet<string>();
                foreach (string s in r)
                {
                    var ee = File.ReadAllLines(s);
                    foreach (string s1 in ee)
                    {
                        set.Add(s1);
                    }
                }

                ISession ses = Configure.Session;
                List<MDetails> detailsList = new List<MDetails>();
                foreach (string s in set)
                {

                    string sse = s.Trim().Trim(new[] { '\t' });
                    if (string.IsNullOrWhiteSpace(sse)) continue;
                    if (i > 709)
                    {

                    }

                    try
                    {
                        var ss = ses.Query<MDetails>().Count(a => a.Number == sse);
                        i++;
                        if (ss == 0)
                        {
                            detailsList.Add(new MDetails() { Number = s.Trim().Trim(new[] { '\t' }) });
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }

                }

                if (detailsList.Any())
                {
                    ses.InsertBulk(detailsList);

                }



            }
            catch (Exception e)
            {
                MessageBox.Show("Init base error: " + e);
            }
            finally
            {
                try
                {
                    DataGridOrder.ItemsSource = Configure.Session.Query<MDetails>().OrderBy(s => s.Id).ToList();
                }
                catch (Exception e)
                {
                    MessageBox.Show("Открытие таблици: " + e);
                }
            }

        }

        private void FolderDetail_OnClick(object sender, RoutedEventArgs e)
        {

            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                dialog.Description = "Папка где лежат текстовые файлы с наименованием деталей";
                dialog.SelectedPath = _settingsCore.PathDetails;
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    _settingsCore.PathDetails = dialog.SelectedPath;
                    _settingsCore.Save();
                }
            }
        }

        private void FolderPdf_OnClick(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.OpenFileDialog())
            {
                dialog.Title = "Файл PDF для поиска";
                dialog.FileName = _settingsCore.PathPdfFile;
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    _settingsCore.PathPdfFile = dialog.FileName;
                    _settingsCore.Save();
                }
            }
        }

        private void ShowDialog_OnClick(object sender, RoutedEventArgs e)
        {
            if (DataGridOrder.SelectedItem == null) return;
            var detail = (MDetails)DataGridOrder.SelectedItem;
            Dialog dialog = new Dialog(detail);
            var r = dialog.ShowDialog();
            if (r == true)
            {
                DataGridOrder.ItemsSource = Configure.Session.Query<MDetails>().OrderBy(s => s.Id).ToList();
            }
        }

        private void FreeSearching_OnClick(object sender, RoutedEventArgs e)
        {
            FreeSearching freeSearching = new FreeSearching();
            freeSearching.ShowDialog();
        }

        private void Settings_OnClick(object sender, RoutedEventArgs e)
        {
            new SettingsDialog().ShowDialog();
        }

        private void Print_OnClick(object sender, RoutedEventArgs e)
        {
            List<MDetails> list = Configure.Session.Query<MDetails>().Where(a => a.Verdict == 1).OrderBy(s => s.Id)
                .ToList();
            StringBuilder builder = new StringBuilder();
            foreach (MDetails details in list)
            {
                builder.AppendLine($"{details.Id}  {details.Number}     {details.Description}");
            }

            builder.AppendLine(" ");

            builder.AppendLine($"Итого наименований: {list.Count}");

            string path = $"{Guid.NewGuid()}.txt";
            File.WriteAllText(path, builder.ToString());
            Process.Start(path);
        }

        private void Help_OnClick(object sender, RoutedEventArgs e)
        {
            Process.Start(Path.Combine(Environment.CurrentDirectory,"help","index.html"));
        }
    }
}
