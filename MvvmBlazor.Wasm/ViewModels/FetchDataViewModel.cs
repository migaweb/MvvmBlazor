using MvvmBlazor.Wasm.Models;
using static MvvmBlazor.Wasm.Pages.FetchData;

namespace MvvmBlazor.Wasm.ViewModels
{
  public interface IViewModelBase
  {
    event EventHandler? OnViewModelUpdate;
  }

  public interface IFetchDataViewModel : IViewModelBase
  {
    List<WeatherForecast>? WeatherForecasts { get; set; }
    void AddSunnyForecast();
    void IncreaseTemperature(int weatherForecastId);
    Task RetrieveForecastsAsync();
  }
  public class FetchDataViewModel : BaseViewModel, IFetchDataViewModel
  {
    private List<WeatherForecast>? _weatherForecasts;
    private readonly IFetchDataModel _fetchDataModel;
    public event EventHandler? OnViewModelUpdate;

    public List<WeatherForecast>? WeatherForecasts
    {
      get => _weatherForecasts;
      set => SetValue(ref _weatherForecasts, value);
    }

    public FetchDataViewModel(IFetchDataModel fetchDataModel)
    {
      _fetchDataModel = fetchDataModel;
    }

    public async Task RetrieveForecastsAsync()
    {
      WeatherForecasts = (await _fetchDataModel.RetrieveForecastsAsync()).ToList();
    }

    public void AddSunnyForecast()
    {
      _weatherForecasts ??= new List<WeatherForecast>();

      _weatherForecasts.Add(new WeatherForecast { 
        Id = _weatherForecasts.Count > 0 ? _weatherForecasts.Max(w => w.Id) + 1 : 1,
        Date = DateOnly.FromDateTime(DateTime.Now),
        Summary = "Sunny",
        TemperatureC = 23
      });
    }

    public void IncreaseTemperature(int weatherForecastId)
    {
      var forecast = _weatherForecasts?.FirstOrDefault(w => w.Id == weatherForecastId);

      if (forecast == null) return;

      forecast.TemperatureC++;

      OnViewModelUpdate?.Invoke(this, new EventArgs());
    }
  }
}
