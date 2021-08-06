using Dispetcher.Classes;
using Microsoft.Win32;
using System;
using System.Windows;

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
            Close();
        }

        private void TryToStart()
        {
            var name = MyTextBox.Text;
            try
            {
                NewProces newProces = new(name);
            }
            catch (Exception)
            {
                MessageBox.Show($"Not find '{name}' in curent context");
            }
        }

        private void SerchClick(object sender, RoutedEventArgs e)
        {
            var Name = SerchExeFile();
            try
            {
                NewProces newProces = new(Name);
            }
            catch (Exception)
            {
                MessageBox.Show($"Not find '{Name}' in curent context");
            }
        }

        string SerchExeFile()
        {
            string fileName;
            OpenFileDialog dialog = new()
			{
                Filter = "Programs |*.exe"
            };
            dialog.ShowDialog();
            fileName = dialog.FileName;
            return fileName;

        }
    }
}