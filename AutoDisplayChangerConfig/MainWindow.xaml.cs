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
using System.IO;
using Microsoft.Win32;
namespace AutoDisplayChangerConfig
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string appName;
        string appPath;

        public MainWindow()
        {
            InitializeComponent();
            VideoStartedComboBox.SelectedIndex = 0;
            VideoClosedCombobox.SelectedIndex = 0;

            appName = System.AppDomain.CurrentDomain.FriendlyName;
            appPath = AppDomain.CurrentDomain.BaseDirectory + appName;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string path = PathTextBox.Text;
            string videoStartedSetting = VideoStartedComboBox.Text;
            string videoClosedSetting = VideoClosedCombobox.Text;
            SaveSettings(path,videoStartedSetting,videoClosedSetting);
        }

        public void SaveSettings(string path,string videoStartedSetting,string videoClosedSetting)
        {
            string tempAppName = appName.Replace(".exe", "");

            using (StreamWriter sw = new StreamWriter(path + tempAppName + ".txt"))
            {
                sw.WriteLine("VideoStartedSetting: " + videoStartedSetting);
                sw.WriteLine("VideoClosedSetting: " + videoStartedSetting);
            }

            SetStartup();
        }

        private void SetStartup()
        {
            RegistryKey rk = Registry.CurrentUser.OpenSubKey
                ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);            

            if (RunAtStartUpCheckbox.IsChecked.Value)
                rk.SetValue(appName, appPath);
            else
                rk.DeleteValue(appName, false);
        }
    }
}
