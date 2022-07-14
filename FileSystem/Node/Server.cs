using System.Net.Sockets;
using System.Text;
using Entities;

namespace Node;

public class Server
{
    private readonly TcpClient _client;
    private readonly string _mainPath;
    private readonly Repository _repository;
    private NetworkStream _netStream;
    public Server(TcpClient tcpClient, string path)
    {
        _client = tcpClient;
        _mainPath = path;
        _repository = new Repository();
    }

    public void Process()
    {
        _netStream = null;
        try
        {
            _netStream = _client.GetStream();
            byte[] data = new byte[64];
            byte[] intData = new byte[4];
            while (true)
            {
                _netStream.Read(intData, 0, 4);
                var command = BitConverter.ToInt32(intData);

                string parameters = "";
                if (_netStream.DataAvailable)
                {
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = _netStream.Read(data, 0, data.Length);
                        builder.Append(Encoding.UTF8.GetString(data, 0, bytes));
                    } while (_netStream.DataAvailable);
                    
                    parameters = builder.ToString();
                }
                
                switch (command)
                {
                    case (int)Commands.AddNode:
                        AddNode(parameters);
                        break;
                    case (int)Commands.AddFile:
                        AddFile(parameters);
                        break;
                    case (int)Commands.RemoveFile:
                        RemoveFile(parameters);
                        break;
                    case (int)Commands.GetMainPath:
                        GetMainPath();
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            if (_netStream != null)
                _netStream.Close();
            if (_client != null)
                _client.Close();
        }
    }
    
    public void AddNode(string name)
    {
        _repository.CreateDir(_mainPath +"/"+name);
    }

    public void AddFile(string parameters)
    {
        string[] splitParameters = parameters.Split(" ");
        _repository.CopyFile(splitParameters[0], _mainPath + splitParameters[1]);
    }

    public void RemoveFile(string path)
    {
        _repository.RemoveFile(_mainPath + path);
    }

    public void GetMainPath()
    {
        byte[] data = Encoding.UTF8.GetBytes(_mainPath);
        _netStream.Write(data, 0, data.Length);
    }
}