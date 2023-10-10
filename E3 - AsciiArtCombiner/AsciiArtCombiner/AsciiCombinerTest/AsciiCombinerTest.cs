using FileCombiner;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace AsciiCombinerTest
{
    public class CombinerUnitTest
    {
        [Fact]
        public void Combine_ShouldCombine()
        {
            List<string> contents = new()
            {
                "+====+\r\n|    |\r\n|    |\r\n|    |\r\n+====+",
                "      \r\n (  ) \r\n  )(  \r\n (  ) \r\n      ",
                "      \r\n  ::  \r\n      \r\n  ..  \r\n      "
            };
            string result = "+====+\r\n|(::)|\r\n| )( |\r\n|(..)|\r\n+====+";
            Assert.Equal(result, AsciiCombiner.Combine(contents));
        }

        [Fact]
        public void Combine_ShouldNotChange()
        {
            List<string> contents = new()
            {
                "+====+",
                "+----+"
            };
            Assert.Equal(contents[0], AsciiCombiner.Combine(contents));
        }

        [Fact]
        public void Combine_ShouldCombineWithDifferentSize()
        {
            List<string> contents = new()
            {
                "H ll  W rld!",
                " e  o  o    !"
            };
            Assert.Equal("Hello World!!", AsciiCombiner.Combine(contents));
        }
    }
}