using System;

namespace WordCounter
{
    class Program
    {
        private const string InvalidArgumentsCount = "Incorrect number of arguments! Usage: WordCounter.exe <site url>";
        private const string FilePath = "../../../word-count.txt";
        
        private static string GetInputString(string[] args)
        {
            if (args.Length != 1)
            {
                throw new Exception(InvalidArgumentsCount);
            }
            return args[0];
        }

        public static void Main(string[] args)
        {
            try
            {
                string inputString = GetInputString(args);

                var wordCounter = new WordCounter(inputString, FilePath, Console.Out);
                wordCounter.CountWordsOnPage();
                wordCounter.PrintWordCountToFile();
                wordCounter.PrintInfoToConsole();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
