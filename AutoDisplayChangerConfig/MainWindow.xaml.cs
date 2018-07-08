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
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;

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
            VideoClosedComboBox.SelectedIndex = 0;

            appName = AppDomain.CurrentDomain.FriendlyName;
            appPath = AppDomain.CurrentDomain.BaseDirectory + appName;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string path = PathTextBox.Text;
            string videoStartedSetting = CheckSetting(VideoStartedComboBox.Text);
            string videoClosedSetting = CheckSetting(VideoClosedComboBox.Text);

            if (path != "")
            {
                path += '\\';
                SaveSettings(path, videoStartedSetting, videoClosedSetting);
            }
            else
                System.Windows.MessageBox.Show("Please enter a valid path");

        }

        private string CheckSetting(string text)
        {
            switch (text)
            {
                default: return ""; 
                case "Primary screen":
                    return "internal";
                case "Mirror":
                    return "clone";
                case "Extend":
                    return "extend";
                case "Secondary screen":
                    return "external";
            }
        }

        public void SaveSettings(string path,string videoStartedSetting,string videoClosedSetting)
        {
            string tempAppName = appName.Replace(".exe", "");

            string tempPath = path + "\\" + tempAppName + ".txt";
            Console.WriteLine("thís: " + tempPath);
            using (StreamWriter sw = new StreamWriter(tempPath,false))
            {
                Console.WriteLine("writing..");
                sw.WriteLine("VideoStartedSetting: " + videoStartedSetting);
                sw.WriteLine("VideoClosedSetting: " + videoClosedSetting);
            }

            SetStartup();

            string toRemove = "\\AutoDisplayChangerConfig";

            int nr = appPath.IndexOf(toRemove);
            Console.WriteLine(nr);
            string temp = appPath.Substring(0, nr); //appPath.Remove(nr, toRemove.Length);

            string src = temp + @"\AutoDisplayChanger\bin\Debug\AutoDisplayChanger.exe";
            Console.WriteLine(src);
            try
            {
                File.Copy(src, path + "AutoDisplayChanger.exe",true);
            }
            catch
            {
               System.Windows.MessageBox.Show("Please enter a valid path");
            }
        }

        private void SetStartup()
        {
            RegistryKey rk = Registry.CurrentUser.CreateSubKey
                ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (RunAtStartUpCheckbox.IsChecked.Value)
            {
                rk.SetValue("AutoDisplayChanger", PathTextBox.Text + "\\AutoDisplayChanger.exe");

                Console.WriteLine("Setting register value");
            }
            else
                rk.DeleteValue("AutoDisplayChanger.exe", false);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog
            {
                IsFolderPicker = true
            };
            using (dialog)
            {
                if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    PathTextBox.Text = dialog.FileName;
                }
            }
        }
    }
}
