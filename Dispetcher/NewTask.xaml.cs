using Dispetcher.Classes;
using Microsoft.Win32;
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

namespace Dispetcher
{

    public partial class NewTask : Window
    {

        public NewTask()
        {
            InitializeComponent();
        }

        private void Okclick(object sender, RoutedEventArgs e)
        {
            TryToStart();
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TryToStart()
        {
            var Name = MyTextBox.Text;
            try
            {
                NewProces newProces = new NewProces(Name);
            }
            catch (Exception)
            {
                MessageBox.Show($"Not find '{Name}' in curent context");
            }
        }

        private void SerchClick(object sender, RoutedEventArgs e)
        {
            var Name = SerchExeFile();
            try
            {
                NewProces newProces = new NewProces(Name);
            }
            catch (Exception)
            {
                MessageBox.Show($"Not find '{Name}' in curent context");
            }
        }

        string SerchExeFile()
        {
            string FileName;

            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "Programs |*.exe"
            };
            dialog.ShowDialog();
            FileName = dialog.FileName;
            return FileName;

        }
    }
}

