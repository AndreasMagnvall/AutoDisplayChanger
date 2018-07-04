using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDisplayChanger
{
    class Program
    {
        private static bool started = false;

        static void Main(string[] args)
        {
            while (started == false)
            {
                System.Threading.Thread.Sleep(1000);

                if (Process.GetProcessesByName("vlc").Length > 0)
                {
                    started = true;
                    DisplayChanger.Start();
                   
                    Console.WriteLine("test");
                }
                else
                {
                    try
                    {
                        DisplayChanger.Kill();
                    }
                    catch { }
                    Console.WriteLine("test2");
                    started = false;
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
            Arguments = "/mirror"
        }
        };

    }  
}
