using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sederMamadot
{
    class sederMamadot
    {
        static string prefix = "<html><head></head>"
                          + " <body style=\"font-family: David;\">"
                          + "<div dir=\"rtl\" >"
                          + "<center><span style=\"color:#BE32BE;\"><span style=\"font-weight:bold; font-size:18px;\"><span style = \"color:#BE32BE;\" >"
                          + "<small> בס''ד -  כל הזכויות שמורות(c) ל ר' פנחס ראובן שליט''א </small></span></span></span></center>"
                          + "<div>"
                            + "<b><u><span style=\"color:RGB(34,80,136);\"><u>סדר מעמדות</u></span><br></u></b>"
                            + "כתוב במשנה <small>(תענית פרק ד')</small>, התקינו נביאים ראשונים עשרים וארבע משמרות, על כל משמר ומשמר היה מעמד בירושלים, של כהנים של לוים ושל ישראלים. הגיע זמן המשמר לעלות, כהנים ולוים עולים לירושלים, וישראל שבאותו משמר מתכנסים ולעריהם וקוראין במעשה בראשית. ביום הראשון בראשית ויהי רקיע, בשני יהי רקיע ויקוו המים וכו'. ובגמ' שם <small>(כז:)</small> וישראל שבאותו משמר מתכנסין בעריהן וקורין במעשה בראשית: מנהני מילי א''ר יעקב בר אחא אמר רב אסי, אלמלא מעמדות, לא נתקיימו שמים וארץ שנאמר <small>(בראשית טו)</small> ויאמר ה' אלהים במה אדע כי אירשנה, אמר אברהם רבש''ע שמא ישראל חוטאין לפניך אתה עושה להם כדור המבול וכדור הפּלגה, א''ל לאו, אמר לפניו, רבש''ע הודיעני במה אירשנה, א''ל <small>(בראשית טו)</small> קחה לי עגלה משולשת ועז משולשת וגו', אמר לפניו, רבש''ע תינח בזמן שבית המקדש קיים בזמן שאין בית המקדש קיים מה תהא עליהם, אמר לו, כבר תקנתי להם סדר קרבנות, בזמן שקוראין בהן לפני מעלה אני עליהם כאילו הקריבום לפני, ואני מוחל להם על כל עונותיהם. עכ''ל."
                            + "</div>"
                          ;
        static string suffix = "</div></body></html>";
        static void Main(string[] args)
        {
            string parentPath = @"D:\EranDoc\Android Develop\develop\HokLeisrael\addition\sederMamadot\sederMamadotOriginal.html";
            string targetPath = @"D:\EranDoc\Android Develop\develop\HokLeisrael\addition\sederMamadot\final";

            string result;
            using (StreamReader reader = new StreamReader(parentPath, Encoding.Default))
            {
                result = reader.ReadToEnd();
                result = result.Replace("font-size", "fz");
                for (int i = 1; i <= 7; i++)
                {
                    string dayHtml = GetDayString(result, i);
                    File.WriteAllText(targetPath + "/sederMamadot_" + i + ".html", dayHtml, Encoding.UTF8);
                }

            }
        }

        public static string GetDayString(string result, int i)
        {
            string Href1 = "HtmpReportNum000" + i + "_L3";
            string Href2 = "HtmpReportNum000" + (i + 1) + "_L3";
            if (i == 7)
            {
                Href2 = "<!--BODY_END-->";
            }

            int startOffset = result.IndexOf(Href1);
            int endOffset = result.IndexOf(Href2);

            int start = result.IndexOf(Href1, startOffset + 1) - 9;
            int end = result.IndexOf(Href2, endOffset + 1) - 9;
            if (i == 7)
            {
                end = endOffset;
            }
            string dayString = result.Substring(start, end - start);

            return prefix + dayString + suffix;
        }
    }
}
