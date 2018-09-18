using System;
using System.Text;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Source: https://www.c-sharpcorner.com/uploadfile/bhushanbhure/websocket-server-using-httplistener-and-client-with-client/
/// </summary>

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "ws://localhost/WebsocketHttpListenerDemo";

            if (args.Length > 0)
            {
                url = args[0];
            }

            Console.WriteLine("Connecting to " + url);
            Connect(url).Wait();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        public static async Task Connect(string uri)
        {
            Thread.Sleep(1000); //wait for a sec, so server starts and ready to accept connection..

            ClientWebSocket webSocket = null;
            try
            {
                webSocket = new ClientWebSocket();
                await webSocket.ConnectAsync(new Uri(uri), CancellationToken.None);
                await Task.WhenAll(Receive(webSocket), Send(webSocket));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: {0}", ex);
            }
            finally
            {
                if (webSocket != null)
                    webSocket.Dispose();

                Console.WriteLine();
                Console.WriteLine("WebSocket closed.");
            }
        }

        private static async Task Send(ClientWebSocket webSocket)
        {
            
            while (webSocket.State == WebSocketState.Open)
            {
                Console.Write("Text to send to server: ");
                string stringToSend = Console.ReadLine();

                byte[] buffer = Encoding.UTF8.GetBytes(stringToSend);

                Console.WriteLine("Sending:  " + stringToSend);
                if (stringToSend == "close")
                {
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closed by client", CancellationToken.None);
                }
                else
                {
                    await webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
                }

                await Task.Delay(1000);
                Console.WriteLine("");
            }
        }

        private static async Task Receive(ClientWebSocket webSocket)
        {
            byte[] buffer = new byte[1024];

            while (webSocket.State == WebSocketState.Open)
            {
                var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
                }
                else
                {
                    Console.WriteLine("Received: " + Encoding.UTF8.GetString(buffer, 0, result.Count).TrimEnd('\0'));
                }
            }
        }
    }
}
