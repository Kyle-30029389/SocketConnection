using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace SocketConnection
{
    public class Connection
    {
        private string hostname;
        private int port;
        int byteCount;
        NetworkStream stream;
        byte[] sendData;
        TcpClient client;
        public Connection(string IPV4, int Port)
        {
            hostname = IPV4;
            port = Port;
        }

        public Connection(int Port)
        {
            Console.WriteLine("Please Enter a IPV4 Address");
            hostname = Console.ReadLine();
        }

        public Connection(string IPV4)
        {
            Console.WriteLine("Please Enter a Port Number");
            port = int.Parse(Console.ReadLine());
        }

        ~Connection()
        {
            SendMessage("stop");
            stream.Close();
            client.Close();
        }

        public string Connect()
        {
            try
            {
                Console.WriteLine($"Atempt Connection {hostname}:{port}");
                client = new TcpClient(hostname, port);
                Console.WriteLine("Connection Made");
                Thread.Sleep(1000);
                return ("Connection Made");
            }
            catch (System.Net.Sockets.SocketException)
            {
                Console.WriteLine("Connection Failed");
                return ("Connection Failed");
            }
        }

        public string Disconect()
        {
            SendMessage("stop");
            stream.Close();
            client.Close();
            return ("Disconnect");
        }


        public string SendMessage(string message)
        {
            try
            {
                byteCount = Encoding.ASCII.GetByteCount(message);
                sendData = new byte[byteCount];
                sendData = Encoding.ASCII.GetBytes(message);
                stream = client.GetStream();
                stream.Write(sendData, 0, sendData.Length);


                if (message == "stop")
                {
                    stream.Close();
                    client.Close();
                }
                Thread.Sleep(300);
                return ("Message Sent");

            }
            catch (System.NullReferenceException)
            {
                Console.WriteLine("Connection Not working");
                return ("Connection Not working");
            }
        }
    }
}
