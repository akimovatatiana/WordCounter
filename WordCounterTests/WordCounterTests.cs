using System;
using System.IO;
using NUnit.Framework;

namespace WordCounterTests
{
    public class WordCounterTests
    {
        private const string InvalidSiteUrl = "Invalid site url! Usage: WordCounter.exe <site url>";
        private const string FilePath = "../../../word-count-test.txt";

        [Test]
        public void CountWordsOnPage_WithEmptyUrlString_ShouldThrowException()
        {
            var inputString = "";
            var wordCounter = new WordCounter.WordCounter(inputString, FilePath, Console.Out);
            
            try
            {
                wordCounter.CountWordsOnPage();
            }
            catch (Exception e)
            {
                Assert.AreEqual(InvalidSiteUrl, e.Message);
                return;
            }
            
            Assert.Fail("The expected exception was not thrown.");
        }
        
        [Test]
        public void CountWordsOnPage_WithInvalidUrlString_ShouldThrowException()
        {
            var inputString = "abs";
            var wordCounter = new WordCounter.WordCounter(inputString, FilePath, Console.Out);
            
            try
            {
                wordCounter.CountWordsOnPage();
            }
            catch (Exception e)
            {
                Assert.AreEqual(InvalidSiteUrl, e.Message);
                return;
            }
            
            Assert.Fail("The expected exception was not thrown.");
        }
        
        [Test]
        public void PrintWordCountToFile_WithValidUrlString_ShouldPrintCorrectWordCountToFile()
        {
            var inputString = "https://abzakovo.com/";
            var expectedFileContent = "Word count: 367\n";
            
            var wordCounter = new WordCounter.WordCounter(inputString, FilePath, Console.Out);
            
            wordCounter.CountWordsOnPage();
            wordCounter.PrintWordCountToFile();
            
            using var streamReader = new StreamReader(FilePath);
            
            Assert.AreEqual(expectedFileContent, streamReader.ReadToEnd());
        }
        
        [Test]
        public void PrintInfoToConsole_WithValidUrlString_ShouldPrintCorrectInfoToConsole()
        {
            var inputString = "https://abzakovo.com/";
            var expectedInfo = "Word count: 367\nUnique word count: 205\n";

            var stringWriter = new StringWriter();
            
            var wordCounter = new WordCounter.WordCounter(inputString, FilePath, stringWriter);
            
            wordCounter.CountWordsOnPage();
            wordCounter.PrintInfoToConsole();

            Assert.AreEqual(expectedInfo, stringWriter.ToString());
        }
    }
}