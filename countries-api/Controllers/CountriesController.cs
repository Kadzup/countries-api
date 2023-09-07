﻿using System.Text.Json;
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
    public async Task<IEnumerable<Country>?> FindCountriesByName(string name)
    {
        List<Country>? countries = await FetchCountries();

        if(name == "common")
        {
            return countries;
        }

        List<Country>? foundCountries = countries?.
            Where(country =>
            {
                return country.Name.Common.Contains(name, StringComparison.OrdinalIgnoreCase) ||
                       country.Name.Official.Contains(name, StringComparison.OrdinalIgnoreCase);
            })
            .ToList();

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
}