using System.IO;

namespace Platform.Utilities
{
    public class FileResponse<T> : Response<T>
    {
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public string DirectoryPath { get; set; }
        public MemoryStream FileStream { get; set; }
    }
}
