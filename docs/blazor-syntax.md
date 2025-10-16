# Blazor Razor Syntax - Komplett Guide

## Innledning

Blazor bruker **Razor-syntaks** for å blande HTML og C#-kode i samme fil. Razor er en markup-syntaks som lar deg bygge dynamiske webapplikasjoner ved å kombinere statisk HTML med serverside C#-logikk.

---

## 1. Grunnleggende Razor-syntaks

### @-tegnet - Overgangen mellom HTML og C#

`@`-tegnet er det magiske tegnet som forteller Razor at nå kommer C#-kode.

```razor
@* Dette er en kommentar i Razor *@
<h1>Velkommen, @DateTime.Now.ToString("dd.MM.yyyy")!</h1>
```

**Resultat:**

```html
<h1>Velkommen, 06.10.2025!</h1>
```

---

## 2. Variabler og uttrykk

### Enkle uttrykk

```razor
@{
    string navn = "Kari Nordmann";
    int alder = 25;
    DateTime dato = DateTime.Now;
}

<h2>Brukerinformasjon</h2>
<p>Navn: @navn</p>
<p>Alder: @alder år</p>
<p>I dag: @dato.ToString("dddd, dd. MMMM yyyy")</p>
<p>Neste år blir du: @(alder + 1) år</p>
```
<div style="page-break-after:always;"></div>

**Viktig:** Bruk parenteser `@(...)` for komplekse uttrykk.

### Kodeblokker med @{ }

```razor
@{
    // Dette er en kodeblokk - her kan du skrive vanlig C#-kode
    var produkter = new List<string> { "Laptop", "Mus", "Tastatur" };
    var antallProdukter = produkter.Count;
    var erHandlevogn = antallProdukter > 0;
    
    // Beregninger
    decimal totalPris = 15999.00m;
    decimal mva = totalPris * 0.25m;
}

<div class="handlevogn">
    <h3>Din handlevogn (@antallProdukter produkter)</h3>
    <p>Totalpris: @totalPris.ToString("C", new CultureInfo("nb-NO"))</p>
    <p>MVA (25%): @mva.ToString("C", new CultureInfo("nb-NO"))</p>
</div>
```

<div style="page-break-after:always;"></div>

## 3. Kontrollstrukturer

### If-setninger

```razor
@{
    bool erInnlogget = true;
    string brukerrolle = "Admin";
    int poengsum = 85;
}

@* Enkel if *@
@if (erInnlogget)
{
    <div class="velkommen">
        <h2>Velkommen tilbake!</h2>
    </div>
}

@* If-else *@
@if (brukerrolle == "Admin")
{
    <div class="admin-panel">
        <button class="btn btn-danger">Slett bruker</button>
        <button class="btn btn-warning">Rediger innstillinger</button>
    </div>
}
else if (brukerrolle == "Moderator")
{
    <div class="moderator-panel">
        <button class="btn btn-warning">Moderer innhold</button>
    </div>
}
else
{
    <div class="bruker-panel">
        <button class="btn btn-primary">Vis profil</button>
    </div>
}

@* Betinget CSS-klasse *@
<div class="poengsum @(poengsum >= 80 ? "høy-poengsum" : "lav-poengsum")">
    Din poengsum: @poengsum
</div>
```
<div style="page-break-after:always;"></div>

### Switch-uttrykk (C# 8+)

```razor
@{
    string dag = DateTime.Now.DayOfWeek.ToString();
    string dagmelding = dag switch
    {
        "Monday" => "Mandag - Ny uke starter! ☕",
        "Tuesday" => "Tirsdag - Kom igjen! 💪",
        "Wednesday" => "Onsdag - Halvveis der! 🎯",
        "Thursday" => "Torsdag - Snart helg! 🎉",
        "Friday" => "Fredag - HELG! 🥳",
        "Saturday" or "Sunday" => "Helg - Hvil deg! 😴",
        _ => "Ukjent dag 🤔"
    };
}

<div class="dag-melding">
    <h3>@dagmelding</h3>
</div>
```


<div style="page-break-after:always;"></div>

## 4. Loops (Løkker)

### Foreach-løkker

```razor
@{
    var studenter = new List<(string Navn, int Alder, string Studie)>
    {
        ("Emma Hansen", 22, "Informatikk"),
        ("Lars Olsen", 24, "Design"),
        ("Sofia Berg", 21, "Økonomi"),
        ("Magnus Lie", 23, "Informatikk")
    };
}

<div class="student-liste">
    <h3>Våre studenter</h3>
    
    @foreach (var student in studenter)
    {
        <div class="student-kort">
            <h4>@student.Navn</h4>
            <p><strong>Alder:</strong> @student.Alder år</p>
            <p><strong>Studieretning:</strong> @student.Studie</p>
            
            @if (student.Studie == "Informatikk")
            {
                <span class="badge bg-primary">IT-student</span>
            }
        </div>
    }
</div>

@* Med index *@
<ol class="nummerert-liste">
    @foreach (var (student, index) in studenter.Select((s, i) => (s, i)))
    {
        <li>
            <strong>Nr. @(index + 1):</strong> @student.Navn (@student.Alder år)
        </li>
    }
</ol>
```
<div style="page-break-after:always;"></div>

### For-løkker

```razor
@{
    string[] måneder = { "Januar", "Februar", "Mars", "April", "Mai", "Juni" };
}

<div class="måned-liste">
    @for (int i = 0; i < måneder.Length; i++)
    {
        <div class="måned-item @(i % 2 == 0 ? "partall" : "oddetall")">
            <span class="nummer">@(i + 1).</span>
            <span class="navn">@måneder[i]</span>
        </div>
    }
</div>

@* Nested loops *@
<table class="gangetabell">
    @for (int i = 1; i <= 5; i++)
    {
        <tr>
            @for (int j = 1; j <= 5; j++)
            {
                <td>@(i * j)</td>
            }
        </tr>
    }
</table>
```

<div style="page-break-after:always;"></div>

## 5. Komponenter og parametere

### Definere parametere

```razor
@* BrukerProfil.razor *@
<div class="bruker-profil">
    <img src="@ProfilBilde" alt="Profilbilde for @Navn" class="profil-avatar" />
    <div class="profil-info">
        <h3>@Navn</h3>
        <p class="tittel">@Tittel</p>
        
        @if (!string.IsNullOrEmpty(Beskrivelse))
        {
            <p class="beskrivelse">@Beskrivelse</p>
        }
        
        @if (ErAktiv)
        {
            <span class="status aktiv">🟢 Pålogget</span>
        }
        else
        {
            <span class="status inaktiv">🔴 Offline</span>
        }
        
        @if (OnProfilKlikk.HasDelegate)
        {
            <button class="btn btn-primary" @onclick="HandleProfilKlikk">
                Vis profil
            </button>
        }
    </div>
</div>

@code {
    [Parameter] public string Navn { get; set; } = "";
    [Parameter] public string Tittel { get; set; } = "";
    [Parameter] public string? Beskrivelse { get; set; }
    [Parameter] public string ProfilBilde { get; set; } = "/images/default-avatar.png";
    [Parameter] public bool ErAktiv { get; set; } = false;
    [Parameter] public EventCallback<string> OnProfilKlikk { get; set; }
    
    private async Task HandleProfilKlikk()
    {
        await OnProfilKlikk.InvokeAsync(Navn);
    }
}
```
<div style="page-break-after:always;"></div>

### Bruke komponenter

```razor
@* I en forelder-komponent *@
<div class="team-side">
    <h2>Vårt team</h2>
    
    <BrukerProfil Navn="Emma Hansen"
                  Tittel="Senior Utvikler"
                  Beskrivelse="Spesialist på Blazor og .NET"
                  ProfilBilde="/images/emma.jpg"
                  ErAktiv="true"
                  OnProfilKlikk="HandleProfilKlikk" />
                  
    <BrukerProfil Navn="Lars Olsen"
                  Tittel="Designer"
                  ErAktiv="false"
                  OnProfilKlikk="HandleProfilKlikk" />
</div>

@code {
    private void HandleProfilKlikk(string navn)
    {
        // Navigate til profil eller vis modal
        Console.WriteLine($"Profil klikket: {navn}");
    }
}
```

<div style="page-break-after:always;"></div>

## 6. Event-håndtering

### Forskjellige event-typer

```razor
<div class="event-demo">
    <h3>Event-håndtering eksempler</h3>
    
    @* Klikk-events *@
    <button class="btn btn-primary" @onclick="HandleSimpleClick">
        Enkel klikk
    </button>
    
    <button class="btn btn-secondary" @onclick="@(() => teller++)" >
        Inline klikk (Teller: @teller)
    </button>
    
    <button class="btn btn-info" @onclick="@((e) => HandleClickWithArgs(e))">
        Klikk med argumenter
    </button>
    
    @* Input events *@
    <div class="input-gruppe">
        <input type="text" 
               @oninput="HandleInput" 
               @onchange="HandleChange"
               @onfocus="HandleFocus"
               @onblur="HandleBlur"
               placeholder="Skriv noe..." 
               class="form-control" />
        
        <p>Input verdi: @inputVerdi</p>
        <p>Change verdi: @changeVerdi</p>
        <p>Har fokus: @harFokus</p>
    </div>
    
    @* Tastatur events *@
    <input type="text" 
           @onkeypress="HandleKeyPress"
           @onkeydown="HandleKeyDown"
           @onkeyup="HandleKeyUp"
           placeholder="Prøv forskjellige taster..." 
           class="form-control" />
    
    @* Mus events *@
    <div class="mus-area" 
         @onmouseover="HandleMouseOver"
         @onmouseleave="HandleMouseLeave"
         @onmousemove="HandleMouseMove">
        <p>Hover over denne boksen!</p>
        <p>Mus posisjon: X=@musX, Y=@musY</p>
    </div>
    
    @* Skjema events *@
    <form @onsubmit="HandleSubmit" @onsubmit:preventDefault="true">
        <input @bind="skjemaData" placeholder="Skriv noe..." required />
        <button type="submit" class="btn btn-success">Send</button>
    </form>
</div>

@code {
    private int teller = 0;
    private string inputVerdi = "";
    private string changeVerdi = "";
    private bool harFokus = false;
    private int musX = 0, musY = 0;
    private string skjemaData = "";
    
    private void HandleSimpleClick()
    {
        Console.WriteLine("Knapp klikket!");
    }
    
    private void HandleClickWithArgs(MouseEventArgs e)
    {
        Console.WriteLine($"Klikket på posisjon: {e.ClientX}, {e.ClientY}");
    }
    
    private void HandleInput(ChangeEventArgs e)
    {
        inputVerdi = e.Value?.ToString() ?? "";
    }
    
    private void HandleChange(ChangeEventArgs e)
    {
        changeVerdi = e.Value?.ToString() ?? "";
    }
    
    private void HandleFocus() => harFokus = true;
    private void HandleBlur() => harFokus = false;
    
    private void HandleKeyPress(KeyboardEventArgs e)
    {
        if (e.Key == "Enter")
        {
            Console.WriteLine("Enter trykket!");
        }
    }
    
    private void HandleKeyDown(KeyboardEventArgs e) => Console.WriteLine($"Key down: {e.Key}");
    private void HandleKeyUp(KeyboardEventArgs e) => Console.WriteLine($"Key up: {e.Key}");
    
    private void HandleMouseOver() => Console.WriteLine("Mus over!");
    private void HandleMouseLeave() => Console.WriteLine("Mus forlot!");
    
    private void HandleMouseMove(MouseEventArgs e)
    {
        musX = (int)e.ClientX;
        musY = (int)e.ClientY;
    }
    
    private void HandleSubmit()
    {
        Console.WriteLine($"Skjema sendt med data: {skjemaData}");
        skjemaData = ""; // Tøm skjema
    }
}
```

<div style="page-break-after:always;"></div>

## 7. Data Binding

### Two-way binding med @bind

```razor
<div class="databinding-demo">
    <h3>Data Binding Eksempler</h3>
    
    @* Tekstinput *@
    <div class="input-gruppe">
        <label>Navn:</label>
        <input @bind="bruker.Navn" @bind:event="oninput" class="form-control" />
        <p>Hilsen: Hei, @bruker.Navn!</p>
    </div>
    
    @* Number input *@
    <div class="input-gruppe">
        <label>Alder:</label>
        <input type="number" @bind="bruker.Alder" class="form-control" />
        <p>Du er @bruker.Alder år gammel</p>
        <p>Neste år: @(bruker.Alder + 1) år</p>
    </div>
    
    @* Checkbox *@
    <div class="checkbox-gruppe">
        <label>
            <input type="checkbox" @bind="bruker.ErStudent" />
            Er du student?
        </label>
        
        @if (bruker.ErStudent)
        {
            <div class="student-info">
                <p>🎓 Studentrabatt tilgjengelig!</p>
                
                <label>Studieretning:</label>
                <select @bind="bruker.Studieretning" class="form-select">
                    <option value="">Velg studieretning</option>
                    <option value="Informatikk">Informatikk</option>
                    <option value="Design">Design</option>
                    <option value="Økonomi">Økonomi</option>
                    <option value="Markedsføring">Markedsføring</option>
                </select>
            </div>
        }
    </div>
    
    @* Radio buttons *@
    <div class="radio-gruppe">
        <label>Hvilket operativsystem foretrekker du?</label>
        
        <label>
            <input type="radio" name="os" value="Windows" @bind="bruker.OperativSystem" />
            Windows
        </label>
        <label>
            <input type="radio" name="os" value="Mac" @bind="bruker.OperativSystem" />
            macOS
        </label>
        <label>
            <input type="radio" name="os" value="Linux" @bind="bruker.OperativSystem" />
            Linux
        </label>
        
        <p>Du foretrekker: @bruker.OperativSystem</p>
    </div>
    
    @* Dato og tid *@
    <div class="dato-gruppe">
        <label>Fødselsdato:</label>
        <input type="date" @bind="bruker.Fødselsdato" @bind:format="yyyy-MM-dd" class="form-control" />
        
        <label>Møtetidspunkt:</label>
        <input type="datetime-local" @bind="bruker.Møtetid" class="form-control" />
        
        <p>Alder basert på fødselsdato: @BeregnAlder() år</p>
    </div>
    
    @* Slider / Range *@
    <div class="slider-gruppe">
        <label>Erfaring (0-10 år):</label>
        <input type="range" min="0" max="10" @bind="bruker.Erfaring" class="form-range" />
        <span class="erfaring-verdi">@bruker.Erfaring år</span>
        
        <div class="erfaring-level">
            @if (bruker.Erfaring <= 2)
            {
                <span class="badge bg-success">Nybegynner 🌱</span>
            }
            else if (bruker.Erfaring <= 5)
            {
                <span class="badge bg-warning">Mellom 📈</span>
            }
            else
            {
                <span class="badge bg-danger">Ekspert 🚀</span>
            }
        </div>
    </div>
    
    @* Textarea *@
    <div class="textarea-gruppe">
        <label>Om deg selv:</label>
        <textarea @bind="bruker.Beskrivelse" @bind:event="oninput" 
                  rows="4" placeholder="Fortell litt om deg selv..." 
                  class="form-control"></textarea>
        <small>Antall tegn: @(bruker.Beskrivelse?.Length ?? 0)</small>
    </div>
    
    @* Sammendrag *@
    <div class="sammendrag">
        <h4>Ditt sammendrag:</h4>
        <ul>
            <li><strong>Navn:</strong> @bruker.Navn</li>
            <li><strong>Alder:</strong> @bruker.Alder år</li>
            <li><strong>Student:</strong> @(bruker.ErStudent ? "Ja" : "Nei")</li>
            @if (bruker.ErStudent && !string.IsNullOrEmpty(bruker.Studieretning))
            {
                <li><strong>Studieretning:</strong> @bruker.Studieretning</li>
            }
            <li><strong>OS:</strong> @bruker.OperativSystem</li>
            <li><strong>Erfaring:</strong> @bruker.Erfaring år</li>
        </ul>
    </div>
</div>

@code {
    private Bruker bruker = new();
    
    private int BeregnAlder()
    {
        if (bruker.Fødselsdato == default)
            return 0;
            
        var iDag = DateTime.Today;
        var alder = iDag.Year - bruker.Fødselsdato.Year;
        
        if (bruker.Fødselsdato.Date > iDag.AddYears(-alder))
            alder--;
            
        return alder;
    }
    
    public class Bruker
    {
        public string Navn { get; set; } = "";
        public int Alder { get; set; } = 20;
        public bool ErStudent { get; set; } = false;
        public string Studieretning { get; set; } = "";
        public string OperativSystem { get; set; } = "";
        public DateTime Fødselsdato { get; set; } = DateTime.Today.AddYears(-20);
        public DateTime Møtetid { get; set; } = DateTime.Now;
        public int Erfaring { get; set; } = 0;
        public string? Beskrivelse { get; set; }
    }
}
```

<div style="page-break-after:always;"></div>

## 8. Direktiver og attributter

### @page - Routing

```razor
@* Enkel rute *@
@page "/produkter"

@* Rute med parametere *@
@page "/produkt/{id:int}"
@page "/bruker/{brukernavn}"

@* Flere ruter for samme komponent *@
@page "/"
@page "/hjem"
@page "/home"

<h1>Velkommen til hjemmesiden!</h1>

@code {
    [Parameter] public int Id { get; set; }
    [Parameter] public string? Brukernavn { get; set; }
}
```

### @layout - Layout

```razor
@page "/admin"
@layout AdminLayout

<h1>Admin-panel</h1>
```

### @rendermode - Rendering

```razor
@* Interaktiv server-side rendering *@
@page "/counter"
@rendermode InteractiveServer

@* Statisk server-side rendering (standard) *@
@page "/info"
@rendermode @(new Microsoft.AspNetCore.Components.Web.StaticServerRenderMode())

@* Prerendering med interaktivitet *@
@rendermode @(new Microsoft.AspNetCore.Components.Web.InteractiveServerRenderMode(prerender: true))
```

<div style="page-break-after:always;"></div>

### @inject - Dependency Injection

```razor
@page "/weather"
@inject HttpClient Http
@inject IJSRuntime JSRuntime
@inject NavigationManager Navigation
@inject ILogger<Weather> Logger

<h3>Værdata</h3>

@if (værdata == null)
{
    <p><em>Laster værdata...</em></p>
}
else
{
    <table class="table">
        @foreach (var værdato in værdata)
        {
            <tr>
                <td>@værdato.Date.ToShortDateString()</td>
                <td>@værdato.TemperatureC°C</td>
                <td>@værdato.Summary</td>
            </tr>
        }
    </table>
}

@code {
    private WeatherForecast[]? værdata;
    
    protected override async Task OnInitializedAsync()
    {
        try
        {
            værdata = await Http.GetFromJsonAsync<WeatherForecast[]>("sample-data/weather.json");
            Logger.LogInformation("Værdata lastet inn");
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Feil ved lasting av værdata");
        }
    }
    
    private async Task NavigerTilHjem()
    {
        await JSRuntime.InvokeVoidAsync("alert", "Navigerer til hjem!");
        Navigation.NavigateTo("/");
    }
    
    public class WeatherForecast
    {
        public DateOnly Date { get; set; }
        public int TemperatureC { get; set; }
        public string? Summary { get; set; }
    }
}
```

### @using og @namespace

```razor
@* Importere namespaces *@
@using System.Text.Json
@using Microsoft.AspNetCore.Components.Forms

@* Sette namespace for komponenten *@
@namespace MinApp.Components.Shared
```

<div style="page-break-after:always;"></div>

## 9. Komponent livssyklus

```razor
@page "/livssyklus"
@implements IDisposable

<h3>Komponent Livssyklus Demo</h3>

<div class="livssyklus-info">
    <h4>Livssyklus hendelser:</h4>
    <ul>
        @foreach (var hendelse in livssyklusHendelser)
        {
            <li>@hendelse</li>
        }
    </ul>
    
    <button @onclick="TvingOppdatering" class="btn btn-primary">
        Tving re-render
    </button>
</div>

@code {
    private List<string> livssyklusHendelser = new();
    private Timer? timer;
    
    // 1. Konstruktør kjøres først (ikke vist her)
    
    // 2. SetParametersAsync - når parametere settes
    public override Task SetParametersAsync(ParameterView parameters)
    {
        livssyklusHendelser.Add($"SetParametersAsync - {DateTime.Now:HH:mm:ss}");
        return base.SetParametersAsync(parameters);
    }
    
    // 3. OnInitialized - kjøres én gang når komponenten initialiseres
    protected override void OnInitialized()
    {
        livssyklusHendelser.Add($"OnInitialized - {DateTime.Now:HH:mm:ss}");
        
        // Start en timer som oppdaterer komponenten
        timer = new Timer(async _ =>
        {
            livssyklusHendelser.Add($"Timer tick - {DateTime.Now:HH:mm:ss}");
            await InvokeAsync(StateHasChanged);
        }, null, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(5));
    }
    
    // 4. OnInitializedAsync - asynkron versjon
    protected override async Task OnInitializedAsync()
    {
        livssyklusHendelser.Add($"OnInitializedAsync start - {DateTime.Now:HH:mm:ss}");
        
        // Simuler API-kall
        await Task.Delay(1000);
        
        livssyklusHendelser.Add($"OnInitializedAsync slutt - {DateTime.Now:HH:mm:ss}");
    }
    
    // 5. OnParametersSet - kjøres hver gang parametere endres
    protected override void OnParametersSet()
    {
        livssyklusHendelser.Add($"OnParametersSet - {DateTime.Now:HH:mm:ss}");
    }
    
    // 6. OnAfterRender - kjøres etter hver rendering
    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender)
        {
            livssyklusHendelser.Add($"OnAfterRender (første gang) - {DateTime.Now:HH:mm:ss}");
            StateHasChanged(); // Dette vil trigge en ny render
        }
        else
        {
            livssyklusHendelser.Add($"OnAfterRender - {DateTime.Now:HH:mm:ss}");
        }
    }
    
    // 7. OnAfterRenderAsync - asynkron versjon
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            livssyklusHendelser.Add($"OnAfterRenderAsync (første gang) - {DateTime.Now:HH:mm:ss}");
            
            // Her kan du gjøre JavaScript-kall som trenger at DOM er ferdig rendret
            // await JSRuntime.InvokeVoidAsync("initializeComponent");
        }
    }
    
    // ShouldRender - bestemmer om komponenten skal re-rendres
    protected override bool ShouldRender()
    {
        // Returner false for å stoppe rendering
        // Nyttig for performance-optimalisering
        return true;
    }
    
    private void TvingOppdatering()
    {
        livssyklusHendelser.Add($"StateHasChanged kalt manuelt - {DateTime.Now:HH:mm:ss}");
        StateHasChanged();
    }
    
    // Dispose - cleanup når komponenten fjernes
    public void Dispose()
    {
        timer?.Dispose();
        Console.WriteLine("Komponent disposed");
    }
}
```

<div style="page-break-after:always;"></div>

## 10. Avanserte Razor-funksjoner

### @ref - Komponent referanser

```razor
<div class="ref-demo">
    <h3>Komponent Referanser</h3>
    
    @* Referanse til HTML-element *@
    <input @ref="navnInput" type="text" placeholder="Skriv navn..." class="form-control" />
    <button @onclick="SettFokusNavnInput" class="btn btn-primary">Sett fokus</button>
    
    @* Referanse til Blazor-komponent *@
    <BrukerProfil @ref="profilKomponent" 
                  Navn="@valgtBruker" 
                  Tittel="Utvikler" />
    
    <button @onclick="OppdaterProfil" class="btn btn-secondary">Oppdater profil</button>
</div>

@code {
    private ElementReference navnInput;
    private BrukerProfil profilKomponent = default!;
    private string valgtBruker = "Test Bruker";
    
    private async Task SettFokusNavnInput()
    {
        await navnInput.FocusAsync();
    }
    
    private void OppdaterProfil()
    {
        // Du kan kalle public metoder på komponenten
        // profilKomponent.OppdaterData();
    }
}
```

### @key - Ytelse optimalisering

```razor
<div class="key-demo">
    <h3>@key Direktiv for Lister</h3>
    
    <button @onclick="BlandListe" class="btn btn-primary">Bland liste</button>
    
    <div class="liste-container">
        @foreach (var person in personer)
        {
            @* Uten @key vil Blazor ha problemer med å spore endringer *@
            <div @key="person.Id" class="person-kort">
                <h4>@person.Navn</h4>
                <p>ID: @person.Id</p>
                <input type="text" @bind="person.Kommentar" placeholder="Kommentar..." />
            </div>
        }
    </div>
</div>

@code {
    private List<Person> personer = new()
    {
        new Person { Id = 1, Navn = "Emma", Kommentar = "" },
        new Person { Id = 2, Navn = "Lars", Kommentar = "" },
        new Person { Id = 3, Navn = "Sofia", Kommentar = "" }
    };
    
    private void BlandListe()
    {
        var random = new Random();
        personer = personer.OrderBy(x => random.Next()).ToList();
    }
    
    public class Person
    {
        public int Id { get; set; }
        public string Navn { get; set; } = "";
        public string Kommentar { get; set; } = "";
    }
}
```

### Betinget rendering og null-håndtering

```razor
<div class="betinget-rendering">
    <h3>Betinget Rendering</h3>
    
    @* Null-conditional operator *@
    <p>Antall karakterer i navn: @(bruker?.Navn?.Length ?? 0)</p>
    
    @* Null-coalescing *@
    <p>Visningsnavn: @(bruker?.Navn ?? "Ukjent bruker")</p>
    
    @* Betinget attributter *@
    <button class="btn @(erAktiv ? "btn-success" : "btn-secondary")" 
            disabled="@(!erAktiv)">
        @(erAktiv ? "Aktiv" : "Inaktiv")
    </button>
    
    @* Betinget innhold *@
    @if (laster)
    {
        <div class="spinner">Laster...</div>
    }
    else if (feil != null)
    {
        <div class="alert alert-danger">@feil</div>
    }
    else if (data?.Any() == true)
    {
        <ul>
            @foreach (var item in data)
            {
                <li>@item</li>
            }
        </ul>
    }
    else
    {
        <p>Ingen data å vise.</p>
    }
</div>

@code {
    private Bruker? bruker = new() { Navn = "Test Bruker" };
    private bool erAktiv = true;
    private bool laster = false;
    private string? feil = null;
    private List<string>? data = new() { "Item 1", "Item 2", "Item 3" };
    
    public class Bruker
    {
        public string? Navn { get; set; }
    }
}
```

<div style="page-break-after:always;"></div>

## 11. Best Practices og vanlige feil

### ✅ Best Practices

```razor
@* RIKTIG: Bruk beskrivende navn *@
@{
    var brukerListe = await HentAktiveBrukere();
}

@* RIKTIG: Håndter null-verdier *@
@if (brukerListe?.Any() == true)
{
    @foreach (var bruker in brukerListe)
    {
        <div>@bruker.Navn</div>
    }
}

@* RIKTIG: Bruk @key for lister *@
@foreach (var item in items)
{
    <div @key="item.Id">@item.Navn</div>
}

@* RIKTIG: Separated event handlers *@
<button @onclick="HandleClick">Klikk</button>

@code {
    private async Task HandleClick()
    {
        // Async operasjoner her
        await SomeAsyncOperation();
    }
}
```
<div style="page-break-after:always;"></div>

### ❌ Vanlige feil

```razor
@* FEIL: Inline kompleks logikk *@
<div>@(users.Where(u => u.IsActive && u.Role == "Admin").FirstOrDefault()?.Name ?? "No admin")</div>

@* BEDRE: Bruk metoder eller properties *@
<div>@AdminUserName</div>

@code {
    private string AdminUserName => 
        users.FirstOrDefault(u => u.IsActive && u.Role == "Admin")?.Name ?? "No admin";
}

@* FEIL: Glemme @key i lister *@
@foreach (var user in users)
{
    <UserCard User="user" /> @* Kan føre til feil state *@
}

@* RIKTIG: *@
@foreach (var user in users)
{
    <UserCard @key="user.Id" User="user" />
}

@* FEIL: Direkte DOM-manipulasjon *@
@code {
    private async Task BadExample()
    {
        await JSRuntime.InvokeVoidAsync("document.getElementById('myDiv').innerHTML = 'Hello'");
    }
}

@* BEDRE: La Blazor håndtere DOM *@
<div id="myDiv">@message</div>

@code {
    private string message = "Hello";
}
```

<div style="page-break-after:always;"></div>

## 12. Praktisk øvelse: Komplett kontaktskjema

```razor
@page "/kontakt"
@rendermode InteractiveServer

<PageTitle>Kontakt oss</PageTitle>

<div class="kontakt-side">
    <h2>Kontakt oss</h2>
    
    <EditForm Model="kontaktSkjema" OnValidSubmit="SendSkjema" OnInvalidSubmit="HandleInvalidSubmit">
        <DataAnnotationsValidator />
        <ValidationSummary class="validation-summary" />
        
        <div class="skjema-gruppe">
            <label for="navn">Fullt navn *</label>
            <InputText id="navn" @bind-Value="kontaktSkjema.Navn" class="form-control" placeholder="Ola Nordmann" />
            <ValidationMessage For="@(() => kontaktSkjema.Navn)" />
        </div>
        
        <div class="skjema-gruppe">
            <label for="epost">E-post *</label>
            <InputText id="epost" type="email" @bind-Value="kontaktSkjema.Epost" class="form-control" placeholder="ola@eksempel.no" />
            <ValidationMessage For="@(() => kontaktSkjema.Epost)" />
        </div>
        
        <div class="skjema-gruppe">
            <label for="telefon">Telefonnummer</label>
            <InputText id="telefon" @bind-Value="kontaktSkjema.Telefon" class="form-control" placeholder="+47 123 45 678" />
            <ValidationMessage For="@(() => kontaktSkjema.Telefon)" />
        </div>
        
        <div class="skjema-gruppe">
            <label for="kategori">Kategori *</label>
            <InputSelect id="kategori" @bind-Value="kontaktSkjema.Kategori" class="form-select">
                <option value="">Velg kategori</option>
                <option value="Support">Teknisk support</option>
                <option value="Sales">Salg</option>
                <option value="Billing">Fakturering</option>
                <option value="Other">Annet</option>
            </InputSelect>
            <ValidationMessage For="@(() => kontaktSkjema.Kategori)" />
        </div>
        
        <div class="skjema-gruppe">
            <label for="prioritet">Prioritet</label>
            <div class="radio-gruppe">
                <label><InputRadio Name="prioritet" Value="Low" @bind-Value="kontaktSkjema.Prioritet" /> Lav</label>
                <label><InputRadio Name="prioritet" Value="Medium" @bind-Value="kontaktSkjema.Prioritet" /> Medium</label>
                <label><InputRadio Name="prioritet" Value="High" @bind-Value="kontaktSkjema.Prioritet" /> Høy</label>
            </div>
        </div>
        
        <div class="skjema-gruppe">
            <label for="melding">Melding *</label>
            <InputTextArea id="melding" @bind-Value="kontaktSkjema.Melding" rows="5" class="form-control" 
                           placeholder="Beskriv ditt spørsmål eller problem..." />
            <ValidationMessage For="@(() => kontaktSkjema.Melding)" />
            <small class="tegn-teller">@(kontaktSkjema.Melding?.Length ?? 0) / 1000 tegn</small>
        </div>
        
        <div class="skjema-gruppe">
            <label>
                <InputCheckbox @bind-Value="kontaktSkjema.AbonnerNyhetsbrev" />
                Jeg ønsker å motta nyhetsbrev
            </label>
        </div>
        
        <div class="skjema-gruppe">
            <label>
                <InputCheckbox @bind-Value="kontaktSkjema.AccepterVilkår" />
                Jeg aksepterer <a href="/vilkår" target="_blank">vilkårene</a> *
            </label>
            <ValidationMessage For="@(() => kontaktSkjema.AccepterVilkår)" />
        </div>
        
        <div class="knapp-gruppe">
            <button type="submit" class="btn btn-primary" disabled="@sender">
                @if (sender)
                {
                    <span class="spinner"></span>
                    <text>Sender...</text>
                }
                else
                {
                    <text>Send melding</text>
                }
            </button>
            
            <button type="button" @onclick="TømSkjema" class="btn btn-secondary">Tøm skjema</button>
        </div>
    </EditForm>
    
    @if (!string.IsNullOrEmpty(resultatMelding))
    {
        <div class="alert @(erSuksess ? "alert-success" : "alert-danger")">
            @resultatMelding
        </div>
    }
    
    @if (visForhåndsvisning)
    {
        <div class="forhåndsvisning">
            <h4>Forhåndsvisning av din melding:</h4>
            <div class="melding-kort">
                <p><strong>Fra:</strong> @kontaktSkjema.Navn (@kontaktSkjema.Epost)</p>
                <p><strong>Kategori:</strong> @kontaktSkjema.Kategori</p>
                <p><strong>Prioritet:</strong> @kontaktSkjema.Prioritet</p>
                <p><strong>Melding:</strong></p>
                <div class="melding-innhold">@kontaktSkjema.Melding</div>
            </div>
        </div>
    }
</div>

@code {
    private KontaktSkjema kontaktSkjema = new();
    private bool sender = false;
    private string resultatMelding = "";
    private bool erSuksess = false;
    private bool visForhåndsvisning = false;
    
    protected override void OnInitialized()
    {
        // Sett standard verdier
        kontaktSkjema.Prioritet = "Medium";
    }
    
    private async Task SendSkjema()
    {
        sender = true;
        resultatMelding = "";
        
        try
        {
            // Simuler API-kall
            await Task.Delay(2000);
            
            // Her ville du sendt til en service
            // await KontaktService.SendMelding(kontaktSkjema);
            
            resultatMelding = "Takk for din melding! Vi tar kontakt så snart som mulig.";
            erSuksess = true;
            
            // Tøm skjema etter vellykket sending
            kontaktSkjema = new() { Prioritet = "Medium" };
        }
        catch (Exception ex)
        {
            resultatMelding = $"Det oppstod en feil: {ex.Message}";
            erSuksess = false;
        }
        finally
        {
            sender = false;
        }
    }
    
    private void HandleInvalidSubmit()
    {
        resultatMelding = "Vennligst rett opp feilene i skjemaet før du sender.";
        erSuksess = false;
    }
    
    private void TømSkjema()
    {
        kontaktSkjema = new() { Prioritet = "Medium" };
        resultatMelding = "";
        visForhåndsvisning = false;
    }
    
    private void VisForhåndsvisning()
    {
        visForhåndsvisning = !visForhåndsvisning;
    }
}

@* Komponent-spesifikk CSS *@
<style>
    .kontakt-side {
        max-width: 600px;
        margin: 0 auto;
        padding: 20px;
    }
    
    .skjema-gruppe {
        margin-bottom: 1rem;
    }
    
    .radio-gruppe {
        display: flex;
        gap: 1rem;
    }
    
    .radio-gruppe label {
        display: flex;
        align-items: center;
        gap: 0.5rem;
    }
    
    .knapp-gruppe {
        display: flex;
        gap: 1rem;
        margin-top: 2rem;
    }
    
    .tegn-teller {
        color: #666;
        font-size: 0.875rem;
    }
    
    .spinner {
        display: inline-block;
        width: 1rem;
        height: 1rem;
        border: 2px solid currentColor;
        border-radius: 50%;
        border-right-color: transparent;
        animation: spin 1s linear infinite;
    }
    
    @keyframes spin {
        to { transform: rotate(360deg); }
    }
    
    .forhåndsvisning {
        margin-top: 2rem;
        padding: 1rem;
        background: #f8f9fa;
        border-radius: 0.375rem;
    }
    
    .melding-kort {
        background: white;
        padding: 1rem;
        border-radius: 0.375rem;
        border: 1px solid #dee2e6;
    }
    
    .melding-innhold {
        background: #f8f9fa;
        padding: 0.5rem;
        border-radius: 0.25rem;
        white-space: pre-wrap;
    }
</style>

@* Data modell med validering *@
@code {
    using System.ComponentModel.DataAnnotations;
    
    public class KontaktSkjema
    {
        [Required(ErrorMessage = "Navn er påkrevd")]
        [StringLength(100, ErrorMessage = "Navnet kan ikke være lengre enn 100 tegn")]
        public string Navn { get; set; } = "";
        
        [Required(ErrorMessage = "E-post er påkrevd")]
        [EmailAddress(ErrorMessage = "Ugyldig e-postadresse")]
        public string Epost { get; set; } = "";
        
        [Phone(ErrorMessage = "Ugyldig telefonnummer")]
        public string? Telefon { get; set; }
        
        [Required(ErrorMessage = "Kategori må velges")]
        public string Kategori { get; set; } = "";
        
        public string Prioritet { get; set; } = "Medium";
        
        [Required(ErrorMessage = "Melding er påkrevd")]
        [StringLength(1000, MinimumLength = 10, 
                     ErrorMessage = "Meldingen må være mellom 10 og 1000 tegn")]
        public string Melding { get; set; } = "";
        
        public bool AbonnerNyhetsbrev { get; set; } = false;
        
        [Range(typeof(bool), "true", "true", 
               ErrorMessage = "Du må akseptere vilkårene")]
        public bool AccepterVilkår { get; set; } = false;
    }
}
```

<div style="page-break-after:always;"></div>

## Sammendrag

Blazor Razor-syntaks gir deg mulighet til å:

1. **Blande HTML og C#** med `@`-tegnet
2. **Lage dynamisk innhold** med variabler og uttrykk  
3. **Kontrollere flow** med if/else, loops og switch
4. **Håndtere events** med `@onclick`, `@oninput`, etc.
5. **Binde data** med `@bind` og `@bind-Value`
6. **Lage komponenter** med parametere og callbacks
7. **Optimalisere ytelse** med `@key` og livssyklus-metoder

**Neste steg:** Øv deg på eksemplene i dette dokumentet og eksperimenter med egne komponenter!

---

## Nyttige ressurser

- [Microsoft Blazor Documentation](https://docs.microsoft.com/aspnet/core/blazor/)
- [Razor Syntax Reference](https://docs.microsoft.com/aspnet/core/mvc/views/razor)
- [Blazor University](https://blazor-university.com/)
- [Awesome Blazor](https://github.com/AdrienTorris/awesome-blazor)