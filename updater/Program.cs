using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.IO.Compression;

namespace Updater
{
    class Program
    {
        static void Main(string[] args)
        {
            LogoPrint();
            if (args.Length > 0)
            {
                string zipPath = AppDomain.CurrentDomain.BaseDirectory + args[0] + ".zip";
                if (File.Exists(zipPath))
                {
                    Console.WriteLine("Zip " + zipPath + " ready.");
                    string extractPath = AppDomain.CurrentDomain.BaseDirectory + "extract";
                    Console.WriteLine("Unzipping in " + extractPath);
                    string source = AppDomain.CurrentDomain.BaseDirectory + "extract";
                    string dest = AppDomain.CurrentDomain.BaseDirectory;
                    if (Directory.Exists(source))
                        Directory.Delete(source, true);
                    Console.WriteLine("Destenation folder " + dest);
                    ZipFile.ExtractToDirectory(zipPath, extractPath);
                    Thread.Sleep(1000);
                    CopyFolder(source, dest);
                    Directory.Delete(extractPath, true);
                    Console.WriteLine("\n\nComplete. Starting program");
                    Thread.Sleep(1400);
                    Process.Start(args[0] + ".exe");
                }
            }
            else
            {
                Console.WriteLine("Please indicate the name of Archive in launch parametrs!\nPress any button to continue...");
                Console.ReadKey();
            }

        }
        static public void CopyFolder(string sourceFolder, string destFolder)
        {
            if (!Directory.Exists(destFolder))
            {
                Directory.CreateDirectory(destFolder);
                Console.WriteLine("Directory created" + destFolder);
            }

            string[] files = Directory.GetFiles(sourceFolder);

            foreach (string file in files)
            {
                try
                {
                    //if (file == "updater.exe") return;
                    string name = Path.GetFileName(file);
                    Console.WriteLine("Copy: " + name);
                    string dest = Path.Combine(destFolder, name);
                    if (File.Exists(dest))
                    {
                        File.Replace(file, dest, null);
                        Console.WriteLine("Replaced: " + file);
                    }
                    else
                    {
                        File.Move(file, dest);
                        Console.WriteLine("Copied: " + file);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(file + " " + ex.Message);
                }
            }
            string[] folders = Directory.GetDirectories(sourceFolder);
            foreach (string folder in folders)
            {
                string name = Path.GetFileName(folder);
                string dest = Path.Combine(destFolder, name);
                CopyFolder(folder, dest);
            }
        }
        static void LogoPrint()
        {
            string[] title = new string[] { @"  _____ _____  _   _           _       _                 ", @" | ____|__  / | | | |_ __   __| | __ _| |_ ___ _ __      ",
            @" |  _|   / /  | | | | '_ \ / _` |/ _` | __/ _ \ '__|     " ,@" | |___ / /_  | |_| | |_) | (_| | (_| | ||  __/ |        ",@" |_____/____|  \___/| .__/ \__,_|\__,_|\__\___|_|        ",
            @"  _             ___ |_|             ____  _          _ _ ",@" | |__  _   _  |_ _|_ __ ___  _ __ / ___|| |__   ___| | |",@" | '_ \| | | |  | || '__/ _ \| '_ \\___ \| '_ \ / _ \ | |",
            @" | |_) | |_| |  | || | | (_) | | | |___) | | | |  __/ | |",@" |_.__/ \__, | |___|_|  \___/|_| |_|____/|_| |_|\___|_|_|",@"        |___/                                            "};
            for (int o = 0; o < title.Length; o++)
            {
                Console.WriteLine(title[o]);
                Thread.Sleep(12);
            }
            Thread.Sleep(200);
            Console.WriteLine("Version: " + "1.0.1.0");
        }

    }
}



/* Console.WriteLine(@"
_____ _____  _   _           _       _                 
| ____|__  / | | | |_ __   __| | __ _| |_ ___ _ __      
|  _|   / /  | | | | '_ \ / _` |/ _` | __/ _ \ '__|     
| |___ / /_  | |_| | |_) | (_| | (_| | ||  __/ |        
|_____/____|  \___/| .__/ \__,_|\__,_|\__\___|_|        
_             ___ |_|             ____  _          _ _ 
| |__  _   _  |_ _|_ __ ___  _ __ / ___|| |__   ___| | |
| '_ \| | | |  | || '__/ _ \| '_ \\___ \| '_ \ / _ \ | |
| |_) | |_| |  | || | | (_) | | | |___) | | | |  __/ | |
|_.__/ \__, | |___|_|  \___/|_| |_|____/|_| |_|\___|_|_|
|___/                                            ");*/
