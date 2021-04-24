using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace TableParser
{
    [TestFixture]
    public class FieldParserTaskTests
    {
        [TestCase("text", new[] { "text" })]
        [TestCase("hello world", new[] { "hello", "world" })]
        [TestCase(" hello  world ", new[] { "hello", "world" })]
        [TestCase("\\ a", new[] { "\\", "a" })]
        [TestCase("\"a \'b\' \'c\' d\"", new[] { "a \'b\' \'c\' d" })]
        [TestCase("\'\"1\" \"2\" \"3\"\'", new[] { "\"1\" \"2\" \"3\"" })]
        [TestCase("a \"b c d e\"", new[] { "a", "b c d e" })]
        [TestCase("a\"b\"", new[] { "a", "b" })]
        [TestCase("a\"b", new[] { "a", "b" })]
        [TestCase(@"\""a""", new[] { "\\", "a" })]
        [TestCase(@"""abc ", new[] { "abc " })]
        [TestCase(@"""\""a""", new[] { "\"a" })]
        [TestCase("\"\"", new[] { "" })]
        [TestCase("a\'b\'c", new[] { "a", "b", "c" })]
        [TestCase("", new string[0])]
        [TestCase("\'\\\' \\\'\'", new[] { "\' \'" })]
        public static void RunTests(string input, string[] expectedResult)
        {
            var actualResult = FieldsParserTask.ParseLine(input);
            Assert.AreEqual(expectedResult.Length, actualResult.Count);
            for (int i = 0; i < expectedResult.Length; ++i)
            {
                Assert.AreEqual(expectedResult[i], actualResult[i].Value);
            }
        }
		
		[TestCase("''", 0, "", 2)]
        [TestCase("'a'", 0, "a", 3)]
        [TestCase("\"a \'b\' \'c\' d\"", 0, "a 'b' 'c' d", 13)]
        [TestCase("\'b\"a\'\"", 0, "b\"a", 5)]
        [TestCase("abc\"defa", 3, "defa", 5)]
        public void RunTest(string line, int startIndex, string expectedValue, int expectedLength)
        {
            var actualToken = QuotedFieldTask.ReadQuotedField(line, startIndex);
            Assert.AreEqual(actualToken, new Token(expectedValue, startIndex, expectedLength));
        }
    }
	
	public static class CharExtensions
    {
        public static bool IsQuote(this char symbol)
        {
            return (symbol == '\"' || symbol == '\'') ? true : false;
        }
    }

    public class FieldsParserTask
    {
        public static List<Token> ParseLine(string line)
        {
            var builder = new StringBuilder();
            var list = new List<Token>();
            for (var i = 0; i < line.Length; i++)
            {
                if (line[i].IsQuote())
                {
                    i = ReadQuotedField(line, i, list);
                    continue;
                }
                if (line[i] == ' ' && builder.Length != 0)
                {
                    ReadField(i - 1, list, builder);                   
                    continue;
                }
                if ((line[i] == ' ') && builder.Length == 0) continue;
                builder.Append(line[i]);
                if (i + 1 == line.Length && builder.Length != 0 ||
                   (i + 1 < line.Length && (line[i + 1].IsQuote())))
                    ReadField(i, list, builder);
            }

            return list;
        }
        
        private static void ReadField(int endIndex, List<Token> list, StringBuilder builder)
        {
            if (builder.Length != 0)
            {
                list.Add(new Token(builder.ToString(), endIndex + 1 - builder.Length, builder.Length));
                builder.Clear();
            }
        }

        public static int ReadQuotedField(string line, int startIndex, List<Token> list)
        {
            var quotedToken = QuotedFieldTask.ReadQuotedField(line, startIndex);
            list.Add(quotedToken);
            return quotedToken.GetIndexNextToToken() - 1;
        }
    }
}
