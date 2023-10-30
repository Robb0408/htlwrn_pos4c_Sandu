using System.Text.Json.Serialization;

namespace LogAnalysis.Logic
{
	public class Photo
	{
		[JsonPropertyName("pic")]
		public required string Pic { get; set; }

		[JsonPropertyName("takenBy")]
		public required string TakenBy { get; set; }

		[JsonPropertyName("uploadYear")]
		public int UploadYear { get; set; }
	}
}

