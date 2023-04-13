using System.Net.Http.Json;
using static MvvmBlazor.Wasm.Pages.FetchData;

namespace MvvmBlazor.Wasm.Models
{
  public interface IFetchDataModel
  {
    Task<WeatherForecast[]> RetrieveForecastsAsync();
  }
  public class FetchDataModel : IFetchDataModel
  {
    private readonly HttpClient _http;
    public FetchDataModel(HttpClient http)
    {
      _http = http;
    }
    public async Task<WeatherForecast[]> RetrieveForecastsAsync()
    {
      WeatherForecast[]? data = await _http.GetFromJsonAsync<WeatherForecast[]?>("sample-data/weather.json");

      if (data == null)
      {
        return Array.Empty<WeatherForecast>();
      }

      return data;
    }
  }
}
