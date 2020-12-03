using System;
using System.IO;
using System.Threading.Tasks;

namespace DesktopManager
{
    class SmallLogAllTogether
    {
        // Mögliche Varianten.....
        // await SmallLogAllTogether.Logger("Programm gestartet....");
        // await SmallLogCreate.Logger("Programm gestartet....");
        // await SmallLogError.Logger("Programm gestartet....");
        // await SmallLogSystem.Logger("Programm gestartet....");
        // await SmallLogUser.Logger("Programm gestartet....");

        public static async Task VerifyDir(string path)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(path);
                if (!dir.Exists)
                {
                    dir.Create();
                }
            }
            catch { }
        }

        public async static Task Logger(string lines)
        {
            string appDirectory = System.AppContext.BaseDirectory;
            string path = (appDirectory + @"Data\SmallLog\");

            //string path = "C:/Log/";
            VerifyDir(path);
            string fileName = DateTime.Now.Year.ToString() + "." + DateTime.Now.Month.ToString() + "." + DateTime.Now.Day.ToString() + "_Log_AllTogether.txt";

            string path_file = path + fileName;


            //Test async - verschlukt aber logging zeilen
            /*
            await Task.Run(() => WriteFile(path_file,lines));
            }
             internal static async void WriteFile(string path_file, string lines)
            {
           */


            try
            {
                System.IO.StreamWriter file = new System.IO.StreamWriter(path_file, true);
                file.WriteLine(DateTime.Now.ToString() + ": " + lines);
                file.Close();
            }
            catch (Exception) { }

        }


        internal static async Task Logger(string v, string message)
        {
            throw new NotImplementedException();
        }
    }
}
