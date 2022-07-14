using System.Net.Sockets;
using System.Text;
using Entities;


namespace FileSystem;

public class MainServer
{
    private const string _server = "127.0.0.1";
    private NetworkStream _netStream;
    private TcpClient _tcpClient;
    private List<NodeEntity> _nodes;
    
    public MainServer()
    {
        _nodes = new List<NodeEntity>(3);
    }

    public void ChooseCommand(string fullCommand)
    {
        string[] commandParts = fullCommand.Split(" ");
        switch (commandParts[0])
        {
            case "/add-node":
                AddNode(commandParts[1], Int32.Parse(commandParts[2]), Int64.Parse(commandParts[3]));
                break;
            case "/add-file":
                AddFile(commandParts[1], commandParts[2]);
                break;
            case "/remove-file":
                RemoveFile(commandParts[1], true);
                break;
            case "/exec":
                Execute(commandParts[1]);
                break;
            case "/clean-node":
                CleanNode(commandParts[1]);
                break;
            case "/balance-node":
                Balance();
                break;
        }
    }
    
    public void ConnectClient(int port)
    {
        _tcpClient = new TcpClient(_server, port);
        _netStream = _tcpClient.GetStream();
    }

    public void WriteToStream(Commands command, string? parameters)
    {
        byte[] enumBytes = BitConverter.GetBytes((int)command);
        _netStream.Write(enumBytes, 0, enumBytes.Length);
        if (parameters != null)
        {
            byte[] data = Encoding.UTF8.GetBytes(parameters);
            _netStream.Write(data, 0, data.Length);
        }
    }

    public string ReadFromStream()
    {
        byte[] data = new byte[64];
        StringBuilder builder = new StringBuilder();
        int bytes = 0;
        do
        {
            bytes = _netStream.Read(data, 0, data.Length);
            builder.Append(Encoding.UTF8.GetString(data, 0, bytes));
        } while (_netStream.DataAvailable);

        return builder.ToString();
    }
    public NodeEntity FindNode(string nodePath)
    {
        foreach (var node in _nodes)
        {
            if (nodePath.Contains(node.Name))
            {
                return node;
            }
        }

        return new NodeEntity("", -1, -1);
    }
    
    public void AddNode(string name, int port, long maxSize)
    {
        if (FindNode(name).Port != -1) return;
        _nodes.Add(new NodeEntity(name, port, maxSize));
        ConnectClient(port);
        WriteToStream(Commands.AddNode, name);
    }

    public long FindSize(NodeEntity node)
    {
        long size = 0;
        foreach (var file in node.Files)
        {
            size += file.Value;
        }

        return size;
    }
    public void AddFile(string filePath, string nodePath)
    {
        var node = FindNode(nodePath);
        if (node.Port == -1 || node.Files.Count == node.MaxSize) return;
        var fileSize = new FileInfo(filePath).Length;
        if (FindSize(node) + fileSize > node.MaxSize) return;
        node.Files.Add(nodePath, fileSize);
        ConnectClient(node.Port);
        WriteToStream(Commands.AddFile, new StringBuilder().Append(filePath).Append(' ').Append(nodePath).ToString());
    }
    
    public void RemoveFile(string path, bool withVirtual)
    {
        var node = FindNode(path);
        if (node.Port == -1) return;
        if (withVirtual)
        {
            node.Files.Remove(path);
        }

        ConnectClient(node.Port);
        WriteToStream(Commands.RemoveFile, path);
    }

    public void CleanNode(string name)
    {
        var node = FindNode(name);
        if (node.Port == -1) return;
        ConnectClient(node.Port);
        WriteToStream(Commands.GetMainPath, null);
        string mainPath = ReadFromStream();
        
        int numOfFiles = node.Files.Count;
        int i = 0;
        List<string> filesToDelete = new List<string>(numOfFiles);
        while(i < numOfFiles)
        {
            foreach (var anotherNode in _nodes)
            {
                if (anotherNode.Name != node.Name)
                {
                    while (FindSize(anotherNode) < anotherNode.MaxSize && i < numOfFiles)
                    {
                        AddFile(new StringBuilder().Append(mainPath).Append(node.Files.First().Key).ToString(), 
                        new StringBuilder().Append('/').Append(anotherNode.Name)
                            .Append(node.Files.First().Key.Substring(node.Files.First().Key.LastIndexOf("/", StringComparison.Ordinal))).ToString());
                        filesToDelete.Add(node.Files.First().Key);
                        node.Files.Remove(node.Files.First().Key);
                        i++;
                    }
                }
                
                if (i == numOfFiles) break;
            }
        }

        foreach (var file in filesToDelete)
        {
            RemoveFile(file, false);
        }
    }
    
    public void Balance()
    {
        long allFilesSize = 0;
        foreach (var node in _nodes)
        {
            allFilesSize +=FindSize(node);
        }

        long normalSize = allFilesSize / _nodes.Count;
        if (allFilesSize % _nodes.Count != 0)
        {
            normalSize++;
        }

        List<string> filesToDelete = new List<string>();
        foreach (var node in _nodes)
        {
            if (FindSize(node) > normalSize)
            {
                ConnectClient(node.Port);
                WriteToStream(Commands.GetMainPath, null);
                var mainPath = ReadFromStream();
                foreach (var anotherNode in _nodes)
                {
                    if (anotherNode.Name != node.Name && FindSize(anotherNode) < normalSize)
                    {
                        while (FindSize(node) > normalSize && FindSize(anotherNode)< normalSize)
                        {
                            var minFile = node.Files.MinBy(x => x.Value).Key;
                            if (FindSize(anotherNode) + node.Files[minFile] < normalSize)
                            {
                                AddFile(new StringBuilder().Append(mainPath).Append(minFile).ToString(), 
                                    new StringBuilder().Append('/').Append(anotherNode.Name).Append(minFile.Substring(minFile.LastIndexOf("/", StringComparison.Ordinal))).ToString());
                                filesToDelete.Add(minFile);
                                node.Files.Remove(minFile);
                            }
                            else
                            {
                                break;
                            }
                        }
                        
                        if (FindSize(node) <= normalSize)
                        {
                            break;
                        }
                    }
                    
                }
            }
        }

        foreach (var file in filesToDelete)
        {
            RemoveFile(file, false);
        }
    }
    public void Execute(string path)
    {
        using (StreamReader reader = new StreamReader(path))
        {
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                ChooseCommand(line);
            }
        }
    }
}