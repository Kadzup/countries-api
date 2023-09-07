using System.Text.Json.Serialization;

namespace countries_api.Models;

public class CountryName
{
    public CountryName(string common, string official)
    {
        Common = common ?? throw new ArgumentNullException(nameof(common));
        Official = official ?? throw new ArgumentNullException(nameof(official));
    }

    [JsonPropertyName("common")]
    public string Common { get; set; }

    [JsonPropertyName("official")]
    public string Official { get; set; }
}

