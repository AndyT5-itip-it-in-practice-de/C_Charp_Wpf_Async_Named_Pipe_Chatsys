// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MessengerExceptionEventArgs.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the MessengerExceptionEventArgs type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
using System;

namespace ChatSys
{
    /// <summary>
    /// Messenger Exception Event Arguments Class.
    /// </summary>
    public class MessengerExceptionEventArgs : EventArgs
    {
        /// <summary>
        /// Gets Exception.
        /// </summary>
        public Exception Exception { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MessengerExceptionEventArgs"/> class.
        /// </summary>
        /// <param name="exception">The exception.</param>
        public MessengerExceptionEventArgs(Exception exception)
        {
            this.Exception = exception;
        }
    }    
}
