using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BL
{
    public class FileBL
    {
        public long LinesCnt { get; set; }
        public long WordsCnt { get; set; }
        public long DistinctWordsCnt { get; set; }
        public double AvgSentenceLength { get; set; }
        public int MaxSentenceLength { get; set; }
        public int LongestSeqWithoutK { get; set; }
        public Dictionary<string, int> Colors { get; set; }

        //1//        
        public static long CountLines(string filePath)
        {
            long count = 0;
            using (StreamReader r = new StreamReader(filePath))
            {
                string line;
                while ((line = r.ReadLine()) != null)
                {
                    count++;
                }
            }
            return count;
        }

        //2//
        public static long CountWords(string filePath)
        {
            long wordsCount = 0;
            using (StreamReader file = File.OpenText(filePath))
            {
                string line;
                Regex reg_exp = new Regex("[^a-zA-Z0-9]");
                do
                {
                    line = file.ReadLine();
                    if (line != null)
                    {
                        line = reg_exp.Replace(line, " ");
                        string[] words = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        wordsCount += words.Length;
                    }
                }
                while (line != null);
            }
            return wordsCount;
        }

        //3//
        public static long CountWordsDistinct(string filePath)
        {
            string text = File.ReadAllText(filePath);
            Regex reg_exp = new Regex("[^a-zA-Z0-9]");
            text = reg_exp.Replace(text, " ");
            string[] words = text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var word_query = words.Distinct();
            string[] result = word_query.ToArray();
            return result.Length;
        }

        //4//
        //public static double AvgLengthOfSentenceVers1(string filePath)
        //{
        //    string text = File.ReadAllText(filePath);
        //    text = text.Replace("\n", " ");
        //    string[] sentences = text.Split(new char[] { '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);
        //    double avg = sentences.Select((s) => s.Trim().Length).Average();
        //    return avg;
        //}

        public static double AvgLengthOfSentenceVers2(string filePath)
        {
            string text = File.ReadAllText(filePath);
            string[] sentences = text.Split(new char[] { '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);
            Regex reg_exp = new Regex("[^a-zA-Z0-9]");
            double avg = sentences.Select((s) =>
            {
                s=reg_exp.Replace(s, " ");
                return s.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length;
            }).Average();
            return avg;
        }


        //public static int MaxLengthOfSentenceVers1(string filePath)
        //{
        //    string text = File.ReadAllText(filePath);
        //    text = text.Replace("\n", " ");
        //    string[] sentences = text.Split(new char[] { '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);
        //    int max = sentences.Select((s) => s.Length).Max();
        //    return max;
        //}


        public static int MaxLengthOfSentenceVers2(string filePath)
        {
            string text = File.ReadAllText(filePath);
            string[] sentences = text.Split(new char[] { '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);
            Regex reg_exp = new Regex("[^a-zA-Z0-9]");
            int max = sentences.Select((s) => { s = reg_exp.Replace(s, " "); return s.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length; }).Max();
            return max;
        }


        //6//
        public static int LongestSequenceWithoutK(string filePath)
        {
            string text = File.ReadAllText(filePath);
            Regex regex = new Regex("[^a-zA-Z0-9]");
            text = regex.Replace(text, " ");
            Regex reg_exp = new Regex(@"[^\s]*[kK]{1,}[^\s]*");
            text = reg_exp.Replace(text, "?");
            string[] wordsSeq = text.Split(new string[] { "?" }, StringSplitOptions.RemoveEmptyEntries);
            int result = wordsSeq.Select((ws) => ws.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).Length).Max();
            return result;
        }

        //8//
        public static Dictionary<string, int> CountColors(string filePath)
        {
            Dictionary<string, int> fileColors = new Dictionary<string, int>();
            string[] colorsName = new string[]
            { "black", "white", "gray", "silver", "maroon", "red", "purple", "fushsia", "green", "lime", "olive", "yellow", "navy", "blue", "teal","aqua" };
            List<string> words = new List<string>();
            using (StreamReader file = File.OpenText(filePath))
            {
                string line;
                Regex reg_exp = new Regex("[^a-zA-Z0-9]");
                do
                {
                    line = file.ReadLine();
                    if (line != null)
                    {
                        line = reg_exp.Replace(line, " ");
                        List<string> currentWords = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Where((w)=>colorsName.Contains(w.ToLower())).ToList();
                        words=words.Concat(currentWords).ToList();
                    }
                }
                while (line != null);
            }
            foreach (string clr in colorsName)
            {
                fileColors.Add(clr, words.Where((c) => c.ToLower().Equals(clr)).Count());
            }
            return fileColors;
        }
    }
}
