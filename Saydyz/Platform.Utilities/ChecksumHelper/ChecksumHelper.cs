using System.IO;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Platform.Utilities.ChecksumHelper
{
    public class ChecksumHelper : IChecksumHelper
    {
        public Task<string> CalculateChecksum(IFormFile file)
        {
            throw new System.NotImplementedException("Checksum verification is under development.");
        }

        public Task<string> CalculateChecksum(string file)
        {
            throw new System.NotImplementedException();
        }
    }
}
