using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenIshHi
{
    public static class BenIshHi
    {
         static void Main(string[] args)
        {
            //parseOneFile();
            parseAllYear();
        }

        private static void parseAllYear()
        {
            //string parentPath = @"D:\EranDoc\Android Develop\develop\BenIshHi\original";
            //string targetPath = @"D:\EranDoc\Android Develop\develop\BenIshHi\final";

            string parentPath = @"D:\Eran\EranDoc\Android Develop\develop\BenIshHi\original";
            string targetPath = @"D:\Eran\EranDoc\Android Develop\develop\BenIshHi\final";

            foreach (var pathDirectory in Directory.GetDirectories(parentPath))
            {
                string directoryName = System.IO.Path.GetFileName(pathDirectory);
                bool isExistsDirectory = System.IO.Directory.Exists(targetPath + "\\" + directoryName);
                if (!isExistsDirectory)
                    System.IO.Directory.CreateDirectory(targetPath + "\\" + directoryName);

                foreach (var pathDirectory2 in Directory.GetDirectories(pathDirectory))
                {
                    string directoryName2 = System.IO.Path.GetFileName(pathDirectory2);
                    bool isExistsDirectory2 = System.IO.Directory.Exists(pathDirectory2 + "\\" + directoryName2);
                    if (!isExistsDirectory2)
                        System.IO.Directory.CreateDirectory(targetPath + "\\" + directoryName + "\\" + directoryName2);


                    foreach (var pathFile in Directory.GetFiles(pathDirectory2))
                    {
                        string fileNameTarget = System.IO.Path.GetFileNameWithoutExtension(pathFile);
                      //  if (fileNameTarget != "Balak")
                        {
                            string result = ClearHtmlPath(pathFile);
                            File.WriteAllText(targetPath + "\\" + directoryName + "\\" + directoryName2 + "\\" + fileNameTarget + ".html", result, Encoding.UTF8);
                   
                        }
                    }
                }
            }
        }

        public static void parseOneFile()
        {

            string parash = "Breshit";
            //string pathFile = @"C:\Users\Eran\Desktop\develop\BenIshHi\original\Year_2\Breshit\" + parash + ".html";
            string pathFile = @"D:\Eran\EranDoc\Android Develop\output\" + parash + ".html";
            string result = ClearHtmlPath(pathFile);

            //File.WriteAllText(@"C:\Users\Eran\Desktop\develop\BenIshHi\" + parash + ".html", result, Encoding.UTF8);
            File.WriteAllText(@"D:\Eran\EranDoc\Android Develop\output\" + parash + ".html", result, Encoding.UTF8);
        }

        public static string ClearHtmlPath(string pathFile)
        {
            string result;

            using (StreamReader reader = new StreamReader(pathFile, Encoding.UTF8))
            {
                result = reader.ReadToEnd();
            }

            return ClearHtmlString(result);
        }

        public static string ClearHtmlString(string result)
        {
            int start = result.IndexOf("<title>");
            int final = result.IndexOf("</head>", start);
            result = result.Remove(start, final - start);

            start = result.IndexOf("<body");
            if (start != -1)
            {
                final = result.IndexOf("<div id=\"content_wrapper\">", start);
                if (final != -1)
                {
                    result = result.Remove(start, final - start);
                    result.Insert(start, "<body>");
                }
            }

            start = result.IndexOf("<div id=\"mw-mf-viewport\">");
            if (start != -1)
            {
                final = result.IndexOf("<div id=\"content\">", start);
                if (final != -1)
                {
                    result = result.Remove(start, final - start);
                }
            }

            start = result.IndexOf("<div id=\"central-auth-images\"");
            if (start != -1)
            {
                final = result.IndexOf("</div", start);
                if (final != -1)
                {
                    result = result.Remove(start, final - start);
                }
            }

            start = result.IndexOf("<div id=\"page-secondary-actions\">");
            if (start != -1)
            {
                final = result.IndexOf("</div", start);
                if (final != -1)
                {
                    result = result.Remove(start, final - start);
                }
            }


            start = result.IndexOf("<div id=\"mw-mf-page-left\"");
            if (start != -1)
            {
                final = result.IndexOf("<div id=\"mw-mf-page-center\"", start);
                if (final == -1)
                {
                    final = result.IndexOf("<div id='mw-mf-page-center'", start);
                }
                result = result.Remove(start, final - start);
            }

            start = result.IndexOf("<form action=\"/w/index.php\" class=\"search-box\">");
            if (start != -1)
            {
                final = result.IndexOf("</form>", start) + 7;
                result = result.Remove(start, final - start);
            }



            start = result.IndexOf("<div class=\"flatToc\"");
            if (start != -1)
            {
                final = result.IndexOf("<p>", start);
                result = result.Remove(start, final - start);
            }

            start = result.IndexOf("<div id=\"toc\"");
            if (start != -1)
            {
                final = result.IndexOf("<p>", start);
                result = result.Remove(start, final - start);
            }

            start = result.IndexOf("<div id=\"siteSub\">");
            if (start != -1)
            {
                final = result.IndexOf("<p>", start);
                result = result.Remove(start, final - start);
            }


            start = result.IndexOf("<div id=\"jump-to-nav\"");
            if (start != -1)
            {
                final = result.IndexOf("</div>", start);
                result = result.Remove(start, final - start);
            }



            start = result.IndexOf("<div id=\"mw-navigation\">");
            if (start != -1)
            {
                final = result.IndexOf("</body>", start);
                result = result.Remove(start, final - start);
            }



            start = result.IndexOf("<div id=\"mw-mf-viewport\"");
            if (start != -1)
            {
                final = result.IndexOf("<div id=\"content\"", start);
                if (final != -1)
                {
                    result = result.Remove(start, final - start);
                }
            }

            start = result.IndexOf("<div class=\"printfooter\"");
            if (start != -1)
            {
                final = result.IndexOf("</div>", start);
                result = result.Remove(start, final - start);
            }

            start = result.IndexOf("<header");
            if (start != -1)
            {
                final = result.IndexOf("</header>", start);
                result = result.Remove(start, final - start);
            }

            start = result.IndexOf("<footer");
            if (start != -1)
            {
                final = result.IndexOf("</footer>", start);
                result = result.Remove(start, final - start);
            }

            start = result.IndexOf("<div id=\"mw-fr-revisiontag\"");
            if (start != -1)
            {
                final = result.IndexOf("</div>", start) + 6;
                result = result.Remove(start, final - start);
            }

            start = result.IndexOf("<div class=\"header\">");
            if (start != -1)
            {
                final = result.IndexOf("</div>", start) + 7;
                result = result.Remove(start, final - start);
            }

            start = result.IndexOf("<a id=\"mw-mf-last-modified\"");
            if (start != -1)
            {
                final = result.IndexOf("</a>", start) + 4;
                result = result.Remove(start, final - start);
            }


            start = result.IndexOf("<ul id=\"page-actions\"");
            if (start != -1)
            {
                final = result.IndexOf("</ul>", start) + 5;
                result = result.Remove(start, final - start);
            }

            start = result.IndexOf("<center>");
            if (start != -1)
            {
                final = result.IndexOf("</center>", start) + 9;
                result = result.Remove(start, final - start);
            }

            start = result.IndexOf("<center>");
            if (start != -1)
            {
                final = result.IndexOf("</center>", start) + 9;
                result = result.Remove(start, final - start);
            }



            //start = 0;
            //while (result.IndexOf("<a",start) != -1)
            //{
            //    start = result.IndexOf("<a",start);
            //    final = result.IndexOf("</a>", start) + 4;

            //    string noRemoveContent = result.Substring(start, final - start);
            //    if (!noRemoveContent.Contains("mw-redirect"))//link in the text
            //    {
            //        result = result.Remove(start, final - start);
            //    }
            //    start++;
            //}

            //remove link in the text and remain only text
            result = result.Replace("<a", "<a1");
            result = result.Replace("</a>", "</a1>");

            result = result.Replace("עריכה", "");

            start = result.IndexOf("<div id=\"footer\"");
            if (start != -1)
            {
                final = result.IndexOf("</div>", start) + 6;
                result = result.Remove(start, final - start);
            }

            start = result.IndexOf("<footer id=\"footer\"");
            if (start != -1)
            {
                final = result.IndexOf("</footer>", start) + 9;
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

            ////ויקפדיה חוגגת עשור
            start = result.IndexOf("<div class=\"mw-dismissable-notice-close\">");
            if (start != -1)
            {
                final = result.IndexOf("</div>", start) + 6;
                result = result.Remove(start, final - start);
            }

            start = result.IndexOf("<div class=\"post-content\"");
            if (start != -1)
            {
                final = result.IndexOf("</div>", start) + 6;
                result = result.Remove(start, final - start);
            }

            start = result.IndexOf("<div class=\"last-modified-bar");
            if (start != -1)
            {
                final = result.IndexOf("</div>", start) + 6;
                result = result.Remove(start, final - start);
            }

            start = result.IndexOf("<div class=\"mw-dismissable-notice-body\">");
            if (start != -1)
            {
                final = result.IndexOf("</div>", start) + 12;
                result = result.Remove(start, final - start);
            }


            start = result.IndexOf("<link");
            if (start != -1)
            {
                final = result.IndexOf("/>", start) + 2;
                result = result.Remove(start, final - start);
            }


            ////


            result = result.Replace(".D7.A4.D7.AA.D7.99.D7.97.D7.94", "intro");
            result = result.Replace(".D7.94.D7.9C.D7.9B.D7.95.D7.AA", "halach");
            result = result.Replace(".D7.90.D7.95.D7.AA_", ""); //אות

            // result = result.Replace(".D7.90", "1").Replace(".D7.91", "2").Replace(".D7.92", "3").Replace(".D7.93", "4").Replace(".D7.94", "5").Replace(".D7.95", "6")
            //  .Replace(".D7.96", "7").Replace(".D7.97", "8").Replace(".D7.98", "9").Replace(".D7.99", "10");

            return result;
        }
    }
}
