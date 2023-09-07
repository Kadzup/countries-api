using System.Text.Json.Serialization;

namespace countries_api.Models;

public class Country
{
    public Country(CountryName name, string region, List<string> capital, Dictionary<string, Currency> currencies, Dictionary<string, string> languages, long population)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Region = region ?? throw new ArgumentNullException(nameof(region));
        Capital = capital ?? throw new ArgumentNullException(nameof(capital));
        Currencies = currencies ?? throw new ArgumentNullException(nameof(currencies));
        Languages = languages ?? throw new ArgumentNullException(nameof(languages));
        Population = population;
    }

    [JsonPropertyName("name")]
    public CountryName Name { get; set; }

    [JsonPropertyName("region")]
    public string Region { get; set; }

    [JsonPropertyName("capital")]
    public List<string> Capital { get; set; }

    [JsonPropertyName("currencies")]
    public Dictionary<string, Currency> Currencies { get; set; }

    [JsonPropertyName("languages")]
    public Dictionary<string, string> Languages { get; set; }

    [JsonPropertyName("population")]
    public long Population { get; set; }
}

