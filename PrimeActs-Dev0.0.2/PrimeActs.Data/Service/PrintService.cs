using PrimeActs.Domain.ViewModels;
using PrimeActs.Infrastructure.Logging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.IO;

namespace PrimeActs.Data.Service
{
    public class PrintService : IPrintService
    {
        private readonly ILogger _logger;

        // Default constructor needed for ServiceHost
        public PrintService()
            : this(new Log4NetLogger())
        { }

        public PrintService(ILogger logger)
        {
            _logger = logger;
        }

        const int MAX_WIDTH = 270; // Max width Patrick found through trial and error
        //private Font _arial12Bold = new Font("Arial", 12, FontStyle.Bold);
        //private Font _arial10Regular = new Font("Arial", 10, FontStyle.Regular);
        //private Font _arial6Regular = new Font("Arial", 6, FontStyle.Regular);
        private Font _arial12Bold = new Font("Arial", 14, FontStyle.Bold);
        private Font _arial10Regular = new Font("Arial", 12, FontStyle.Regular);
        private Font _arial6Regular = new Font("Arial", 8, FontStyle.Regular);
        private SolidBrush _blackBrush = new SolidBrush(Color.Black);
        private SolidBrush _whiteBrush = new SolidBrush(Color.White);
        private SolidBrush _lightBrush = new SolidBrush(Color.Azure);
        private StringFormat _formatCenter = new StringFormat
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center
        };
        private StringFormat _formatLeft = new StringFormat
        {
            Alignment = StringAlignment.Near,
            LineAlignment = StringAlignment.Center
        };
        private StringFormat _formatRight = new StringFormat()
        {
            Alignment = StringAlignment.Far,
            LineAlignment = StringAlignment.Center
        };

        private void Print<T>(string documentName, string printerName, T model, Action<object, PrintPageEventArgs, T> printPageMethod)
        {
            PrintDocument pd = new PrintDocument();
            pd.DocumentName = documentName;
            pd.PrintController = new StandardPrintController();
            pd.PrintPage += (sender, e) => printPageMethod(sender, e, model);

            PrinterSettings ps = new PrinterSettings();
            ps.PrinterName = printerName;
            pd.PrinterSettings = ps;
            pd.Print();
        }

        public void PrintDocument(string[] printerNames, PrintDocumentModel document)
        {
            foreach (var printerName in printerNames)
            {
                if (document.RawPrinter == true)
                {
                    //var printerNametest = "EPSON FX Series 1 (80)";
                    var printerNametest = printerNames[0];
                   // var printerNametest = "PDF Complete";
                    Task t = Task.Run(() => Print<PrintDocumentModel>("Cheque", printerNametest, document, RawPrintDocument));
                    t.Wait();
                    //Task.Run(() => Print<PrintDocumentModel>("PDF Print", printerNametest, document, PDFRawPrintDocument));
                }
                else
                {
                    //Task.Run(() => Print<PrintDocumentModel>("Customer Receipt", printerName, document, PrintDocument));
                    Task t = Task.Run(() => Print<PrintDocumentModel>("Customer Receipt", printerName, document, PrintDocument));
                    t.Wait(); // wait until finished to try and avoid Graphics context in use error DC - 25/07/16
                }
            }
        }

        #region Print Document PrintPageEventHandler

      //  private void async PrintDocument(object sender, PrintPageEventArgs e, PrintDocumentModel document)
            private void PrintDocument(object sender, PrintPageEventArgs e, PrintDocumentModel document)
        {
            int documentPageWidth = MAX_WIDTH;
            if (document.PageWidth.HasValue) documentPageWidth = document.PageWidth.Value; //DC Added PageWidth to Document Value   
            
            using (Graphics g = e.Graphics)
            {
                float y = 12; // changed from 10 to 12 DC 
                
                foreach (var section in document.Sections)
                {
                    foreach (var line in section.Lines)
                    {
                        PrintLine(g, line, ref y, documentPageWidth);
                    }
                    y += 12; // changed from 10 to 12 DC 
                }
            }

            e.HasMorePages = false;
            return;
        }

        // Partially complete printing a raw described document to PDF as a proof of concept, while eating lunch - DC
        private void PDFRawPrintDocument(object sender, PrintPageEventArgs e, PrintDocumentModel document)
        {
            int documentPageWidth = MAX_WIDTH;
            if (document.PageWidth.HasValue) documentPageWidth = document.PageWidth.Value; //DC Added PageWidth to Document Value   
            //e.PageSettings.PrinterSettings.
            //e.PageSettings.PaperSize.PaperName = "A3";

            using (Graphics g = e.Graphics)
            {
                float y = 12; // changed from 10 to 12 DC 
                int lineCount = 0;
                foreach (var section in document.Sections)
                {
                    foreach (var line in section.Lines)
                    {
                        PrintLine(g, line, ref y, documentPageWidth);
                        PrintLineRawAsGraphic(g, line, ref y, documentPageWidth,5);
                    }
                    y += 12; // changed from 10 to 12 DC 
                    lineCount = lineCount++;
                    if (lineCount >= 57) { e.HasMorePages = true; lineCount = 0; }
                }
            }
            e.HasMorePages = false;
        }

        private void RawPrintDocument(object sender, PrintPageEventArgs e, PrintDocumentModel document)
        {
            int documentPageWidth = MAX_WIDTH;
            if (document.PageWidth.HasValue) documentPageWidth = document.PageWidth.Value; //DC Added PageWidth to Document Value   
            // Initialise Printer
            var pd = (System.Drawing.Printing.PrintDocument)sender;
            string printerName = pd.PrinterSettings.PrinterName;
            string ESC = "\u001B";
            string cmds = ESC + "@"; //Initializes the printer (ESC @)
            string condensed = ESC + "g";
            string subscript = ESC + "!17";
            const string FORM_FEED = "\f";
            RawPrinterHelper.SendStringToPrinter(printerName, cmds);
            if (document.Condensed.Value == true) RawPrinterHelper.SendStringToPrinter(printerName, condensed);
            if (document.Condensed.Value == true) RawPrinterHelper.SendStringToPrinter(printerName, FORM_FEED);
            //if (document.Condensed.Value == true) RawPrinterHelper.SendStringToPrinter(printerName, subscript);
           
                foreach (var section in document.Sections)
                {
                    foreach (var line in section.Lines)
                    {
                        System.Threading.Thread.Sleep(400);
                            // prevent buffer overrun, will need to find optimum value and make configuarable
                        if (document.Condensed.Value == true)
                            RawPrinterHelper.SendStringToPrinter(printerName, condensed); // 05/07/2016 Test
                        RawPrintLine(printerName, line, documentPageWidth);
                            // also add the page length and count lines printed
                        
                        //var lineout = RawPrintLineToFile(line);
                        
                    }

                
                }
            //TestPrintRaw();
        }

        private void TestPrintRaw()
        {
            const string ESC = "\u001B";
            string cmds = ESC + "@"; //Initializes the printer (ESC @)
            const string TOF = "\u00FF";
            const string FORM_FEED = "\f";

            string s = cmds + "Print Test RAW from PrimeActs"; // device-dependent string, need a FormFeed?
            s = s + Environment.NewLine;
            s = s + "01234567890123456789012345678901234567890123456789012345678901234567890123456789";
            s = s + Environment.NewLine;
            s = s + FORM_FEED;
            RawPrinterHelper.SendStringToPrinter("EPSON FX Series 1 (80)", s);
        }

        private void PrintLineRawAsGraphic(Graphics g, LineModel line, ref float y, int pageWidth, int scaleFactor)
        {
            //int height = line.GetLineHeight(g, MAX_WIDTH, "Arial", 6, FontStyle.Regular);
            int height = line.GetLineHeight(g, pageWidth, "Arial", 8, FontStyle.Regular); // change from 6 to 8;
            foreach (var lineItem in line.LineItems)
            {
                if (lineItem.Image == null)
                {
                    lineItem.Width = lineItem.Width * scaleFactor;
                   
                    Font font = new Font(lineItem.FontName ?? "Arial", lineItem.FontSize ?? 8,
                        lineItem.FontStyle ?? FontStyle.Regular);
                    
                    var lineItemHeight = lineItem.GetLineItemHeight(g, pageWidth, "Arial", 8, FontStyle.Regular);
                    // DC wider printer
                   
                    RectangleF rectF1 = new RectangleF(lineItem.X ?? 12, y, lineItem.Width ?? pageWidth, lineItemHeight); // changed from 10 to 12 DC 

                    StringFormat format = new StringFormat
                    {
                        Alignment = lineItem.Align ?? StringAlignment.Near,
                        LineAlignment = StringAlignment.Center
                    };

                    if (lineItem.Reverse == 1) // White on Black Printing
                    {
                        g.FillRectangle(_blackBrush, rectF1);
                        g.DrawString(lineItem.Text, font, _whiteBrush, rectF1, format);
                    }
                    else // Black text
                    {
                        if (lineItem.Invisible == 1)
                        {                           
                            g.DrawString(lineItem.Text, font, _lightBrush, rectF1, format);
                        }
                        else
                        {
                            g.DrawString(lineItem.Text, font, _blackBrush, rectF1, format);
                        }
                    }
                }
                else
                {
                    var alignment = lineItem.Align ?? StringAlignment.Near;
                    int x;
                    if (alignment == StringAlignment.Near)
                    {
                        x = 10;
                    }
                    else if (alignment == StringAlignment.Center)
                    {
                        // x = (MAX_WIDTH - lineItem.Image.Width + 10) / 2;
                        x = (pageWidth - lineItem.Image.Width + 10) / 2;
                    }
                    else
                    {
                        // x = MAX_WIDTH - lineItem.Image.Width + 10;
                        x = pageWidth - lineItem.Image.Width + 10;
                    }

                    RectangleF rectF1 = new RectangleF(x, y, lineItem.Image.Width, height);
                    g.DrawImage(lineItem.Image, rectF1);
                }
            }
            y += height;
        }

        private void PrintLine(Graphics g, LineModel line, ref float y, int pageWidth)
        {
            //int height = line.GetLineHeight(g, MAX_WIDTH, "Arial", 6, FontStyle.Regular);
            int height = line.GetLineHeight(g, pageWidth, "Arial", 8, FontStyle.Regular); // change from 6 to 8;
            foreach (var lineItem in line.LineItems)
            {
                if (lineItem.Image == null)
                {
                    //Font font = new Font(lineItem.FontName ?? "Arial", lineItem.FontSize ?? 6, lineItem.FontStyle ?? FontStyle.Regular);
                    Font font = new Font(lineItem.FontName ?? "Arial", lineItem.FontSize ?? 8,
                        lineItem.FontStyle ?? FontStyle.Regular);
                    //var lineItemHeight = lineItem.GetLineItemHeight(g, MAX_WIDTH, "Arial", 6, FontStyle.Regular);
                    // var lineItemHeight = lineItem.GetLineItemHeight(g, pageWidth, "Arial", 6, FontStyle.Regular);
                    var lineItemHeight = lineItem.GetLineItemHeight(g, pageWidth, "Arial", 8, FontStyle.Regular);
                        // DC wider printer
                    //RectangleF rectF1 = new RectangleF(lineItem.X ?? 10, y, lineItem.Width ?? MAX_WIDTH, lineItemHeight);
                    RectangleF rectF1 = new RectangleF(lineItem.X ?? 12, y, lineItem.Width ?? pageWidth, lineItemHeight); // changed from 10 to 12 DC 

                    StringFormat format = new StringFormat
                    {
                        Alignment = lineItem.Align ?? StringAlignment.Near,
                        LineAlignment = StringAlignment.Center
                    };

                    if (lineItem.Reverse == 1) // White on Black Printing
                    {
                        g.FillRectangle(_blackBrush, rectF1);
                        g.DrawString(lineItem.Text, font, _whiteBrush, rectF1, format);
                    }
                    else // Black text
                    {
                        if (lineItem.Invisible == 1)
                        {
                            //RectangleF rectF2 = new RectangleF(lineItem.X ?? 12, y, lineItem.Width ?? pageWidth, lineItemHeight);
                            g.DrawString(lineItem.Text, font, _lightBrush, rectF1, format);
                        }
                        else
                        {
                            g.DrawString(lineItem.Text, font, _blackBrush, rectF1, format);
                        }
                    }
                }
                else
                {
                    var alignment = lineItem.Align ?? StringAlignment.Near;
                    int x;
                    if (alignment == StringAlignment.Near)
                    {
                        x = 10;
                    }
                    else if (alignment == StringAlignment.Center)
                    {
                       // x = (MAX_WIDTH - lineItem.Image.Width + 10) / 2;
                        x = (pageWidth - lineItem.Image.Width + 10) / 2;
                    }
                    else
                    {
                       // x = MAX_WIDTH - lineItem.Image.Width + 10;
                        x = pageWidth - lineItem.Image.Width + 10;
                    }

                    RectangleF rectF1 = new RectangleF(x, y, lineItem.Image.Width, height);
                    g.DrawImage(lineItem.Image, rectF1);
                }
            }
            y += height;
        }

        private void RawPrintLine(string printerName, LineModel line, int pageWidth)
        {
            string outputstring = "";
            foreach (var lineItem in line.LineItems)
            {
                if (lineItem.Image == null)
                {
                    var formatString = formatAlignRawLineItem(lineItem);
                    outputstring += formatString;                   
                }
            }
            outputstring += Environment.NewLine;
            RawPrinterHelper.SendStringToPrinter(printerName, outputstring);
           
           
        }

        private string RawPrintLineToFile( LineModel line)
        {
            string outputstring = "";
            foreach (var lineItem in line.LineItems)
            {
                if (lineItem.Image == null)
                {
                    var formatString = formatAlignRawLineItem(lineItem);
                    outputstring += formatString;
                }
            }
            outputstring += Environment.NewLine;
            return outputstring;
        }

        private static string formatAlignRawLineItem(LineItemModel lineItem)
        {
            var formatArgs = "{0}";
            string lineItemAdjust = lineItem.Text;

            if (lineItem.Width.HasValue)
            {
                if (lineItem.Align == StringAlignment.Center)
                {
                    if (lineItem.Text.Length < lineItem.Width)
                    {
                        lineItemAdjust = lineItem.Text.CenterString(lineItem.Width.Value);
                    }
                }
                if (lineItem.Align == StringAlignment.Near)
                {
                    formatArgs = "{0,-" + lineItem.Width.Value.ToString() + "}";
                }
                if (lineItem.Align == StringAlignment.Far)
                {
                    formatArgs = "{0," + lineItem.Width.Value.ToString() + "}";
                }
            }

            var formatString = string.Format(formatArgs, lineItemAdjust);
            return formatString;
        }
        #endregion
    }
    public static class StringExtensions
    {

        public static string CenterString(this string stringToCenter, int totalLength)
        {
            return stringToCenter.PadLeft(((totalLength - stringToCenter.Length) / 2)
                                + stringToCenter.Length)
                       .PadRight(totalLength);
        }
    }
    public class RawPrinterHelper
    {

        // Structure and API declarions:
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public class DOCINFOA
        {
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDocName;
            [MarshalAs(UnmanagedType.LPStr)]
            public string pOutputFile;
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDataType;
        }
        [DllImport("winspool.Drv", EntryPoint = "OpenPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool OpenPrinter([MarshalAs(UnmanagedType.LPStr)] string szPrinter, out IntPtr hPrinter, IntPtr pd);

        [DllImport("winspool.Drv", EntryPoint = "ClosePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool ClosePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartDocPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool StartDocPrinter(IntPtr hPrinter, Int32 level, [In, MarshalAs(UnmanagedType.LPStruct)] DOCINFOA di);

        [DllImport("winspool.Drv", EntryPoint = "EndDocPrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool EndDocPrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool StartPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "EndPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool EndPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "WritePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, Int32 dwCount, out Int32 dwWritten);

        // SendBytesToPrinter()
        // When the function is given a printer name and an unmanaged array
        // of bytes, the function sends those bytes to the print queue.
        // Returns true on success, false on failure.
        public static bool SendBytesToPrinter(string szPrinterName, IntPtr pBytes, Int32 dwCount)
        {
            Int32 dwError = 0, dwWritten = 0;
            IntPtr hPrinter = new IntPtr(0);
            DOCINFOA di = new DOCINFOA();
            bool bSuccess = false; // Assume failure unless you specifically succeed.

            di.pDocName = "My C#.NET RAW Document";
            di.pDataType = "RAW";

            // Open the printer.
            if (OpenPrinter(szPrinterName.Normalize(), out hPrinter, IntPtr.Zero))
            {
                // Start a document.
                if (StartDocPrinter(hPrinter, 1, di))
                {
                    // Start a page.
                    if (StartPagePrinter(hPrinter))
                    {
                        // Write your bytes.
                        bSuccess = WritePrinter(hPrinter, pBytes, dwCount, out dwWritten);
                        EndPagePrinter(hPrinter);
                    }
                    EndDocPrinter(hPrinter);
                }
                ClosePrinter(hPrinter);
            }
            // If you did not succeed, GetLastError may give more information
            // about why not.
            if (bSuccess == false)
            {
                dwError = Marshal.GetLastWin32Error();
            }
            return bSuccess;
        }

        public static bool SendFileToPrinter(string szPrinterName, string szFileName)
        {
            // Open the file.
            FileStream fs = new FileStream(szFileName, FileMode.Open);
            // Create a BinaryReader on the file.
            BinaryReader br = new BinaryReader(fs);
            // Dim an array of bytes big enough to hold the file's contents.
            Byte[] bytes = new Byte[fs.Length];
            bool bSuccess = false;
            // Your unmanaged pointer.
            IntPtr pUnmanagedBytes = new IntPtr(0);
            int nLength;

            nLength = Convert.ToInt32(fs.Length);
            // Read the contents of the file into the array.
            bytes = br.ReadBytes(nLength);
            // Allocate some unmanaged memory for those bytes.
            pUnmanagedBytes = Marshal.AllocCoTaskMem(nLength);
            // Copy the managed byte array into the unmanaged array.
            Marshal.Copy(bytes, 0, pUnmanagedBytes, nLength);
            // Send the unmanaged bytes to the printer.
            bSuccess = SendBytesToPrinter(szPrinterName, pUnmanagedBytes, nLength);
            // Free the unmanaged memory that you allocated earlier.
            Marshal.FreeCoTaskMem(pUnmanagedBytes);
            return bSuccess;
        }
        public static bool SendStringToPrinter(string szPrinterName, string szString)
        {
            IntPtr pBytes;
            Int32 dwCount;
            // How many characters are in the string?
            dwCount = szString.Length;
            // Assume that the printer is expecting ANSI text, and then convert
            // the string to ANSI text.
            pBytes = Marshal.StringToCoTaskMemAnsi(szString);
            // Send the converted ANSI string to the printer.
            SendBytesToPrinter(szPrinterName, pBytes, dwCount);
            //SendBytesToPrinter(szPrinterName, pBytes, dwCount);
            //byte feed = 12;
            //byte[] buffer = new byte[1];

            //buffer[0] = feed;
            //dwCount = buffer.Length;

            //IntPtr print = Marshal.AllocCoTaskMem((int)buffer.Length);

            //Marshal.Copy(buffer, 0, print, (int)buffer.Length);

            //SendBytesToPrinter(szPrinterName, print, dwCount);

            Marshal.FreeCoTaskMem(pBytes);
            return true;
        }
    }
}
