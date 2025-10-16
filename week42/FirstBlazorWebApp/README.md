# Blazor Web App - Prosjektstruktur og Filforklaring

## Innledning

Dette dokumentet forklarer alle viktige deler av et standard Blazor Server-prosjekt. Som nybegynner i Blazor og C# vil dette hjelpe deg å forstå hvordan alt henger sammen.

## Hva er Blazor?

Blazor er et web-framework fra Microsoft som lar deg bygge interaktive webapplikasjoner ved hjelp av C# i stedet for JavaScript. Det finnes to hovedtyper:

- **Blazor Server**: Kjører på serveren, sender UI-oppdateringer via SignalR
- **Blazor WebAssembly**: Kjører i nettleseren via WebAssembly

Dette prosjektet bruker Blazor Server med interaktive komponenter.

---

## Prosjektfiler

### 1. `1-First-BlazorWebApp.csproj`

**Hva det er:** Prosjektfilen som definerer hvordan applikasjonen skal bygges.

```xml
<Project Sdk="Microsoft.NET.Sdk.Web">
  <PropertyGroup>
    <TargetFramework>net10.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
  </PropertyGroup>
</Project>
```

**Forklaring:**

- `Sdk="Microsoft.NET.Sdk.Web"`: Dette er en web-applikasjon
- `TargetFramework>net10.0`: Bruker .NET 10
- `Nullable>enable`: Aktiverer nullable reference types for bedre kodesikkerhet
- `ImplicitUsings>enable`: Automatisk inkludering av vanlige using-statements

<div style="page-break-after:always;"></div>

### 2. `Program.cs`

**Hva det er:** Startpunktet for applikasjonen. Her konfigureres tjenester og HTTP-pipeline.

```csharp
var builder = WebApplication.CreateBuilder(args);

// Legger til tjenester
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Konfigurerer HTTP-pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
```

**Forklaring:**

- `AddRazorComponents()`: Registrerer Razor-komponenter
- `AddInteractiveServerComponents()`: Aktiverer server-side interaktivitet
- `UseExceptionHandler()`: Håndterer feil i produksjon
- `UseHttpsRedirection()`: Tvinger HTTPS
- `UseAntiforgery()`: Beskyttelse mot CSRF-angrep
- `MapRazorComponents<App>()`: Setter opp routing for komponenter

<div style="page-break-after:always;"></div>

## Components-mappen

### 3. `Components/App.razor`

**Hva det er:** Rot-komponenten som definerer HTML-strukturen for hele appen.

```html
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <base href="/" />
    <link rel="stylesheet" href="bootstrap.min.css" />
    <link rel="stylesheet" href="app.css" />
</head>
<body>
    <Routes />
    <ReconnectModal />
    <script src="_framework/blazor.web.js"></script>
</body>
</html>
```

**Forklaring:**

- `<Routes />`: Her rendres alle sider basert på URL
- `<ReconnectModal />`: Vises hvis tilkobling til server mistes
- `blazor.web.js`: Blazor's JavaScript-runtime

### 4. `Components/Routes.razor`

**Hva det er:** Definerer hvordan URL-er mappes til komponenter.

**Viktigheten:** Dette er "trafikk-kontrollen" som bestemmer hvilken side som vises basert på URL-en brukeren besøker.

### 5. `Components/_Imports.razor`

**Hva det er:** Globale using-statements som gjelder for alle komponenter.

**Forklaring:** I stedet for å skrive `@using Microsoft.AspNetCore.Components` i hver fil, defineres det her én gang.

<div style="page-break-after:always;"></div>

## Layout-komponenter

### 6. `Components/Layout/MainLayout.razor`

**Hva det er:** Hovedlayouten som omkranser alle sider.

```razor
@inherits LayoutComponentBase

<div class="page">
    <div class="sidebar">
        <NavMenu />
    </div>
    <main>
        <div class="top-row px-4">
            <a href="https://learn.microsoft.com/aspnet/core/" target="_blank">About</a>
        </div>
        <article class="content px-4">
            @Body
        </article>
    </main>
</div>
```
<div style="page-break-after:always;"></div>

**Forklaring:**

- `@inherits LayoutComponentBase`: Arver fra Blazor's layout-klasse
- `<NavMenu />`: Inkluderer navigasjonsmenyen
- `@Body`: Her rendres innholdet fra hver side

<div style="page-break-after:always;"></div>

### 7. `Components/Layout/NavMenu.razor`

**Hva det er:** Navigasjonsmenyen som vises på alle sider.

```razor
<nav class="nav flex-column">
    <div class="nav-item px-3">
        <NavLink class="nav-link" href="" Match="NavLinkMatch.All">
            <span aria-hidden="true"></span> Home
        </NavLink>
    </div>
    <div class="nav-item px-3">
        <NavLink class="nav-link" href="counter">
            <span aria-hidden="true"></span> Counter
        </NavLink>
    </div>
</nav>
```

**Forklaring:**

- `NavLink`: Blazor-komponent som lager lenker med automatisk CSS-klasser for aktiv side
- `href=""`: Lenke til hjemmesiden
- `href="counter"`: Lenke til counter-siden

---

## Side-komponenter (Pages)

### 8. `Components/Pages/Home.razor`

**Hva det er:** Hjemmesiden av applikasjonen.

```razor
@page "/"

<PageTitle>Home</PageTitle>

<h1>Hello, world!</h1>

Welcome to your new app.
```

**Forklaring:**

- `@page "/"`: Denne siden vises når brukeren går til rot-URL-en
- `<PageTitle>`: Setter tittelen i nettleser-fanen

<div style="page-break-after:always;"></div>

### 9. `Components/Pages/Counter.razor`

**Hva det er:** En interaktiv side som demonstrerer Blazor's evner.

```razor
@page "/counter"
@rendermode InteractiveServer

<PageTitle>Counter</PageTitle>

<h1>Counter</h1>

<p role="status">Current count: @currentCount</p>

<button class="btn btn-primary" @onclick="IncrementCount">Click me</button>

@code {
    private int currentCount = 0;

    private void IncrementCount()
    {
        currentCount++;
    }
}
```

**Forklaring:**

- `@page "/counter"`: Denne siden vises på /counter URL-en
- `@rendermode InteractiveServer`: Aktiverer server-side interaktivitet
- `@currentCount`: Viser verdien av variabelen i HTML
- `@onclick="IncrementCount"`: Knytter klikk-event til C#-metode
- `@code { ... }`: C#-kode som tilhører denne komponenten

### 10. `Components/Pages/Weather.razor`

**Hva det er:** Demonstrerer hvordan man kan vise data fra en service.

**Viktigheten:** Viser hvordan man kan jobbe med data og dependency injection.

### 11. `Components/Pages/Error.razor` og `NotFound.razor`

**Hva det er:** Feilhåndtering-sider.

- `Error.razor`: Vises når det oppstår en feil
- `NotFound.razor`: Vises når brukeren går til en side som ikke finnes

<div style="page-break-after:always;"></div>

## Konfigurasjonsfiler

### 12. `appsettings.json` og `appsettings.Development.json`

**Hva det er:** Konfigurasjonsfiler for applikasjonen.

- `appsettings.json`: Generelle innstillinger
- `appsettings.Development.json`: Innstillinger kun for utviklingsmiljø

**Eksempel:**

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "..."
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information"
    }
  }
}
```

### 13. `Properties/launchSettings.json`

**Hva det er:** Definerer hvordan applikasjonen startes under utvikling.

**Forklaring:** Setter port-nummer, SSL-innstillinger, og miljøvariabler.

<div style="page-break-after:always;"></div>

## Statiske filer (wwwroot)

### 14. `wwwroot/app.css`

**Hva det er:** Globale CSS-stiler for applikasjonen.

### 15. `wwwroot/favicon.png`

**Hva det er:** Ikonet som vises i nettleser-fanen.

### 16. `wwwroot/lib/bootstrap/`

**Hva det er:** Bootstrap CSS-framework for styling.

---

## Viktige konsepter for nybegynnere

### Razor-syntaks

- `@`: Brukes for å bytte mellom HTML og C#
- `@code { }`: C#-kodeblokk
- `@if`, `@foreach`, `@for`: Kontrollstrukturer
- `@page`: Definerer routing
- `@using`: Importerer namespaces

### Komponenter

- **Gjenbrukbare**: Kan brukes flere steder
- **Parametere**: Kan ta imot data via `[Parameter]`
- **Events**: Kan sende data tilbake til foreldrene
- **Lifecycle**: Har metoder som `OnInitialized()`, `OnAfterRender()` etc.

### Data Binding

- **One-way**: `@variabel` - viser data
- **Two-way**: `@bind="variabel"` - synkroniserer input med variabel
- **Event binding**: `@onclick="Method"` - kobler events til metoder

### Dependency Injection

- Tjenester registreres i `Program.cs`
- Injiseres i komponenter med `@inject`
- Eksempel: `@inject HttpClient Http`

<div style="page-break-after:always;"></div>

## Mappehierarki forklart

```
1-First-BlazorWebApp/
├── Components/              # Alle Razor-komponenter
│   ├── Layout/             # Layout-komponenter (nav, layout)
│   ├── Pages/              # Sider som har @page-direktiv
│   ├── App.razor           # Rot-komponent med HTML-struktur
│   ├── Routes.razor        # Routing-konfigurasjon
│   └── _Imports.razor      # Globale using-statements
├── Properties/             # Prosjekt-metadata
├── wwwroot/               # Statiske filer (CSS, bilder, JS)
├── Program.cs             # Applikasjonens startpunkt
└── *.csproj              # Prosjektfil med konfigurasjon
```


## Nyttige ressurser

- [Microsoft Learn - Blazor](https://learn.microsoft.com/en-us/aspnet/core/blazor/)
- [Blazor University](https://blazor-university.com/)
- [.NET Documentation](https://docs.microsoft.com/en-us/dotnet/)

---

