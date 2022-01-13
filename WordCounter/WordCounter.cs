using System;
using System.IO;
using System.Linq;
using HtmlAgilityPack;

namespace WordCounter
{
    public class WordCounter
    {
        private const string InvalidSiteUrl = "Invalid site url! Usage: WordCounter.exe <site url>";

        private readonly string _inputString;
        private readonly string _filePath;
        private readonly TextWriter _textWriter;
        
        private int _wordCount = 0;
        private int _uniqueWordCount = 0;

        public WordCounter(string inputString, string filePath, TextWriter textWriter)
        {
            _inputString = inputString;
            _filePath = filePath;
            _textWriter = textWriter;
        }

        public void CountWordsOnPage()
        {
            var web = new HtmlWeb();

            try
            {
                var document = web.Load(_inputString);

                var nodes = document.DocumentNode.SelectNodes("//text()[not(parent::script)][not(parent::style)]")
                    .Select(t => t.InnerHtml);

                char[] separator = {' '};

                var allWords = nodes.SelectMany(node => node
                    .Split(separator, StringSplitOptions.RemoveEmptyEntries)
                    .Where(word => char.IsLetter(word[0]))
                    .Select(word => word.ToLower())
                    .ToList()
                ).ToList();
                
                _wordCount = allWords.Count;

                _uniqueWordCount = allWords.Distinct().Count();
            }
            catch
            {
                throw new Exception(InvalidSiteUrl);
            }
        }
        
        public void PrintWordCountToFile()
        {
            using var streamWriter = new StreamWriter(_filePath);
            streamWriter.WriteLine("Word count: " + _wordCount);
        }
        
        public void PrintInfoToConsole()
        {
            _textWriter.WriteLine("Word count: " + _wordCount);
            _textWriter.WriteLine("Unique word count: " + _uniqueWordCount);
        }
    }
}