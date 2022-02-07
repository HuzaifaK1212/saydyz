using System.IO;
using System.Threading.Tasks;

namespace Platform.Utilities.PDFGenerator
{
    public interface IPDFGenerator<T> where T : BasePDFGeneratorRequest
    {
        Task<Response<string>> GeneratePdf(T pdfGeneratorRequest);
    }
}
