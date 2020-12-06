using System.IO;
using System.Text;

namespace WikiDownloadBIH
{
    class WikiDownloadBIH
    {
        static void Main(string[] args)
        {
            string result;
            string csvPath = @"C:\Users\eran_cohen\Documents\GitHub\files-parser\WikiDownloadBIH\assets\benIshHaiCSV.csv";
            string targetPath = @"C:\Users\eran_cohen\Documents\GitHub\final\";

            string[] csvParse = ReadAndSplitCsvFile(csvPath);

            using (var webClient = new System.Net.WebClient())
            {
                webClient.Encoding = Encoding.UTF8;
                for (int i = 0; i < csvParse.Length; i++)
                {
                    if (csvParse[i] != "")
                    {
                        string[] parashaSplit = csvParse[i].Replace("\r", "").Split(',');
                        string wikiPath = parashaSplit[6];
                        result = webClient.DownloadString(wikiPath);
                        result = BenIshHi.BenIshHi.ClearHtmlString(result);
                        File.WriteAllText(targetPath + parashaSplit[1] + "\\" + parashaSplit[4] + "\\" + parashaSplit[3] + ".html", result, Encoding.UTF8);
                    }
                }
            }
            
        }

        private static string[] ReadAndSplitCsvFile(string csvPath)
        {
            string[] result;
            using (StreamReader reader = new StreamReader(csvPath, Encoding.UTF8))
            {
                result = reader.ReadToEnd().Split('\n');
            }
            return result;
        }
    }
}
