using System.Text.Json;
using countries_api.Controllers;
using countries_api.Models;

namespace contries_api_ut;

public class CountriesControllerTest
{
    private async Task<List<Country>?> FetchCountries()
    {
        const string apiUrl = "https://restcountries.com/v3.1/all?fields=name,currencies,capital,region,languages,population";

        using (HttpClient client = new HttpClient())
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                string jsonValue = await response.Content.ReadAsStringAsync();
                List<Country>? countries = JsonSerializer.Deserialize<List<Country>>(jsonValue);

                return countries;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }

        return null;
    }

    [Fact]
    public async void FindByName()
    {
        var countries = await FetchCountries();
        string name = "uk";

        var found = CountriesController.FindByName(countries, name);

        Assert.NotEmpty(found);
        Assert.True(found?.Any(country => country.Name.Common == "Ukraine"));
    }

    [Fact]
    public async void FindByNameNotFound()
    {
        var countries = await FetchCountries();
        string name = "test";

        var found = CountriesController.FindByName(countries, name);

        Assert.Empty(found);
    }

    [Fact]
    public async void FindByPopulation()
    {
        var countries = await FetchCountries();
        int population = 10;

        var found = CountriesController.FindByPopulation(countries, population);

        Assert.NotEmpty(found);
        Assert.True(found?.Any(country => country.Population <= population * 1_000_000));
    }

    [Fact]
    public async void FindByPopulationNotFound()
    {
        var countries = await FetchCountries();
        int population = 0;

        var found = CountriesController.FindByPopulation(countries, population);

        Assert.Empty(found);
    }

    [Fact]
    public async void SortAsc()
    {
        var countries = await FetchCountries();

        var sorted = CountriesController.SortByName(countries, "ascend");

        Assert.Equal(sorted, countries?.OrderBy(country => country.Name.Common).ToList());
    }

    [Fact]
    public async void SortDes()
    {
        var countries = await FetchCountries();

        var sorted = CountriesController.SortByName(countries, "descend");

        Assert.Equal(sorted, countries?.OrderByDescending(country => country.Name.Common).ToList());
    }

    [Fact]
    public async void Pagination()
    {
        var countries = await FetchCountries();

        var found = CountriesController.Pagination(countries, 5, 1);

        Assert.NotEmpty(found);
        Assert.Equal(5, found?.Count);

        var foundNext = CountriesController.Pagination(countries, 5, 2);

        Assert.NotEmpty(foundNext);
        Assert.NotEqual(found, foundNext);
    }
}
