// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MessageServer.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the MessageServer type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
using System;
using System.IO;
using System.IO.Pipes;
using System.Windows;
using System.Windows.Threading;

namespace ChatSys
{
    /// <summary>
    /// A Named pipe message server. 
    /// </summary>
    public class MessageServer8
    {
        /// <summary>
        /// Local server location.
        /// </summary>
        public const string LOCAL_SERVER = "."; //not use

        /// <summary>
        /// The default time out.
        /// </summary>
        private const int DEFAULT_TIME_OUT = 500;  //not use


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
        public static async void SendMessageAsync(string pipeName8, string message8, string LOCAL_SERVER, int int_Server_Send_Sleep_Time, int int_Server_DEFAULT_TIME_OUT)

        {
            try
            {

                //----------------------------------------------------------------------------------------------------
                //string LOCAL_SERVER = ((MainWindow)Application.Current.MainWindow).txtbx_Server_LOCAL_SERVER.Text;
                //----------------------------------------------------------------------------------------------------

                using (var pipe =
                    new NamedPipeClientStream(LOCAL_SERVER, pipeName8, PipeDirection.InOut, PipeOptions.Asynchronous))
                using (var stream = new StreamWriter(pipe))
                {
#if DEBUG
                    //----------------------------------------------------------------------------------------------------
                    //string Server_Send_Sleep_Time = ((MainWindow)Application.Current.MainWindow).txtbx_Server_Send_Sleep_Time.Text;
                    //int int_Server_Send_Sleep_Time = Convert.ToInt32(Server_Send_Sleep_Time);
                    //----------------------------------------------------------------------------------------------------
                    System.Threading.Thread.Sleep(int_Server_Send_Sleep_Time);
#endif
                    //----------------------------------------------------------------------------------------------------
                    //string Server_DEFAULT_TIME_OUT = ((MainWindow)Application.Current.MainWindow).txtbx_Server_DEFAULT_TIME_OUT.Text;
                    //int int_Server_DEFAULT_TIME_OUT = Convert.ToInt32(Server_DEFAULT_TIME_OUT);
                    //----------------------------------------------------------------------------------------------------
                    pipe.Connect(int_Server_DEFAULT_TIME_OUT);


                    // write the message to the pipe stream 
                    await stream.WriteAsync(message8);
                }
            }
            catch (Exception exception)
            {
                OnSendMessageException(pipeName8, new MessengerExceptionEventArgs(exception));
            }
        }



        /// <summary>
        /// On Send Message Exception Event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        /// (object sender, MessengerExceptionEventArgs e)
        protected static void OnSendMessageException(object sender, MessengerExceptionEventArgs e)
        {
            if (SendMessageException != null)
            {
                SendMessageException(sender, e);
            }
        }




    }
}