using System;
using System.Collections.Generic;
using System.Text;

namespace ICRServer
{
    interface IServer
    {
        /// <summary>
        /// Starts the server.
        /// </summary>
        void Start();
        
        /// <summary>
        /// Waits asynchronously for any client connections.
        /// </summary>
        void WaitForConnections();

        /// <summary>
        /// Accepts a client connection, and waits asynchronously for commands from the accepted client.
        /// </summary>
        /// <param name="result"></param>
        void AcceptConnection(IAsyncResult result);

        /// <summary>
        /// Receives a command from a client, executes it and sends response to the client.
        /// </summary>
        /// <param name="result"></param>
        void ReceiveCommand(IAsyncResult result);

        /// <summary>
        /// Finalizes sending the response to the client.
        /// </summary>
        /// <param name="result"></param>
        void FinalizeSending(IAsyncResult result);

    }
}
