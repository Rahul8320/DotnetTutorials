using System.Net;
using FormulaApp.Api.Configurations;
using FormulaApp.Api.Models;
using FormulaApp.Api.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace FormulaApp.Api.Services;

public class FanService(HttpClient httpClient, IOptions<ApiServiceConfig> config) : IFanService
{
    private readonly ApiServiceConfig _serviceConfig = config.Value;

    public async Task<List<Fan>?> GetAllFans()
    {
        var response = await httpClient.GetAsync(_serviceConfig.Url);

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return [];
        }

        if (response.StatusCode == HttpStatusCode.OK)
        {
            var fans = await response.Content.ReadFromJsonAsync<List<Fan>>();
            return fans;
        }

        return null;
    }
}
