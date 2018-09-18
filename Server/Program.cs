using System;

namespace Server
{
    /// <summary>
    /// Source: https://www.c-sharpcorner.com/uploadfile/bhushanbhure/websocket-server-using-httplistener-and-client-with-client/
    /// </summary>

    class Program
    {
        static void Main(string[] args)
        {
            WebsocketServer websocketServer = new WebsocketServer();

            Console.WriteLine("Starting websocket listener at ws://localhost:80/WebsocketHttpListenerDemo");
            websocketServer.Start("http://+:80/WebsocketHttpListenerDemo/");

            Console.WriteLine("Press any key to exit ...");
            Console.ReadKey();
        }
    }
}
