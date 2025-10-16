# Blazor Komponenter - Komplett Oversikt

Denne guiden gir en **fullstendig oversikt over Blazor-komponenter**, fra grunnleggende konsepter til avanserte teknikker.  
Den dekker **komponenttyper**, **lifecycle-metoder**, **databinding**, **events** og **best practices** for å bygge robuste Blazor-applikasjoner.

---

## 🧱 1. Grunnleggende Blazor-komponent

En Blazor-komponent består av **markup** (HTML/Razor) og **logikk** (C#).

```razor
<!-- Counter.razor -->
<div class="counter">
    <h3>Current count: @currentCount</h3>
    <button class="btn btn-primary" @onclick="IncrementCount">Click me</button>
</div>

@code {
    private int currentCount = 0;

    private void IncrementCount()
    {
        currentCount++;
    }
}
```

🔹 **Forklaring:**

- **Markup-seksjonen** inneholder HTML og Razor-syntaks (`@currentCount`).
- **`@code`-blokken** inneholder C#-logikk og state.
- **Data binding** skjer automatisk når state endres (`@currentCount`).
- **Event handling** med `@onclick` kobler HTML-events til C#-metoder.

<div style="page-break-after:always;"></div>

## 🔧 2. Komponent med parametere

Komponenter kan motta data gjennom **parametere** for å gjøre dem gjenbrukbare.

```razor
<!-- PersonCard.razor -->
<div class="card">
    <div class="card-header">
        <h4>@Name</h4>
    </div>
    <div class="card-body">
        <p><strong>Age:</strong> @Age</p>
        <p><strong>Email:</strong> @Email</p>
        <button class="btn btn-info" @onclick="OnContactClick">Contact</button>
    </div>
</div>

@code {
    [Parameter] public string Name { get; set; } = string.Empty;
    [Parameter] public int Age { get; set; }
    [Parameter] public string Email { get; set; } = string.Empty;
    [Parameter] public EventCallback OnContactClick { get; set; }
}
```

Brukes slik:

```razor
<PersonCard Name="Yngve Magnussen" 
            Age="35" 
            Email="yngve@mail.com"
            OnContactClick="HandleContact" />
```

🔹 **Forklaring:**

- **`[Parameter]`** gjør properties tilgjengelige som HTML-attributter.
- **`EventCallback`** lar parent-komponenter reagere på child-events.
- **To-way databinding** er mulig med `EventCallback<T>`.

<div style="page-break-after:always;"></div>

## 🔄 3. Two-way databinding med `@bind`

Blazor støtter **two-way databinding** for form-elementer og egne komponenter.

```razor
<!-- UserForm.razor -->
<div class="form-group">
    <label>Name:</label>
    <input @bind="user.Name" class="form-control" />
</div>

<div class="form-group">
    <label>Age:</label>
    <input @bind="user.Age" type="number" class="form-control" />
</div>

<div class="form-group">
    <label>Email:</label>
    <input @bind="user.Email" type="email" class="form-control" />
</div>

<p>Hello @user.Name, you are @user.Age years old!</p>

@code {
    private User user = new();

    public class User
    {
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Email { get; set; } = string.Empty;
    }
}
```

### Egen komponent med two-way binding:

```razor
<!-- CustomInput.razor -->
<input @bind="CurrentValue" @bind:event="oninput" class="form-control" />

@code {
    private string currentValue = string.Empty;

    [Parameter]
    public string Value { get; set; } = string.Empty;

    [Parameter]
    public EventCallback<string> ValueChanged { get; set; }

    private string CurrentValue
    {
        get => Value;
        set
        {
            if (value == Value) return;
            ValueChanged.InvokeAsync(value);
        }
    }
}
```

Brukes slik:

```razor
<CustomInput @bind-Value="myValue" />
```

🔹 **Forklaring:**

- **`@bind`** setter opp automatisk two-way binding.
- For egne komponenter: `Value` + `ValueChanged` = `@bind-Value`.
- **`@bind:event`** kan spesifisere hvilket DOM-event som trigger oppdatering.

<div style="page-break-after:always;"></div>

## ⚡ 4. Component Lifecycle

Blazor-komponenter har flere **lifecycle-metoder** for ulike faser av komponentens liv.

```razor
<!-- LifecycleDemo.razor -->
<h3>Lifecycle Demo - @currentTime</h3>
<p>Component initialized @initTime</p>

@code {
    private string currentTime = string.Empty;
    private string initTime = string.Empty;
    private Timer? timer;

    protected override void OnInitialized()
    {
        initTime = DateTime.Now.ToString("HH:mm:ss");
        Console.WriteLine("OnInitialized called");
    }

    protected override async Task OnInitializedAsync()
    {
        await Task.Delay(100); // Simulate async work
        Console.WriteLine("OnInitializedAsync called");
    }

    protected override void OnParametersSet()
    {
        Console.WriteLine("OnParametersSet called");
    }

    protected override async Task OnParametersSetAsync()
    {
        await Task.CompletedTask;
        Console.WriteLine("OnParametersSetAsync called");
    }

    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender)
        {
            timer = new Timer(UpdateTime, null, 0, 1000);
            StateHasChanged(); // Force re-render
        }
        Console.WriteLine($"OnAfterRender called (firstRender: {firstRender})");
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await Task.CompletedTask; // JS interop often done here
        }
        Console.WriteLine($"OnAfterRenderAsync called (firstRender: {firstRender})");
    }

    private void UpdateTime(object? state)
    {
        currentTime = DateTime.Now.ToString("HH:mm:ss");
        InvokeAsync(StateHasChanged); // Thread-safe UI update
    }

    public override void Dispose()
    {
        timer?.Dispose();
        Console.WriteLine("Component disposed");
    }
}
```

🔹 **Lifecycle-rekkefølge:**

1. **`OnInitialized()`** / **`OnInitializedAsync()`** - Kun første gang
2. **`OnParametersSet()`** / **`OnParametersSetAsync()`** - Hver gang parametere endres  
3. **`OnAfterRender()`** / **`OnAfterRenderAsync()`** - Etter hver render
4. **`Dispose()`** - Når komponenten fjernes

<div style="page-break-after:always;"></div>

## 📊 5. Conditional rendering og loops

Blazor støtter **betinget rendering** og **loops** med Razor-syntaks.

```razor
<!-- DataList.razor -->
@if (IsLoading)
{
    <div class="spinner-border" role="status">
        <span class="sr-only">Loading...</span>
    </div>
}
else if (Items?.Any() == true)
{
    <ul class="list-group">
        @foreach (var item in Items)
        {
            <li class="list-group-item d-flex justify-content-between">
                <span>@item.Name</span>
                <span class="badge badge-primary">@item.Count</span>
            </li>
        }
    </ul>
}
else
{
    <div class="alert alert-info">
        <h4>No items found</h4>
        <p>Try adding some items or adjusting your search criteria.</p>
    </div>
}

@switch (ViewMode)
{
    case "grid":
        <div class="row">
            @foreach (var item in Items ?? Enumerable.Empty<Item>())
            {
                <div class="col-md-4 mb-3">
                    <div class="card">
                        <div class="card-body">
                            <h5>@item.Name</h5>
                        </div>
                    </div>
                </div>
            }
        </div>
        break;
    case "list":
        <!-- List view markup -->
        break;
    default:
        <p>Unknown view mode: @ViewMode</p>
        break;
}

@code {
    [Parameter] public bool IsLoading { get; set; }
    [Parameter] public IEnumerable<Item>? Items { get; set; }
    [Parameter] public string ViewMode { get; set; } = "list";

    public class Item
    {
        public string Name { get; set; } = string.Empty;
        public int Count { get; set; }
    }
}
```

🔹 **Forklaring:**

- **`@if`** / **`@else`** for betinget visning.
- **`@foreach`** for å iterere over samlinger.
- **`@switch`** for multiple conditions.
- **Null-safe operators** (`?.`, `??`) anbefales.

<div style="page-break-after:always;"></div>

## 🎛️ 6. Event handling og callbacks

Blazor støtter alle standard **DOM-events** og egendefinerte **callbacks**.

```razor
<!-- EventDemo.razor -->
<div class="event-demo">
    <h3>Event Handling Demo</h3>
    
    <!-- Basic events -->
    <button @onclick="HandleClick" class="btn btn-primary">Click Me</button>
    
    <!-- Event with parameters -->
    <button @onclick="@(() => HandleClickWithParam("Hello!"))" class="btn btn-info">
        Click with Parameter
    </button>
    
    <!-- Prevent default -->
    <form @onsubmit="HandleSubmit" @onsubmit:preventDefault="true">
        <input @bind="inputValue" />
        <button type="submit">Submit</button>
    </form>
    
    <!-- Keyboard events -->
    <input @onkeypress="HandleKeyPress" 
           @onkeydown="HandleKeyDown"
           placeholder="Type something..." />
    
    <!-- Mouse events -->
    <div class="mouse-area p-4 border" 
         @onmouseenter="() => mouseStatus = 'Mouse entered'"
         @onmouseleave="() => mouseStatus = 'Mouse left'"
         @onmousemove="HandleMouseMove">
        <p>@mouseStatus</p>
        <p>Mouse position: (@mouseX, @mouseY)</p>
    </div>
    
    <!-- Custom EventCallback -->
    <ChildComponent OnCustomEvent="HandleCustomEvent" />
</div>

@code {
    private string inputValue = string.Empty;
    private string mouseStatus = "Move mouse over the area";
    private int mouseX, mouseY;

    private void HandleClick()
    {
        Console.WriteLine("Button clicked!");
    }

    private void HandleClickWithParam(string message)
    {
        Console.WriteLine($"Button clicked with: {message}");
    }

    private async Task HandleSubmit()
    {
        Console.WriteLine($"Form submitted with value: {inputValue}");
        await Task.CompletedTask;
    }

    private void HandleKeyPress(KeyboardEventArgs e)
    {
        Console.WriteLine($"Key pressed: {e.Key}");
    }

    private void HandleKeyDown(KeyboardEventArgs e)
    {
        if (e.Key == "Enter")
        {
            Console.WriteLine("Enter key pressed!");
        }
    }

    private void HandleMouseMove(MouseEventArgs e)
    {
        mouseX = (int)e.ClientX;
        mouseY = (int)e.ClientY;
    }

    private void HandleCustomEvent(string data)
    {
        Console.WriteLine($"Custom event received: {data}");
    }
}
```

### Child component med EventCallback:

```razor
<!-- ChildComponent.razor -->
<button @onclick="EmitCustomEvent" class="btn btn-warning">
    Trigger Custom Event
</button>

@code {
    [Parameter] public EventCallback<string> OnCustomEvent { get; set; }

    private async Task EmitCustomEvent()
    {
        await OnCustomEvent.InvokeAsync("Data from child component");
    }
}
```

🔹 **Forklaring:**

- **DOM-events**: `@onclick`, `@onkeypress`, `@onmousemove`, etc.
- **Event arguments**: `MouseEventArgs`, `KeyboardEventArgs`, etc.
- **Prevent default**: `@onclick:preventDefault="true"`.
- **EventCallback**: For parent-child kommunikasjon.

<div style="page-break-after:always;"></div>

## 🏗️ 7. Component composition og slots

Blazor støtter **komponent-komposisjon** gjennom RenderFragments og navngitte slots.

```razor
<!-- Modal.razor -->
<div class="modal fade show d-block" tabindex="-1" role="dialog">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            @if (HeaderContent != null)
            {
                <div class="modal-header">
                    @HeaderContent
                    <button type="button" class="close" @onclick="OnClose">
                        <span>&times;</span>
                    </button>
                </div>
            }
            
            <div class="modal-body">
                @BodyContent
            </div>
            
            @if (FooterContent != null)
            {
                <div class="modal-footer">
                    @FooterContent
                </div>
            }
        </div>
    </div>
</div>

@code {
    [Parameter] public RenderFragment? HeaderContent { get; set; }
    [Parameter] public RenderFragment? BodyContent { get; set; }
    [Parameter] public RenderFragment? FooterContent { get; set; }
    [Parameter] public EventCallback OnClose { get; set; }
}
```

Brukes slik:

```razor
<Modal OnClose="CloseModal">
    <HeaderContent>
        <h4 class="modal-title">Confirm Action</h4>
    </HeaderContent>
    <BodyContent>
        <p>Are you sure you want to delete this item?</p>
    </BodyContent>
    <FooterContent>
        <button class="btn btn-secondary" @onclick="CloseModal">Cancel</button>
        <button class="btn btn-danger" @onclick="ConfirmDelete">Delete</button>
    </FooterContent>
</Modal>
```

### Table template med generics:

```razor
<!-- DataTable.razor -->
@typeparam TItem where TItem : class

<table class="table table-striped">
    @if (HeaderTemplate != null)
    {
        <thead>
            @HeaderTemplate
        </thead>
    }
    <tbody>
        @if (Items != null)
        {
            @foreach (var item in Items)
            {
                @RowTemplate(item)
            }
        }
    </tbody>
</table>

@code {
    [Parameter] public IEnumerable<TItem>? Items { get; set; }
    [Parameter] public RenderFragment? HeaderTemplate { get; set; }
    [Parameter] public RenderFragment<TItem>? RowTemplate { get; set; }
}
```

Brukes slik:

```razor
<DataTable Items="@users">
    <HeaderTemplate>
        <tr>
            <th>Name</th>
            <th>Email</th>
            <th>Actions</th>
        </tr>
    </HeaderTemplate>
    <RowTemplate Context="user">
        <tr>
            <td>@user.Name</td>
            <td>@user.Email</td>
            <td>
                <button @onclick="() => EditUser(user)" class="btn btn-sm btn-primary">
                    Edit
                </button>
            </td>
        </tr>
    </RowTemplate>
</DataTable>
```

🔹 **Forklaring:**

- **Navngitte RenderFragments** gir flere "slots" i én komponent.
- **`RenderFragment<T>`** gir tilgang til data i templates.
- **Generics** (`@typeparam`) gjør komponenter type-safe.

<div style="page-break-after:always;"></div>

## 🎨 8. CSS og styling

Blazor støtter flere måter å style komponenter på.

### CSS Isolation (Scoped CSS):

```razor
<!-- StyledComponent.razor -->
<div class="container">
    <h1 class="title">Scoped Styling</h1>
    <p class="description">This styling only applies to this component.</p>
</div>
```

```css
/* StyledComponent.razor.css */
.container {
    background-color: #f8f9fa;
    padding: 20px;
    border-radius: 8px;
}

.title {
    color: #007bff;
    margin-bottom: 10px;
}

.description {
    color: #6c757d;
    font-style: italic;
}
```

### Dynamic CSS classes:

```razor
<!-- DynamicStyling.razor -->
<div class="@GetContainerClass()">
    <button class="@GetButtonClass()" @onclick="ToggleActive">
        @(IsActive ? "Deactivate" : "Activate")
    </button>
</div>

@code {
    [Parameter] public bool IsActive { get; set; }
    [Parameter] public string Theme { get; set; } = "light";

    private string GetContainerClass()
    {
        return $"container theme-{Theme} {(IsActive ? "active" : "inactive")}";
    }

    private string GetButtonClass()
    {
        var baseClass = "btn";
        var themeClass = IsActive ? "btn-success" : "btn-primary";
        return $"{baseClass} {themeClass}";
    }

    private void ToggleActive()
    {
        IsActive = !IsActive;
    }
}
```

### Inline styling:

```razor
<div style="background-color: @BackgroundColor; padding: @Padding;">
    <p>Dynamically styled content</p>
</div>

@code {
    [Parameter] public string BackgroundColor { get; set; } = "#ffffff";
    [Parameter] public string Padding { get; set; } = "10px";
}
```

🔹 **Forklaring:**

- **CSS Isolation**: `.razor.css`-filer gir scoped styling.
- **Dynamic classes**: Bygg CSS-klasser programmatisk.
- **Inline styles**: Bruk med måte, anbefales ikke for kompleks styling.

<div style="page-break-after:always;"></div>

## 🔌 9. JavaScript Interop (JSInterop)

Blazor kan **kommunisere med JavaScript** for avansert funksjonalitet.

```razor
<!-- JSInteropDemo.razor -->
<div>
    <h3>JavaScript Interop</h3>
    <button @onclick="ShowAlert" class="btn btn-primary">Show JS Alert</button>
    <button @onclick="GetWindowSize" class="btn btn-info">Get Window Size</button>
    <button @onclick="FocusElement" class="btn btn-warning">Focus Input</button>
    
    <input @ref="inputElement" placeholder="This can be focused from C#" />
    
    <p>Window size: @windowSize</p>
</div>

@inject IJSRuntime JSRuntime

@code {
    private ElementReference inputElement;
    private string windowSize = "Unknown";

    private async Task ShowAlert()
    {
        await JSRuntime.InvokeVoidAsync("alert", "Hello from Blazor!");
    }

    private async Task GetWindowSize()
    {
        var width = await JSRuntime.InvokeAsync<int>("getWindowWidth");
        var height = await JSRuntime.InvokeAsync<int>("getWindowHeight");
        windowSize = $"{width} x {height}";
    }

    private async Task FocusElement()
    {
        await inputElement.FocusAsync();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            // Initialize JavaScript functions
            await JSRuntime.InvokeVoidAsync("initializeComponent");
        }
    }
}
```

### JavaScript functions (wwwroot/js/site.js):

```javascript
// Custom JavaScript functions for Blazor interop
window.getWindowWidth = () => window.innerWidth;
window.getWindowHeight = () => window.innerHeight;

window.initializeComponent = () => {
    console.log('Component initialized from JavaScript');
};

// Calling Blazor methods from JavaScript
window.blazorInterop = {
    callDotNetMethod: (dotNetReference, methodName, ...args) => {
        return dotNetReference.invokeMethodAsync(methodName, ...args);
    }
};
```

### Calling C# from JavaScript:

```razor
@implements IDisposable

<button @onclick="RegisterForCallback">Register for JS Callback</button>

@inject IJSRuntime JSRuntime

@code {
    private DotNetObjectReference<JSInteropDemo>? objRef;

    private async Task RegisterForCallback()
    {
        objRef = DotNetObjectReference.Create(this);
        await JSRuntime.InvokeVoidAsync("registerBlazorCallback", objRef);
    }

    [JSInvokable]
    public void ReceiveCallbackFromJS(string message)
    {
        Console.WriteLine($"Received from JS: {message}");
        StateHasChanged();
    }

    public void Dispose()
    {
        objRef?.Dispose();
    }
}
```

🔹 **Forklaring:**

- **`IJSRuntime`**: Service for å kalle JavaScript fra C#.
- **`InvokeVoidAsync`**: For JS-funksjoner uten return-verdi.
- **`InvokeAsync<T>`**: For JS-funksjoner med return-verdi.
- **`[JSInvokable]`**: Lar JavaScript kalle C#-metoder.
- **`ElementReference`**: Referanse til DOM-elementer.

<div style="page-break-after:always;"></div>

## 📡 10. Dependency Injection og Services

Blazor støtter **Dependency Injection** for å dele data og funksjonalitet mellom komponenter.

### Service definition:

```csharp
// Services/UserService.cs
public interface IUserService
{
    Task<List<User>> GetUsersAsync();
    Task<User?> GetUserByIdAsync(int id);
    Task SaveUserAsync(User user);
    event Action OnUsersChanged;
}

public class UserService : IUserService
{
    private readonly HttpClient httpClient;
    private List<User> users = new();

    public event Action? OnUsersChanged;

    public UserService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<List<User>> GetUsersAsync()
    {
        if (!users.Any())
        {
            users = await httpClient.GetFromJsonAsync<List<User>>("api/users") ?? new();
        }
        return users;
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
        var allUsers = await GetUsersAsync();
        return allUsers.FirstOrDefault(u => u.Id == id);
    }

    public async Task SaveUserAsync(User user)
    {
        // Save logic here
        await httpClient.PostAsJsonAsync("api/users", user);
        
        // Update local cache
        var existingIndex = users.FindIndex(u => u.Id == user.Id);
        if (existingIndex >= 0)
            users[existingIndex] = user;
        else
            users.Add(user);
        
        // Notify subscribers
        OnUsersChanged?.Invoke();
    }
}
```

### Service registration (Program.cs):

```csharp
builder.Services.AddScoped<IUserService, UserService>();
```

### Using services in components:

```razor
<!-- UserList.razor -->
<div class="user-list">
    <h3>Users</h3>
    
    @if (isLoading)
    {
        <p>Loading users...</p>
    }
    else if (users.Any())
    {
        @foreach (var user in users)
        {
            <div class="card mb-2">
                <div class="card-body">
                    <h5>@user.Name</h5>
                    <p>@user.Email</p>
                </div>
            </div>
        }
    }
    else
    {
        <p>No users found.</p>
    }
</div>

@inject IUserService UserService
@implements IDisposable

@code {
    private List<User> users = new();
    private bool isLoading = true;

    protected override async Task OnInitializedAsync()
    {
        // Subscribe to service events
        UserService.OnUsersChanged += HandleUsersChanged;
        
        // Load initial data
        await LoadUsers();
    }

    private async Task LoadUsers()
    {
        isLoading = true;
        try
        {
            users = await UserService.GetUsersAsync();
        }
        finally
        {
            isLoading = false;
        }
    }

    private async void HandleUsersChanged()
    {
        await LoadUsers();
        await InvokeAsync(StateHasChanged);
    }

    public void Dispose()
    {
        UserService.OnUsersChanged -= HandleUsersChanged;
    }

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
```

🔹 **Forklaring:**

- **`@inject`**: Injiserer services direkte i komponenter.
- **Service events**: Lar komponenter reagere på data-endringer.
- **Scoped services**: Lever så lenge som bruker-sesjonen.
- **Singleton services**: Deles mellom alle brukere.

<div style="page-break-after:always;"></div>

## 📋 11. Sammenligningstabell - Komponenttyper

| Komponenttype | Bruksområde | Eksempel | Hovedfunksjoner |
|---------------|-------------|-----------|----------------|
| **Basic Component** | Enkel UI-logikk | `Counter.razor` | State, events, databinding |
| **Parameterized Component** | Gjenbrukbare UI-elementer | `PersonCard.razor` | Input parametere, EventCallback |
| **Template Component** | Fleksible layout-komponenter | `DataTable<T>.razor` | RenderFragment, generics |
| **Layout Component** | Side-layouts | `MainLayout.razor` | @Body, @inherits LayoutComponentBase |
| **Form Component** | Brukerinput | `UserForm.razor` | Two-way binding, validering |
| **Service Component** | Data-tilgang | `UserList.razor` | Dependency injection, async |

<div style="page-break-after:always;"></div>

## 🎯 12. Best Practices

### Performance:

```razor
<!-- Use @key for efficient list rendering -->
@foreach (var item in items)
{
    <div @key="item.Id">@item.Name</div>
}

<!-- Avoid unnecessary re-renders -->
@if (shouldRender)
{
    <ExpensiveComponent />
}
```

### State management:

```csharp
// Keep state minimal and focused
public class ComponentState
{
    public bool IsLoading { get; set; }
    public string? Error { get; set; }
    public List<Item> Items { get; set; } = new();
}

// Use records for immutable data
public record User(int Id, string Name, string Email);
```

### Error handling:

```razor
<!-- ErrorBoundary.razor -->
<ErrorBoundary>
    <ChildContent>
        @ChildContent
    </ChildContent>
    <ErrorContent Context="ex">
        <div class="alert alert-danger">
            <h4>Something went wrong</h4>
            <p>@ex.Message</p>
        </div>
    </ErrorContent>
</ErrorBoundary>

@code {
    [Parameter] public RenderFragment? ChildContent { get; set; }
}
```

🔹 **Viktige prinsipper:**

- **Single Responsibility**: Hver komponent skal ha én hovedoppgave.
- **Composition over Inheritance**: Bruk RenderFragments fremfor arv.
- **Immutable Data**: Bruk records og unngå direkte mutations.
- **Error Boundaries**: Håndter feil på komponentnivå.
- **Performance**: Bruk `@key` og unngå unødvendige re-renders.

<div style="page-break-after:always;"></div>

## ✅ 13. Oppsummering

Blazor-komponenter er **byggesteinene** i enhver Blazor-applikasjon.

| Konsept | Formål | Nøkkelord |
|---------|--------|-----------|
| **Parameters** | Data inn i komponenten | `[Parameter]`, two-way binding |
| **Events** | Kommunikasjon ut av komponenten | `EventCallback`, DOM-events |
| **Lifecycle** | Kontroll over komponentens liv | `OnInitialized`, `OnAfterRender` |
| **Composition** | Fleksible og gjenbrukbare komponenter | `RenderFragment`, slots |
| **Services** | Delt funksjonalitet | Dependency Injection, `@inject` |

👉 **Kort sagt:**  

- **Start enkelt** med basic komponenter og parametere  
- **Bygg videre** med lifecycle, events og composition  
- **Optimaliser** med services, error handling og performance  

---
