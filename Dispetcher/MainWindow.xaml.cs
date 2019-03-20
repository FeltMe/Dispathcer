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
            WhiteModee.IsChecked = true;
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

            if (string.IsNullOrEmpty(AppName) == false)
            {
                Process[] processes = Process.GetProcessesByName(AppName);
                foreach (var item in processes)
                {
                    item.Kill();
                }
            }
            else if (string.IsNullOrEmpty(ProcName) == false)
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

            OpenFileDialog ofd = new OpenFileDialog
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
                var inf = (asm.CreateInstance(nspace + "." + mname));
                var method = inf.GetType().GetMethod("Do");
                MessageBox.Show((string)method.Invoke(inf, null));
            }

            //string[] dllFileNames = null;
            //ICollection<Assembly> assemblies = new List<Assembly>(dllFileNames.Length);
            //foreach (string dllFile in dllFileNames)
            //{
            //    AssemblyName an = GetAssemblyName(dllFile);
            //    Assembly assembly = Assembly.Load(an);
            //    assemblies.Add(assembly);
            //}
            //AssemblyName name = new AssemblyName(opf.FileName);
            //Assembly assembly = Assembly.Load(name);
            //assemblies.Add(assembly);
            //Type pluginType = typeof(IAddition);



            //AppDomain Plagin = AppDomain.CreateDomain("Plugin");
            //Assembly asm = Plagin.Load(opf.FileName);
            //var inf = asm.CreateInstance("myLibrary.Info");
            //var method = inf.GetType().GetMethod("Do");
            //method.Invoke(inf, new object[] {});



            // Assembly asm = typeof(IAddition).Assembly;
            // dynamic inf = asm.CreateInstance("MyPlugin.Plugin");
            // inf.Do();
            // var cpu = inf.CPUUsage();
            // var mem = inf.MemUsage();
            // CpuLabel.Content = cpu;
            // MemLabel.Content = mem;
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
            var BackBrush = new SolidColorBrush();
            var ForegrnBrush = new SolidColorBrush();
            if (StyleColor == 0)
            {
                BackBrush = Brushes.White;
                ForegrnBrush = Brushes.Blue;
            }
            else
            {
                BackBrush = Brushes.Black;
                ForegrnBrush = Brushes.Blue;
            }

            MyMenu.Background = BackBrush;
            MyMenu.Opacity = 0.9;
            MyMenu.Foreground = ForegrnBrush;

            MyTapControl.Background = BackBrush;
            MyTapControl.Opacity = 0.9;
            MyTapControl.Foreground = ForegrnBrush;

            EndTaskBtn.Background = BackBrush;
            EndTaskBtn.Opacity = 0.9;
            EndTaskBtn.Foreground = ForegrnBrush;

            NewTaskBtn.Background = BackBrush;
            NewTaskBtn.Opacity = 0.9;
            NewTaskBtn.Foreground = ForegrnBrush;

            MyGrid.Background = BackBrush;
            MyGrid.Opacity = 0.9;

            Tab1.Background = BackBrush;
            Tab1.Opacity = 0.9;
            Tab1.Foreground = ForegrnBrush;

            Tab2.Background = BackBrush;
            Tab2.Opacity = 0.9;
            Tab2.Foreground = ForegrnBrush;

            ProcList.Background = BackBrush;
            ProcList.Opacity = 0.9;
            ProcList.Foreground = ForegrnBrush;

            AppList.Background = BackBrush;
            AppList.Opacity = 0.9;
            AppList.Foreground = ForegrnBrush;
        }
    }
}
