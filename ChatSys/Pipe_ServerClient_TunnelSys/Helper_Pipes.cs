using ChatSys.Pipe_ServerClient_TunnelSys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace ChatSys.Helper_Klassen
{
    class Helper_Pipes
    {

        //-CategoryNames----------------------------------------------------------
        // LogWrite  // AdjustWrite_toIni // AdjustWrite_toProgr // ActionStarter // InfoMsg  //


        //-PipeNames--------------------------------------------------------------
        // Pipe-Maine-to-Client-LogWrite

        // Pipe-Maine-to-Client-AdjustWrite_toIni
        // Pipe-Client-to-Maine-AdjustWrite_toProgr
        // Pipe-Maine-to-Client-AdjustWrite_toProgr

        // Pipe-Maine-to-Client-ActionStarter

        // Pipe-Maine-to-Client-InfoMsg
        // Pipe-Client-to-Maine-InfoMsg


        //-CategoryNames----------------------------------------------------------
        // LogWrite  // AdjustWrite_toIni // AdjustWrite_toProgr // ActionStarter // InfoMsg  //


        //-PipeNames--------------------------------------------------------------
        // Pipe-Maine-to-Client-LogWrite

        // Pipe-Maine-to-Client-AdjustWrite_toIni
        // Pipe-Client-to-Maine-AdjustWrite_toProgr
        // Pipe-Maine-to-Client-AdjustWrite_toProgr

        // Pipe-Maine-to-Client-ActionStarter

        // Pipe-Maine-to-Client-InfoMsg
        // Pipe-Client-to-Maine-InfoMsg

        public static async Task LogWrite(string LoggCategorie, string Message, string ActionServer, string ServerNo, string ActionClient, string AnswerClient)
        {
            await Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                            new Action(() =>
                            {
                            try {
                                    //---------------------------Greife Auf anderes Woindow zu---------------------------------------------------------------------------
                                    var targetWindow = Application.Current.Windows.Cast<Window>().FirstOrDefault(window => window is MainWindow) as MainWindow;

                                    //Clean
                                    targetWindow.PipeConWind.txtbx_Message_Category.Text = "";
                                    targetWindow.PipeConWind.txtbx_Message_Server_ActionNeedet.Text = "";
                                    targetWindow.PipeConWind.txtbx_Message_Server_IP.Text = "";
                                    targetWindow.PipeConWind.txtbx_Message_Server_PipeName.Text = "";
                                    targetWindow.PipeConWind.txtbx_Message_Server_Number.Text = "";
                                    targetWindow.PipeConWind.txtbx_Message_Value1.Text = "";
                                    targetWindow.PipeConWind.txtbx_Message_Value2.Text = "";
                                    targetWindow.PipeConWind.txtbx_Message_Value3.Text = "";
                                    targetWindow.PipeConWind.txtbx_Message_Value4.Text = "";
                                    targetWindow.PipeConWind.txtbx_Message_Value5.Text = "";
                                    targetWindow.PipeConWind.txtbx_Message_Client_ActionNeedet.Text = "";
                                    targetWindow.PipeConWind.txtbx_Message_Client_AnswerNeedet.Text = "";

                                    //Fill

                                    targetWindow.PipeConWind.txtbx_Message_Category.Text = "LogWrite";
                                    targetWindow.PipeConWind.txtbx_Message_Server_ActionNeedet.Text = ActionServer;
                                    // Server IP
                                    // Server PipeName
                                    targetWindow.PipeConWind.txtbx_Message_Server_Number.Text = ServerNo;
                                    // 
                                    targetWindow.PipeConWind.txtbx_Message_Value1.Text = LoggCategorie;
                                    targetWindow.PipeConWind.txtbx_Message_Value2.Text = Message;
                                    targetWindow.PipeConWind.txtbx_Message_Value3.Text = "-";
                                    targetWindow.PipeConWind.txtbx_Message_Value4.Text = "-";
                                    targetWindow.PipeConWind.txtbx_Message_Value5.Text = "-";
                                    //
                                    targetWindow.PipeConWind.txtbx_Message_Client_ActionNeedet.Text = ActionClient;
                                    targetWindow.PipeConWind.txtbx_Message_Client_AnswerNeedet.Text = AnswerClient;
                                }
                                catch (Exception ex)
                                { System.Windows.MessageBox.Show("Helper_Pipes.LogWrite - Error =\r\n" + ex.Message); }

                            }));
        }
   

        //---------------------------------------------------------------------------------------------------------------------------------


        //---------------------------------------------------------------------------------------------------------------------------------

        public static async Task AdjustWrite_toIni(string FileName, string Group, string Caller, string Value, string ActionServer, string ServerNo, string ActionClient, string AnswerClient)
        {
            await Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                            new Action(() =>
                            {

                                try 
                                { 
                                    //---------------------------Greife Auf anderes Woindow zu---------------------------------------------------------------------------
                                    var targetWindow = Application.Current.Windows.Cast<Window>().FirstOrDefault(window => window is MainWindow) as MainWindow;

                                    //Clean
                                    targetWindow.PipeConWind.txtbx_Message_Category.Text = "";
                                    targetWindow.PipeConWind.txtbx_Message_Server_ActionNeedet.Text = "";
                                    targetWindow.PipeConWind.txtbx_Message_Server_IP.Text = "";
                                    targetWindow.PipeConWind.txtbx_Message_Server_PipeName.Text = "";
                                    targetWindow.PipeConWind.txtbx_Message_Server_Number.Text = "";
                                    targetWindow.PipeConWind.txtbx_Message_Value1.Text = "";
                                    targetWindow.PipeConWind.txtbx_Message_Value2.Text = "";
                                    targetWindow.PipeConWind.txtbx_Message_Value3.Text = "";
                                    targetWindow.PipeConWind.txtbx_Message_Value4.Text = "";
                                    targetWindow.PipeConWind.txtbx_Message_Value5.Text = "";
                                    targetWindow.PipeConWind.txtbx_Message_Client_ActionNeedet.Text = "";
                                    targetWindow.PipeConWind.txtbx_Message_Client_AnswerNeedet.Text = "";

                                    //Fill

                                    targetWindow.PipeConWind.txtbx_Message_Category.Text = "AdjustWrite_toIni";
                                    targetWindow.PipeConWind.txtbx_Message_Server_ActionNeedet.Text = ActionServer;
                                    // Server IP
                                    // Server PipeName
                                    targetWindow.PipeConWind.txtbx_Message_Server_Number.Text = ServerNo;
                                    // 
                                    targetWindow.PipeConWind.txtbx_Message_Value1.Text = FileName;
                                    targetWindow.PipeConWind.txtbx_Message_Value2.Text = Caller;
                                    targetWindow.PipeConWind.txtbx_Message_Value3.Text = Group;
                                    targetWindow.PipeConWind.txtbx_Message_Value4.Text = Value;
                                    targetWindow.PipeConWind.txtbx_Message_Value5.Text = "-";
                                    //
                                    targetWindow.PipeConWind.txtbx_Message_Client_ActionNeedet.Text = ActionClient;
                                    targetWindow.PipeConWind.txtbx_Message_Client_AnswerNeedet.Text = AnswerClient;

                                }
                                catch (Exception ex)
                                { System.Windows.MessageBox.Show("Helper_Pipes.AdjustWrite_toIni - Error =\r\n" + ex.Message); }

        }));
        }

        //---------------------------------------------------------------------------------------------------------------------------------

        public static async Task ActionStarter(string RealButtonName, string Name, string Action, string ActionServer, string ServerNo, string ActionClient, string AnswerClient)
        {
            await Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                            new Action(() =>
                            {

                                try 
                                { 
                                    //---------------------------Greife Auf anderes Woindow zu---------------------------------------------------------------------------
                                    var targetWindow = Application.Current.Windows.Cast<Window>().FirstOrDefault(window => window is MainWindow) as MainWindow;

                                    //Clean
                                    targetWindow.PipeConWind.txtbx_Message_Category.Text = "";
                                    targetWindow.PipeConWind.txtbx_Message_Server_ActionNeedet.Text = "";
                                    targetWindow.PipeConWind.txtbx_Message_Server_IP.Text = "";
                                    targetWindow.PipeConWind.txtbx_Message_Server_PipeName.Text = "";
                                    targetWindow.PipeConWind.txtbx_Message_Server_Number.Text = "";
                                    targetWindow.PipeConWind.txtbx_Message_Value1.Text = "";
                                    targetWindow.PipeConWind.txtbx_Message_Value2.Text = "";
                                    targetWindow.PipeConWind.txtbx_Message_Value3.Text = "";
                                    targetWindow.PipeConWind.txtbx_Message_Value4.Text = "";
                                    targetWindow.PipeConWind.txtbx_Message_Value5.Text = "";
                                    targetWindow.PipeConWind.txtbx_Message_Client_ActionNeedet.Text = "";
                                    targetWindow.PipeConWind.txtbx_Message_Client_AnswerNeedet.Text = "";

                                    //Fill

                                    targetWindow.PipeConWind.txtbx_Message_Category.Text = "ActionStarter";
                                    targetWindow.PipeConWind.txtbx_Message_Server_ActionNeedet.Text = ActionServer;
                                    // Server IP
                                    // Server PipeName
                                    targetWindow.PipeConWind.txtbx_Message_Server_Number.Text = ServerNo;
                                    // 
                                    targetWindow.PipeConWind.txtbx_Message_Value1.Text = RealButtonName;
                                    targetWindow.PipeConWind.txtbx_Message_Value2.Text = Name;
                                    targetWindow.PipeConWind.txtbx_Message_Value3.Text = Action;
                                    targetWindow.PipeConWind.txtbx_Message_Value4.Text = "-";
                                    targetWindow.PipeConWind.txtbx_Message_Value5.Text = "-";
                                    //
                                    targetWindow.PipeConWind.txtbx_Message_Client_ActionNeedet.Text = ActionClient;
                                    targetWindow.PipeConWind.txtbx_Message_Client_AnswerNeedet.Text = AnswerClient;
                            }
                                catch (Exception ex)
                                { System.Windows.MessageBox.Show("Helper_Pipes.ActionStarter - Error =\r\n" + ex.Message); }


        }));
        }

    }
}
