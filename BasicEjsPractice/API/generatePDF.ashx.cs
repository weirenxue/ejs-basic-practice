using iText.Html2pdf;
using System;
using System.IO;
using System.Web;
using System.Net;
using iText.Layout.Font;
using iText.Html2pdf.Resolver.Font;
using iText.IO.Font;

namespace BasicEjsPractice.API
{
    /// <summary>
    /// Summary description for generatePDF
    /// </summary>
    public class generatePDF : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string[] FONTS =
            {
                // msjh.ttc[0], the first one ttf in msjh.ttc.
                @"C:\Windows\Fonts\msjh.ttc,0",
            };
            // If the POST body has uncoded symbols,such as "<" and ">", 
            // it will be judged as potentially dangerous. And put the POST
            // body to Request.Unvalidated.Form.
            String HTML = context.Request.Unvalidated.Form["content"] == null ? context.Request.Form["content"]: context.Request.Unvalidated.Form["content"];
            // Check whether the HTML has content.
            if (HTML != null && HTML != "")
            {
                // Define the date as the pdf file name.
                String filename = "D:\\" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";

                ConverterProperties properties = new ConverterProperties();
                // Deal with font provider.
                FontProvider fontProvider = new DefaultFontProvider(false, false, false);
                foreach (string font in FONTS)
                {
                    FontProgram fontProgram = FontProgramFactory.CreateFont(font);
                    fontProvider.AddFont(fontProgram);
                }
                // Set font type
                properties.SetFontProvider(fontProvider);
                // Set the base uri (that is, the root) of the website.
                // HttpContext.Current.Server.MapPath("~") : the root of website.
                properties.SetBaseUri(HttpContext.Current.Server.MapPath("~"));

                // Convert html to pdf.
                HtmlConverter.ConvertToPdf(WebUtility.HtmlDecode(HTML), new FileStream(filename, FileMode.Create), properties);
            }

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}