# Blazor Built-in Components - Komplett Oversikt

Denne guiden dekker **alle innebygde komponenter** som følger med Blazor, organisert etter kategori.  
Fra **form-komponenter** til **navigasjon** og **layout** - alt du trenger for å bygge komplette webapplikasjoner uten eksterne biblioteker.

---

## 📝 1. Form Components - EditForm og validering

### EditForm - Hovedkomponent for skjemaer

`EditForm` er kjernekomponenten for alle skjemaer i Blazor med innebygd validering.

```razor
<!-- BasicForm.razor -->
<EditForm Model="@user" OnValidSubmit="@HandleValidSubmit" OnInvalidSubmit="@HandleInvalidSubmit">
    <DataAnnotationsValidator />
    <ValidationSummary />

    <div class="form-group">
        <label>First Name:</label>
        <InputText @bind-Value="user.FirstName" class="form-control" />
        <ValidationMessage For="@(() => user.FirstName)" />
    </div>

    <div class="form-group">
        <label>Email:</label>
        <InputText @bind-Value="user.Email" class="form-control" />
        <ValidationMessage For="@(() => user.Email)" />
    </div>

    <div class="form-group">
        <label>Age:</label>
        <InputNumber @bind-Value="user.Age" class="form-control" />
        <ValidationMessage For="@(() => user.Age)" />
    </div>

    <button type="submit" class="btn btn-primary">Submit</button>
</EditForm>

@code {
    private User user = new();

    private void HandleValidSubmit()
    {
        Console.WriteLine("Form is valid!");
    }

    private void HandleInvalidSubmit()
    {
        Console.WriteLine("Form has validation errors!");
    }

    public class User
    {
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Range(18, 120)]
        public int Age { get; set; }
    }
}
```

🔹 **EditForm properties:**

- **`Model`** - Objektet som bindes til skjemaet
- **`OnValidSubmit`** - Trigges når skjemaet er gyldig og submittes
- **`OnInvalidSubmit`** - Trigges når skjemaet har valideringsfeil
- **`OnSubmit`** - Trigges alltid ved submit (uavhengig av validering)

<div style="page-break-after:always;"></div>

## 📥 2. Input Components - Alle innebygde input-typer

### Tekst-inputs

```razor
<!-- Text inputs -->
<InputText @bind-Value="model.Name" placeholder="Enter name" />
<InputTextArea @bind-Value="model.Description" rows="4" />
<InputPassword @bind-Value="model.Password" />

<!-- Numeric inputs -->
<InputNumber @bind-Value="model.Age" />
<InputNumber @bind-Value="model.Price" TValue="decimal" />

<!-- Date inputs -->
<InputDate @bind-Value="model.BirthDate" />
<InputDate @bind-Value="model.StartTime" TValue="DateTime" />

<!-- Selection inputs -->
<InputSelect @bind-Value="model.Category">
    <option value="">Select category...</option>
    <option value="tech">Technology</option>
    <option value="health">Health</option>
    <option value="finance">Finance</option>
</InputSelect>

<!-- Boolean input -->
<InputCheckbox @bind-Value="model.IsActive" />

<!-- File input -->
<InputFile OnChange="@HandleFileSelection" multiple />

<!-- Radio buttons -->
<InputRadioGroup @bind-Value="model.Gender">
    <InputRadio Value="@("M")" /> Male
    <InputRadio Value="@("F")" /> Female
    <InputRadio Value="@("O")" /> Other
</InputRadioGroup>
```
<div style="page-break-after:always;"></div>

### Avanserte input-eksempler

```razor
<!-- Custom formatting -->
<InputNumber @bind-Value="model.Price" 
             @bind-Value:format="C" 
             class="form-control" />

<!-- Custom date format -->
<InputDate @bind-Value="model.EventDate" 
           @bind-Value:format="yyyy-MM-dd" 
           class="form-control" />

<!-- Multi-select -->
<InputSelect @bind-Value="selectedItems" TValue="string[]">
    @foreach (var option in availableOptions)
    {
        <option value="@option.Value">@option.Text</option>
    }
</InputSelect>

<!-- File upload with restrictions -->
<InputFile OnChange="@HandleFileUpload" 
           accept=".jpg,.png,.pdf" 
           multiple="false" />
```

🔹 **Felles properties for input-komponenter:**

- **`@bind-Value`** - Two-way databinding
- **`class`** - CSS-klasser
- **`disabled`** - Deaktiver input
- **`readonly`** - Kun lesbar
- **`placeholder`** - Placeholder-tekst
- **`TValue`** - Spesifiser datatype eksplisitt

<div style="page-break-after:always;"></div>

## ✅ 3. Validation Components - Valideringskomponenter

### Validatorer

```razor
<!-- Data Annotations Validator -->
<DataAnnotationsValidator />

<!-- Custom validator -->
<FluentValidator TValidator="UserValidator" />

<!-- Validation summary -->
<ValidationSummary />

<!-- Field-specific validation -->
<ValidationMessage For="@(() => model.Email)" />

<!-- Custom validation message -->
<ValidationMessage For="@(() => model.Password)">
    <div class="text-danger">Password is required and must be at least 8 characters.</div>
</ValidationMessage>
```

### Custom validation

```razor
<!-- ObjectGraphDataAnnotationsValidator for nested objects -->
<EditForm Model="@order">
    <ObjectGraphDataAnnotationsValidator />
    
    <InputText @bind-Value="order.Customer.Name" />
    <ValidationMessage For="@(() => order.Customer.Name)" />
    
    <InputText @bind-Value="order.Customer.Email" />
    <ValidationMessage For="@(() => order.Customer.Email)" />
</EditForm>

@code {
    private Order order = new();

    public class Order
    {
        [Required]
        [ValidateComplexType]
        public Customer Customer { get; set; } = new();
    }

    public class Customer
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}
```

🔹 **Validation komponenter:**

- **`DataAnnotationsValidator`** - Standard validering med attributter
- **`ValidationSummary`** - Viser alle valideringsfeil
- **`ValidationMessage`** - Viser feil for spesifikt felt
- **`ObjectGraphDataAnnotationsValidator`** - For nested objekter

<div style="page-break-after:always;"></div>

## 🧭 4. Navigation Components - Routing og navigasjon

### Router og navigasjon

```razor
<!-- App.razor - Main router setup -->
<Router AppAssembly="@typeof(App).Assembly">
    <Found Context="routeData">
        <RouteView RouteData="@routeData" DefaultLayout="@typeof(MainLayout)" />
        <FocusOnNavigate RouteData="@routeData" Selector="h1" />
    </Found>
    <NotFound>
        <PageTitle>Not found</PageTitle>
        <LayoutView Layout="@typeof(MainLayout)">
            <p role="alert">Sorry, there's nothing at this address.</p>
        </LayoutView>
    </NotFound>
</Router>

<!-- NavLink - Smart navigation links -->
<nav class="navbar">
    <NavLink href="/" Match="NavLinkMatch.All" class="nav-link">
        Home
    </NavLink>
    <NavLink href="/products" class="nav-link">
        Products
    </NavLink>
    <NavLink href="/about" class="nav-link">
        About
    </NavLink>
</nav>

<!-- Programmatic navigation -->
<button @onclick="NavigateToProducts" class="btn btn-primary">
    Go to Products
</button>

@inject NavigationManager Navigation

@code {
    private void NavigateToProducts()
    {
        Navigation.NavigateTo("/products");
    }
}
```
<div style="page-break-after:always;"></div>

### Avansert routing

```razor
<!-- Page with parameters -->
@page "/product/{id:int}"
@page "/product/{id:int}/edit"

<h3>Product Details - ID: @Id</h3>

@code {
    [Parameter] public int Id { get; set; }
}

<!-- Query parameters -->
@page "/search"
@implements IDisposable

<h3>Search Results</h3>
<p>Query: @SearchTerm</p>
<p>Category: @Category</p>

@inject NavigationManager Navigation

@code {
    [SupplyParameterFromQuery] public string? SearchTerm { get; set; }
    [SupplyParameterFromQuery] public string? Category { get; set; }

    protected override void OnInitialized()
    {
        Navigation.LocationChanged += HandleLocationChanged;
    }

    private void HandleLocationChanged(object? sender, LocationChangedEventArgs e)
    {
        StateHasChanged();
    }

    public void Dispose()
    {
        Navigation.LocationChanged -= HandleLocationChanged;
    }
}
```

🔹 **Navigation komponenter:**

- **`Router`** - Hovedrouting-komponent
- **`NavLink`** - Navigasjonslinker med aktiv-state
- **`NavigationManager`** - Service for programmatisk navigasjon
- **`FocusOnNavigate`** - Accessibility for fokus

<div style="page-break-after:always;"></div>

## 🏗️ 5. Layout Components - Struktur og layout

### Layout komponenter

```razor
<!-- MainLayout.razor -->
@inherits LayoutComponentBase

<div class="page">
    <div class="sidebar">
        <NavMenu />
    </div>

    <main class="main">
        <div class="top-row px-4">
            <LoginDisplay />
        </div>

        <article class="content px-4">
            @Body
        </article>
    </main>
</div>

<!-- LayoutView - Dynamic layout selection -->
<LayoutView Layout="@GetLayoutType()">
    <h1>Dynamic Layout Content</h1>
</LayoutView>

@code {
    private Type GetLayoutType()
    {
        // Logic to determine layout
        return typeof(MainLayout);
    }
}
```

### Nested layouts

```razor
<!-- AdminLayout.razor -->
@layout MainLayout
@inherits LayoutComponentBase

<div class="admin-container">
    <div class="admin-sidebar">
        <AdminNavigation />
    </div>
    <div class="admin-content">
        @Body
    </div>
</div>

<!-- CascadingValue for sharing data -->
<CascadingValue Value="@currentUser">
    <div class="user-context">
        @ChildContent
    </div>
</CascadingValue>

@code {
    [Parameter] public RenderFragment? ChildContent { get; set; }
    private User currentUser = new();
}

<!-- Consuming cascading values -->
@code {
    [CascadingParameter] public User? CurrentUser { get; set; }
}
```

🔹 **Layout komponenter:**

- **`LayoutComponentBase`** - Base klasse for layouts
- **`LayoutView`** - Dynamisk layout-valg
- **`CascadingValue`** - Del data ned i komponenthierarkiet
- **`@Body`** - Placeholder for sideinnhold

<div style="page-break-after:always;"></div>

## 🔄 6. Virtualization Components - Performance for store lister

### Virtualize for store datasett

```razor
<!-- Virtual scrolling for large lists -->
<div style="height: 400px; overflow-y: auto;">
    <Virtualize Items="@allItems" Context="item">
        <div class="item">
            <h4>@item.Title</h4>
            <p>@item.Description</p>
        </div>
    </Virtualize>
</div>

<!-- Async virtualization -->
<div style="height: 400px; overflow-y: auto;">
    <Virtualize ItemsProvider="@LoadItems" Context="item">
        <ItemContent>
            <div class="item">
                <h4>@item.Title</h4>
                <p>@item.Description</p>
            </div>
        </ItemContent>
        <Placeholder>
            <div class="placeholder">
                Loading...
            </div>
        </Placeholder>
    </Virtualize>
</div>

@code {
    private List<Item> allItems = GenerateItems(10000);

    private async ValueTask<ItemsProviderResult<Item>> LoadItems(ItemsProviderRequest request)
    {
        // Simulate async data loading
        await Task.Delay(100);
        
        var items = allItems
            .Skip(request.StartIndex)
            .Take(request.Count)
            .ToArray();

        return new ItemsProviderResult<Item>(items, allItems.Count);
    }

    private static List<Item> GenerateItems(int count)
    {
        return Enumerable.Range(1, count)
            .Select(i => new Item 
            { 
                Id = i, 
                Title = $"Item {i}", 
                Description = $"Description for item {i}" 
            })
            .ToList();
    }

    public class Item
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
```

🔹 **Virtualize properties:**

- **`Items`** - Statisk liste med data
- **`ItemsProvider`** - Async data loading
- **`ItemSize`** - Fast størrelse per element (performance)
- **`OverscanCount`** - Hvor mange ekstra elementer som pre-rendres

<div style="page-break-after:always;"></div>

## 🚨 7. Error Handling Components - Feilhåndtering

### ErrorBoundary

```razor
<!-- ErrorBoundary wrapper -->
<ErrorBoundary>
    <ChildContent>
        <RiskyComponent />
        <AnotherComponent />
    </ChildContent>
    <ErrorContent Context="exception">
        <div class="alert alert-danger">
            <h4>Oops! Something went wrong</h4>
            <p>@exception.Message</p>
            <button @onclick="RecoverFromError" class="btn btn-primary">
                Try Again
            </button>
        </div>
    </ErrorContent>
</ErrorBoundary>

<!-- Nested error boundaries -->
<ErrorBoundary>
    <ChildContent>
        <MainContent />
        
        <ErrorBoundary>
            <ChildContent>
                <CriticalSection />
            </ChildContent>
            <ErrorContent Context="exception">
                <div class="alert alert-warning">
                    Critical section failed: @exception.Message
                </div>
            </ErrorContent>
        </ErrorBoundary>
    </ChildContent>
    <ErrorContent Context="exception">
        <div class="alert alert-danger">
            Application error: @exception.Message
        </div>
    </ErrorContent>
</ErrorBoundary>

@code {
    private void RecoverFromError()
    {
        // Reset error state
        StateHasChanged();
    }
}
```

🔹 **ErrorBoundary:**

- **`ChildContent`** - Innhold som beskyttes
- **`ErrorContent`** - Vises når feil oppstår
- **Context** - Exception-objektet
- **Nested boundaries** - Flere nivåer av feilhåndtering

<div style="page-break-after:always;"></div>

## 📱 8. Dynamic Components - Dynamisk komponentlasting

### DynamicComponent

```razor
<!-- Dynamic component loading -->
<div class="dynamic-container">
    <label>Select component type:</label>
    <select @onchange="HandleComponentChange">
        <option value="">Choose...</option>
        <option value="counter">Counter</option>
        <option value="weather">Weather</option>
        <option value="todo">Todo List</option>
    </select>

    @if (selectedComponentType != null)
    {
        <DynamicComponent Type="@selectedComponentType" 
                         Parameters="@componentParameters" />
    }
</div>

@code {
    private Type? selectedComponentType;
    private Dictionary<string, object>? componentParameters;

    private void HandleComponentChange(ChangeEventArgs e)
    {
        var componentName = e.Value?.ToString();
        
        selectedComponentType = componentName switch
        {
            "counter" => typeof(Counter),
            "weather" => typeof(WeatherForecast),
            "todo" => typeof(TodoList),
            _ => null
        };

        componentParameters = componentName switch
        {
            "counter" => new Dictionary<string, object> 
            { 
                { "InitialValue", 10 } 
            },
            "weather" => new Dictionary<string, object>
            {
                { "City", "Oslo" }
            },
            _ => null
        };
    }
}

<!-- Advanced dynamic rendering -->
@foreach (var widget in widgets)
{
    <div class="widget">
        <h4>@widget.Title</h4>
        <DynamicComponent Type="@widget.ComponentType"
                         Parameters="@widget.Parameters" />
    </div>
}

@code {
    private List<Widget> widgets = new()
    {
        new Widget 
        { 
            Title = "User Counter", 
            ComponentType = typeof(Counter),
            Parameters = new Dictionary<string, object> { { "InitialValue", 5 } }
        },
        new Widget 
        { 
            Title = "Weather Widget", 
            ComponentType = typeof(WeatherDisplay),
            Parameters = new Dictionary<string, object> { { "Location", "Bergen" } }
        }
    };

    public class Widget
    {
        public string Title { get; set; } = string.Empty;
        public Type ComponentType { get; set; } = typeof(object);
        public Dictionary<string, object>? Parameters { get; set; }
    }
}
```

🔹 **DynamicComponent:**

- **`Type`** - Type av komponenten som skal rendres
- **`Parameters`** - Dictionary med parametere til komponenten
- **Runtime loading** - Last komponenter basert på brukervalg eller data

<div style="page-break-after:always;"></div>

## 🎭 9. Templated Components - Egne template-komponenter

### QuickGrid (ny i .NET 8+)

```razor
<!-- QuickGrid for tabeller -->
<QuickGrid Items="@users" Pagination="@pagination">
    <PropertyColumn Property="@(u => u.Name)" Sortable="true" />
    <PropertyColumn Property="@(u => u.Email)" />
    <PropertyColumn Property="@(u => u.Age)" Format="N0" />
    <TemplateColumn Title="Actions">
        <button @onclick="() => EditUser(context)" class="btn btn-sm btn-primary">
            Edit
        </button>
        <button @onclick="() => DeleteUser(context)" class="btn btn-sm btn-danger">
            Delete
        </button>
    </TemplateColumn>
</QuickGrid>

<Paginator State="@pagination" />

@code {
    private IQueryable<User> users = GenerateUsers().AsQueryable();
    private PaginationState pagination = new() { ItemsPerPage = 10 };

    private void EditUser(User user)
    {
        // Edit logic
    }

    private void DeleteUser(User user)
    {
        // Delete logic
    }
}
```

### Custom templated component

```razor
<!-- DataGrid.razor - Egen templated komponent -->
@typeparam TItem where TItem : class

<div class="data-grid">
    @if (HeaderTemplate != null)
    {
        <div class="grid-header">
            @HeaderTemplate
        </div>
    }
    
    <div class="grid-body">
        @if (Items != null)
        {
            @foreach (var item in Items)
            {
                <div class="grid-row">
                    @ItemTemplate(item)
                </div>
            }
        }
        else
        {
            <div class="no-data">
                @(NoDataTemplate ?? DefaultNoDataContent)
            </div>
        }
    </div>
    
    @if (FooterTemplate != null)
    {
        <div class="grid-footer">
            @FooterTemplate
        </div>
    }
</div>

@code {
    [Parameter] public IEnumerable<TItem>? Items { get; set; }
    [Parameter] public RenderFragment? HeaderTemplate { get; set; }
    [Parameter] public RenderFragment<TItem>? ItemTemplate { get; set; }
    [Parameter] public RenderFragment? FooterTemplate { get; set; }
    [Parameter] public RenderFragment? NoDataTemplate { get; set; }

    private RenderFragment DefaultNoDataContent => @<p>No items to display.</p>;
}
```

Brukes slik:

```razor
<DataGrid Items="@products">
    <HeaderTemplate>
        <h3>Product Catalog</h3>
    </HeaderTemplate>
    <ItemTemplate Context="product">
        <div class="product-card">
            <h4>@product.Name</h4>
            <p>Price: @product.Price.ToString("C")</p>
        </div>
    </ItemTemplate>
    <FooterTemplate>
        <p>Total products: @products.Count()</p>
    </FooterTemplate>
</DataGrid>
```

🔹 **Templated komponenter:**

- **`QuickGrid`** - Innebygd tabellkomponent med sortering og paginering
- **Custom templates** - Lag egne fleksible komponenter
- **`@typeparam`** - Type-safe templates
- **Multiple render fragments** - Flere template-områder

<div style="page-break-after:always;"></div>

## 📋 10. Sammenligningstabell - Alle innebygde komponenter

| Kategori | Komponent | Formål | Nøkkelfunksjoner |
|----------|-----------|---------|-----------------|
| **Forms** | `EditForm` | Skjemavalidering | Model binding, validation |
| | `DataAnnotationsValidator` | Validering | Attribute-basert validering |
| | `ValidationSummary` | Feiloversikt | Alle valideringsfeil |
| | `ValidationMessage` | Feltvalidering | Spesifikk feltfeil |
| **Input** | `InputText` | Tekstinput | String binding |
| | `InputNumber` | Numerisk input | Number binding |
| | `InputDate` | Datoinput | DateTime binding |
| | `InputSelect` | Dropdown | Enum/string binding |
| | `InputCheckbox` | Avkrysning | Boolean binding |
| | `InputFile` | Filopplasting | File handling |
| | `InputRadioGroup` | Radio buttons | Group selection |
| **Navigation** | `Router` | URL routing | Page routing |
| | `NavLink` | Navigasjonslinker | Active state |
| | `NavigationManager` | Programmatisk navigering | URL manipulation |
| **Layout** | `LayoutComponentBase` | Layout base | Page structure |
| | `LayoutView` | Dynamisk layout | Runtime layout selection |
| | `CascadingValue` | Data deling | Hierarchical data |
| **Performance** | `Virtualize` | Virtual scrolling | Large lists |
| | `ErrorBoundary` | Feilhåndtering | Exception boundaries |
| | `DynamicComponent` | Dynamic loading | Runtime components |
| **Data** | `QuickGrid` | Datatabeller | Sorting, pagination |

<div style="page-break-after:always;"></div>

## 🎯 11. Best Practices for innebygde komponenter

### Form validation

```razor
<!-- Comprehensive form setup -->
<EditForm EditContext="@editContext" OnValidSubmit="@HandleValidSubmit">
    <DataAnnotationsValidator />
    <CustomValidator />
    
    <!-- Always include validation messages -->
    <ValidationSummary />
    
    <!-- Use appropriate input types -->
    <InputText @bind-Value="model.Email" type="email" />
    <InputNumber @bind-Value="model.Age" min="0" max="120" />
    
    <!-- Custom validation styling -->
    <ValidationMessage For="@(() => model.Email)" class="text-danger" />
</EditForm>

@code {
    private EditContext editContext;
    private Model model = new();

    protected override void OnInitialized()
    {
        editContext = new EditContext(model);
    }
}
```

### Performance optimization

```razor
<!-- Use Virtualize for large datasets -->
<Virtualize Items="@largeDataset" ItemSize="50">
    <div class="item" style="height: 50px;">
        @context.Name
    </div>
</Virtualize>

<!-- Use @key for efficient rendering -->
@foreach (var item in items)
{
    <ItemComponent @key="item.Id" Item="@item" />
}

<!-- Avoid unnecessary re-renders -->
@if (showExpensiveComponent)
{
    <ExpensiveComponent />
}
```

### Error handling

```razor
<!-- Layer error boundaries appropriately -->
<ErrorBoundary>
    <ChildContent>
        <!-- Critical app sections -->
        <MainNavigation />
        
        <ErrorBoundary>
            <ChildContent>
                <!-- User content that might fail -->
                <UserContent />
            </ChildContent>
            <ErrorContent Context="ex">
                <div class="user-error">Content unavailable</div>
            </ErrorContent>
        </ErrorBoundary>
    </ChildContent>
    <ErrorContent Context="ex">
        <div class="app-error">Application error occurred</div>
    </ErrorContent>
</ErrorBoundary>
```

🔹 **Viktige prinsipper:**

- **Bruk riktig komponent** for riktig formål
- **Kombiner komponenter** for kompleks funksjonalitet  
- **Valider alltid** brukerinput med `EditForm`
- **Optimaliser ytelse** med `Virtualize` og `@key`
- **Håndter feil** med `ErrorBoundary`

<div style="page-break-after:always;"></div>

## ✅ 12. Oppsummering

Blazor kommer med et **rikt sett av innebygde komponenter** som dekker de fleste behov.

| Behov | Blazor løsning | Hovedkomponenter |
|-------|----------------|------------------|
| **Skjemaer** | Form + Input komponenter | `EditForm`, `InputText`, `DataAnnotationsValidator` |
| **Navigasjon** | Router komponenter | `Router`, `NavLink`, `NavigationManager` |
| **Layout** | Layout komponenter | `LayoutComponentBase`, `CascadingValue` |
| **Performance** | Virtualization | `Virtualize`, `@key` |
| **Feilhåndtering** | Error boundaries | `ErrorBoundary` |
| **Flexibilitet** | Dynamic components | `DynamicComponent`, templated components |

👉 **Kort sagt:**  

- **Start med innebygde komponenter** - dekker 90% av behovene
- **Kombiner intelligently** - bygg komplekse løsninger med enkle byggeklosser  
- **Utvid når nødvendig** - lag egne komponenter når innebygde ikke strekker til

---