using System;
using System.Text.Json;
using countries_api.Controllers;
using countries_api.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;

namespace contries_api_ut;

public class CountriesControllerTest
{
    private static readonly string apiUrl = "https://restcountries.com/v3.1/all?fields=name,currencies,capital,region,languages,population";

    static private async Task<List<Country>?> FetchCountries()
    {
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
    public async void GetCountriesNoArgs()
    {
        var loggerMock = new Mock<ILogger<CountriesController>>();

        var inMemorySettings = new Dictionary<string, string> {
            {"ApiUrl", apiUrl},
        };

        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        var controller = new CountriesController(loggerMock.Object, configuration);

        var result = await controller.GetCountries();

        Assert.NotEmpty(result);
    }

    [Fact]
    public async void GetCountriesByName()
    {
        var loggerMock = new Mock<ILogger<CountriesController>>();

        var inMemorySettings = new Dictionary<string, string> {
            {"ApiUrl", apiUrl},
        };

        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        var controller = new CountriesController(loggerMock.Object, configuration);

        var result = await controller.GetCountries(name:"uk");

        Assert.NotEmpty(result);
        Assert.True(result?.Any(country => country.Name.Common == "Ukraine"));
    }

    [Fact]
    public async void GetCountriesByPopulation()
    {
        var loggerMock = new Mock<ILogger<CountriesController>>();

        var inMemorySettings = new Dictionary<string, string> {
            {"ApiUrl", apiUrl},
        };

        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        var controller = new CountriesController(loggerMock.Object, configuration);

        var result = await controller.GetCountries(population: 5);

        Assert.NotEmpty(result);
        Assert.True(result?.Any(country => country.Population <= 5 * 1_000_000));
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
