
# Blazor Placeholders og RenderFragments

Denne guiden forklarer **alle placeholder-typer som finnes i Blazor**, hva de brukes til, og hvordan de fungerer i praksis.  
Den dekker både **layouts** og **komponenter**, og viser hvordan du kan bygge fleksible strukturer med `@Body`, `@ChildContent` og `RenderFragment`.

---

## 🧩 1. `@Body` – Placeholder for sider i layouts

`@Body` brukes kun i **layout-komponenter** og representerer innholdet til den siden som bruker layouten.

```razor
<!-- MainLayout.razor -->
@inherits LayoutComponentBase

<div class="container">
    <header>Header content</header>
    <main>
        @Body
    </main>
    <footer>Footer content</footer>
</div>
```

🔹 **Forklaring:**

- `@Body` fylles automatisk med innholdet fra den aktive siden (`.razor`-filen som navigeres til).  
- Når du navigerer til en side som bruker denne layouten, blir sideinnholdet satt inn der `@Body` står.  
- Brukes kun i filer som **arver `LayoutComponentBase`**.

---

## 🧱 2. `@Layout` – Velger hvilken layout som brukes

`@Layout` er ikke en placeholder i seg selv, men definerer **hvilken layout** som brukes av en side eller komponent.

```razor
@page "/"
@layout MainLayout

<h1>Home</h1>
<p>Welcome to the homepage!</p>
```

🔹 **Forklaring:**  

- `@layout` må stå øverst i `.razor`-filen.  
- Forteller Blazor at denne siden skal settes inn i layouten `MainLayout.razor`.  
- Når siden rendres, vil innholdet vises der `@Body` står i layouten.

## 🧩 3. `@ChildContent` – Placeholder for innhold i komponenter

`@ChildContent` er **komponentenes egen placeholder**, og fungerer som en lokal variant av `@Body`.

```razor
<!-- Panel.razor -->
<div class="card">
    <div class="card-body">
        @ChildContent
    </div>
</div>

@code {
    [Parameter] public RenderFragment? ChildContent { get; set; }
}
```

Brukes slik:

```razor
<Panel>
    <p>This content is inserted into @ChildContent.</p>
</Panel>
```

🔹 **Forklaring:**

- `@ChildContent` viser alt som står mellom `<Panel>` og `</Panel>`.  
- Typen `RenderFragment` er en “mal” for et stykke UI som Blazor kan rendere.  
- Veldig nyttig for **layout-komponenter**, **kort**, **modalvinduer** osv.  
- Kun én `ChildContent` per komponent – men du kan lage flere ved å bruke **navngitte RenderFragments**.

<div style="page-break-after:always;"></div>

## 🧩 4. Navngitte `RenderFragment`s – Flere placeholders i én komponent

Du kan definere **flere placeholders** ved å lage flere `RenderFragment`-parametere.

```razor
<!-- TwoSectionPanel.razor -->
<div class="border p-3">
    <header>@Header</header>
    <section>@Body</section>
</div>

@code {
    [Parameter] public RenderFragment? Header { get; set; }
    [Parameter] public RenderFragment? Body { get; set; }
}
```

Brukes slik:

```razor
<TwoSectionPanel>
    <Header>
        <h3>Panel Title</h3>
    </Header>
    <Body>
        <p>Panel content goes here.</p>
    </Body>
</TwoSectionPanel>
```

🔹 **Forklaring:**  

- `RenderFragment` kan navngis fritt (`Header`, `Body`, `Footer`, osv.).  
- Hvert navngitte område blir en egen “placeholder” der du kan sette inn markup.  
- Dette fungerer akkurat som **“slots”** i Vue eller **`ng-content`** i Angular.

<div style="page-break-after:always;"></div>

## 🧩 5. Nested layouts – `@Body` i flere nivåer

Du kan ha flere lag med layouts, for eksempel et hovedoppsett og et dashbord-oppsett.

```razor
<!-- MainLayout.razor -->
<div class="outer">
    <Sidebar />
    @Body
</div>
```

```razor
<!-- DashboardLayout.razor -->
@inherits LayoutComponentBase
<div class="dashboard">
    <NavMenu />
    <section>@Body</section>
</div>
```

```razor
<!-- DashboardPage.razor -->
@layout DashboardLayout
<h1>Welcome to the dashboard</h1>
```

🔹 **Forklaring:**  

- `DashboardLayout` mottar `@Body` fra `DashboardPage.razor`.  
- `MainLayout` mottar `@Body` fra `DashboardLayout`.  
- Layouts kan **nestes ubegrenset** for å bygge komplekse UI-strukturer.

<div style="page-break-after:always;"></div>

## 🧠 6. `RenderFragment` – Programmatisk UI-innhold

`RenderFragment` er selve typen bak `@ChildContent`.  
Du kan bruke den til å **bygge dynamisk innhold i C#**.

```csharp
RenderFragment content = builder =>
{
    builder.OpenElement(0, "p");
    builder.AddContent(1, "This was rendered dynamically!");
    builder.CloseElement();
};
```

Du kan så bruke dette fragmentet i en komponent:

```razor
<div>
    @content
</div>
```

🔹 **Forklaring:**  

- `RenderFragment` lar deg bygge opp markup fra C#-kode.  
- Brukes for **avanserte komponenter**, som tabeller, modalvinduer, eller dynamiske UI-generatorer.

<div style="page-break-after:always;"></div>

## ⚙️ 7. `RenderFragment<T>` – Placeholder med data (template pattern)

Du kan også bruke en **generisk RenderFragment** for å sende innhold som en “mal” med data.

```razor
<!-- TableTemplate.razor -->
@typeparam TItem

<table class="table">
    <tbody>
        @foreach (var item in Items)
        {
            @RowTemplate(item)
        }
    </tbody>
</table>

@code {
    [Parameter] public IEnumerable<TItem>? Items { get; set; }
    [Parameter] public RenderFragment<TItem>? RowTemplate { get; set; }
}
```

Brukes slik:

```razor
<TableTemplate Items="@products">
    <RowTemplate Context="product">
        <tr>
            <td>@product.Name</td>
            <td>@product.Price</td>
        </tr>
    </RowTemplate>
</TableTemplate>
```

🔹 **Forklaring:**  

- `RenderFragment<T>` lar deg sende inn både markup og **konkret data** (`T`).  
- Gir deg “templating” i Blazor – veldig kraftig for tabeller, kort og lister.

<div style="page-break-after:always;"></div>

## 🧱 8. Andre relevante direktiver

| Direktiv | Bruksområde | Forklaring |
|-----------|--------------|------------|
| `@page` | Sider | Angir at komponenten er tilgjengelig via URL |
| `@layout` | Sider | Velger layout for siden |
| `@inherits` | Alle | Lar deg arve fra en baseklasse |
| `@namespace` | Alle | Setter komponentens namespace |
| `@using` | Alle | Importerer namespaces |

<div style="page-break-after:always;"></div>

## 📋 9. Sammenligningstabell

| Placeholder / Konsept | Type | Bruksområde | Forklaring |
|------------------------|------|--------------|-------------|
| `@Body` | Layout-placeholder | Layouts | Viser sideinnhold |
| `@ChildContent` | Komponent-placeholder | Komponenter | Viser nested innhold |
| `RenderFragment` | Type | Komponenter | Generisk UI-mal |
| `RenderFragment<T>` | Type med data | Komponenter | Mal med datakontekst |
| `@Layout` | Direktive | Sider | Angir layout |
| Nested `@Body` | Placeholder | Flere layouts | Gjør det mulig med hierarki |

---

## 🧭 10. Når bør du bruke hva?

| Behov | Løsning | Eksempel |
|-------|----------|-----------|
| Vise sideinnhold i layout | `@Body` | `MainLayout.razor` |
| Lage komponent som wrapper innhold | `@ChildContent` | `<Panel>...</Panel>` |
| Lage fleksibel komponent med flere seksjoner | Navngitte `RenderFragment`s | `Header`, `Body`, `Footer` |
| Dynamisk generert markup fra kode | `RenderFragment` | Bygge UI programmatisk |
| Repetere innhold for en liste med data | `RenderFragment<T>` | Tabell, liste, kort |

---

## ✅ 11. Oppsummering

Blazor bruker **RenderFragments** som byggeklosser for alt dynamisk innhold.

| Nivå | Placeholder | Forklaring |
|------|-------------|-------------|
| **Layout-nivå** | `@Body` | Viser sidens innhold |
| **Komponent-nivå** | `@ChildContent` | Viser nested markup |
| **Avansert nivå** | `RenderFragment` / `RenderFragment<T>` | Dynamiske eller datadrevne komponenter |

👉 **Kort sagt:**  

- `@Body` → Layoutens innhold  
- `@ChildContent` → Komponentens innhold  
- `RenderFragment` → Byggesteinen for alt dynamisk UI i Blazor

---
