using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server;

public class Node
{
    private const string _server = "127.0.0.1";
    private NetworkStream _netStream;
    private TcpListener _tcpListener;
    
    public Node(int port)
    {
        _netStream = null;
        _tcpListener = new TcpListener(IPAddress.Parse("127.0.0.1"), port);
        _tcpListener.Start();
    }

    public void ReadSmt()
    {
        while (true)
        {
            TcpClient client = _tcpListener.AcceptTcpClient();
            _netStream = client.GetStream();
            byte[] data = new byte[64]; // буфер для получаемых данных
            StringBuilder builder = new StringBuilder();
            int bytes = 0;
            bytes = _netStream.Read(data, 0, data.Length);
            builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            string message = builder.ToString();
            Console.WriteLine(message);
            _tcpListener.Stop();
        }
    }
}