using ChatSys.MessageServer;
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

namespace ChatSys.SecWindows
{
    /// <summary>
    /// Interaktionslogik für Wpf_PipeConnectWindow.xaml
    /// </summary>
    public partial class Wpf_PipeConnectWindow : Window
    {
        private string messageReceievd;
        public string _findword;
        public string _afterJumps1;
        public string _afterJumps2;
        public string _afterJumps3;


        public Wpf_PipeConnectWindow()
        {
            InitializeComponent();

            // https://www.codeproject.com/Tips/441841/Csharp-Named-Pipes-with-Async

            string Sender = "";

            string appDirectory = System.AppContext.BaseDirectory;
            txtbx_AppDirectory.Text = appDirectory;

            DateTime today = DateTime.Now;
            string datetime = today.ToString("yyyy-MM-dd_HH:mm:ss");
            lbl_DateTime.Content = datetime;

            Ini_Managment(Sender, new RoutedEventArgs());

            Combobox_Fill(Sender, new RoutedEventArgs());

            GetLocalIPAddress(Sender, new RoutedEventArgs());

            MessageServer_SendMessageException(Sender, new RoutedEventArgs());


        }

        private void Combobox_Fill(string sender, RoutedEventArgs routedEventArgs)
        {
            ///-------------------------------------------------------------------------------------
            cmbbx_Message_Category.Items.Add("LogWrite");
            cmbbx_Message_Category.Items.Add("AdjustWrite_toIni");
            cmbbx_Message_Category.Items.Add("AdjustWrite_toProgr");
            cmbbx_Message_Category.Items.Add("ActionStarter");
            ///-------------------------------------------------------------------------------------

            cmbbx_Message_Server_ActionNeedet.Items.Add("true");
            cmbbx_Message_Server_ActionNeedet.Items.Add("false");
            ///-------------------------------------------------------------------------------------

            cmbbx_Message_Client_ActionNeedet.Items.Add("true");
            cmbbx_Message_Client_ActionNeedet.Items.Add("false");
            ///-------------------------------------------------------------------------------------
            ///
            cmbbx_Message_Client_AnswerNeedet.Items.Add("true");
            cmbbx_Message_Client_AnswerNeedet.Items.Add("false");
        }

        private void GetLocalIPAddress(string sender, RoutedEventArgs routedEventArgs)
        {

            string localIP;
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 65530);
                IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                localIP = endPoint.Address.ToString();

                txtbx_Local_IP.Text = localIP;
            }




        }


        private void MessageServer_SendMessageException(string Sender, RoutedEventArgs routedEventArgs)
        {
            // string sender = "";
            //--------------------------------------------------------------------
            MessageServer1.SendMessageException += (sender, e) =>
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                new Action(() =>
                {
                    //MessageBox.Show(e.Exception.Message);
                    string appDirectory = System.AppContext.BaseDirectory;

                    DateTime today = DateTime.Now;
                    string datetime = today.ToString("yyyy-MM-dd_HH:mm:ss");

                    Listbox_Outbox.Items.Add(datetime + " - " + " --> Error MessageServer1 :  " + e.Exception.Message);
                    Listbox_Scroll_to_End_Click(sender, new RoutedEventArgs());

                    // if the sender is the listening pipe then make it so it can be started again 
                    if (sender != null && sender.ToString() == this.txtListenPipeName.Text)
                    {
                        this.butListen.IsEnabled = true;
                    }
                }));
            };




        }

        private void Ini_Managment(object sender, RoutedEventArgs routedEventArgs)
        {
            try
            {
                //lade ini files-------------------------------------------------
                string appDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
                string Pipe_Managment_ini_folder_file = (appDirectory + "Data/Pipe_Managment.ini");


                /*
                 * Pipe_Managment.ini
                 * 
                [Server_Names]
                Name1 = Pipe_DesktopIcon_Action
                Name2 = Pipe_Logging_Writer
                Name3 = Pipe_DM_AutoRestarter
                Name4 = Pipe_DM_AutoBackuper
                Name5 = Pipe_DM_AutoUpdater
                Name6 = Pipe_Ini_Read_Writer

                [Client_Names]
                Name1 = Pipe_DesktopManager_Maine
                
                [Client_AutoStart]
                Answer=Yes

                [Server_LOCAL_SERVER]
                Adress= .

                [Server_Send_Sleep_Time]
                Time_ms=250

                [Server_DEFAULT_TIME_OUT]
                Time_ms=250

                [Client_WatchReturn_Sleep_Time]
                Time_ms=250
                */

                INIFile inifile1 = new INIFile(Pipe_Managment_ini_folder_file, true);
                string value1 = inifile1.GetValue("Server_Names", "Name1");
                string value2 = inifile1.GetValue("Server_Names", "Name2");
                string value3 = inifile1.GetValue("Server_Names", "Name3");
                string value4 = inifile1.GetValue("Server_Names", "Name4");
                string value5 = inifile1.GetValue("Server_Names", "Name5");
                string value6 = inifile1.GetValue("Server_Names", "Name6");

                txtbx_Server1_SendPipeName1.Text = value1;
                txtbx_Server2_SendPipeName2.Text = value2;
                txtbx_Server3_SendPipeName3.Text = value3;
                txtbx_Server4_SendPipeName4.Text = value4;
                txtbx_Server5_SendPipeName5.Text = value5;
                txtbx_Server6_SendPipeName6.Text = value6;

                string value10 = inifile1.GetValue("Server_LOCAL_SERVER", "Adress");
                txtListenPipeName.Text = value10;

                string value11 = inifile1.GetValue("Server_Send_Sleep_Time", "Time_ms");
                txtListenPipeName.Text = value11;

                string value12 = inifile1.GetValue("Server_DEFAULT_TIME_OUT", "Time_ms");
                txtListenPipeName.Text = value12;

                string value13 = inifile1.GetValue("Client_WatchReturn_Sleep_Time", "Time_ms");
                txtListenPipeName.Text = value13;

                string value14 = inifile1.GetValue("Client_Names", "Name1");
                txtListenPipeName.Text = value14;


                string value21 = inifile1.GetValue("Client_AutoStart", "Answer");
                if (value21 == "Yes")
                {
                    txtbx_Client_Autostart_YesNo.Text = value21;
                    string Sender = "";
                    butListen_Click(Sender, new RoutedEventArgs());
                }
                if (value21 == "No")
                {
                    txtbx_Client_Autostart_YesNo.Text = value21;
                }
            }

            catch (Exception ex)
            {
                //await SmallLogAllTogether.Logger("Ini_Managment - Error =\r\n" + ex.Message);
                //await SmallLogError.Logger("Ini_Managment - Error =\r\n" + ex.Message);
                System.Windows.MessageBox.Show("PipeManagment -- Ini_Managment - Error =\r\n" + ex.Message);
            }
        }



        /// <summary>
        /// Send Message Test.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void butSenden_Server1_Click(object sender, RoutedEventArgs e)
        {
            var pipeName1 = txtbx_Server1_SendPipeName1.Text;
            string message1 = txtbx_Server1_Send_Text1.Text;

            //----------------------------------------------------------------------------------------------
            string LOCAL_SERVER = txtbx_Server_LOCAL_SERVER.Text;

            string Server_Send_Sleep_Time = txtbx_Server_Send_Sleep_Time.Text;
            int int_Server_Send_Sleep_Time = Convert.ToInt32(Server_Send_Sleep_Time);

            string Server_DEFAULT_TIME_OUT = txtbx_Server_DEFAULT_TIME_OUT.Text;
            int int_Server_DEFAULT_TIME_OUT = Convert.ToInt32(Server_DEFAULT_TIME_OUT);
            //----------------------------------------------------------------------------------------------

            DateTime today = DateTime.Now;
            string datetime = today.ToString("yyyy-MM-dd_HH:mm:ss");

            Listbox_Outbox.Items.Add(datetime + " - Server1 - " + pipeName1 + " - PipeServer Send Message :   " + message1);
            Listbox_Scroll_to_End_Click(sender, new RoutedEventArgs());

            //var message = String.Format("Sending to pipe:={0}\nListening on pipe:={1}",txtSendPipeName.Text,txtListenPipeName.Text);

            TaskEx.Run(() => MessageServer1.SendMessageAsync(pipeName1, message1, LOCAL_SERVER, int_Server_Send_Sleep_Time, int_Server_DEFAULT_TIME_OUT));
            //----------------------------------------------------------------------------------------------
        }

        private void butSenden_Server2_Click(object sender, RoutedEventArgs e)
        {
            var pipeName2 = txtbx_Server2_SendPipeName2.Text;
            string message2 = txtbx_Server2_Send_Text2.Text;

            //----------------------------------------------------------------------------------------------
            string LOCAL_SERVER = txtbx_Server_LOCAL_SERVER.Text;

            string Server_Send_Sleep_Time = txtbx_Server_Send_Sleep_Time.Text;
            int int_Server_Send_Sleep_Time = Convert.ToInt32(Server_Send_Sleep_Time);

            string Server_DEFAULT_TIME_OUT = txtbx_Server_DEFAULT_TIME_OUT.Text;
            int int_Server_DEFAULT_TIME_OUT = Convert.ToInt32(Server_DEFAULT_TIME_OUT);
            //----------------------------------------------------------------------------------------------

            DateTime today = DateTime.Now;
            string datetime = today.ToString("yyyy-MM-dd_HH:mm:ss");

            Listbox_Outbox.Items.Add(datetime + " - Server2 - " + pipeName2 + " - PipeServer Send Message :   " + message2);
            Listbox_Scroll_to_End_Click(sender, new RoutedEventArgs());

            //var message = String.Format("Sending to pipe:={0}\nListening on pipe:={1}",txtSendPipeName.Text,txtListenPipeName.Text);

            TaskEx.Run(() => MessageServer2.SendMessageAsync(pipeName2, message2, LOCAL_SERVER, int_Server_Send_Sleep_Time, int_Server_DEFAULT_TIME_OUT));
            //----------------------------------------------------------------------------------------------

        }

        private void butSenden_Server3_Click(object sender, RoutedEventArgs e)
        {
            var pipeName3 = txtbx_Server3_SendPipeName3.Text;
            string message3 = txtbx_Server3_Send_Text3.Text;

            //----------------------------------------------------------------------------------------------
            string LOCAL_SERVER = txtbx_Server_LOCAL_SERVER.Text;

            string Server_Send_Sleep_Time = txtbx_Server_Send_Sleep_Time.Text;
            int int_Server_Send_Sleep_Time = Convert.ToInt32(Server_Send_Sleep_Time);

            string Server_DEFAULT_TIME_OUT = txtbx_Server_DEFAULT_TIME_OUT.Text;
            int int_Server_DEFAULT_TIME_OUT = Convert.ToInt32(Server_DEFAULT_TIME_OUT);
            //----------------------------------------------------------------------------------------------

            DateTime today = DateTime.Now;
            string datetime = today.ToString("yyyy-MM-dd_HH:mm:ss");

            Listbox_Outbox.Items.Add(datetime + " - Server3 - " + pipeName3 + " - PipeServer Send Message :   " + message3);
            Listbox_Scroll_to_End_Click(sender, new RoutedEventArgs());

            //var message = String.Format("Sending to pipe:={0}\nListening on pipe:={1}",txtSendPipeName.Text,txtListenPipeName.Text);

            TaskEx.Run(() => MessageServer3.SendMessageAsync(pipeName3, message3, LOCAL_SERVER, int_Server_Send_Sleep_Time, int_Server_DEFAULT_TIME_OUT));
            //----------------------------------------------------------------------------------------------

        }

        private void butSenden_Server4_Click(object sender, RoutedEventArgs e)
        {
            var pipeName4 = txtbx_Server4_SendPipeName4.Text;
            string message4 = txtbx_Server4_Send_Text4.Text;

            //----------------------------------------------------------------------------------------------
            string LOCAL_SERVER = txtbx_Server_LOCAL_SERVER.Text;

            string Server_Send_Sleep_Time = txtbx_Server_Send_Sleep_Time.Text;
            int int_Server_Send_Sleep_Time = Convert.ToInt32(Server_Send_Sleep_Time);

            string Server_DEFAULT_TIME_OUT = txtbx_Server_DEFAULT_TIME_OUT.Text;
            int int_Server_DEFAULT_TIME_OUT = Convert.ToInt32(Server_DEFAULT_TIME_OUT);
            //----------------------------------------------------------------------------------------------

            DateTime today = DateTime.Now;
            string datetime = today.ToString("yyyy-MM-dd_HH:mm:ss");

            Listbox_Outbox.Items.Add(datetime + " - Server4 - " + pipeName4 + " - PipeServer Send Message :   " + message4);
            Listbox_Scroll_to_End_Click(sender, new RoutedEventArgs());

            //var message = String.Format("Sending to pipe:={0}\nListening on pipe:={1}",txtSendPipeName.Text,txtListenPipeName.Text);

            TaskEx.Run(() => MessageServer4.SendMessageAsync(pipeName4, message4, LOCAL_SERVER, int_Server_Send_Sleep_Time, int_Server_DEFAULT_TIME_OUT));
            //----------------------------------------------------------------------------------------------

        }

        private void butSenden_Server5_Click(object sender, RoutedEventArgs e)
        {
            var pipeName5 = txtbx_Server5_SendPipeName5.Text;
            string message5 = txtbx_Server5_Send_Text5.Text;

            //----------------------------------------------------------------------------------------------
            string LOCAL_SERVER = txtbx_Server_LOCAL_SERVER.Text;

            string Server_Send_Sleep_Time = txtbx_Server_Send_Sleep_Time.Text;
            int int_Server_Send_Sleep_Time = Convert.ToInt32(Server_Send_Sleep_Time);

            string Server_DEFAULT_TIME_OUT = txtbx_Server_DEFAULT_TIME_OUT.Text;
            int int_Server_DEFAULT_TIME_OUT = Convert.ToInt32(Server_DEFAULT_TIME_OUT);
            //----------------------------------------------------------------------------------------------

            DateTime today = DateTime.Now;
            string datetime = today.ToString("yyyy-MM-dd_HH:mm:ss");

            Listbox_Outbox.Items.Add(datetime + " - Server5 - " + pipeName5 + " - PipeServer Send Message :   " + message5);
            Listbox_Scroll_to_End_Click(sender, new RoutedEventArgs());

            //var message = String.Format("Sending to pipe:={0}\nListening on pipe:={1}",txtSendPipeName.Text,txtListenPipeName.Text);

            TaskEx.Run(() => MessageServer5.SendMessageAsync(pipeName5, message5, LOCAL_SERVER, int_Server_Send_Sleep_Time, int_Server_DEFAULT_TIME_OUT));
            //----------------------------------------------------------------------------------------------

        }

        private void butSenden_Server6_Click(object sender, RoutedEventArgs e)
        {
            var pipeName6 = txtbx_Server6_SendPipeName6.Text;
            string message6 = txtbx_Server6_Send_Text6.Text;

            //----------------------------------------------------------------------------------------------
            string LOCAL_SERVER = txtbx_Server_LOCAL_SERVER.Text;

            string Server_Send_Sleep_Time = txtbx_Server_Send_Sleep_Time.Text;
            int int_Server_Send_Sleep_Time = Convert.ToInt32(Server_Send_Sleep_Time);

            string Server_DEFAULT_TIME_OUT = txtbx_Server_DEFAULT_TIME_OUT.Text;
            int int_Server_DEFAULT_TIME_OUT = Convert.ToInt32(Server_DEFAULT_TIME_OUT);
            //----------------------------------------------------------------------------------------------

            DateTime today = DateTime.Now;
            string datetime = today.ToString("yyyy-MM-dd_HH:mm:ss");

            Listbox_Outbox.Items.Add(datetime + " - Server6 - " + pipeName6 + " - PipeServer Send Message :   " + message6);
            Listbox_Scroll_to_End_Click(sender, new RoutedEventArgs());

            //var message = String.Format("Sending to pipe:={0}\nListening on pipe:={1}",txtSendPipeName.Text,txtListenPipeName.Text);

            TaskEx.Run(() => MessageServer6.SendMessageAsync(pipeName6, message6, LOCAL_SERVER, int_Server_Send_Sleep_Time, int_Server_DEFAULT_TIME_OUT));
            //----------------------------------------------------------------------------------------------

        }
        /// <summary>
        /// Listen Test.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void butListen_Click(object sender, RoutedEventArgs e)
        {
            var pipeName = txtListenPipeName.Text;

            //--------------------------------------------------------------------------------------------------------------------------------
            string Client_WatchReturn_Sleep_Time = txtbx_Client_WatchReturn_Sleep_Time.Text;
            int int_Client_WatchReturn_Sleep_Time = Convert.ToInt32(Client_WatchReturn_Sleep_Time);
            //--------------------------------------------------------------------------------------------------------------------------------


            this.butListen.IsEnabled = false;

            TaskEx.Run(() => MessageServer1.StartListeningAsync(pipeName, int_Client_WatchReturn_Sleep_Time, messageReceievd =>
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                new Action(() =>
                {
                    /*
                    var message = String.Format("Original Message:=\n\n{0}\nServer details:=\n\nSending to pipe:={1}\nListening on pipe:={2}",
                            messageReceievd,
                            txtSendPipeName.Text,
                            txtListenPipeName.Text);
                    */
                    //MessageBox.Show(this, message, "Message Receieved");
                    //-------------
                    txtbx_Empfangen_Text.Text = messageReceievd;

                    DateTime today = DateTime.Now;
                    string datetime = today.ToString("yyyy-MM-dd_HH:mm:ss");

                    Listbox_Outbox.Items.Add(datetime + " - " + "PipeClient Listen Message :   " + messageReceievd);
                    Listbox_Outbox.Items.Add(".");
                    Listbox_Scroll_to_End_Click(sender, new RoutedEventArgs());


                    Check_String(sender, messageReceievd, new RoutedEventArgs());



                    //-------------
                    //-------------


                }));
            }));
        }

        private async void Check_String(object sender, string messageReceievd, RoutedEventArgs routedEventArgs)
        {
            try
            {
                /*

                //---------------------------------------------------------------------------------------------
                //    { Log } SmallLogAllTogether [Hello] 
                //---------------------------------------------------------------------------------------------
                string SearchItem1 = "Log";
                if (messageReceievd.Contains(SearchItem1) == true)
                {
                    txtbx_Found_Command.Text = SearchItem1;
                    Listbox_Outbox.Items.Add("  --> Found_Command =  " + SearchItem1);
                    Listbox_Scroll_to_End_Click(sender, new RoutedEventArgs());

                    //---------------------------------------------------------------------------------------------
                    string SearchItem2 = "SmallLogAllTogether";
                    if (messageReceievd.Contains(SearchItem2) == true)
                    {
                        txtbx_Variable_after1.Text = SearchItem2;
                        Listbox_Outbox.Items.Add("  --> Variable_after1 =  " + SearchItem2);
                        Listbox_Scroll_to_End_Click(sender, new RoutedEventArgs());

                    }
                    //---------------------------------------------------------------------------------------------

                    //---------------------------------------------------------------------------------------------
                    Match mtch = Regex.Match(messageReceievd, @"\[((\s*?.*?)*?)\]");
                    if (mtch.Success)
                    {
                        _findword = (mtch.Groups[1].Value);
                        txtbx_Variable_after2.Text = _findword;
                        Listbox_Outbox.Items.Add("  --> Variable_after2 =  " + _findword);
                        Listbox_Outbox.Items.Add(".");
                        Listbox_Scroll_to_End_Click(sender, new RoutedEventArgs());
                    }
                    //---------------------------------------------------------------------------------------------


                    if (SearchItem2 == "SmallLogAllTogether") //wenn eintrag gefunden wird, mache aktion...
                    {
                        string LogEntry = ("MainePipe SendLog  =   " + _findword);
                        await SmallLogAllTogether.Logger(LogEntry);

                        Listbox_Outbox.Items.Add("  <-Action-> Write Log <-SmallLogAllTogether-> " + LogEntry);
                        Listbox_Outbox.Items.Add(".");
                        Listbox_Scroll_to_End_Click(sender, new RoutedEventArgs());
                    }


                }
                //---------------------------------------------------------------------------------------------

                //---------------------------------------------------------------------------------------------
                //    { Adj } System [Test - Name = Andy] 
                //---------------------------------------------------------------------------------------------
                string SearchItem11 = "Adj";
                if (messageReceievd.Contains(SearchItem11) == true)
                {
                    txtbx_Found_Command.Text = SearchItem11;
                    Listbox_Outbox.Items.Add("  --> Found_Command =  " + SearchItem11);
                    Listbox_Scroll_to_End_Click(sender, new RoutedEventArgs());

                    //---------------------------------------------------------------------------------------------
                    string SearchItem12 = "System";
                    if (messageReceievd.Contains(SearchItem12) == true)
                    {
                        txtbx_Variable_after1.Text = SearchItem12;
                        Listbox_Outbox.Items.Add("  --> Variable_after1 =  " + SearchItem12);
                        Listbox_Scroll_to_End_Click(sender, new RoutedEventArgs());

                    }
                    //---------------------------------------------------------------------------------------------

                    //---------------------------------------------------------------------------------------------
                    Match mtch = Regex.Match(messageReceievd, @"\[((\s*?.*?)*?)\]");
                    if (mtch.Success)
                    {
                        _findword = (mtch.Groups[1].Value);
                        txtbx_Variable_after2.Text = _findword;
                        Listbox_Outbox.Items.Add("  --> Variable_after2 =  " + _findword);
                        Listbox_Outbox.Items.Add(".");
                        Listbox_Scroll_to_End_Click(sender, new RoutedEventArgs());


                        // MessageBox.Show("variable = " + _findword);
                        var theString = _findword;

                        if (theString.Contains(','))
                        {
                            string theString1 = theString.Split(',')[0]; //all bevore charter - 0
                                                                         // MessageBox.Show("theString2 = " + theString1);
                            _afterJumps1 = theString1;

                            string theString2 = theString.Split(',')[1]; //all avter charter - 1
                                                                         // MessageBox.Show("theString3 = " + theString2);
                            _afterJumps2 = theString2;

                            string theString3 = theString.Split(',')[2]; //all avter charter - 1
                                                                         // MessageBox.Show("theString3 = " + theString3);
                            _afterJumps3 = theString3;
                        }


                        Listbox_Outbox.Items.Add("  --> Split-1 <--  " + _afterJumps1);
                        Listbox_Outbox.Items.Add("  --> Split-2 <--  " + _afterJumps2);
                        Listbox_Outbox.Items.Add("  --> Split-3 <--  " + _afterJumps3);

                        Listbox_Outbox.Items.Add(".");
                        Listbox_Scroll_to_End_Click(sender, new RoutedEventArgs());
                    }
                    //---------------------------------------------------------------------------------------------


                    if (SearchItem12 == "System") //wenn eintrag gefunden wird, mache aktion...
                    {

                        string system_ini_file = ("System.ini");
                        string appDirectory = System.AppContext.BaseDirectory;
                        string system_ini_fileFolder = (appDirectory + "\\Data\\" + system_ini_file);

                        string EntryName = _afterJumps1;
                        string keyword = _afterJumps2;
                        string value = _afterJumps3;

                        if (string.IsNullOrEmpty(EntryName)) { MessageBox.Show("Abbruch *Einstellung schreiben in ini - EntryName \r\n" + EntryName); return; }
                        if (string.IsNullOrEmpty(keyword)) { MessageBox.Show("Abbruch *Einstellung schreiben in ini - keyword \r\n" + keyword); return; }
                        if (string.IsNullOrEmpty(value)) { MessageBox.Show("Abbruch *Einstellung schreiben in ini - value \r\n" + value); return; }



                        //string Answer = "";
                        //string sys_ini_folder_file = (appDirectory + "Data/System.ini");
                        //INIFile inifile33 = new INIFile(sys_ini_folder_file, true);
                        //await inifile33.SetValueAsyncAsync("Prozesse_Start_erlaubt_DM_PrAutorestarter_Yes_No", "Answer", Answer);

                        INIFile inifile1 = new INIFile(system_ini_fileFolder, true);
                        await inifile1.SetValueAsyncAsync(EntryName, keyword, value);

                        Listbox_Outbox.Items.Add("  <-Action-> Write in inifile <-System.ini-> " + EntryName + " - " + keyword + " - " + value);
                        Listbox_Outbox.Items.Add(".");
                        Listbox_Scroll_to_End_Click(sender, new RoutedEventArgs());
                    }

                }
                //---------------------------------------------------------------------------------------------
                */
            }
            catch (Exception ex)
            {
                //MessageBox.Show("PrAutoRestart_Start_Click = Error =\r\n" + CompleteAppfolder + "\r\n" + ex.Message);
                Listbox_Outbox.Items.Add("--");
                Listbox_Outbox.Items.Add("butListen_Click  -> Check_String = Error =\r\n" + messageReceievd + "\r\n" + ex.Message);
                Listbox_Outbox.Items.Add("--");
            }

        }



        private void New_Window(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
        }

        private void Close(object sender, RoutedEventArgs e)
        {

            this.Close();
            Application.Current.Shutdown();
            // System.Windows.Application.Current.Shutdown();
        }

        private void Restart(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            this.Close();
            Application.Current.Shutdown();
            // System.Windows.Application.Current.Shutdown();
        }

        private void Listbox_Scroll_to_End_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Listbox_Outbox.SelectedIndex = Listbox_Outbox.Items.Count - 1;
                Listbox_Outbox.ScrollIntoView(Listbox_Outbox.SelectedItem);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Listbox_Scroll_to_End_Click - Error =\r\n" + ex.Message);
            }
        }

        private void Clear_Listbox(object sender, RoutedEventArgs e)
        {
            Listbox_Outbox.Items.Clear();
        }

        private void Client_Neme_toall_Server_Click(object sender, RoutedEventArgs e)
        {
            txtbx_Server1_SendPipeName1.Text = txtListenPipeName.Text;
            txtbx_Server2_SendPipeName2.Text = txtListenPipeName.Text;
            txtbx_Server3_SendPipeName3.Text = txtListenPipeName.Text;
            txtbx_Server4_SendPipeName4.Text = txtListenPipeName.Text;
            txtbx_Server5_SendPipeName5.Text = txtListenPipeName.Text;
            txtbx_Server6_SendPipeName6.Text = txtListenPipeName.Text;
            txtbx_Server7_SendPipeName7.Text = txtListenPipeName.Text;
        }

        private void Client_Restart_Click(object sender, RoutedEventArgs e)
        {
            butListen_Click(sender, new RoutedEventArgs());
        }

        private void Set_thisNetAdress_as_Server(object sender, RoutedEventArgs e)
        {
            txtbx_Server_LOCAL_SERVER.Text = txtbx_Local_IP.Text;
        }


        private void Open_Directory(object sender, RoutedEventArgs e)
        {
            string open_Appfolder = txtbx_AppDirectory.Text;


            if (!(Directory.Exists(open_Appfolder)))
            {
                MessageBox.Show("Ordner *Appfolder* existiert nicht - bitte anlegen: \r\n" + open_Appfolder);
                return;
            }
            //MessageBox.Show("Datei *Exec-Comman-existiert");

            try
            {
                Process.Start("explorer.exe", open_Appfolder);
            }
            catch (Exception ex)
            {
                MessageBox.Show("open_Appfolder = Error =\r\n" + open_Appfolder + "\r\n" + ex.Message);
                // await SmallLogError.Logger("open_Appfolder = Error =\r\n" + open_Appfolder + "\r\n" + ex.Message);
                // await SmallLogAllTogether.Logger("open_Appfolder = Error =\r\n" + open_Appfolder + "\r\n" + ex.Message);
            }
        }

        private void Set_default_IP_Server_LOCAL_SERVER(object sender, RoutedEventArgs e)
        {
            txtbx_Server_LOCAL_SERVER.Text = ".";
        }

        private void cmbbx_Message_Category_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //wenn kein Eintrag selektiert wurde dann....
                if (cmbbx_Message_Category.SelectedIndex == -1)
                {
                    txtbx_Message_Category.Text = string.Empty; // txtbox leeren String einfügen
                }
                else
                {
                    txtbx_Message_Category.Text = cmbbx_Message_Category.SelectedItem.ToString();
                }
            }
            catch (Exception ex)
            {
                //await SmallLogAllTogether.Logger("CmboBx_Load_Ini_File_Show_all_txtfiles_SelectionChanged - Error =\r\n" + ex.Message);
                //await SmallLogError.Logger("CmboBx_Load_Ini_File_Show_all_txtfiles_SelectionChanged - Error =\r\n" + ex.Message);
                System.Windows.MessageBox.Show("cmbbx_Message_Category_SelectionChanged - Error =\r\n" + ex.Message);
            }

        }



        private void Create_lsonString_Click(object sender, RoutedEventArgs e)
        {
            //serializes ---------------------------------------------
            try
                {
            Message_Translator MessTransl = new Message_Translator
            {
                
                    MessageCategory = txtbx_Message_Category.Text,
                    Server_ActionNeedet = txtbx_Message_Server_ActionNeedet.Text,
                    //bool mit großem true?????? soll klein
                    Server_IP = txtbx_Message_Server_IP.Text,
                    Server_PipeName = txtbx_Message_Server_PipeName.Text,

                    value1 = txtbx_Message_Value1.Text,
                    value2 = txtbx_Message_Value2.Text,
                    value3 = txtbx_Message_Value3.Text,
                    value4 = txtbx_Message_Value4.Text,
                    value5 = txtbx_Message_Value5.Text,

                    Client_ActionNeedet = txtbx_Message_Client_ActionNeedet.Text,
                    Client_AnswerNeedet = txtbx_Message_Client_AnswerNeedet.Text,
                    };

                    string jsonString = JsonConvert.SerializeObject(MessTransl, Formatting.Indented);

                    txtbx_Message_Created_jsonString.Text = jsonString;


                    Listbox_Outbox.Items.Add(".");
                    Listbox_Outbox.Items.Add("*** Create JsonString ***");
                    Listbox_Outbox.Items.Add(jsonString);
                    Listbox_Scroll_to_End_Click(sender, new RoutedEventArgs());
        }
                catch (Exception ex)
                {
                MessageBox.Show("Create_lsonString_Click = Error =\r\n" + ex.Message);
                //await SmallLogError.Logger("txtbx_Mousecatch_Canvas_Value_Top_Y_TextChanged = Error =\r\n" + ex.Message);
                //await SmallLogAllTogether.Logger("txtbx_Mousecatch_Canvas_Value_Top_Y_TextChanged = Error =\r\n" + ex.Message);
    }
    }

    private void Read_lsonString_Click(object sender, RoutedEventArgs e)
        {
            //Deserializes ---------------------------------------------

            string jsonString = txtbx_Message_Created_jsonString.Text;

            Message_Translator MessTransl = JsonConvert.DeserializeObject<Message_Translator>(jsonString);

            txtbx_catch_Message_Category.Text = (MessTransl.MessageCategory);
            txtbx_catch_Message_Server_ActionNeedet.Text = (MessTransl.Server_ActionNeedet).ToString();

            txtbx_catch_Message_Server_IP.Text = (MessTransl.Server_IP);
            txtbx_catch_Message_Server_PipeName.Text = (MessTransl.Server_PipeName);

            txtbx_catch_Message_Value1.Text = (MessTransl.value1);
            txtbx_catch_Message_Value2.Text = (MessTransl.value2);
            txtbx_catch_Message_Value3.Text = (MessTransl.value3);
            txtbx_catch_Message_Value4.Text = (MessTransl.value4);
            txtbx_catch_Message_Value5.Text = (MessTransl.value5);

            txtbx_catch_Message_Client_ActionNeedet.Text = (MessTransl.Client_ActionNeedet).ToString();
            txtbx_catch_Message_Client_AnswerNeedet.Text = (MessTransl.Client_AnswerNeedet).ToString();

        }

        private void Send_jsonString_to_Server1_Click(object sender, RoutedEventArgs e)
        {
            txtbx_Server1_Send_Text1.Text = txtbx_Message_Created_jsonString.Text;
        }

        private void Hide_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void cmbbx_Message_Server_ActionNeedet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //wenn kein Eintrag selektiert wurde dann....
                if (cmbbx_Message_Server_ActionNeedet.SelectedIndex == -1)
                {
                    txtbx_catch_Message_Server_ActionNeedet.Text = string.Empty; // txtbox leeren String einfügen
                }
                else
                {
                    txtbx_catch_Message_Server_ActionNeedet.Text = cmbbx_Message_Server_ActionNeedet.SelectedItem.ToString();
                }
            }
            catch (Exception ex)
            {
                //await SmallLogAllTogether.Logger("CmboBx_Load_Ini_File_Show_all_txtfiles_SelectionChanged - Error =\r\n" + ex.Message);
                //await SmallLogError.Logger("CmboBx_Load_Ini_File_Show_all_txtfiles_SelectionChanged - Error =\r\n" + ex.Message);
                System.Windows.MessageBox.Show("cmbbx_Message_Server_ActionNeedet_SelectionChanged - Error =\r\n" + ex.Message);
            }
        }

        private void cmbbx_Message_Client_ActionNeedet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //wenn kein Eintrag selektiert wurde dann....
                if (cmbbx_Message_Client_ActionNeedet.SelectedIndex == -1)
                {
                    txtbx_catch_Message_Client_ActionNeedet.Text = string.Empty; // txtbox leeren String einfügen
                }
                else
                {
                    txtbx_catch_Message_Client_ActionNeedet.Text = cmbbx_Message_Client_ActionNeedet.SelectedItem.ToString();
                }
            }
            catch (Exception ex)
            {
                //await SmallLogAllTogether.Logger("CmboBx_Load_Ini_File_Show_all_txtfiles_SelectionChanged - Error =\r\n" + ex.Message);
                //await SmallLogError.Logger("CmboBx_Load_Ini_File_Show_all_txtfiles_SelectionChanged - Error =\r\n" + ex.Message);
                System.Windows.MessageBox.Show("cmbbx_Message_Client_ActionNeedet_SelectionChanged - Error =\r\n" + ex.Message);
            }
        }

        private void cmbbx_Message_Client_AnswerNeedet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //wenn kein Eintrag selektiert wurde dann....
                if (cmbbx_Message_Client_AnswerNeedet.SelectedIndex == -1)
                {
                    txtbx_catch_Message_Client_AnswerNeedet.Text = string.Empty; // txtbox leeren String einfügen
                }
                else
                {
                    txtbx_catch_Message_Client_AnswerNeedet.Text = cmbbx_Message_Client_AnswerNeedet.SelectedItem.ToString();
                }
            }
            catch (Exception ex)
            {
                //await SmallLogAllTogether.Logger("CmboBx_Load_Ini_File_Show_all_txtfiles_SelectionChanged - Error =\r\n" + ex.Message);
                //await SmallLogError.Logger("CmboBx_Load_Ini_File_Show_all_txtfiles_SelectionChanged - Error =\r\n" + ex.Message);
                System.Windows.MessageBox.Show("cmbbx_Message_Client_AnswerNeedet_SelectionChanged - Error =\r\n" + ex.Message);
            }
        }

        private void txtbx_Message_Server_Number_TextChanged(object sender, TextChangedEventArgs e)
        {
            string ServerActionNeedet = txtbx_Message_Server_ActionNeedet.Text;
            string ServerNo = txtbx_Message_Server_Number.Text;

            if (ServerActionNeedet == "true")
            {
                txtbx_Message_Server_IP.Text = txtbx_Local_IP.Text;

                //----------------------------------------------------------------------------------------------------
                if (ServerNo == "S1")//(Desktop Links)
                {
                    txtbx_Message_Server_PipeName.Text = txtbx_Server1_SendPipeName1.Text;

                    //Serialisize
                    Create_lsonString_Click(sender, new RoutedEventArgs());

                  //  txtbx_Server1_Send_Text1.Text = "";
                  //  txtbx_Server1_Send_Text1.Text = txtbx_Message_Created_jsonString.Text;
                  //  string value = txtbx_Server1_Send_Text1.Text;
                  //  if (string.IsNullOrEmpty(value)) { MessageBox.Show("Abbruch *txtbx_Message_Server_Number_TextChanged s1 - value \r\n" + "Sendbox empty"); return; }

                    //Send
                  //  butSenden_Server1_Click(sender, new RoutedEventArgs());
                }
                //----------------------------------------------------------------------------------------------------
                if (ServerNo == "S2")//(Logging)
                {
                    txtbx_Message_Server_PipeName.Text = txtbx_Server2_SendPipeName2.Text;

                    //Serialisize
                    Create_lsonString_Click(sender, new RoutedEventArgs());

                  //  txtbx_Server2_Send_Text2.Text = "";
                  //  txtbx_Server2_Send_Text2.Text = txtbx_Message_Created_jsonString.Text;
                  //  string value = txtbx_Server2_Send_Text2.Text;
                 //   if (string.IsNullOrEmpty(value)) { MessageBox.Show("Abbruch *txtbx_Message_Server_Number_TextChanged s2 - value \r\n" + "Sendbox empty"); return; }

                    //Send
                 //  butSenden_Server2_Click(sender, new RoutedEventArgs());

                }
                //----------------------------------------------------------------------------------------------------
                if (ServerNo == "S3")//(DM_PrAutorestarter)
                {
                    txtbx_Message_Server_PipeName.Text = txtbx_Server3_SendPipeName3.Text;
                }
                //----------------------------------------------------------------------------------------------------
                if (ServerNo == "S4")//(DM_PrAutobackuper)
                {
                    txtbx_Message_Server_PipeName.Text = txtbx_Server4_SendPipeName4.Text;
                }
                //----------------------------------------------------------------------------------------------------
                if (ServerNo == "S5")//(DM_PrAutoUpdater)
                {
                    txtbx_Message_Server_PipeName.Text = txtbx_Server5_SendPipeName5.Text;
                }
                //----------------------------------------------------------------------------------------------------
                if (ServerNo == "S6")//(Ini_Read_Writer)
                {
                    txtbx_Message_Server_PipeName.Text = txtbx_Server6_SendPipeName6.Text;

                    //Serialisize
                    Create_lsonString_Click(sender, new RoutedEventArgs());

                //    txtbx_Server6_Send_Text6.Text = "";
                 //   txtbx_Server6_Send_Text6.Text = txtbx_Message_Created_jsonString.Text;
                 //   string value = txtbx_Server6_Send_Text6.Text;
                 //   if (string.IsNullOrEmpty(value)) { MessageBox.Show("Abbruch *txtbx_Message_Server_Number_TextChanged s6 - value \r\n" + "Sendbox empty"); return; }

                    //Send
                //    butSenden_Server6_Click(sender, new RoutedEventArgs());

                }
                //----------------------------------------------------------------------------------------------------
                if (ServerNo == "S7")//(WriteBack-AnswerServer)
                {
                    txtbx_Message_Server_PipeName.Text = txtbx_Server7_SendPipeName7.Text;
                }
            }


        }
    }
}