# Best Practices for Kodeorganisering - Modellklasser i Blazor

## Innledning

Når Blazor-prosjektet ditt vokser, blir det viktig å organisere koden på en måte som gjør den lett å vedlikeholde, teste og gjenbruke. Modellklasser bør flyttes ut av Razor-komponenter og organiseres i dedikerte mapper og namespaces.

---

## 1. Nåværende situasjon (Dårlig praksis)

### Problemer med inline modellklasser

I den nåværende `Weather.razor`-filen er `WeatherForecast`-klassen definert inne i komponenten:

```razor
@code {
    // Komponent-logikk

    private class WeatherForecast  // 🚫 Dårlig praksis
    {
        public DateOnly Date { get; set; }
        public int TemperatureC { get; set; }
        public string? Summary { get; set; }
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
}
```

**Problemer med denne tilnærmingen:**

- ❌ **Ikke gjenbrukbar** - kan kun brukes i denne komponenten
- ❌ **Vanskelig å teste** - må teste hele komponenten for å teste modellen
- ❌ **Dårlig separasjon** - blander presentasjon og data-modell
- ❌ **Skalerer ikke** - blir kaotisk når prosjektet vokser

<div style="page-break-after:always;"></div>

## 2. Anbefalt mappestruktur

### 2.1 Grunnleggende struktur

```text
1-First-BlazorWebApp/
├── Components/
│   ├── Pages/           # Blazor-sider
│   └── Layout/          # Layout-komponenter
├── Models/              # 📁 Alle modellklasser
│   ├── Weather/         # 📁 Domene-spesifikke modeller
│   ├── User/            # 📁 Bruker-relaterte modeller
│   └── Common/          # 📁 Delte modeller
├── Services/            # 📁 Tjenester og business logic
├── Data/               # 📁 Database-relatert (hvis relevant)
├── Utilities/          # 📁 Hjelpeklasser
└── wwwroot/            # Statiske filer
```

### 2.2 Avansert struktur (for større prosjekter)

```text
1-First-BlazorWebApp/
├── Components/
├── Core/               # 📁 Kjernefunksjonalitet
│   ├── Models/         # 📁 Alle modellklasser
│   │   ├── Entities/   # 📁 Database-entiteter
│   │   ├── DTOs/       # 📁 Data Transfer Objects
│   │   └── ViewModels/ # 📁 View-spesifikke modeller
│   ├── Services/       # 📁 Tjenester
│   ├── Interfaces/     # 📁 Grensesnitt
│   └── Extensions/     # 📁 Extension methods
├── Infrastructure/     # 📁 Eksterne avhengigheter
└── Shared/            # 📁 Delt kode på tvers av prosjekter
```

<div style="page-break-after:always;"></div>

## 3. Praktisk implementasjon

### 3.1 Opprett modellklasser

La oss flytte `WeatherForecast` ut av komponenten:

#### Opprett: `Models/Weather/WeatherForecast.cs`

```csharp
using System.ComponentModel.DataAnnotations;

namespace _1_First_BlazorWebApp.Models.Weather
{
    /// <summary>
    /// Representerer en værprognose for en spesifikk dag
    /// </summary>
    public class WeatherForecast
    {
        /// <summary>
        /// Datoen for prognosen
        /// </summary>
        [Required]
        public DateOnly Date { get; set; }
        
        /// <summary>
        /// Temperatur i Celsius
        /// </summary>
        [Range(-50, 60, ErrorMessage = "Temperaturen må være mellom -50°C og 60°C")]
        public int TemperatureC { get; set; }
        
        /// <summary>
        /// Beskrivelse av værforholdene
        /// </summary>
        [Required(ErrorMessage = "Sammendrag er påkrevd")]
        [StringLength(100, ErrorMessage = "Sammendrag kan ikke være lengre enn 100 tegn")]
        public string? Summary { get; set; }
        
        /// <summary>
        /// Temperatur i Fahrenheit (beregnet property)
        /// </summary>
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
        
        /// <summary>
        /// Indikerer om det er kaldt (under 10°C)
        /// </summary>
        public bool IsCold => TemperatureC < 10;
        
        /// <summary>
        /// Indikerer om det er varmt (over 25°C)
        /// </summary>
        public bool IsHot => TemperatureC > 25;
        
        /// <summary>
        /// Returnerer en vennlig beskrivelse av temperaturen
        /// </summary>
        public string TemperatureDescription => TemperatureC switch
        {
            < 0 => "Frysende",
            < 10 => "Kaldt",
            < 20 => "Behagelig",
            < 30 => "Varmt",
            _ => "Meget varmt"
        };
    }
}
```

#### Opprett: `Models/Weather/WeatherSummary.cs`

```csharp
namespace _1_First_BlazorWebApp.Models.Weather
{
    /// <summary>
    /// Sammendrag av værdata for en periode
    /// </summary>
    public class WeatherSummary
    {
        public double AverageTemperature { get; set; }
        public int MinTemperature { get; set; }
        public int MaxTemperature { get; set; }
        public int TotalDays { get; set; }
        public string MostCommonCondition { get; set; } = string.Empty;
        
        /// <summary>
        /// Oppretter et sammendrag basert på en liste med prognoser
        /// </summary>
        public static WeatherSummary CreateFromForecasts(IEnumerable<WeatherForecast> forecasts)
        {
            var forecastList = forecasts.ToList();
            
            if (!forecastList.Any())
            {
                return new WeatherSummary();
            }
            
            return new WeatherSummary
            {
                AverageTemperature = Math.Round(forecastList.Average(f => f.TemperatureC), 1),
                MinTemperature = forecastList.Min(f => f.TemperatureC),
                MaxTemperature = forecastList.Max(f => f.TemperatureC),
                TotalDays = forecastList.Count,
                MostCommonCondition = forecastList
                    .GroupBy(f => f.Summary)
                    .OrderByDescending(g => g.Count())
                    .First().Key ?? "Ukjent"
            };
        }
    }
}
```

### 3.2 Opprett tjenesteklasser

#### Opprett: `Services/WeatherService.cs`

```csharp
using _1_First_BlazorWebApp.Models.Weather;

namespace _1_First_BlazorWebApp.Services
{
    public interface IWeatherService
    {
        Task<WeatherForecast[]> GetForecastAsync(int days = 5);
        Task<WeatherSummary> GetSummaryAsync(int days = 5);
    }
    
    public class WeatherService : IWeatherService
    {
        private readonly string[] _summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild",
            "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        
        public async Task<WeatherForecast[]> GetForecastAsync(int days = 5)
        {
            // Simuler API-kall
            await Task.Delay(500);
            
            var startDate = DateOnly.FromDateTime(DateTime.Now);
            
            return Enumerable.Range(1, days).Select(index => new WeatherForecast
            {
                Date = startDate.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = _summaries[Random.Shared.Next(_summaries.Length)]
            }).ToArray();
        }
        
        public async Task<WeatherSummary> GetSummaryAsync(int days = 5)
        {
            var forecasts = await GetForecastAsync(days);
            return WeatherSummary.CreateFromForecasts(forecasts);
        }
    }
}
```

### 3.3 Registrer tjenester

Oppdater `Program.cs`:

```csharp
using _1_First_BlazorWebApp.Components;
using _1_First_BlazorWebApp.Services;  // Legg til denne linjen

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Registrer custom tjenester
builder.Services.AddScoped<IWeatherService, WeatherService>();  // Legg til denne linjen

var app = builder.Build();

// Resten av konfigurasjonen...
app.Run();
```

### 3.4 Oppdater Weather.razor

Nå kan vi forenkle komponenten betydelig:

```razor
@page "/weather"
@attribute [StreamRendering]
@inject IWeatherService WeatherService
@using _1_First_BlazorWebApp.Models.Weather
@using _1_First_BlazorWebApp.Services

<PageTitle>Weather</PageTitle>

<div class="weather-page">
    <h1>Værprognose</h1>
    
    <p>Denne komponenten viser værdata ved hjelp av organiserte modeller og tjenester.</p>
    
    @if (forecasts == null)
    {
        <div class="loading">
            <p><em>Laster værdata...</em></p>
            <div class="spinner"></div>
        </div>
    }
    else
    {
        <!-- Sammendrag -->
        @if (summary != null)
        {
            <div class="weather-summary card mb-4">
                <div class="card-header">
                    <h3>Sammendrag (@summary.TotalDays dager)</h3>
                </div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-3">
                            <strong>Gjennomsnitt:</strong> @summary.AverageTemperature°C
                        </div>
                        <div class="col-md-3">
                            <strong>Min:</strong> @summary.MinTemperature°C
                        </div>
                        <div class="col-md-3">
                            <strong>Max:</strong> @summary.MaxTemperature°C
                        </div>
                        <div class="col-md-3">
                            <strong>Vanligst:</strong> @summary.MostCommonCondition
                        </div>
                    </div>
                </div>
            </div>
        }
        
        <!-- Detaljert prognose -->
        <div class="weather-forecast">
            <table class="table table-striped">
                <thead>
                    <tr>
                        <th>Dato</th>
                        <th>Temperatur</th>
                        <th>Følelse</th>
                        <th>Forhold</th>
                        <th>Status</th>
                    </tr>
                </thead>
                <tbody>
                    @foreach (var forecast in forecasts)
                    {
                        <tr class="@GetRowCssClass(forecast)">
                            <td>
                                <strong>@forecast.Date.ToString("ddd, dd. MMM", new CultureInfo("nb-NO"))</strong>
                            </td>
                            <td>
                                <span class="temperature">
                                    @forecast.TemperatureC°C 
                                    <small class="text-muted">(@forecast.TemperatureF°F)</small>
                                </span>
                            </td>
                            <td>
                                <span class="badge @GetTemperatureBadgeClass(forecast)">
                                    @forecast.TemperatureDescription
                                </span>
                            </td>
                            <td>@forecast.Summary</td>
                            <td>
                                @if (forecast.IsCold)
                                {
                                    <span class="badge bg-primary">❄️ Kaldt</span>
                                }
                                else if (forecast.IsHot)
                                {
                                    <span class="badge bg-warning">🔥 Varmt</span>
                                }
                                else
                                {
                                    <span class="badge bg-success">😊 Behagelig</span>
                                }
                            </td>
                        </tr>
                    }
                </tbody>
            </table>
        </div>
        
        <!-- Handlinger -->
        <div class="weather-actions mt-4">
            <button class="btn btn-primary" @onclick="RefreshData">
                🔄 Last inn på nytt
            </button>
            <button class="btn btn-secondary" @onclick="LoadMoreDays">
                📅 Last flere dager
            </button>
        </div>
    }
</div>

@code {
    private WeatherForecast[]? forecasts;
    private WeatherSummary? summary;
    private int currentDays = 5;

    protected override async Task OnInitializedAsync()
    {
        await LoadWeatherData();
    }
    
    private async Task LoadWeatherData()
    {
        forecasts = null; // Vis loading
        StateHasChanged();
        
        forecasts = await WeatherService.GetForecastAsync(currentDays);
        summary = await WeatherService.GetSummaryAsync(currentDays);
    }
    
    private async Task RefreshData()
    {
        await LoadWeatherData();
    }
    
    private async Task LoadMoreDays()
    {
        currentDays += 5;
        await LoadWeatherData();
    }
    
    private string GetRowCssClass(WeatherForecast forecast)
    {
        return forecast switch
        {
            { IsCold: true } => "table-info",
            { IsHot: true } => "table-warning",
            _ => ""
        };
    }
    
    private string GetTemperatureBadgeClass(WeatherForecast forecast)
    {
        return forecast switch
        {
            { TemperatureC: < 0 } => "bg-primary",
            { TemperatureC: < 10 } => "bg-info", 
            { TemperatureC: < 20 } => "bg-success",
            { TemperatureC: < 30 } => "bg-warning",
            _ => "bg-danger"
        };
    }
}

<style>
    .weather-page {
        max-width: 1200px;
        margin: 0 auto;
    }
    
    .weather-summary {
        background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
        color: white;
    }
    
    .weather-summary .card-header {
        background: rgba(255, 255, 255, 0.1);
        border-bottom: 1px solid rgba(255, 255, 255, 0.2);
    }
    
    .loading {
        text-align: center;
        padding: 2rem;
    }
    
    .spinner {
        border: 4px solid #f3f3f3;
        border-top: 4px solid #3498db;
        border-radius: 50%;
        width: 40px;
        height: 40px;
        animation: spin 2s linear infinite;
        margin: 0 auto;
    }
    
    @keyframes spin {
        0% { transform: rotate(0deg); }
        100% { transform: rotate(360deg); }
    }
    
    .temperature {
        font-size: 1.1rem;
        font-weight: bold;
    }
    
    .weather-actions {
        text-align: center;
    }
</style>
```

<div style="page-break-after:always;"></div>

## 4. Avanserte organisering-patterns

### 4.1 Repository Pattern (for dataaksess)

#### Opprett: `Data/IWeatherRepository.cs`

```csharp
using _1_First_BlazorWebApp.Models.Weather;

namespace _1_First_BlazorWebApp.Data
{
    public interface IWeatherRepository
    {
        Task<WeatherForecast[]> GetForecastsAsync(DateTime fromDate, int days);
        Task<WeatherForecast?> GetForecastByDateAsync(DateOnly date);
        Task SaveForecastAsync(WeatherForecast forecast);
    }
    
    // Mock implementation (erstatt med database-kode senere)
    public class MockWeatherRepository : IWeatherRepository
    {
        private readonly List<WeatherForecast> _forecasts = new();
        
        public async Task<WeatherForecast[]> GetForecastsAsync(DateTime fromDate, int days)
        {
            await Task.Delay(100); // Simuler database-kall
            
            // Return mock data eller data fra _forecasts
            return GenerateMockForecasts(fromDate, days);
        }
        
        public async Task<WeatherForecast?> GetForecastByDateAsync(DateOnly date)
        {
            await Task.Delay(50);
            return _forecasts.FirstOrDefault(f => f.Date == date);
        }
        
        public async Task SaveForecastAsync(WeatherForecast forecast)
        {
            await Task.Delay(100);
            _forecasts.RemoveAll(f => f.Date == forecast.Date);
            _forecasts.Add(forecast);
        }
        
        private WeatherForecast[] GenerateMockForecasts(DateTime fromDate, int days)
        {
            var summaries = new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };
            var startDate = DateOnly.FromDateTime(fromDate);
            
            return Enumerable.Range(0, days).Select(index => new WeatherForecast
            {
                Date = startDate.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = summaries[Random.Shared.Next(summaries.Length)]
            }).ToArray();
        }
    }
}
```

### 4.2 DTO Pattern (Data Transfer Objects)

#### Opprett: `Models/Weather/DTOs/WeatherForecastDto.cs`

```csharp
namespace _1_First_BlazorWebApp.Models.Weather.DTOs
{
    /// <summary>
    /// DTO for data-overføring fra eksterne APIer
    /// </summary>
    public class WeatherForecastDto
    {
        public string Date { get; set; } = string.Empty;
        public double Temperature { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        
        /// <summary>
        /// Konverterer DTO til domain model
        /// </summary>
        public WeatherForecast ToWeatherForecast()
        {
            return new WeatherForecast
            {
                Date = DateOnly.Parse(Date),
                TemperatureC = (int)Math.Round(Temperature),
                Summary = Description
            };
        }
    }
}
```
<div style="page-break-after:always;"></div>

### 4.3 Factory Pattern

#### Opprett: `Services/WeatherServiceFactory.cs`

```csharp
using _1_First_BlazorWebApp.Data;

namespace _1_First_BlazorWebApp.Services
{
    public interface IWeatherServiceFactory
    {
        IWeatherService CreateService(string serviceType = "default");
    }
    
    public class WeatherServiceFactory : IWeatherServiceFactory
    {
        private readonly IServiceProvider _serviceProvider;
        
        public WeatherServiceFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        
        public IWeatherService CreateService(string serviceType = "default")
        {
            return serviceType.ToLower() switch
            {
                "mock" => new MockWeatherService(),
                "api" => new ApiWeatherService(_serviceProvider.GetRequiredService<HttpClient>()),
                _ => _serviceProvider.GetRequiredService<IWeatherService>()
            };
        }
    }
    
    // Implementasjoner...
    public class MockWeatherService : IWeatherService
    {
        // Mock implementasjon
        public async Task<WeatherForecast[]> GetForecastAsync(int days = 5)
        {
            // Mock data
            await Task.Delay(100);
            return Array.Empty<WeatherForecast>();
        }
        
        public async Task<WeatherSummary> GetSummaryAsync(int days = 5)
        {
            await Task.Delay(100);
            return new WeatherSummary();
        }
    }
    
    public class ApiWeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;
        
        public ApiWeatherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public async Task<WeatherForecast[]> GetForecastAsync(int days = 5)
        {
            // Kall til ekstern API
            // var response = await _httpClient.GetFromJsonAsync<WeatherForecastDto[]>($"api/weather?days={days}");
            // return response?.Select(dto => dto.ToWeatherForecast()).ToArray() ?? Array.Empty<WeatherForecast>();
            
            await Task.Delay(200);
            return Array.Empty<WeatherForecast>();
        }
        
        public async Task<WeatherSummary> GetSummaryAsync(int days = 5)
        {
            var forecasts = await GetForecastAsync(days);
            return WeatherSummary.CreateFromForecasts(forecasts);
        }
    }
}
```

<div style="page-break-after:always;"></div>

## 5. Oppdatert Program.cs med alle tjenester

```csharp
using _1_First_BlazorWebApp.Components;
using _1_First_BlazorWebApp.Services;
using _1_First_BlazorWebApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Registrer HTTP Client
builder.Services.AddHttpClient();

// Registrer repositories
builder.Services.AddScoped<IWeatherRepository, MockWeatherRepository>();

// Registrer tjenester
builder.Services.AddScoped<IWeatherService, WeatherService>();
builder.Services.AddScoped<IWeatherServiceFactory, WeatherServiceFactory>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();
app.UseAntiforgery();
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
```

<div style="page-break-after:always;"></div>

## 6. Testing

### 6.1 Enhetstest for modeller

#### Opprett: `Tests/Models/WeatherForecastTests.cs`

```csharp
using _1_First_BlazorWebApp.Models.Weather;
using Xunit;

namespace _1_First_BlazorWebApp.Tests.Models
{
    public class WeatherForecastTests
    {
        [Fact]
        public void TemperatureF_ShouldCalculateCorrectly()
        {
            // Arrange
            var forecast = new WeatherForecast { TemperatureC = 0 };
            
            // Act
            var temperatureF = forecast.TemperatureF;
            
            // Assert
            Assert.Equal(32, temperatureF);
        }
        
        [Theory]
        [InlineData(-10, true)]
        [InlineData(5, true)]
        [InlineData(15, false)]
        public void IsCold_ShouldReturnCorrectValue(int temperatureC, bool expected)
        {
            // Arrange
            var forecast = new WeatherForecast { TemperatureC = temperatureC };
            
            // Act & Assert
            Assert.Equal(expected, forecast.IsCold);
        }
        
        [Theory]
        [InlineData(-5, "Frysende")]
        [InlineData(5, "Kaldt")]
        [InlineData(15, "Behagelig")]
        [InlineData(25, "Varmt")]
        [InlineData(35, "Meget varmt")]
        public void TemperatureDescription_ShouldReturnCorrectDescription(int temperature, string expected)
        {
            // Arrange
            var forecast = new WeatherForecast { TemperatureC = temperature };
            
            // Act & Assert
            Assert.Equal(expected, forecast.TemperatureDescription);
        }
    }
}
```

<div style="page-break-after:always;"></div>

## 7. Best Practices sammendrag

### ✅ DO (Gjør dette)

1. **Separer bekymringer**: Modeller, tjenester og komponenter i egne mapper
2. **Bruk namespaces**: Organiser kode logisk med meningsfulle namespaces
3. **Interface-segregation**: Definer grensesnitt for alle tjenester
4. **Dependency Injection**: Registrer og injiser avhengigheter
5. **Dokumenter koden**: Bruk XML-comments for offentlige APIs
6. **Valider data**: Bruk DataAnnotations for modell-validering
7. **Test modeller**: Skriv enhetstest for business logic
8. **Bruk DTOs**: For data-overføring fra eksterne kilder

### ❌ DON'T (Ikke gjør dette)

1. **Inline modeller**: Ikke definer modeller inne i komponenter
2. **Alt i en fil**: Ikke putt alle klasser i samme fil
3. **Hardkod data**: Ikke hardkod verdier - bruk konfigurasjon
4. **Ignorer async**: Ikke blokkér async-operasjoner
5. **Skap avhengigheter**: Unngå tette koblinger mellom komponenter
6. **Glem feilhåndtering**: Alltid håndter potensielle feil

---

## 8. Fremtidige utvidelser

Denne strukturen forbereder deg for:

- **Database-integrasjon** med Entity Framework
- **Eksterne API-kall** for reelle værdata
- **Caching** av data for bedre ytelse
- **SignalR** for real-time oppdateringer
- **Unit testing** av all business logic
- **Integration testing** av tjenester

---

## Konklusjon

Ved å organisere koden på denne måten får du:

✅ **Bedre vedlikeholdbarhet** - klar separasjon av bekymringer
✅ **Gjenbrukbarhet** - modeller kan brukes på tvers av komponenter  
✅ **Testbarhet** - enklere å skrive enhetstester
✅ **Skalerbarhet** - strukturen vokser med prosjektet
✅ **Profesjonell kode** - følger industri-standarder

