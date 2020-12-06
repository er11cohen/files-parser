using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orhotHaim
{
    class orhotHaim
    {
         static string prefix = "<html><head></head>"
                          + " <body style=\"font-family: David;\" dir=\"rtl\">"
                          + "<div  >"
                          + "<center><span style=\"color:#BE32BE;\"><span style=\"font-weight:bold; \">"
                          + "<span style=\"color:#BE32BE;\"><small>בס''ד -  כל הזכויות שמורות (c) ל ר פנחס ראובן שליט''א </small></center></span></span></span></span><CENTER><p></p>"
                          +"<span style=\"font-weight:bold; \">"
                          + "ארחות חיים<BR></span></CENTER><CENTER>רבינו הראש זצלה''ה</CENTER><CENTER><BR>וְאֵלֶּה הַדְּבָרִים שֶׁיִּזָּהֵר בָּהֶם לָסוּר מִמּוֹקְשֵׁי מָוֶת וְלֵאוֹר בְּאוֹר הַחַיִּים<BR><BR></CENTER>"                          
                          + "</div>"
                          ;
         static  string suffix = "</div></body></html>";


        static void Main(string[] args)
        {
            string parentPath = @"D:\EranDoc\Android Develop\develop\HokLeisrael\addition\orhotHaim\orhotHaimOriginal.html";
            string targetPath = @"D:\EranDoc\Android Develop\develop\HokLeisrael\addition\orhotHaim\final";

            string result;
            using (StreamReader reader = new StreamReader(parentPath, Encoding.Default))
            {
                result = reader.ReadToEnd();
                result = result.Replace("font-size", "fz");
                for (int i = 0; i < 7; i++)
                {
                    string dayHtml = GetDayString(result, i);
                    File.WriteAllText(targetPath + "/orhotHaim_" + (i + 1) + ".html", dayHtml, Encoding.UTF8);
                }

            }
        }

        public static string GetDayString(string result, int i)
        {
            string Href1 = "HtmpReportNum000" + i + "_L2";
            string Href2 = "HtmpReportNum000" + (i + 1) + "_L2";
            
            if (i == 6)
            {
                Href2 = "<!--BODY_END-->";
            }

            int startOffset = result.IndexOf(Href1);
            int endOffset = result.IndexOf(Href2);

            int start = result.IndexOf(Href1, startOffset + 1) - 9;
            int end = result.IndexOf(Href2, endOffset + 1) - 9;
            if (i == 6)
            {
                end = endOffset;
            }
            string dayString = result.Substring(start, end - start);

            return prefix + dayString + suffix;
        }
    }
}
