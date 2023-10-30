using System.Text.Json.Serialization;

namespace LogAnalysis.Logic
{
	public class Photo
	{
		[JsonPropertyName("pic")]
		public string Pic { get; set; }

		[JsonPropertyName("takenBy")]
		public string TakenBy { get; set; }

		[JsonPropertyName("uploadYear")]
		public int UploadYear { get; set; }
	}
}

