using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Utilities.ChecksumHelper
{
    public interface IChecksumHelper
    {
        Task<string> CalculateChecksum(IFormFile file);
        Task<string> CalculateChecksum(string filePath);
    }
}
