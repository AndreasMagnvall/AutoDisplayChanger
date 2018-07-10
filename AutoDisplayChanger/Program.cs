using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Threading;

namespace AutoDisplayChanger
{
    class Program
    {
        static string appName;
        static string appPath;

        static string onVideoStartedSetting;
        static string onVideoClosedSetting;

        private static bool started = false;

        static void Main(string[] args)
        {
            appName = AppDomain.CurrentDomain.FriendlyName;
            appPath = AppDomain.CurrentDomain.BaseDirectory + appName;

        //    ReadSettings();

            while (true)
            {              
                if (Process.GetProcessesByName("vlc").Length > 0 && started == false)
                {
                    Console.WriteLine(started);
                    ReadSettings();
                    DisplayChanger.StartInfo.Arguments = "/" + onVideoStartedSetting;                   
                    DisplayChanger.Start();
                    started = true;
                }
                else if (Process.GetProcessesByName("vlc").Length == 0 && started == true)
                {
                    DisplayChanger.StartInfo.Arguments = "/" + onVideoClosedSetting;
                    DisplayChanger.Start();
                    started = false;
                }
                Thread.Sleep(1000);
            }                  
        }

        private static Process DisplayChanger = new Process
        {
            StartInfo =
        {
            CreateNoWindow = true,
            WindowStyle = ProcessWindowStyle.Hidden,
            FileName = @"C:\Windows\Sysnative\DisplaySwitch.exe",
        }
        };

        private static void ReadSettings()
        {
            Console.WriteLine("reading");
            try
            {
                using (StreamReader sr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "\\AutoDisplayChangerConfig.txt"))
                {
                    string buffer = sr.ReadLine();

                    onVideoStartedSetting = buffer.Replace("VideoStartedSetting: ", "");
                    buffer = sr.ReadLine();
                    onVideoClosedSetting = buffer.Replace("VideoClosedSetting: ", "");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }  
}
