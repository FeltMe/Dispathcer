using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Diagnostics;
using Dispetcher.Classes;
using Microsoft.Win32;
using System.Reflection;
using System.Collections.ObjectModel;

namespace Dispetcher
{
    public partial class MainWindow : Window
    {
        protected ViewModeler setTabs = new();
        protected ObservableCollection<MyProcesessHeader> processes = new();
        protected ObservableCollection<MyAppHeader> app = new();
        private readonly List<IAddition> plugins = new();

        public MainWindow()
        {
            InitializeComponent();
            AppList.ItemsSource = app;
            ProcList.ItemsSource = processes;
            WhiteModee.IsChecked = true;
        }

        private void NewTaskClick(object sender, RoutedEventArgs e)
        {
            NewTask newTask = new();
            newTask.ShowDialog();
            RefreshTabs();
        }

        private void EndTaskClick(object sender, RoutedEventArgs e)
        {
            KillProc();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetApps();
            SetProcesess();
            SetRefreshTimer();
        }

        private void SetApps()
        {
            AppList.ItemsSource = null;
            app.Clear();
            foreach (var item in setTabs.AddApplication())
            {
                app.Add(new MyAppHeader() { AppNamee = item.AppNamee });
            }
            AppList.ItemsSource = app;
        }


        private void SetProcesess()
        {
            ProcList.ItemsSource = null;
            processes.Clear();
            foreach (var item in setTabs.AddProcesess())
            {
                processes.Add(new MyProcesessHeader() { Id = item.Id, Name = item.Name, Priority = item.Priority, Descripts = item.Descripts });
            }
            ProcList.ItemsSource = processes;

        }

        private void KillProc()
        {
            string AppName = null;
            string ProcName = null;
            if (AppList.SelectedItem != null)
            {
                AppName = (AppList.SelectedItem as MyAppHeader).AppNamee;
            }
            if (ProcList.SelectedItem != null)
            {
                ProcName = (ProcList.SelectedItem as MyProcesessHeader).Name;
            }

            if (!string.IsNullOrEmpty(AppName))
            {
                Process[] processes = Process.GetProcessesByName(AppName);
                foreach (var item in processes)
                {
                    item.Kill();
                }
            }
            else if (!string.IsNullOrEmpty(ProcName))
            {
                int id = ProcList.SelectedIndex;
                MyProcesessHeader my = (MyProcesessHeader)ProcList.Items[id];
                Process process = Process.GetProcessesByName(my.Name)[0];
                process.Kill();
            }
            else
            {
                MessageBox.Show("No item selected");
            }
        }

        private void MenuItem_StartNewProgram(object sender, RoutedEventArgs e)
        {
            NewTask newTask = new();
            newTask.ShowDialog();
        }

        private void ExitClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void UpOfAll(object sender, RoutedEventArgs e)
        {
            this.Topmost = true;
        }

        private void DownOfAll(object sender, RoutedEventArgs e)
        {
            this.Topmost = false;
        }

        private void IdkWhatToDohear(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("I don't know what to do hear :(");
        }

        private void AddPlugin(object sender, RoutedEventArgs e)
        {

            OpenFileDialog ofd = new()
			{
                Filter = "Dll Files | *.dll"
            };

            string nspace = "", mname = "";
            if (ofd.ShowDialog() == true)
            {
                var path = ofd.FileName;
                Assembly asm = Assembly.LoadFrom(path);
                var md = asm.GetModules();
                foreach (var item in md)
                {
                    var m = item.GetTypes();
                    foreach (var item2 in m)
                    {
                        MessageBox.Show(item2.Name);
                        nspace = item2.Namespace;
                        mname = item2.Name;
                    }
                }
                dynamic inf = (asm.CreateInstance(nspace + "." + mname));
                inf.Do();
                foreach (var item in inf.OutputParams)
                {
                    CpuLabel.Content = item;
                }
            }

            
        }

        public void RefreshTabs()
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                SetProcesess();
                SetApps();
            });
        }

        private void SetRefreshTimer()
        {
            System.Timers.Timer t = new()
            {
                AutoReset = true,
                Interval = 5000,
            };
            t.Elapsed += (a, b) =>
            {
                RefreshTabs();
            };
            t.Start();
        }

        private void WhiteModeChange(object sender, RoutedEventArgs e)
        {
            int color = 0;
            SwitchColorMyDispetcher(color);
        }

        private void DarkModeChange(object sender, RoutedEventArgs e)
        {
            int color = 1;
            SwitchColorMyDispetcher(color);
        }

        private void SwitchColorMyDispetcher(int StyleColor)
        {
            SolidColorBrush backBrush = new();
            SolidColorBrush foregrnBrush = new();
            if (StyleColor == 0)
            {
                backBrush = Brushes.White;
                foregrnBrush = Brushes.Blue;
            }
            else
            {
                backBrush = Brushes.Black;
                foregrnBrush = Brushes.Blue;
            }

            MyMenu.Background = backBrush;
            MyMenu.Opacity = 0.9;
            MyMenu.Foreground = foregrnBrush;

            MyTapControl.Background = backBrush;
            MyTapControl.Opacity = 0.9;
            MyTapControl.Foreground = foregrnBrush;

            EndTaskBtn.Background = backBrush;
            EndTaskBtn.Opacity = 0.9;
            EndTaskBtn.Foreground = foregrnBrush;

            NewTaskBtn.Background = backBrush;
            NewTaskBtn.Opacity = 0.9;
            NewTaskBtn.Foreground = foregrnBrush;

            MyGrid.Background = backBrush;
            MyGrid.Opacity = 0.9;

            Tab1.Background = backBrush;
            Tab1.Opacity = 0.9;
            Tab1.Foreground = foregrnBrush;

            Tab2.Background = backBrush;
            Tab2.Opacity = 0.9;
            Tab2.Foreground = foregrnBrush;

            ProcList.Background = backBrush;
            ProcList.Opacity = 0.9;
            ProcList.Foreground = foregrnBrush;

            AppList.Background = backBrush;
            AppList.Opacity = 0.9;
            AppList.Foreground = foregrnBrush;
        }
    }
}
