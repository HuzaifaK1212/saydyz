using System;
using System.Collections.Generic;
using System.Text;

namespace Platform.Utilities.PDFGenerator
{
    public class BasePDFGeneratorRequest
    {
        bool UseHeader { get; set; } = true;
        bool UseCoverPage { get; set; }
        bool UseFooter { get; set; } = true;
    }
}
