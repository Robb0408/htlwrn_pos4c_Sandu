using FileCombiner;

namespace AsciiCombinerTest
{
    public class CombinerUnitTest
    {
        [Fact]
        public void Combine_ShouldCombineLines()
        {
            // Arrange
            List<string> lines = new List<string>
            {
            "  *    *  " +
            "          " +
            "\\        /" +
            " \\______/ ",

            "          " +
            "    /\\    " +
            "          " +
            "          "
            };

            Assert.Equal(
                "  *    *  " + 
                "    /\\    " + 
                "\\        /" + 
                " \\______/ ", 
                AsciiCombiner.Combine(lines)
                );
        }

        [Fact]
        public void Combine_ShouldCombineFiles()
        {
            List<string> contents = new()
            {
                // ne doch war richtig nvm
            };
        }
    }
}