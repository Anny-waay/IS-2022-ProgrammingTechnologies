using System.Net;
using System.Net.Sockets;
using System.Text;
using Entities;

namespace Node;

public class ServerToClient
{
    private const string _server = "127.0.0.1";
    private NetworkStream _netStream;
    private TcpListener _tcpListener;
    private string _mainPath;

    public ServerToClient(string path, int port)
    {
        _mainPath = path;
        _netStream = null;
        _tcpListener = new TcpListener(IPAddress.Parse(_server), port);
    }

    public void Start()
    {
        try
        {
            _tcpListener.Start();
            Console.WriteLine("Ожидание подключений...");
 
            while(true)
            {
                TcpClient client = _tcpListener.AcceptTcpClient();
                Server server = new Server(client, _mainPath);
 
                Thread clientThread = new Thread(server.Process);
                clientThread.Start();
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            if(_tcpListener!=null)
                _tcpListener.Stop();
        }
    }
}
    
