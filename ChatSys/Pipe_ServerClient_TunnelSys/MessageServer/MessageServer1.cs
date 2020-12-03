// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MessageServer.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the MessageServer type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
using ChatSys.Pipe_ServerClient_TunnelSys;
using System;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Windows;
using System.Windows.Threading;

namespace ChatSys
{
    /// <summary>
    /// A Named pipe message server. 
    /// </summary>
    public class MessageServer1
    {
        /// <summary>
        /// Local server location.
        /// </summary>
        //public const string LOCAL_SERVER = ".";

        /// <summary>
        /// The default time out.
        /// </summary>
        //private const int DEFAULT_TIME_OUT = 500;

        /// <summary>
        /// Send Message Exception Event Handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        public delegate void SendMessageExceptionEventHandler(object sender, MessengerExceptionEventArgs e);

        /// <summary>
        /// Send Message Exception Event.
        /// </summary>
        public static event SendMessageExceptionEventHandler SendMessageException;

        /// <summary>
        /// Send a message to pipe server.
        /// </summary>
        /// <param name="pipeName">The pipe server name.</param>
        /// <param name="message">The message to send.</param>          
        public static async void SendMessageAsync(string pipeName1, string message1, string LOCAL_SERVER, int int_Server_Send_Sleep_Time, int int_Server_DEFAULT_TIME_OUT)
        {
            try
            {
                //----------------------------------------------------------------------------------------------------
                //string LOCAL_SERVER = ((MainWindow)Application.Current.MainWindow).txtbx_Server_LOCAL_SERVER.Text;
                //----------------------------------------------------------------------------------------------------

                using (var pipe =
                    new NamedPipeClientStream(LOCAL_SERVER, pipeName1, PipeDirection.InOut, PipeOptions.Asynchronous))
                using (var stream = new StreamWriter(pipe))
                {
#if DEBUG
                    //----------------------------------------------------------------------------------------------------
               //     string Server_Send_Sleep_Time = ((MainWindow)Application.Current.MainWindow).txtbx_Server_Send_Sleep_Time.Text;
                   // int int_Server_Send_Sleep_Time = Convert.ToInt32(Server_Send_Sleep_Time);
                    //----------------------------------------------------------------------------------------------------
                    System.Threading.Thread.Sleep(int_Server_Send_Sleep_Time);
#endif
                    //----------------------------------------------------------------------------------------------------
                   // string Server_DEFAULT_TIME_OUT = ((MainWindow)Application.Current.MainWindow).txtbx_Server_DEFAULT_TIME_OUT.Text;
                  //  int int_Server_DEFAULT_TIME_OUT = Convert.ToInt32(Server_DEFAULT_TIME_OUT);
                    //----------------------------------------------------------------------------------------------------
                    pipe.Connect(int_Server_DEFAULT_TIME_OUT);



                    // write the message to the pipe stream 
                    await stream.WriteAsync(message1);
                }
            }
            catch (Exception exception)
            {
                OnSendMessageException(pipeName1, new MessengerExceptionEventArgs(exception));
            }
        }

        /// <summary>
        /// On Send Message Exception Event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        protected static void OnSendMessageException(object sender, MessengerExceptionEventArgs e)
        {
            if (SendMessageException != null)
            {
                SendMessageException(sender, e);
            }
        }

        /// <summary>
        /// Start listening on the pipe server.
        /// </summary>
        /// <param name="pipeName">The pipe server name.</param>
        /// <param name="messageRecieved">The action to perform when a message is recieved.</param>
        public static async void StartListeningAsync(string pipeName, int int_Client_WatchReturn_Sleep_Time, Action<string> messageRecieved)
        {
            try
            {
                while (true)
                {
                    using (var pipe = new NamedPipeServerStream(pipeName, PipeDirection.InOut, 1, PipeTransmissionMode.Byte, PipeOptions.Asynchronous))
                    {
#if DEBUG
                        // testing pause 
                        //----------------------------------------------------------------------------------------------------
                       // string Client_WatchReturn_Sleep_Time = ((MainWindow)Application.Current.MainWindow).txtbx_Client_WatchReturn_Sleep_Time.Text;
                       // int int_Client_WatchReturn_Sleep_Time = Convert.ToInt32(Client_WatchReturn_Sleep_Time);
                        //----------------------------------------------------------------------------------------------------

                        System.Threading.Thread.Sleep(int_Client_WatchReturn_Sleep_Time);
#endif
                        // wait for the connection
                        await pipe.WaitForConnectionAsync();

                        using (var streamReader = new StreamReader(pipe))
                        {
                            // read the message from the stream - async
                            var message = await streamReader.ReadToEndAsync();
                            if (messageRecieved != null)
                            {
                                // invoke the message received action 
                                messageRecieved(message);



                                /* -------------------------------------------------------------------------------------------------------------------
                                * -------------------------------------------------------------------------------------------------------------------
                                * -------------------------------------------------------------------------------------------------------------------
                                * -------------------------------------------------------------------------------------------------------------------
                                */
#pragma warning disable CS4014 // Da auf diesen Aufruf nicht gewartet wird, wird die Ausführung der aktuellen Methode vor Abschluss des Aufrufs fortgesetzt.
                                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                             new Action(() =>
                             {
                                 //NEW AUFRUF ANDERES fENSTER-----------------------------------------------------------
                                 try
                                 {
                                     //---------------------------Greife Auf anderes Window zu-------------------------------------------------------------------------
                                     var targetWindow = Application.Current.Windows.Cast<Window>().FirstOrDefault(window => window is Wpf_PipeConnectWindow) as Wpf_PipeConnectWindow;

                                     targetWindow.txtbx_Empfangen_Text.Text = (message);
                                 }
                                 catch (Exception ex)
                                 {
                                     MessageBox.Show("MessageServer1:  StartListeningAsync = Error =\r\n" + ex.Message);
                                     //await SmallLogError.Logger("txtbx_Mousecatch_Canvas_Value_Top_Y_TextChanged = Error =\r\n" + ex.Message);
                                     //await SmallLogAllTogether.Logger("txtbx_Mousecatch_Canvas_Value_Top_Y_TextChanged = Error =\r\n" + ex.Message);

                                 }
                                 //NEW AUFRUF ANDERES fENSTER-----------------------------------------------------------

                                 /*
                                 foreach (Wpf_PipeConnectWindow window in Application.Current.Windows)
                                 {
                                     if (window.GetType() == typeof(Wpf_PipeConnectWindow))
                                     {
                                         (window as Wpf_PipeConnectWindow).txtbx_Empfangen_Text.Text = (message);
                                     }
                                 }
                                 */

                             }));

#pragma warning restore CS4014 // Da auf diesen Aufruf nicht gewartet wird, wird die Ausführung der aktuellen Methode vor Abschluss des Aufrufs fortgesetzt.
                                /* -------------------------------------------------------------------------------------------------------------------
                                * -------------------------------------------------------------------------------------------------------------------
                                * -------------------------------------------------------------------------------------------------------------------
                                * -------------------------------------------------------------------------------------------------------------------
                                */

                            }
                        }
                        if (pipe.IsConnected)
                        {
                            // must disconnect 
                            pipe.Disconnect();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                OnSendMessageException(pipeName, new MessengerExceptionEventArgs(exception));
            }
        }
    }
}