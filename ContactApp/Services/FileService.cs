using ContactApp.Interfaces;
using System.Diagnostics;

namespace ContactApp.Services;

internal class FileService : IFileService
{
    private readonly string _filePath = "contactlist.json";

    public string GetContentFromFile()
    {
        try
        {
            using (var sr = new StreamReader(_filePath))
            {
                return sr.ReadToEnd();
            }
        }
        catch (Exception ex) 
        { 
            Debug.WriteLine(ex.Message);
            return string.Empty;
        }
    }

    public void SaveContentToFile(string content)
    {
        try
        {
            using (var sw = new StreamWriter(_filePath)) 
              
            {
                sw.WriteLine(content);
            }
        }
        catch (Exception ex) 
        { 
            Debug.WriteLine(ex.Message); 
        }
    }
}
