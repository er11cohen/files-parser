using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODLV
{
    class Program
    {
        static string prefix = "<html><head></head> <body>  <div dir=\"rtl\" style=\"font-family: david;\" >"
            + "<center><span style=\"color:#BE32BE;\">"
            +"<span style=\"font-weight:bold;\"><span style=\"color:#BE32BE;\"><small>"
+ "בס''ד - כל הזכויות שמורות (c) למו''ר הרב יצחק יוסף שליט''א<br>אסור להעתיק קטעים מהספר ללא ציון המקור!"
+"</small></span></span></span></center><center>"
+"<big><span style=\"font-weight:bold;\">"
+"אוצר דינים לאשה ולבת<br></span></big></center><center>על מצוות האשה, חיוביה, פטוריה, ומנהגיה"
+"</center><center>לפי סדר השלחן ערוך - מהדורת תשס''ה"
+"</center><center><big><big><b>הראשל''צ הרב יצחק יוסף שליט''א</b></big></big>"
+"</center><center><span style=\"color:blue;\">מסיבות טכניות לא הבאנו כאן את ההערות המובאות בספר המודפס</span>	"
+"</center>";
      

        static string suffix = "</div></body></html>";
        static void Main(string[] args)
        {

            CreateODLV();
           // WriteHtmlReportNumToTxt();
            
        }

        private static void CreateODLV()
        {
            string pathFile = @"D:\EranDoc\Android Develop\develop\ODLV\original\odlv.html";
            string targetPath = @"D:\EranDoc\Android Develop\develop\ODLV\final";

            string html;
            using (StreamReader reader = new StreamReader(pathFile, Encoding.Default))
            {
                html = reader.ReadToEnd();
            }


            int fileNumber = 1;
            string[] htmlSectionArr = new string[] { "00", "14", "36", "48", "60" };
            for (int i = 0; i < 4; i++)
            {
                string result = GetHtmlBetweenSectionToSection(htmlSectionArr[i], htmlSectionArr[i + 1], html);
                result = prefix + result + suffix;
                result = result.Replace("font-size", "fz");
                File.WriteAllText(targetPath + "\\" + "odlv_" + fileNumber++ + ".html", result, Encoding.UTF8);
            }
        }

        private static string GetHtmlBetweenSectionToSection(string startSection, string finalSection, string html)
        {
            string sectionStratStr = "<a name=\"HtmpReportNum00" + startSection + "_L2\">";
            string sectionFinalStr = "<a name=\"HtmpReportNum00" + finalSection + "_L2\">";

            int startIndex = html.IndexOf(sectionStratStr); 
            int finalIndex;
            if (finalSection != "60")
            {
                finalIndex = html.IndexOf(sectionFinalStr, startIndex);
            }
            else//last section
            {
                finalIndex = html.IndexOf("<!--BODY_END-->", startIndex);
            }
            
            string partialHtml = html.Substring(startIndex, finalIndex - startIndex);
            return partialHtml;
        }

        private static void WriteHtmlReportNumToTxt()
        {
            string targetPath = @"D:\EranDoc\Android Develop\develop\ODLV";
            string ReportNum = "";
             for (int i = 0; i < 61; i++)
			{
                string HtmpReport = "HtmpReportNum00";
                if (i < 10)
                {
                    HtmpReport = "HtmpReportNum000";
                }

                ReportNum += HtmpReport + i + "_L2" + "\n";
                File.WriteAllText(targetPath + "\\" + "HtmpReportNum" + ".txt", ReportNum, Encoding.UTF8);

			}
             
        }
    }
}
