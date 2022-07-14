namespace Entities;

public struct NodeEntity
{
    public string Name;
    public int Port;
    public long MaxSize;
    public Dictionary<string, long> Files;

    public NodeEntity(string name, int port, long maxSize)
    {
        Name = name;
        Port = port;
        MaxSize = maxSize;
        Files = new Dictionary<string, long>();
    }

}