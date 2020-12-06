using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabiEliezer
{
    class RabiEliezer
    {
        static string prefix = "<html><head></head>"
                         + " <body font-family: David;\">"

      
           + "<div dir=\"rtl\" >";

        static string suffix = "</div></body></html>";
        static string final = @"D:\Eran\EranDoc\Android Develop\develop\RabiEliezer\final";
        static void Main(string[] args)
        {
            //ParseFromLocale();
            ParseFromWeb();
        }

        private static void ParseFromWeb()
        {
            string[] letters =
            {
                 "א", "ב", "ג", "ד", "ה", "ו", "ז", "ח", "ט", "י",
                    "יא", "יב", "יג", "יד", "טו", "טז", "יז", "יח", "יט", "כ",
                    "כא", "כב", "כג", "כד", "כה", "כו","כז", "כח", "כט", "ל",
                    "לא", "לב", "לג", "לד", "לה", "לו", "לז", "לח", "לט", "מ",
                    "מא", "מב", "מג", "מד", "מה", "מו", "מז", "מח", "מט", "נ",
                    "נא", "נב", "נג", "נד"
            };
            //string[] letters =
            //{
            //     "כז", 
            //};
            using (var webClient = new System.Net.WebClient())
            {
                webClient.Encoding = Encoding.UTF8;
                for (int i = 0; i < letters.Length; i++)
                {
                    string wikiPath = "https://he.wikisource.org/wiki/" + "פרקי_דרבי_אליעזר_פרק_" + letters[i];
                    string result = webClient.DownloadString(wikiPath);
                    result = ClearHtmlString(result);
                    File.WriteAllText(final + "\\" + (i+1) + ".html", result, Encoding.UTF8);
                }
            }
        }


        private static void ParseFromLocale()
        {
            string original = @"D:\Eran\EranDoc\Android Develop\develop\RabiEliezer\original";
            foreach (var filePath in Directory.GetFiles(original))
            {
                string fileName = System.IO.Path.GetFileNameWithoutExtension(filePath);
                string result;
                using (StreamReader reader = new StreamReader(filePath, Encoding.UTF8))
                {
                    result = reader.ReadToEnd();
                }

                result = ClearHtmlString(result);
                File.WriteAllText(final + "\\" + fileName + ".html", result, Encoding.UTF8);
            }
        }

        public static string ClearHtmlString(string result)
        {
            result = result.Replace("<a", "<a1");
            result = result.Replace("</a>", "</a1>");

            result = result.Replace(" · ", " ").Replace("&lt;&lt;", "")
                    .Replace("&gt;&gt;", "").Replace("·", "").Replace("font-size", "fz")
                    .Replace("dd", "dd2").Replace("background", "bg").Replace("border", "brd").Replace("hr", "hr2");

           int start = result.IndexOf("<div id=\"mw-content-text\"");
           int final;
           if (start != -1)
           {
                final = result.IndexOf("<div class=\"printfooter\">", start);
                result = result.Substring(start, final - start);
           }

            start = result.IndexOf("<span class=\"mw-editsection\">");
            if (start != -1)
            {
                final = result.IndexOf("</span>", start);
                result = result.Remove(start, final - start);
            }

            start = result.IndexOf("<span class=\"mw-editsection-bracket\">");
            if (start != -1)
            {
                final = result.IndexOf("</span>", start);
                result = result.Remove(start, final - start);
            }

            while (result.IndexOf("<script>") != -1)
            {
                start = result.IndexOf("<script>");
                final = result.IndexOf("</script>", start) + 9;
                result = result.Remove(start, final - start);
            }

            while (result.IndexOf("<noscript>") != -1)
            {
                start = result.IndexOf("<noscript>");
                final = result.IndexOf("</noscript>", start) + 11;
                result = result.Remove(start, final - start);
            }

            result = result.Replace("חזרה לרשימת הפרקים", "");
            result = result.Replace("מסורת חכמים", "");
            result = result.Replace("עריכה", "");
            result = result.Replace("רבי אליעזר הולך ללמוד תורה:", "");
            result = result.Replace("בראשית רבה מב א", "");
            result = result.Replace("פרקי דרבי אליעזר פרק א", "");
            result = result.Replace("פרקי דרבי אליעזר פרק ב", "");

            return prefix + result + suffix;
        }
    }
}
