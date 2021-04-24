using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace TableParser
{
    [TestFixture]
    public class QuotedFieldTaskTests
    {
        [TestCase("''", 0, "", 2)]
        [TestCase("'a'", 0, "a", 3)]
        [TestCase("\"a \'b\' \'c\' d\"", 0, "a 'b' 'c' d", 13)]
        [TestCase("\'b\"a\'\"", 0, "b\"a", 5)]
		[TestCase("abc\"defa", 3, "defa", 5)]
        public void Test(string line, int startIndex, string expectedValue, int expectedLength)
        {
            var actualToken = QuotedFieldTask.ReadQuotedField(line, startIndex);
            Assert.AreEqual(actualToken, new Token(expectedValue, startIndex, expectedLength));
        }
    }

    class QuotedFieldTask
    {
        public static Token ReadQuotedField(string line, int startIndex)
        {
            StringBuilder builder = new StringBuilder();
            int i;
            for ( i = startIndex + 1; i < line.Length; i++)
            {
                if (line[i] == line[startIndex] && line[i - 1] != '\\')
				{
                    i++;
					break;
				}
				if (line[i] == '\\') 
                    continue;
                builder.Append(line[i]);
            }
            return new Token(builder.ToString(), startIndex, i - startIndex);
        }
    }
}
