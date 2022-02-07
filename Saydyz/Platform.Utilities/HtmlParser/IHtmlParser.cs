using System.Threading.Tasks;

namespace Platform.Utilities.HtmlParser
{
    public interface IHtmlParser
    {
        Task<string> ParseHtmlFile(string filePath, string json);
    }
}
