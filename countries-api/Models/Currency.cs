using System.Text.Json.Serialization;

namespace countries_api.Models;

public class Currency
{
    public Currency(string name, string symbol)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Symbol = symbol ?? throw new ArgumentNullException(nameof(symbol));
    }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("symbol")]
    public string Symbol { get; set; }
}

