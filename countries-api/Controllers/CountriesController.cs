using System.Text.Json;
using countries_api.Models;
using Microsoft.AspNetCore.Mvc;

namespace countries_api.Controllers;

[ApiController]
[Route("countries")]
public class CountriesController : ControllerBase
{
    private readonly ILogger<CountriesController> _logger;
    private IConfiguration _configuration;

    private const string NameCommon = "common";
    private const string SortAscend = "ascend";
    private const string SortDescend = "descend";
    private const int PopulationDefault = 0;
    private const int CountDefault = 10;
    private const int PageDefault = 1;

    public CountriesController(ILogger<CountriesController> logger, IConfiguration config)
    {
        _logger = logger;
        _configuration = config;
    }

    [HttpGet]
    [Route("getCountries")]
    public async Task<IEnumerable<Country>?> GetCountries(
        string name = NameCommon, string sort = SortAscend, int population = PopulationDefault,
        int count = CountDefault, int page = PageDefault)
    {
        List<Country>? countries = await FetchCountries();

        if(name != NameCommon)
        {
            countries = FindByName(countries, name);
        }

        if(population > PopulationDefault)
        {
            countries = FindByPopulation(countries, population);
        }

        countries = SortByName(countries, sort);
        countries = Pagination(countries, count, page);

        return countries;
    }

    private async Task<List<Country>?> FetchCountries() {
        string apiUrl = _configuration.GetValue<string>("ApiUrl");

        using(HttpClient client = new HttpClient())
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                string jsonValue = await response.Content.ReadAsStringAsync();
                List<Country>? countries = JsonSerializer.Deserialize<List<Country>>(jsonValue);

                return countries;
            }
            catch(HttpRequestException ex) {
                _logger.LogError(ex, ex.Message);
            }
            catch(Exception ex) {
                _logger.LogError(ex, ex.Message);
            }
        }

        return null;
    }

    public static List<Country>? FindByName(List<Country>? countries, string name)
    {
        return countries?.
            Where(country =>
            {
                return country.Name.Common.Contains(name, StringComparison.OrdinalIgnoreCase) ||
                       country.Name.Official.Contains(name, StringComparison.OrdinalIgnoreCase);
            })
            .ToList();
    }

    public static List<Country>? FindByPopulation(List<Country>? countries, int population)
    {
        return countries?.Where(country => country.Population < population * 1_000_000).ToList();
    }

    public static List<Country>? SortByName(List<Country>? countries, string sortingMethod) {
        if(sortingMethod == SortDescend)
        {
            return countries?.OrderByDescending(country => country.Name.Common).ToList();
        }

        return countries?.OrderBy(country => country.Name.Common).ToList();
    }

    public static List<Country>? Pagination(List<Country>? countries, int count, int page)
    {
        return countries?.Skip((page - 1) * count).Take(count).ToList();
    }
}