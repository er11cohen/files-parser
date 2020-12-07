using System.IO;
using System.Text;

namespace ReadFile
{
    class HokLeisrael
    {
        static string prefix = "<html><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=windows-1255\"></head>"
                          + " <body lang=EN-US link=\"#3377cc\" vlink=\"#3377cc\" alink=\"#6095d7\" style=\"tab-interval:36.0pt; font-family: David;\">"
                          // +"<div dir=\"rtl\" style=\"text-align: justify; padding:15;font-size:28;\">";
                          + "<div dir=\"rtl\" style=\"text-align: justify;\">";

        static string suffix = "</div></body></html>";

        static void Main(string[] args)
        {

            parseAllDays();
            //parseOneFile();

        }
        //public static void parseOneFile()
        //{
        //    string result;
        //    string pathFile = @"C:\Users\Eran\Desktop\html parashot\Intro\Intro\Intro_7.html";
        //    using (StreamReader reader = new StreamReader(pathFile, Encoding.Default))
        //    {
        //        result = reader.ReadToEnd();
        //    }

        //    result = result.Replace("<script type=\"text/javascript\" src=\"b_defaults.js\"></script>", "");
        //    result = result.Replace("<script type=\"text/javascript\" src=\"b_params.js\"></script>", "");
        //    result = result.Replace("<script type=\"text/javascript\" src=\"b_bh3.js\"></script>", "");
        //    result = result.Replace("background-color: #ffffff;", "background-color: #ffffff; color:black;");
        //    result = result.Replace("font-size", "fz").Replace("<hr style='height:8px;'>", "");

        //    File.WriteAllText(@"C:\Users\Eran\Desktop\html parashot\Intro\Intro\Intro_8.html", result, Encoding.UTF8);
        //}

        public static void parseAllDays()
        {
            //string parentPath = @"D:\Eran\EranDoc\Android Develop\develop\HokLeisrael\html parashot";
            //string targetPath = @"D:\Eran\EranDoc\Android Develop\develop\HokLeisrael\finalParashotWifhHrefAndBold";
            //string parentPath = @"C:\Users\eran_cohen\Documents\GitHub\misc\HL\input";
            string parentPath = @"C:\Users\eran_cohen\Documents\GitHub\files-parser\ReadFile\assets\htmlParashot";
            string targetPath = @"C:\Users\eran_cohen\Documents\GitHub\misc\HL\output";

            foreach (var pathDirectory in Directory.GetDirectories(parentPath))
            {
                string directoryName = System.IO.Path.GetFileName(pathDirectory);
                bool isExistsDirectory = System.IO.Directory.Exists(targetPath + "\\" + directoryName);
                if (!isExistsDirectory)
                    System.IO.Directory.CreateDirectory(targetPath + "\\" + directoryName);


                foreach (var pathFile in Directory.GetFiles(pathDirectory))
                {
                    // string fileName = System.IO.Path.GetFileName(path);
                    string fileNameTarget = System.IO.Path.GetFileNameWithoutExtension(pathFile);

                    bool isExistsSubDirecrory = System.IO.Directory.Exists(targetPath + "\\" + directoryName + "\\" + fileNameTarget);

                    if (!isExistsSubDirecrory)
                        Directory.CreateDirectory(targetPath + "\\" + directoryName + "\\" + fileNameTarget);

                    string result;
                    using (StreamReader reader = new StreamReader(pathFile, Encoding.Default))
                    {
                        result = reader.ReadToEnd();
                    }
                    result = result.Replace("<script type=\"text/javascript\" src=\"b_defaults.js\"></script>", "");
                    result = result.Replace("<script type=\"text/javascript\" src=\"b_params.js\"></script>", "");
                    result = result.Replace("<script type=\"text/javascript\" src=\"b_bh3.js\"></script>", "");
                    result = result.Replace("background-color: #ffffff;", "background-color: #ffffff; color:black;");
                    result = result.Replace("font-size", "fz").Replace("<hr style='height:8px;'>", "");

                    string aliyaHtml = "<small style='background-color:#7694DA; color:white;'>&nbsp";
                    result = result.Replace(aliyaHtml, aliyaHtml + "*");

                    result = result.Replace("שָׁל. ", "שָׁלוֹ").Replace("שְׁל. ", "שְׁלוֹ");

                    result = BoldRashiTora(result);
                    result = BoldMikra(result);

                    for (int i = 0; i < 5; i++)//sunday until friday night
                    {
                        string dayHtml = GetDayString(result, i);

                        //עבור א-ה
                        //נביאים
                        dayHtml = dayHtml.Replace("HtmpReportNum0000_L2", "navie").Replace("HtmpReportNum0007_L2", "navie")
                                .Replace("HtmpReportNum0014_L2", "navie").Replace("HtmpReportNum0021_L2", "navie")
                                .Replace("HtmpReportNum0028_L2", "navie");
                        //כתובים
                        dayHtml = dayHtml.Replace("HtmpReportNum0001_L2", "ketuvim").Replace("HtmpReportNum0008_L2", "ketuvim")
                                .Replace("HtmpReportNum0015_L2", "ketuvim").Replace("HtmpReportNum0022_L2", "ketuvim")
                                .Replace("HtmpReportNum0029_L2", "ketuvim");
                        //משנה
                        dayHtml = dayHtml.Replace("HtmpReportNum0002_L2", "misnha").Replace("HtmpReportNum0009_L2", "misnha")
                                .Replace("HtmpReportNum0016_L2", "misnha").Replace("HtmpReportNum0023_L2", "misnha")
                                .Replace("HtmpReportNum0030_L2", "misnha");
                        //גמרא
                        dayHtml = dayHtml.Replace("HtmpReportNum0003_L2", "gemara").Replace("HtmpReportNum0010_L2", "gemara")
                                .Replace("HtmpReportNum0017_L2", "gemara").Replace("HtmpReportNum0024_L2", "gemara")
                                .Replace("HtmpReportNum0031_L2", "gemara");
                        //זוהר
                        dayHtml = dayHtml.Replace("HtmpReportNum0004_L2", "zohar").Replace("HtmpReportNum0011_L2", "zohar")
                                .Replace("HtmpReportNum0018_L2", "zohar").Replace("HtmpReportNum0025_L2", "zohar")
                                .Replace("HtmpReportNum0032_L2", "zohar");

                        dayHtml = AddZoharTranslate(dayHtml);

                        //הלכה
                        dayHtml = dayHtml.Replace("HtmpReportNum0005_L2", "hlacha").Replace("HtmpReportNum0012_L2", "hlacha")
                                .Replace("HtmpReportNum0019_L2", "hlacha").Replace("HtmpReportNum0026_L2", "hlacha")
                                .Replace("HtmpReportNum0033_L2", "hlacha");
                        //מוסר
                        dayHtml = dayHtml.Replace("HtmpReportNum0006_L2", "musar").Replace("HtmpReportNum0013_L2", "musar")
                                .Replace("HtmpReportNum0020_L2", "musar").Replace("HtmpReportNum0027_L2", "musar")
                                .Replace("HtmpReportNum0034_L2", "musar");
                       
                        File.WriteAllText(targetPath + "\\" + directoryName + "\\" + fileNameTarget + "\\" + fileNameTarget + "_" + (i + 1) + ".html", dayHtml, Encoding.UTF8);
                    }


                    string fridayNight = "HtmpReportNum0005_L3";//ליל שישי
                    string hftara = "HtmpReportNum0035_L2";//הפטרה
                    string friday = "HtmpReportNum0006_L3";//יום שישי

                    int offsetFridayNight = result.IndexOf(fridayNight);
                    int startFridayNight = result.IndexOf(fridayNight, offsetFridayNight + 1) - 9;


                    int offsethftara = result.IndexOf(hftara);
                    int startOffsetHftara = result.IndexOf(hftara, offsethftara + 1) - 9;

                    int offsetFriday = result.IndexOf(friday);
                    int startOffsetFriday = result.IndexOf(friday, offsetFriday + 1) - 9;

                    if (startOffsetFriday < startOffsetHftara)
                    {
                        //fridayNight
                        string dayString = prefix + result.Substring(startFridayNight, startOffsetFriday - startFridayNight) + suffix;
                       
                        File.WriteAllText(targetPath + "\\" + directoryName + "\\" + fileNameTarget + "\\" + fileNameTarget + "_6" + ".html", dayString, Encoding.UTF8);

                        //friday
                        ////
                        string Href1 = "HtmpReportNum0006_L3";
                        int startOffset = result.IndexOf(Href1);
                        int start = result.IndexOf(Href1, startOffset + 1) - 9;
                        int end = result.IndexOf("<!--BODY_END-->");
                        dayString = prefix + result.Substring(start, end - start) + suffix;

                        string doubleHaftara = "Vayetse,Pkude,Shmot,Vayakhel,Aharemot,Kdoshim";
                        if (doubleHaftara.Contains(fileNameTarget))
                        {
                            //עבור יום שישי
                            dayString = dayString.Replace("HtmpReportNum0035_L2", "haftara"/*"haftaraSfaradit"*/).Replace("HtmpReportNum0036_L2", "haftaraAshkenaz")
                                    .Replace("HtmpReportNum0037_L2", "misnha")
                                   .Replace("HtmpReportNum0038_L2", "gemara").Replace("HtmpReportNum0039_L2", "zohar")
                                   .Replace("HtmpReportNum0040_L2", "hlacha").Replace("HtmpReportNum0041_L2", "musar");
                            dayString = AddZoharTranslate(dayString);
                        }
                        else
                        {
                            //עבור יום שישי
                            dayString = dayString.Replace("HtmpReportNum0035_L2", "haftara").Replace("HtmpReportNum0036_L2", "misnha")
                                   .Replace("HtmpReportNum0037_L2", "gemara").Replace("HtmpReportNum0038_L2", "zohar")
                                   .Replace("HtmpReportNum0039_L2", "hlacha").Replace("HtmpReportNum0040_L2", "musar");
                            dayString = AddZoharTranslate(dayString);
                        }

                        
                        File.WriteAllText(targetPath + "\\" + directoryName + "\\" + fileNameTarget + "\\" + fileNameTarget + "_7" + ".html", dayString, Encoding.UTF8);
                        ////
                    }
                    else//ההפטרה לפני יום שישי
                    {
                        //fridayNight
                        string dayString = prefix + result.Substring(startFridayNight, startOffsetHftara - startFridayNight) + suffix;
                        
                        File.WriteAllText(targetPath + "\\" + directoryName + "\\" + fileNameTarget + "\\" + fileNameTarget + "_6" + ".html", dayString, Encoding.UTF8);

                        //friday
                        int end = result.IndexOf("<!--BODY_END-->");
                        dayString = prefix + result.Substring(startOffsetHftara, end - startOffsetHftara) + suffix;

                        //עבור יום שישי
                        dayString = dayString.Replace("HtmpReportNum0035_L2", "haftara").Replace("HtmpReportNum0036_L2", "misnha")
                               .Replace("HtmpReportNum0037_L2", "gemara").Replace("HtmpReportNum0038_L2", "zohar")
                               .Replace("HtmpReportNum0039_L2", "hlacha").Replace("HtmpReportNum0040_L2", "musar");
                        dayString = AddZoharTranslate(dayString);
                        
                        File.WriteAllText(targetPath + "\\" + directoryName + "\\" + fileNameTarget + "\\" + fileNameTarget + "_7" + ".html", dayString, Encoding.UTF8);


                    }
                    
                    ////friday
                    //////
                    //string Href1 = "HtmpReportNum0006_L3";
                    //int startOffset = result.IndexOf(Href1);
                    //int start = result.IndexOf(Href1, startOffset + 1) - 9;
                    //int end = result.IndexOf("<!--BODY_END-->");
                    //string dayString = prefix + result.Substring(start, end - start) + suffix;
                    //File.WriteAllText(targetPath + "\\" + directoryName + "\\" + fileNameTarget + "\\" + fileNameTarget + "_7" + ".html", dayString, Encoding.UTF8);
                    //////
                }


            }

        }

        public static string AddZoharTranslate(string result)
        {
            result = result.Replace("<small>&nbspתרגום הזוהר&nbsp</small>", "<a name=\"zoharTranslate\"><small>&nbspתרגום הזוהר&nbsp</small></a>");
            return result;
        }

        public static string GetDayString(string result, int i)
        {
            string Href1 = "HtmpReportNum000" + i + "_L3";
            string Href2 = "HtmpReportNum000" + (i + 1) + "_L3";

            int startOffset = result.IndexOf(Href1);
            int endOffset = result.IndexOf(Href2);

            int start = result.IndexOf(Href1, startOffset + 1) - 9;
            int end = result.IndexOf(Href2, endOffset + 1) - 9;
            string dayString = result.Substring(start, end - start);

            return prefix + dayString + suffix;
        }


        private static string BoldMikra(string result)
        {
            int SBLHebrewOffset = result.IndexOf("<span style='font-family:SBL Hebrew;'>");
            //  int HftaraOffset = result.IndexOf("HtmpReportNum0035_L2");//the first in header
            int HftaraOffset = result.IndexOf("HtmpReportNum0035_L2", result.IndexOf("HtmpReportNum0035_L2") + 1);//the second

            while (SBLHebrewOffset > 0 && SBLHebrewOffset < HftaraOffset)
            {
                result = result.Insert(SBLHebrewOffset, "<b>");
                while (result[SBLHebrewOffset] != '<' || result[SBLHebrewOffset + 1] != '/'
                    || result[SBLHebrewOffset + 2] != 's' || result[SBLHebrewOffset + 3] != 'p'
                    || result[SBLHebrewOffset + 4] != 'a' || result[SBLHebrewOffset + 5] != 'n'
                    || result[SBLHebrewOffset + 6] != '>')
                {
                    SBLHebrewOffset++;
                }
                SBLHebrewOffset = SBLHebrewOffset + 7;
                result = result.Insert(SBLHebrewOffset, "</b>");
                SBLHebrewOffset = result.IndexOf("<span style='font-family:SBL Hebrew;'>", SBLHebrewOffset + 1);

                HftaraOffset = result.IndexOf("HtmpReportNum0035_L2", result.IndexOf("HtmpReportNum0035_L2") + 1);//because the character change in all loop
            }


            return result;
        }


        private static string BoldRashiTora(string result)
        {

            string rashi = "<span style='background-color:RGB(51,119,204); color:RGB(255,255,255);'>"
                                                  + "<small>&nbspרש''י&nbsp</small></span>";
            int SBLHebrewOffset = result.IndexOf(rashi) + 109;
            //  int HftaraOffset = result.IndexOf("HtmpReportNum0035_L2");//the first in header
            int HftaraOffset = result.IndexOf("HtmpReportNum0035_L2", result.IndexOf("HtmpReportNum0035_L2") + 1);//the second
            bool bold = false;
            while (SBLHebrewOffset > 0 && SBLHebrewOffset < HftaraOffset)
            {


                result = result.Insert(SBLHebrewOffset, "<b>");
                SBLHebrewOffset += 4;
                /* while (result[SBLHebrewOffset] != '<' || result[SBLHebrewOffset + 1] != '/'
                     || result[SBLHebrewOffset + 2] != 's' || result[SBLHebrewOffset + 3] != 'p'
                     || result[SBLHebrewOffset + 4] != 'a' || result[SBLHebrewOffset + 5] != 'n'
                     || result[SBLHebrewOffset + 6] != '>')
                */
                bold = false;
                while (!result.Substring(SBLHebrewOffset).StartsWith("<span style='font-family:SBL Hebrew;'>")
                       && SBLHebrewOffset < HftaraOffset)
                {
                    if (result[SBLHebrewOffset].Equals('.'))
                    {
                        result = result.Insert(SBLHebrewOffset + 1, "</b>");
                        SBLHebrewOffset += 5;
                        bold = false;
                    }
                    else if (result[SBLHebrewOffset] == ':' && result[SBLHebrewOffset + 1] != 'R'
                        && result[SBLHebrewOffset + 1] != '#' && result[SBLHebrewOffset + 1] != 'w'
                        && result[SBLHebrewOffset + 2] != '<' && !bold)
                    {
                        result = result.Insert(SBLHebrewOffset + 1, "<b>");
                        SBLHebrewOffset += 4;
                        bold = true;
                    }
                    else
                    {
                        SBLHebrewOffset++;
                    }

                }

                if (bold)
                {
                    result = result.Insert(SBLHebrewOffset, "</b>");
                }

                SBLHebrewOffset = result.IndexOf(rashi, SBLHebrewOffset);
                //  SBLHebrewOffset += 109;
                HftaraOffset = result.IndexOf("HtmpReportNum0035_L2", result.IndexOf("HtmpReportNum0035_L2") + 1);//because the character change in all loop
            }


            return result;
        }

    }
}
