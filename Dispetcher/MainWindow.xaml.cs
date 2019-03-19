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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using Dispetcher.Classes;
using Microsoft.Win32;
using System.Reflection;
using System.Threading;
using System.Collections.ObjectModel;

namespace Dispetcher
{

    public partial class MainWindow : Window
    {
        protected ViewModeler SetTabs = new ViewModeler();
        protected ObservableCollection<MyProcesessHeader> processes = new ObservableCollection<MyProcesessHeader>();
        protected ObservableCollection<MyAppHeader> app = new ObservableCollection<MyAppHeader>();
        private List<IAddition> plugins = new List<IAddition>();

        public MainWindow()
        {
            InitializeComponent();
            AppList.ItemsSource = app;
            ProcList.ItemsSource = processes;
        }

        private void NewTaskClick(object sender, RoutedEventArgs e)
        {
            NewTask newTask = new NewTask();
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
            foreach (var item in SetTabs.AddApplication())
            {
                app.Add(new MyAppHeader() { AppNamee = item.AppNamee });
            }
            AppList.ItemsSource = app;
        }


        private void SetProcesess()
        {
            ProcList.ItemsSource = null;
            processes.Clear();
            foreach (var item in SetTabs.AddProcesess())
            {
                processes.Add(new MyProcesessHeader() { Id = item.Id, Name = item.Name, Priority = item.Priority, Descripts = item.Descripts });
            }
            ProcList.ItemsSource = processes;

        }

        private void KillProc()
        {
            try
            {
                int id = ProcList.SelectedIndex;
                MyProcesessHeader my = (MyProcesessHeader)ProcList.Items[id];
                Process process = Process.GetProcessesByName(my.Name)[0];
                process.Kill();
            }
            catch
            {
                Console.WriteLine("Acess is denied");
            }
        }

        private void MenuItem_StartNewProgram(object sender, RoutedEventArgs e)
        {
            NewTask newTask = new NewTask();
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
            
                OpenFileDialog opf = new OpenFileDialog
                {
                    Filter = "Dll Files | *.dll"
                };
                opf.ShowDialog();

                var asm = Assembly.Load(opf.FileName);
                dynamic  inf = asm.CreateInstance("MyPlugin.Plugin");
                inf.Do();
                var cpu = inf.CPUUsage();
                var mem = inf.MemUsage();
                CpuLabel.Content = cpu;
                MemLabel.Content = mem;
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
            System.Timers.Timer t = new System.Timers.Timer()
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
    }
}
