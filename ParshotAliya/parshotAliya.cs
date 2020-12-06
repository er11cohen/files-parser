using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParshotAliya
{
    class parshotAliya
    {
        static void Main(string[] args)
        {
            string original = @"D:\Eran\EranDoc\Android Develop\develop\HokLeisrael\html parashot\Bamidbar";
            string final = @"D:\Eran\EranDoc\Android Develop\develop\HokLeisrael\parashotAliya";
            string[] aliyaArr = { "ראשון", "שני", "שלישי", "רביעי", "חמישי", "שישי", "שביעי" };
            string aliyaHtml = "<small style='background-color:#7694DA; color:white;'>&nbsp";
            int sundayIndex = 0, mondayIndex, tuesdayIndex, wednesdayIndex, thursdayIndex, fridayNightIndex, fridayIndex;
            int[] alyotDayIndexArray = new int[7];
            alyotDayIndexArray[0] = 1;//for sunday
            string result = String.Empty;

            string parentPath = @"D:\Eran\EranDoc\Android Develop\develop\HokLeisrael\html parashot";
            foreach (var pathDirectory in Directory.GetDirectories(parentPath))
            {
                foreach (var pathFile in Directory.GetFiles(pathDirectory))
                {
                    string fileName = System.IO.Path.GetFileNameWithoutExtension(pathFile);

                    string file;
                    using (StreamReader reader = new StreamReader(pathFile, Encoding.Default))
                    {
                        file = reader.ReadToEnd();
                    }


                    mondayIndex = file.IndexOf("HtmpReportNum0001_L3") + 10;
                    mondayIndex = file.IndexOf("HtmpReportNum0001_L3", mondayIndex);

                    tuesdayIndex = file.IndexOf("HtmpReportNum0002_L3") + 10;
                    tuesdayIndex = file.IndexOf("HtmpReportNum0002_L3", tuesdayIndex);

                    wednesdayIndex = file.IndexOf("HtmpReportNum0003_L3") + 10;
                    wednesdayIndex = file.IndexOf("HtmpReportNum0003_L3", wednesdayIndex);

                    thursdayIndex = file.IndexOf("HtmpReportNum0004_L3") + 10;
                    thursdayIndex = file.IndexOf("HtmpReportNum0004_L3", thursdayIndex);

                    fridayNightIndex = file.IndexOf("HtmpReportNum0005_L3") + 10;
                    fridayNightIndex = file.IndexOf("HtmpReportNum0005_L3", fridayNightIndex);

                    fridayIndex = file.IndexOf("HtmpReportNum0006_L3") + 10;
                    fridayIndex = file.IndexOf("HtmpReportNum0006_L3", fridayIndex);


                    for (int i = 1; i < aliyaArr.Length; i++)
                    {
                        int index = file.IndexOf(aliyaHtml + aliyaArr[i]);

                        if (index < mondayIndex)
                        {
                            alyotDayIndexArray[i] = 1;
                        }
                        else if (index > mondayIndex && index < tuesdayIndex)
                        {
                            alyotDayIndexArray[i] = 2;
                        }
                        else if (index > tuesdayIndex && index < wednesdayIndex)
                        {
                            alyotDayIndexArray[i] = 3;
                        }
                        else if (index > wednesdayIndex && index < thursdayIndex)
                        {
                            alyotDayIndexArray[i] = 4;
                        }
                        else if (index > thursdayIndex && index < fridayNightIndex)
                        {
                            alyotDayIndexArray[i] = 5;
                        }
                        else if (index > fridayNightIndex && index < fridayIndex)
                        {
                            alyotDayIndexArray[i] = 6;
                        }
                        else if (index > fridayIndex)
                        {
                            alyotDayIndexArray[i] = 7;
                        }

                    }

                    
                     result += fileName +"," + string.Join(",", alyotDayIndexArray) + "\n";
                }
           }



            

            File.WriteAllText(final + "\\" + "parashotAliya.csv", result, Encoding.UTF8);
        }
    }
}
