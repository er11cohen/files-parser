using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenXmlPowerTools;
using DocumentFormat.OpenXml.Packaging;
using System.Xml.Linq;

namespace orHahaim
{
    class orHahaim
    {
        static string prefix = "<html><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=windows-1255\"></head>"
                          + " <body lang=EN-US link=\"#3377cc\" vlink=\"#3377cc\" alink=\"#6095d7\" style=\"tab-interval:36.0pt; font-family: David;\">"

         + " <center><span style=\"color:#BE32BE;\"><span style =\"font-weight:bold;\" >"
         + " <span style=\"color:#BE32BE;\"><small> ל ר' פנחס ראובן שליט''א (c)  בס''ד -  כל הזכויות שמורות</small></span></span></span></center>"

            + "<div dir=\"rtl\" style=\"text-align: justify;\">";

        static string suffix = "</div></body></html>";

        static void Main(string[] args)
        {

            string original = @"D:\Eran\EranDoc\Android Develop\develop\OrHahaim\original";
            string final = @"D:\Eran\EranDoc\Android Develop\develop\OrHahaim\final";

            foreach (var filePath in Directory.GetFiles(original))
            {
                string directoryName = System.IO.Path.GetFileNameWithoutExtension(filePath);
                string directoryPath = final + "\\" + directoryName;
                bool isExistsDirectory = System.IO.Directory.Exists(directoryPath);
                if (!isExistsDirectory)
                    System.IO.Directory.CreateDirectory(directoryPath);

                preparefiles(filePath, directoryPath, directoryName);
            }
        }

        private static void preparefiles(string filePath, string directoryPath, string directoryName)
        {
            string Breshit = "Breshit,Noah,Lekhlkha,Vayera,HayeSara,Toldot,Vayetse,Vayishlah,Vayeshev,Mikets,Vayigash,Vayhi";
            string Shmot = "Shmot,Vaera,Bo,Bshalah,Yitro,Mishpatim,Truma,Ttsave,Kitisa,Vayakhel,Pkude";
            string Vayikra = "Vayikra,Tsav,Shmini,Tazria,Mtsora,Aharemot,Kdoshim,Emor,Bhar,Bhukotay";
            string Bamidbar = "Bamidbar,Naso,Bhaalotkha,Shlahlkha,Korah,Hukat,Balak,Pinhas,Matot,Mase";
            string Dvarim = "Dvarim,Vaethanan,Ekev,Ree,Shoftim,Kitetse,Kitavo,Nitsavim,Vayelekh,Haazinu,Vzothabraha";

            string parshot = String.Empty;
            string result;
            int index = 0;
            using (StreamReader reader = new StreamReader(filePath, Encoding.Default))
            {
                result = reader.ReadToEnd();
            }

            result = result.Replace("font-size", "fz");

            switch (directoryName)
            {
                case "Breshit":
                    parshot = Breshit;
                    break;
                case "Shmot":
                    parshot = Shmot;
                    break;
                case "Vayikra":
                    parshot = Vayikra;
                    break;
                case "Bamidbar":
                    parshot = Bamidbar;
                    break;
                case "Dvarim":
                    parshot = Dvarim;
                    break;
            }

            string[] parshotArr = parshot.Split(',');
            string htmp = "HtmpReportNum00{0}_L5";
            while (true)
            {
                string indexStrStart = index.ToString();
                if (indexStrStart.Length == 1)
                {
                    indexStrStart = "0" + indexStrStart;
                }
                string linkStart = String.Format(htmp, indexStrStart);
                int startOffset = result.IndexOf(linkStart);
                int start = result.IndexOf(linkStart, startOffset + 1) - 9;

                string indexStrEnd = (index+1).ToString();
                if (indexStrEnd.Length == 1)
                {
                    indexStrEnd = "0" + indexStrEnd;
                }
                string linkEnd = String.Format(htmp, indexStrEnd);
                int endOffset = result.IndexOf(linkEnd);
                int end = 0;
                if (endOffset != -1)
                {
                     end = result.IndexOf(linkEnd, endOffset + 1) - 9;
                }
                else
                {
                    end = result.IndexOf("<!--BODY_END-->");
                }


                string parashString = result.Substring(start, end - start);
                File.WriteAllText(directoryPath + "\\" + parshotArr[index++] + ".html", prefix + parashString + suffix, Encoding.UTF8);
                
                if (endOffset == -1)//finished
                {
                    break;
                }
            }
            

        }

        private static void convertDocxToHtmlFunc()
        {
            string parentPath = @"D:\EranDoc\Android Develop\input";
            string targetPath = @"D:\EranDoc\Android Develop\output";

            foreach (var pathDirectory in Directory.GetDirectories(parentPath))
            {
                string directoryName = System.IO.Path.GetFileName(pathDirectory);
                bool isExistsDirectory = System.IO.Directory.Exists(targetPath + "\\" + directoryName);
                if (!isExistsDirectory)
                    System.IO.Directory.CreateDirectory(targetPath + "\\" + directoryName);


                foreach (var filePath in Directory.GetFiles(pathDirectory))
                {
                    string fileNameTarget = System.IO.Path.GetFileNameWithoutExtension(filePath);
                    string targetFullPath = targetPath + "\\" + directoryName + "\\" + fileNameTarget + ".html";
                    convertDocxToHtml(filePath, targetFullPath);
                }
            }
        }

        public static void convertDocxToHtml(string filePath, string targetPath)
        {
            // This example shows the simplest conversion. No images are converted.
            // A cascading style sheet is not used.

            byte[] byteArray = File.ReadAllBytes(filePath);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                memoryStream.Write(byteArray, 0, byteArray.Length);
                using (WordprocessingDocument doc = WordprocessingDocument.Open(memoryStream, true))
                {
                    HtmlConverterSettings settings = new HtmlConverterSettings()
                    {
                       // PageTitle = "My Page Title"
                    };
                    XElement html = HtmlConverter.ConvertToHtml(doc, settings);

                    // Note: the XHTML returned by ConvertToHtmlTransform contains objects of type
                    // XEntity. PtOpenXmlUtil.cs defines the XEntity class. See
                    // http://blogs.msdn.com/ericwhite/archive/2010/01/21/writing-entity-references-using-linq-to-xml.aspx
                    // for detailed explanation.
                    //
                    // If you further transform the XML tree returned by ConvertToHtmlTransform, you
                    // must do it correctly, or entities do not serialize properly.

                   string htmlStr =  html.ToStringNewLineOnAttributes();
                   htmlStr = htmlStr.Replace("href", "href1").Replace("font-size", "fz");

                   File.WriteAllText(targetPath, htmlStr, Encoding.UTF8);
                }
            }
        }
    }
}
