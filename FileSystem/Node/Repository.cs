namespace Node;

public class Repository
{
    public void CreateDir(string path)
    {
        Directory.CreateDirectory(path);
    }

    public void CopyFile(string path, string newPath)
    {
        File.Copy(path, newPath);
    }

    public void RemoveFile(string path)
    {
        File.Delete(path);
    }
}