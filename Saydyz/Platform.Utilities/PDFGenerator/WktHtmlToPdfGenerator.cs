using Platform.Utilities.UserSession;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Platform.Utilities.PDFGenerator
{
    public class WktHtmlToPdfGenerator : IHtmlToPdfGenerator
    {
        private IUserSession UserSession { get; }
        private IConfiguration Configuration { get; }

        public WktHtmlToPdfGenerator(IUserSession userSession, IConfiguration configuration)
        {
            this.UserSession = userSession;
            this.Configuration = configuration;
        }


        public async Task<Response<string>> GeneratePdf(HtmlToPdfGenerationRequest pdfGeneratorRequest)
        {
            var exeLocation = Configuration["PDFGenerator:ExeLocation"];

            return await Task.Run(() =>
            {

                //this should be looked into. Either the generic parameter should handle default conversion 
                //or there must be a explicit generic.
                var outputPath = ((WktHtmlToPdfGenerationRequest)pdfGeneratorRequest).OutputFilePath;

                if (string.IsNullOrEmpty(outputPath))
                    throw new ArgumentException("File output path is required for wkhtmltopdf for pdf file generation.");

                var wktSwitches = Configuration["PDFGenerator:Switches"];
                
                var pdfFileBytes = WktHtmlToPdfDriver.Convert(exeLocation, wktSwitches, pdfGeneratorRequest.HtmlBody);

                var fileName = "";

                using (var pdfStream = new MemoryStream()) {
                    pdfStream.Write(pdfFileBytes, 0, pdfFileBytes.Length);
                    pdfStream.Position = 0;

                    var tmpFilePath = Path.GetTempPath();
                    fileName = Path.Combine(tmpFilePath, outputPath);
                    using (var fs = File.OpenWrite(fileName))
                    {
                        pdfStream.CopyTo(fs);
                    }
                }

                return new Response<string>()
                {
                    Success = true,
                    Message = "In-memory pdf file generated successfully.",
                    Data = fileName
                };
            });
        }
    }
}
