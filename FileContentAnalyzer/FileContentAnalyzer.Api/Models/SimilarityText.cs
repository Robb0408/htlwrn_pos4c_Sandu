using System.ComponentModel.DataAnnotations;

namespace FileContentAnalyzer.Api.Models
{
    public class SimilarityText
    {
        [Required]
        public string Text1 { get; set; } = string.Empty;
        [Required]
        public string Text2 { get; set; } = string.Empty;
    }
}
