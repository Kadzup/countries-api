using System.Text.Json;
using countries_api.Models;
using Microsoft.AspNetCore.Mvc;

namespace countries_api.Controllers;

[ApiController]
[Route("countries")]
public class CountriesController : ControllerBase
{
    private readonly ILogger<CountriesController> _logger;

    public CountriesController(ILogger<CountriesController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [Route("getCountries")]
    public async Task<IEnumerable<Country>?> GetCountries()
    {
        List<Country>? countries = await FetchCountries();

        return countries;
    }

    [HttpGet]
    [Route("findCountriesByName")]
    public async Task<IEnumerable<Country>?> FindCountriesByName(string name = "common")
    {
        List<Country>? countries = await FetchCountries();

        if(name == "common")
        {
            return countries;
        }

        List<Country>? foundCountries = FindByName(countries, name);

        return foundCountries;
    }

    [HttpGet]
    [Route("findCountriesByPopulation")]
    public async Task<IEnumerable<Country>?> FindCountriesByPopulation(int population = 0)
    {
        List<Country>? countries = await FetchCountries();

        if(population <= 0)
        {
            return countries;
        }

        List<Country>? foundCountries = FindByPopulation(countries, population);

        return foundCountries;
    }

    [HttpGet]
    [Route("sortCountriesByName")]
    public async Task<IEnumerable<Country>?> SortCountriesByName(string sort = "ascend")
    {
        List<Country>? countries = await FetchCountries();

        List<Country>? foundCountries = SortByName(countries, sort);

        return foundCountries;
    }

    [HttpGet]
    [Route("pagination")]
    public async Task<IEnumerable<Country>?> Pagination(int count, int page = 0)
    {
        List<Country>? countries = await FetchCountries();

        List<Country>? foundCountries = Pagination(countries, count, page);

        return foundCountries;
    }

    private async Task<List<Country>?> FetchCountries() {
        const string apiUrl = "https://restcountries.com/v3.1/all?fields=name,currencies,capital,region,languages,population";

        using(HttpClient client = new HttpClient())
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (!response.IsSuccessStatusCode) {
                    throw new Exception("Failed to retrieve countries data");
                }

                string jsonValue = await response.Content.ReadAsStringAsync();
                List<Country>? countries = JsonSerializer.Deserialize<List<Country>>(jsonValue);

                return countries;
            }
            catch(HttpRequestException e) {
                _logger.LogError(1, e.ToString());
            }
            catch(Exception e) {
                _logger.LogError(0, e.ToString());
            }
        }

        return null;
    }

    private List<Country>? FindByName(List<Country>? countries, string name)
    {
        return countries?.
            Where(country =>
            {
                return country.Name.Common.Contains(name, StringComparison.OrdinalIgnoreCase) ||
                       country.Name.Official.Contains(name, StringComparison.OrdinalIgnoreCase);
            })
            .ToList();
    }

    private List<Country>? FindByPopulation(List<Country>? countries, int population)
    {
        return countries?.Where(country => country.Population < population * 1_000_000).ToList();
    }

    private List<Country>? SortByName(List<Country>? countries, string sortingMethod) {
        if(sortingMethod == "descend")
        {
            return countries?.OrderByDescending(country => country.Name.Common).ToList();
        }

        return countries?.OrderBy(country => country.Name.Common).ToList();
    }

    private List<Country>? Pagination(List<Country>? countries, int count, int page)
    {
        return countries?.Skip((page - 1) * count).Take(count).ToList();
    }
}