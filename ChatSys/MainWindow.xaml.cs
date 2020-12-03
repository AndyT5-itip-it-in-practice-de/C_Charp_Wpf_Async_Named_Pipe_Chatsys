using ChatSys.Helper_Klassen;
using ChatSys.MessageServer;
using ChatSys.SecWindows;
using DesktopManager;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
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
using System.Windows.Threading;

namespace ChatSys
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        //initialisierung Datagrid-Fenster für komplettes fenster...
        //-------------------------------------------------------------------------------------------------------------------------------------
        public Wpf_PipeConnectWindow PipeConWind = new Wpf_PipeConnectWindow();
        //-------------------------------------------------------------------------------------------------------------------------------------


        public MainWindow()
        {
            InitializeComponent();

            this.Topmost = true;

        }

        private void Wpf_PipeConnectWindow_Click(object sender, RoutedEventArgs e)
        {
            // Wpf_PipeConnectWindow PipContr = new Wpf_PipeConnectWindow();  // neues - vorhandenes fenster laden
           // PipeConWind.Owner = this;
            PipeConWind.Show();
        }

        private async void Write_Log_Click(object sender, RoutedEventArgs e)
        {
            //public static async void LogWrite(string LoggCategorie, string Message, bool ActionServer, String ServerNo, bool ActionClient, bool AnswerClient)


           await  Helper_Pipes.LogWrite("SmallLogAllTogether", "Lalelu", "true", "S2", "true", "true");
        }

        private async void Write_Adjustments_to_ini_Click(object sender, RoutedEventArgs e)
        {
            //public static async void AdjustWrite_toIni(String FileName, string Group, string Caller, string Value, bool ActionServer, String ServerNo, bool ActionClient, bool AnswerClient)

            await Helper_Pipes.AdjustWrite_toIni("System", "Background_Color", "Name", "Red", "true", "s6", "true", "true");

        }

        private async void Run_Program_Click(object sender, RoutedEventArgs e)
        {

            await Helper_Pipes.ActionStarter("Button1", "Explorer", "Explorer", "true", "S1", "true", "true");
        }

    }
    

}

           