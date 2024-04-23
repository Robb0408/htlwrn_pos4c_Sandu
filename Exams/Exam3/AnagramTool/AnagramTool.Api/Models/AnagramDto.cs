// HAU: ℹ️ remove not used usings
using System.ComponentModel.DataAnnotations;

namespace AnagramTool.Api.Models;

// HAU: ℹ️ use a more descriptive name for Request Model e.g. AnagramRequestModel
public class AnagramDto
{
    public string Word1 { get; set; } = string.Empty;
    public string Word2 { get; set; } = string.Empty;
}