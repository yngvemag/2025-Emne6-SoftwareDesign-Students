# CSS-klasser i Blazor - Bootstrap og Custom Styling

## Innledning

Når du ser `class="form-control"`, `class="btn btn-primary"` eller lignende i Blazor-kode, refererer dette til **CSS-klasser** som styrer utseendet og oppførselen til HTML-elementer. Disse klassene er ikke magiske - de er definert i CSS-filer og gir elementer spesifikke stiler.

---

## 1. Hva er CSS-klasser?

### Grunnleggende konsept

CSS-klasser er **gjenbrukbare stildefinisjoner** som kan anvendes på HTML-elementer for å gi dem spesifikk styling.

```html
<!-- HTML med klasse -->
<button class="min-knapp">Klikk meg</button>
```

```css
/* CSS-definisjon av klassen */
.min-knapp {
    background-color: blue;
    color: white;
    padding: 10px 20px;
    border: none;
    border-radius: 5px;
}
```

**Resultat:** En blå knapp med hvit tekst, avrundede hjørner og padding.

<div style="page-break-after:always;"></div>

## 2. Bootstrap - Hovedkilden til klassene du ser

### Hva er Bootstrap?

**Bootstrap** er et populært CSS-framework som kommer ferdig inkludert i Blazor-prosjekter. Det gir deg hundrevis av forhåndsdefinerte klasser for vanlige UI-komponenter.

```html
<!-- Bootstrap er inkludert i App.razor -->
<link rel="stylesheet" href="lib/bootstrap/dist/css/bootstrap.min.css" />
```

### Hvorfor Bootstrap?

1. **Konsistent design** - Alt ser profesjonelt ut
2. **Responsive** - Fungerer på mobil, tablet og desktop
3. **Tidsbesparende** - Ingen behov for å skrive CSS fra bunnen
4. **Veldokumentert** - Masse eksempler og dokumentasjon

---

## 3. Vanlige Bootstrap-kategorier

### 3.1 Skjema-elementer (Forms)

```razor
<!-- Tekstinput med Bootstrap-styling -->
<input type="text" class="form-control" placeholder="Skriv ditt navn" />

<!-- Select dropdown -->
<select class="form-select">
    <option>Velg en optionl</option>
    <option value="1">Option 1</option>
</select>

<!-- Checkbox -->
<input type="checkbox" class="form-check-input" />
<label class="form-check-label">Jeg aksepterer vilkårene</label>

<!-- Radio buttons -->
<input type="radio" class="form-check-input" name="valg" />
<label class="form-check-label">Valg 1</label>
```

**Hva gjør disse klassene?**

- `form-control`: Gir input-felt standard Bootstrap-styling (border, padding, fokus-effekter)
- `form-select`: Spesiell styling for dropdown-menyer
- `form-check-input`: Styling for checkboxes og radio buttons
- `form-check-label`: Tilhørende labels med riktig spacing

<div style="page-break-after:always;"></div>

### 3.2 Knapper (Buttons)

```razor
<!-- Forskjellige knapp-stiler -->
<button class="btn btn-primary">Hovedknapp (blå)</button>
<button class="btn btn-secondary">Sekundær (grå)</button>
<button class="btn btn-success">Suksess (grønn)</button>
<button class="btn btn-danger">Fare (rød)</button>
<button class="btn btn-warning">Advarsel (gul)</button>
<button class="btn btn-info">Info (turkis)</button>
<button class="btn btn-light">Lys</button>
<button class="btn btn-dark">Mørk</button>

<!-- Knapp-størrelser -->
<button class="btn btn-primary btn-lg">Stor knapp</button>
<button class="btn btn-primary">Normal knapp</button>
<button class="btn btn-primary btn-sm">Liten knapp</button>

<!-- Outline-knapper (kun border) -->
<button class="btn btn-outline-primary">Outline Primary</button>
```

**Forklaring:**

- `btn`: Grunnleggende knapp-styling (alle knapper trenger denne)
- `btn-primary`, `btn-danger`, etc.: Fargetemaer
- `btn-lg`, `btn-sm`: Størrelse-modifikatorer
- `btn-outline-*`: Hollow-stil med kun border

### 3.3 Layout og Grid

```razor
<!-- Container for å sentrere innhold -->
<div class="container">
    <div class="row">
        <!-- 12-kolonne grid system -->
        <div class="col-md-6">Venstre halvdel</div>
        <div class="col-md-6">Høyre halvdel</div>
    </div>
    
    <div class="row">
        <div class="col-sm-12 col-md-8">Hovedinnhold</div>
        <div class="col-sm-12 col-md-4">Sidebar</div>
    </div>
</div>

<!-- Flexbox utilities -->
<div class="d-flex justify-content-center align-items-center">
    <div>Sentrert innhold</div>
</div>
```

**Forklaring:**

- `container`: Sentrerer innhold med max-width
- `row`: Oppretter en rad i grid-systemet
- `col-*-*`: Kolonner som tilpasser seg skjermstørrelse
- `d-flex`: Gjør element til flexbox
- `justify-content-*`, `align-items-*`: Flexbox-justering

### 3.4 Spacing (Margin og Padding)

```razor
<!-- Margin klasser -->
<div class="m-3">Margin på alle sider (1rem)</div>
<div class="mt-2">Margin top (0.5rem)</div>
<div class="mb-4">Margin bottom (1.5rem)</div>
<div class="mx-auto">Automatisk margin left/right (sentrering)</div>

<!-- Padding klasser -->
<div class="p-2">Padding på alle sider</div>
<div class="px-3">Padding left/right</div>
<div class="py-4">Padding top/bottom</div>

<!-- Kombinasjoner -->
<div class="mt-3 mb-2 px-4">Multiple spacing klasser</div>
```

**Spacing-system:**

- `m` = margin, `p` = padding
- `t` = top, `b` = bottom, `l` = left, `r` = right, `x` = left/right, `y` = top/bottom
- Tall: 0-5 (0 = 0, 1 = 0.25rem, 2 = 0.5rem, 3 = 1rem, 4 = 1.5rem, 5 = 3rem)

### 3.5 Tekst og typografi

```razor
<!-- Tekststørrelser -->
<h1 class="display-1">Veldig stor overskrift</h1>
<p class="lead">Større avsnitt (lead paragraph)</p>
<small class="text-muted">Liten, dempet tekst</small>

<!-- Tekstjustering -->
<p class="text-start">Venstrejustert</p>
<p class="text-center">Sentrert</p>
<p class="text-end">Høyrejustert</p>

<!-- Tekstfarger -->
<p class="text-primary">Primærfarge</p>
<p class="text-success">Suksessfarge (grønn)</p>
<p class="text-danger">Farefarge (rød)</p>
<p class="text-muted">Dempet farge</p>

<!-- Tekststil -->
<p class="fw-bold">Fet tekst</p>
<p class="fst-italic">Kursiv tekst</p>
<p class="text-decoration-underline">Understreket</p>
```

### 3.6 Bakgrunner og borders

```razor
<!-- Bakgrunnsfarger -->
<div class="bg-primary text-white p-3">Primær bakgrunn</div>
<div class="bg-light p-3">Lys bakgrunn</div>
<div class="bg-danger text-white p-3">Fare-bakgrunn</div>

<!-- Borders -->
<div class="border p-3">Standard border</div>
<div class="border border-primary p-3">Blå border</div>
<div class="rounded p-3 bg-light">Avrundede hjørner</div>
<div class="border rounded-pill p-3">Pill-form</div>
```

<div style="page-break-after:always;"></div>

## 4. Komponenter og Utilities

### 4.1 Alerts (Meldingsbokser)

```razor
<div class="alert alert-success" role="alert">
    <strong>Suksess!</strong> Operasjonen ble gjennomført.
</div>

<div class="alert alert-danger" role="alert">
    <strong>Feil!</strong> Noe gikk galt.
</div>

<div class="alert alert-warning" role="alert">
    <strong>Advarsel!</strong> Vær forsiktig.
</div>
```

### 4.2 Cards (Kort)

```razor
<div class="card" style="width: 18rem;">
    <img src="..." class="card-img-top" alt="...">
    <div class="card-body">
        <h5 class="card-title">Korttittel</h5>
        <p class="card-text">Innhold i kortet.</p>
        <a href="#" class="btn btn-primary">Gå et sted</a>
    </div>
</div>
```

### 4.3 Navigation

```razor
<nav class="navbar navbar-expand-lg navbar-dark bg-dark">
    <div class="container-fluid">
        <a class="navbar-brand" href="#">Min App</a>
        <button class="navbar-toggler" type="button">
            <span class="navbar-toggler-icon"></span>
        </button>
        <div class="navbar-nav">
            <a class="nav-link active" href="#">Hjem</a>
            <a class="nav-link" href="#">Om oss</a>
        </div>
    </div>
</nav>
```

<div style="page-break-after:always;"></div>

## 5. Responsive Design - Skjermstørrelse-klasser

### Breakpoint-system

Bootstrap bruker forskjellige prefiks for å håndtere ulike skjermstørrelser:

```razor
<!-- Responsive kolonner -->
<div class="row">
    <!-- På mobil: full bredde, på tablet+: halvparten -->
    <div class="col-12 col-md-6">
        <div class="card p-3">Kort 1</div>
    </div>
    <div class="col-12 col-md-6">
        <div class="card p-3">Kort 2</div>
    </div>
</div>

<!-- Responsive visibility -->
<div class="d-none d-md-block">Vises kun på medium+ skjermer</div>
<div class="d-block d-md-none">Vises kun på små skjermer</div>

<!-- Responsive text alignment -->
<p class="text-center text-md-start">
    Sentrert på mobil, venstrejustert på større skjermer
</p>
```

**Breakpoints:**

- `xs` (extra small): < 576px (standard, ingen prefiks)
- `sm` (small): ≥ 576px
- `md` (medium): ≥ 768px  
- `lg` (large): ≥ 992px
- `xl` (extra large): ≥ 1200px
- `xxl` (extra extra large): ≥ 1400px

<div style="page-break-after:always;"></div>

## 6. Hvor finner du dokumentasjon?

### 6.1 Offisiell Bootstrap-dokumentasjon

**URL:** [https://getbootstrap.com/docs/](https://getbootstrap.com/docs/)

Denne siden inneholder:

- **Alle tilgjengelige klasser** med eksempler
- **Interaktive demoer** du kan teste
- **Kodeeksempler** du kan kopiere
- **Customization-guide** for å tilpasse Bootstrap

### 6.2 Viktige seksjoner i dokumentasjonen

1. **Layout** - Grid system, containers, breakpoints
2. **Content** - Typography, images, tables  
3. **Forms** - Input fields, validation, layouts
4. **Components** - Buttons, cards, navbars, modals
5. **Utilities** - Spacing, colors, positioning

### 6.3 Praktisk tips for å finne klasser

```razor
@* Når du trenger styling, søk etter: *@

@* Knapper -> Bootstrap docs: "Buttons" *@
<button class="btn btn-outline-success">Grønn outline</button>

@* Skjemaelementer -> Bootstrap docs: "Forms" *@
<input class="form-control form-control-lg" placeholder="Stor input" />

@* Spacing -> Bootstrap docs: "Utilities > Spacing" *@
<div class="mt-5 px-3">Margin top 5, padding x 3</div>

@* Farger -> Bootstrap docs: "Utilities > Colors" *@
<span class="text-info bg-warning">Info tekst på gul bakgrunn</span>
```

<div style="page-break-after:always;"></div>

## 7. Custom CSS vs Bootstrap-klasser

### 7.1 Når bruke Bootstrap

```razor
@* Bruk Bootstrap for standard komponenter *@
<div class="card">
    <div class="card-header">
        <h5 class="card-title mb-0">Produktinformasjon</h5>
    </div>
    <div class="card-body">
        <p class="card-text">Standard layout som ser bra ut.</p>
        <button class="btn btn-primary">Kjøp nå</button>
    </div>
</div>
```

### 7.2 Når lage custom CSS

```razor
@* Custom CSS for spesielle behov *@
<div class="produkt-showcase">
    <div class="produkt-bilde-container">
        <img src="produkt.jpg" class="produkt-hovedbilde" />
    </div>
</div>

<style>
    .produkt-showcase {
        /* Spesiell layout som Bootstrap ikke dekker */
        display: grid;
        grid-template-columns: 2fr 1fr;
        gap: 2rem;
    }
    
    .produkt-hovedbilde {
        /* Custom hover-effekt */
        transition: transform 0.3s ease;
    }
    
    .produkt-hovedbilde:hover {
        transform: scale(1.05);
    }
</style>
```

### 7.3 Kombinere Bootstrap og Custom CSS

```razor
@* Best practice: Bootstrap som base + custom for spesifikke behov *@
<div class="card custom-product-card">
    <img src="..." class="card-img-top product-image" />
    <div class="card-body">
        <h5 class="card-title">@produktNavn</h5>
        <p class="card-text text-muted">@beskrivelse</p>
        <div class="d-flex justify-content-between align-items-center">
            <span class="price-tag">@pris kr</span>
            <button class="btn btn-primary">Legg i kurv</button>
        </div>
    </div>
</div>

<style>
    .custom-product-card {
        /* Utvider Bootstrap card med custom styling */
        transition: box-shadow 0.3s ease;
        border: none;
    }
    
    .custom-product-card:hover {
        box-shadow: 0 4px 15px rgba(0,0,0,0.1);
    }
    
    .product-image {
        height: 200px;
        object-fit: cover;
    }
    
    .price-tag {
        font-size: 1.25rem;
        font-weight: bold;
        color: #28a745;
    }
</style>
```

<div style="page-break-after:always;"></div>

## 8. Praktisk eksempel: Fullstendig komponent

```razor
@page "/produktkatalog"

<div class="container-fluid">
    <!-- Header med Bootstrap navbar -->
    <nav class="navbar navbar-expand-lg navbar-dark bg-primary mb-4">
        <div class="container">
            <span class="navbar-brand mb-0 h1">🛍️ Produktkatalog</span>
            
            <div class="d-flex">
                <input type="search" 
                       class="form-control me-2" 
                       placeholder="Søk produkter..."
                       @bind="søkeord" @bind:event="oninput" />
                <button class="btn btn-outline-light" @onclick="Søk">
                    Søk
                </button>
            </div>
        </div>
    </nav>
    
    <!-- Filter og sortering -->
    <div class="container">
        <div class="row mb-4">
            <div class="col-md-8">
                <div class="btn-group" role="group">
                    <input type="radio" class="btn-check" name="kategori" 
                           id="alle" @bind="valgtKategori" value="" />
                    <label class="btn btn-outline-secondary" for="alle">Alle</label>
                    
                    <input type="radio" class="btn-check" name="kategori" 
                           id="elektronikk" @bind="valgtKategori" value="Elektronikk" />
                    <label class="btn btn-outline-secondary" for="elektronikk">Elektronikk</label>
                    
                    <input type="radio" class="btn-check" name="kategori" 
                           id="klær" @bind="valgtKategori" value="Klær" />
                    <label class="btn btn-outline-secondary" for="klær">Klær</label>
                </div>
            </div>
            
            <div class="col-md-4">
                <select class="form-select" @bind="sortering">
                    <option value="navn">Sorter etter navn</option>
                    <option value="pris-lav">Pris: Lav til høy</option>
                    <option value="pris-høy">Pris: Høy til lav</option>
                </select>
            </div>
        </div>
        
        <!-- Produktgrid -->
        <div class="row">
            @foreach (var produkt in FiltrerteProdukter)
            {
                <div class="col-sm-6 col-md-4 col-lg-3 mb-4">
                    <div class="card h-100 shadow-sm product-card">
                        @if (!string.IsNullOrEmpty(produkt.Bilde))
                        {
                            <img src="@produkt.Bilde" class="card-img-top product-image" alt="@produkt.Navn" />
                        }
                        else
                        {
                            <div class="card-img-top d-flex align-items-center justify-content-center bg-light product-placeholder">
                                <span class="text-muted">Ingen bilde</span>
                            </div>
                        }
                        
                        <div class="card-body d-flex flex-column">
                            <h6 class="card-title">@produkt.Navn</h6>
                            <p class="card-text text-muted small flex-grow-1">@produkt.Beskrivelse</p>
                            
                            <div class="mt-auto">
                                <div class="d-flex justify-content-between align-items-center mb-2">
                                    <span class="h5 mb-0 text-primary">@produkt.Pris.ToString("C", new CultureInfo("nb-NO"))</span>
                                    @if (produkt.ErTilbud)
                                    {
                                        <span class="badge bg-danger">Tilbud!</span>
                                    }
                                </div>
                                
                                <div class="btn-group w-100">
                                    <button class="btn btn-outline-primary btn-sm" 
                                            @onclick="() => VisDetaljer(produkt)">
                                        <i class="bi bi-eye"></i> Vis
                                    </button>
                                    <button class="btn btn-primary btn-sm" 
                                            @onclick="() => LeggIHandlevogn(produkt)">
                                        <i class="bi bi-cart-plus"></i> Kjøp
                                    </button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            }
        </div>
        
        @if (!FiltrerteProdukter.Any())
        {
            <div class="row">
                <div class="col-12">
                    <div class="alert alert-info text-center">
                        <h4>🔍 Ingen produkter funnet</h4>
                        <p class="mb-0">Prøv å endre søkekriteriene dine.</p>
                    </div>
                </div>
            </div>
        }
    </div>
</div>

@code {
    private string søkeord = "";
    private string valgtKategori = "";
    private string sortering = "navn";
    
    private List<Produkt> produkter = new()
    {
        new() { Id = 1, Navn = "iPhone 15", Beskrivelse = "Nyeste iPhone", Kategori = "Elektronikk", Pris = 12999, ErTilbud = true },
        new() { Id = 2, Navn = "T-skjorte", Beskrivelse = "Komfortabel bomull", Kategori = "Klær", Pris = 299, ErTilbud = false },
        new() { Id = 3, Navn = "MacBook Pro", Beskrivelse = "Kraftig laptop", Kategori = "Elektronikk", Pris = 25999, ErTilbud = false }
    };
    
    private IEnumerable<Produkt> FiltrerteProdukter
    {
        get
        {
            var filtrert = produkter.AsEnumerable();
            
            // Søkefilter
            if (!string.IsNullOrWhiteSpace(søkeord))
            {
                filtrert = filtrert.Where(p => 
                    p.Navn.Contains(søkeord, StringComparison.OrdinalIgnoreCase) ||
                    p.Beskrivelse.Contains(søkeord, StringComparison.OrdinalIgnoreCase));
            }
            
            // Kategorifilter
            if (!string.IsNullOrEmpty(valgtKategori))
            {
                filtrert = filtrert.Where(p => p.Kategori == valgtKategori);
            }
            
            // Sortering
            filtrert = sortering switch
            {
                "pris-lav" => filtrert.OrderBy(p => p.Pris),
                "pris-høy" => filtrert.OrderByDescending(p => p.Pris),
                _ => filtrert.OrderBy(p => p.Navn)
            };
            
            return filtrert;
        }
    }
    
    private void Søk()
    {
        // Søk utføres automatisk via FiltrerteProdukter
        StateHasChanged();
    }
    
    private void VisDetaljer(Produkt produkt)
    {
        Console.WriteLine($"Viser detaljer for: {produkt.Navn}");
    }
    
    private void LeggIHandlevogn(Produkt produkt)
    {
        Console.WriteLine($"Lagt til i handlevogn: {produkt.Navn}");
    }
    
    public class Produkt
    {
        public int Id { get; set; }
        public string Navn { get; set; } = "";
        public string Beskrivelse { get; set; } = "";
        public string Kategori { get; set; } = "";
        public decimal Pris { get; set; }
        public bool ErTilbud { get; set; }
        public string? Bilde { get; set; }
    }
}

<style>
    .product-card {
        transition: transform 0.2s ease, box-shadow 0.2s ease;
        cursor: pointer;
    }
    
    .product-card:hover {
        transform: translateY(-5px);
        box-shadow: 0 4px 15px rgba(0,0,0,0.15) !important;
    }
    
    .product-image {
        height: 200px;
        object-fit: cover;
    }
    
    .product-placeholder {
        height: 200px;
    }
    
    .btn-group .btn {
        flex: 1;
    }
</style>
```

**Klasser brukt i dette eksemplet:**

1. **Layout**: `container-fluid`, `container`, `row`, `col-*`
2. **Navigation**: `navbar`, `navbar-brand`, `navbar-dark`, `bg-primary`
3. **Skjema**: `form-control`, `form-select`, `btn-check`
4. **Knapper**: `btn`, `btn-primary`, `btn-outline-*`, `btn-group`
5. **Kort**: `card`, `card-img-top`, `card-body`, `card-title`, `card-text`
6. **Utilities**: `d-flex`, `justify-content-*`, `align-items-*`, `mb-*`, `text-*`
7. **Komponenter**: `badge`, `alert`

<div style="page-break-after:always;"></div>

## 9. Viktige takeaways 

### ✅ Husk dette

1. **CSS-klasser er ikke magiske** - de er definert i CSS-filer
2. **Bootstrap gir deg ferdig styling** - spar tid, få konsistent design
3. **Dokumentasjonen er din venn** - bruk [getbootstrap.com](https://getbootstrap.com) aktivt
4. **Kombiner Bootstrap + Custom CSS** når Bootstrap ikke dekker behovene dine
5. **Responsive design kommer gratis** med Bootstrap-klasser
6. **Konsistens er viktig** - bruk samme klassesett gjennom hele appen



---

## 10. Nyttige ressurser

### Offisielle ressurser

- [Bootstrap Documentation](https://getbootstrap.com/docs/) - Komplett referanse
- [Bootstrap Examples](https://getbootstrap.com/docs/examples/) - Ferdige templates
- [Bootstrap Cheat Sheet](https://bootstrap-cheatsheet.themeselection.com/) - Rask referanse

### Verktøy

- **Browser Developer Tools** - Inspiser og test klasser live
- **VS Code Extensions** - Bootstrap IntelliSense for autocomplete
- **Bootstrap Studio** - Visuell Bootstrap-editor

### Alternative CSS-frameworks

- **Tailwind CSS** - Utility-first approach
- **Bulma** - Moderne CSS framework
- **Foundation** - Enterprise-fokusert framework

