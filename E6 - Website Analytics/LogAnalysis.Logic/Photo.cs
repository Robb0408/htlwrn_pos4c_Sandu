using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
