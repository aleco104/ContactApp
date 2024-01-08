namespace ContactApp.Interfaces;

internal interface IFileService
{
    /// <summary>
    /// Saves a text string to a json-file
    /// </summary>
    /// <param name="content">The text string that is going to be saved to the file</param>
    void SaveContentToFile(string content);


    /// <summary>
    /// Reads the content of the file and returns it as a text string
    /// </summary>
    /// <returns>The content of the file</returns>
    string GetContentFromFile();
}
