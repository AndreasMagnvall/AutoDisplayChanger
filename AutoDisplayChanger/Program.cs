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

            ReadSettings();

            while (true)
            {
                Thread.Sleep(1000);
                Console.WriteLine("test");
                if (Process.GetProcessesByName("vlc").Length > 0)
                {
                    Console.WriteLine("test2");
                  
                        Thread th = new Thread (() =>
                            {
                                Console.WriteLine("test22");
                                DisplayChanger.Start();
                                Console.WriteLine("status is : " + DisplayChanger.Responding);
                            });
                    th.Start();
                }
                else
                {
                 //   started = false;
                    try
                    {
                     //   DisplayChanger.Kill();
                    }
                    catch { }
                }
            }                  
        }

        private static Process DisplayChanger = new Process
        {
            StartInfo =
        {
            CreateNoWindow = true,
            WindowStyle = ProcessWindowStyle.Hidden,
            FileName = @"C:\Windows\Sysnative\DisplaySwitch.exe",
            Arguments = "/" + "extend"
        }
        };

        private static void ReadSettings()
        {
            try
            {
                using (StreamReader sr = new StreamReader("AutoDisplayChangerConfig.txt"))
                {
                    string buffer = sr.ReadLine();

                    onVideoStartedSetting = buffer.Replace("VideoStartedSetting:", "");
                    buffer = sr.ReadLine();
                    onVideoClosedSetting = buffer.Replace("VideoClosedSetting", "");
                }
            }
            catch
            {
                MessageBox.Show("Error: Could not find configuration file");
            }
        }
    }  
}
