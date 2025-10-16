# Blazor Guide

Blazor is a modern web framework from Microsoft that enables developers to build interactive web applications using C# and .NET instead of JavaScript. It leverages the power of WebAssembly and SignalR to create dynamic, responsive user interfaces with familiar C# syntax.

---

## 🌐 What is Blazor?

Blazor lets you build full-stack web applications with C# on both client and server. It offers multiple hosting models to fit different requirements:

### Hosting Models

1. **Blazor Server**
   * Application logic runs on the server
   * UI updates sent to the browser via SignalR (real-time communication)
   * Small download size and fast initial load
   * Requires constant server connection
   * Best for internal applications or when minimizing client-side processing is needed

2. **Blazor WebAssembly (WASM)**
   * Application runs directly in the browser using WebAssembly
   * Can work offline after initial download
   * No constant server dependency
   * Larger initial download (~5-10MB) but cached for subsequent visits
   * Best for public-facing apps and Progressive Web Applications (PWAs)

3. **Blazor Web App** (Latest in .NET 8+)
   * Combines benefits of both Server and WebAssembly approaches
   * Enables per-component render mode decisions
   * Supports static rendering, interactive server rendering, or interactive WebAssembly
   * Best for applications that need flexibility in rendering approach

4. **Blazor Hybrid**
   * Use Blazor components in native apps (.NET MAUI, WPF, WinForms)
   * Share code between web and native apps
   * Access native platform features through .NET APIs

<div style="page-break-after:always"></div>

### When to Choose Each Model

| Factor | Blazor Server | Blazor WebAssembly | Blazor Web App |
|--------|--------------|-------------------|---------------|
| Initial Load | Fast ✅ | Slower ❌ | Fast with progressive enhancement ✅ |
| Offline Support | No ❌ | Yes ✅ | Configurable ✅ |
| Server Dependency | Continuous ❌ | Minimal ✅ | Configurable ✅ |
| Security-sensitive code | Protected on server ✅ | Exposed to client ❌ | Can keep sensitive parts on server ✅ |

---

## 🧩 Core Components and Syntax

### Razor Components

Blazor applications are built using Razor components - self-contained pieces of user interface with their own rendering logic and behavior.

* Components are defined in `.razor` files
* Combine HTML markup with C# code
* Follow a component-based architecture pattern
* Can be nested and reused throughout your application

#### Basic Component Example

```razor
@page "/counter"

<h1>Counter Example</h1>

<p>Current count: @currentCount</p>

<button class="btn btn-primary" @onclick="IncrementCount">Increment</button>

@code {
    private int currentCount = 0;

    private void IncrementCount()
    {
        currentCount++;
    }
}
```

<div style="page-break-after:always"></div>

#### Best Practices for Components

1. **Keep components focused** - Each component should do one thing well
2. **Limit component size** - Break complex UI into smaller components
3. **Use meaningful names** - Name components according to their purpose
4. **Organize components logically** - Group related components in folders
5. **Follow Pascal case naming** - Component filenames should use Pascal case (e.g., `ProductDetail.razor`)

### Routing

Blazor provides a client-side routing system that maps URLs to components.

```razor
@page "/products"
@page "/products/{id:int}"

<h1>Product @Id</h1>

@code {
    [Parameter]
    public int Id { get; set; }
}
```

* The `@page` directive defines routes
* Multiple routes can point to the same component
* Route parameters can be extracted (like `{id:int}`)
* Route constraints can validate parameters (`:int`, `:guid`, `:bool`, etc.)

### Data Binding

Blazor offers one-way and two-way data binding to synchronize data between UI elements and component state.

#### One-way Binding

```razor
<p>Hello, @name!</p>

@code {
    private string name = "World";
}
```

<div style="page-break-after:always"></div>

#### Two-way Binding

```razor
<input @bind="name" @bind:event="oninput" />
<p>Hello, @name!</p>

@code {
    private string name = "World";
}
```

#### Best Practices for Data Binding

1. **Use one-way binding** when possible for better performance
2. **Limit use of `@bind:event="oninput"`** as it triggers on every keystroke
3. **Consider debouncing** for input fields that trigger expensive operations
4. **Use `StateHasChanged()` wisely** - don't overuse it

### Component Parameters

Component parameters allow passing data from parent to child components using attributes.

```razor
<!-- ParentComponent.razor -->
<ChildComponent Title="Product Details" Count="5" />

<!-- ChildComponent.razor -->
<div class="card">
    <div class="card-header">@Title</div>
    <div class="card-body">
        <p>Count: @Count</p>
    </div>
</div>

@code {
    [Parameter]
    public string Title { get; set; } = string.Empty;
    
    [Parameter]
    public int Count { get; set; }
    
    [Parameter]
    [EditorRequired]
    public EventCallback<int> OnCountChanged { get; set; }
}
```

* Use `[Parameter]` attribute to define component parameters
* Use `[EditorRequired]` to mark required parameters
* Use `EventCallback<T>` for component events that notify the parent

### Event Handling

Blazor components can respond to standard DOM events using C# methods.

```razor
<button @onclick="HandleClick">Click Me</button>
<button @onclick="() => HandleClickWithParam('Hello')">Click With Param</button>

@code {
    private void HandleClick()
    {
        // Handle the click event
    }
    
    private void HandleClickWithParam(string message)
    {
        Console.WriteLine(message);
    }
}
```

Common events:

* `@onclick`, `@onchange`, `@oninput`
* `@onsubmit`, `@onfocus`, `@onblur`
* `@onkeydown`, `@onkeyup`, `@onmouseover`

---
<div style="page-break-after:always"></div>

## 📁 Project Structure and Organization

A typical Blazor project structure:

```
BlazorApp/
├── Components/           (Blazor Web App)
│   ├── Pages/           (Routable components)
│   ├── Layout/          (Layout components)
│   └── _Imports.razor   (Common imports)
├── Pages/               (Legacy structure in Blazor Server/WASM)
├── Shared/              (Legacy structure in Blazor Server/WASM)
├── wwwroot/             (Static assets)
│   ├── css/
│   ├── js/
│   └── index.html       (WebAssembly host page)
├── App.razor            (Application root component)
├── Program.cs           (Application entry point)
└── _Imports.razor       (Common imports for all components)
```

### Best Practices for Project Organization

1. **Group related components** in folders by feature or functionality
2. **Use partial classes** for separating markup from logic in complex components
3. **Create reusable UI libraries** with Razor Class Libraries (RCLs)
4. **Organize CSS using isolation** with component-specific `.razor.css` files
5. **Keep components small and focused** following the Single Responsibility Principle

---
<div style="page-break-after:always"></div>

## 💡 Advanced Component Concepts

### Lifecycle Methods

Blazor components have a well-defined lifecycle with methods that execute at specific points:

```razor
@implements IDisposable

@code {
    // Called once when the component is initialized
    protected override void OnInitialized()
    {
        Console.WriteLine("Component initialized");
    }
    
    // Called once after the component is initialized, supports async
    protected override async Task OnInitializedAsync()
    {
        await LoadDataAsync();
    }
    
    // Called when parameters are set or updated
    protected override void OnParametersSet()
    {
        Console.WriteLine("Parameters set");
    }
    
    // Called after parameters are set, supports async
    protected override async Task OnParametersSetAsync()
    {
        await LoadDataBasedOnParametersAsync();
    }
    
    // Called after the component has rendered
    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender)
        {
            Console.WriteLine("First render complete");
        }
    }
    
    // Called after rendering, supports async
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await InitializeJsAsync();
        }
    }
    
    // Cleanup when the component is removed
    public void Dispose()
    {
        Console.WriteLine("Component disposed");
    }
}
```

#### Best Practices for Lifecycle Methods

1. **Use `OnInitializedAsync` for data loading** instead of constructors
2. **Handle first render** specially using the `firstRender` parameter
3. **Clean up resources** with `IDisposable` implementation
4. **Be aware of when state changes** will trigger re-renders

### Component References

Blazor allows capturing references to child components to call their methods:

```razor
<ChildComponent @ref="childComponent" />

<button @onclick="CallChildMethod">Call Child Method</button>

@code {
    private ChildComponent childComponent;
    
    private void CallChildMethod()
    {
        childComponent.MethodToCall();
    }
}
```

### Templates and RenderFragments

`RenderFragment` allows passing UI content (templates) between components:

```razor
<!-- CardComponent.razor -->
<div class="card">
    <div class="card-header">@Title</div>
    <div class="card-body">
        @ChildContent
    </div>
    <div class="card-footer">
        @Footer
    </div>
</div>

@code {
    [Parameter]
    public string Title { get; set; }
    
    [Parameter]
    public RenderFragment ChildContent { get; set; }
    
    [Parameter]
    public RenderFragment Footer { get; set; }
}

<!-- Usage -->
<CardComponent Title="User Details">
    <p>This content goes in the card body.</p>
    
    <Footer>
        <button class="btn btn-primary">Save</button>
    </Footer>
</CardComponent>
```

### Dependency Injection

Blazor fully integrates with .NET's dependency injection system:

```razor
@page "/fetchdata"
@inject IWeatherService WeatherService

<h1>Weather Forecast</h1>

@if (forecasts == null)
{
    <p><em>Loading...</em></p>
}
else
{
    <table class="table">
        <thead>
            <tr>
                <th>Date</th>
                <th>Temp. (C)</th>
                <th>Summary</th>
            </tr>
        </thead>
        <tbody>
            @foreach (var forecast in forecasts)
            {
                <tr>
                    <td>@forecast.Date.ToShortDateString()</td>
                    <td>@forecast.TemperatureC</td>
                    <td>@forecast.Summary</td>
                </tr>
            }
        </tbody>
    </table>
}

@code {
    private WeatherForecast[] forecasts;

    protected override async Task OnInitializedAsync()
    {
        forecasts = await WeatherService.GetForecastAsync();
    }
}
```

#### Registering Services

In `Program.cs`:

```csharp
builder.Services.AddScoped<IWeatherService, WeatherService>();
```

Service lifetimes:
* **Transient**: Created each time they're requested
* **Scoped**: Created once per client connection (Blazor Server) or per application lifetime (Blazor WebAssembly)
* **Singleton**: Created once for the lifetime of the application

---
<div style="page-break-after:always"></div>

## 🔄 Forms and Validation

Blazor provides a rich forms system with built-in validation support.

### Basic Form Example

```razor
<EditForm Model="@person" OnValidSubmit="HandleValidSubmit">
    <DataAnnotationsValidator />
    <ValidationSummary />
    
    <div class="form-group">
        <label for="name">Name:</label>
        <InputText id="name" @bind-Value="person.Name" class="form-control" />
        <ValidationMessage For="@(() => person.Name)" />
    </div>
    
    <div class="form-group">
        <label for="email">Email:</label>
        <InputText id="email" @bind-Value="person.Email" class="form-control" />
        <ValidationMessage For="@(() => person.Email)" />
    </div>
    
    <div class="form-group">
        <label for="age">Age:</label>
        <InputNumber id="age" @bind-Value="person.Age" class="form-control" />
        <ValidationMessage For="@(() => person.Age)" />
    </div>
    
    <button type="submit" class="btn btn-primary">Submit</button>
</EditForm>

@code {
    private Person person = new Person();
    
    private void HandleValidSubmit()
    {
        // Process the valid form
        Console.WriteLine("Form submitted successfully");
    }
    
    public class Person
    {
        [Required]
        [StringLength(50, ErrorMessage = "Name is too long")]
        public string Name { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Range(18, 100, ErrorMessage = "Age must be between 18 and 100")]
        public int Age { get; set; }
    }
}
```

### Form Components

Blazor provides specialized input components for form fields:

* `InputText`: For string values
* `InputTextArea`: For multiline text
* `InputNumber`: For numeric values
* `InputSelect`: For dropdowns
* `InputCheckbox`: For boolean values
* `InputDate`: For date values
* `InputRadio`: For radio buttons
* `InputRadioGroup`: For groups of radio buttons
* `InputFile`: For file uploads

### Form Validation

Validation can be implemented using:

1. **Data Annotations**: Using attributes like `[Required]`, `[StringLength]`, etc.
2. **Custom Validation**: Implementing `IValidatableObject` or custom validators
3. **Programmatic Validation**: Using the `EditContext` directly

#### Best Practices for Forms

1. **Use model binding** with `@bind-Value` instead of manual event handling
2. **Group related fields** with fieldsets and proper labels
3. **Provide clear validation messages** that guide the user
4. **Use appropriate input types** for different data types
5. **Implement client-side validation** to improve user experience
6. **Always validate on the server** as well for security

---
<div style="page-break-after:always"></div>

## 📡 JavaScript Interop

Blazor allows calling JavaScript functions from C# and vice versa.

### Calling JavaScript from C #

```razor
@page "/js-interop"
@inject IJSRuntime JS

<button @onclick="ShowAlert">Show Alert</button>
<button @onclick="CallJsFunction">Call Custom JS Function</button>

@code {
    private async Task ShowAlert()
    {
        await JS.InvokeVoidAsync("alert", "Hello from Blazor!");
    }
    
    private async Task CallJsFunction()
    {
        await JS.InvokeVoidAsync("myCustomFunction", "param1", 42);
    }
}
```

In `wwwroot/index.html` or `wwwroot/js/app.js`:

```javascript
window.myCustomFunction = (param1, param2) => {
    console.log(`Called with ${param1} and ${param2}`);
    return "Result from JS";
};
```

### Calling C# from JavaScript

```razor
@page "/js-callback"
@inject IJSRuntime JS
@implements IDisposable

<button id="js-button">Click me (from JS)</button>

@code {
    private DotNetObjectReference<JsCallbackExample> objRef;
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            objRef = DotNetObjectReference.Create(this);
            await JS.InvokeVoidAsync("setupCallback", objRef);
        }
    }
    
    [JSInvokable]
    public void HandleJsEvent(string message)
    {
        Console.WriteLine($"JS called C# with: {message}");
    }
    
    public void Dispose()
    {
        objRef?.Dispose();
    }
}
```

JavaScript:

```javascript
window.setupCallback = (dotNetHelper) => {
    document.getElementById('js-button').addEventListener('click', () => {
        dotNetHelper.invokeMethodAsync('HandleJsEvent', 'Button was clicked');
    });
};
```

#### Best Practices for JS Interop

1. **Minimize JS interop** - it has performance overhead
2. **Dispose DotNetObjectReference** objects to prevent memory leaks
3. **Use JS isolation** with JavaScript modules (`.js` files imported in components)
4. **Create typed JS interop services** for complex interactions

---
<div style="page-break-after:always"></div>

## 🔁 State Management

### Component State

Each component manages its own state:

```razor
@code {
    private int counter = 0;
    
    private void IncrementCounter()
    {
        counter++;
        StateHasChanged(); // Explicitly notify Blazor to re-render (usually not needed)
    }
}
```

### Parent-Child State

Parent components can pass state to children and receive callbacks when changes occur:

```razor
<!-- Parent.razor -->
<h1>Parent Counter: @parentCounter</h1>
<ChildCounter CurrentCount="parentCounter" OnCountChanged="HandleCountChanged" />

@code {
    private int parentCounter = 0;
    
    private void HandleCountChanged(int newCount)
    {
        parentCounter = newCount;
    }
}

<!-- ChildCounter.razor -->
<div>
    <h2>Child Counter: @CurrentCount</h2>
    <button @onclick="IncrementCounter">Increment</button>
</div>

@code {
    [Parameter]
    public int CurrentCount { get; set; }
    
    [Parameter]
    public EventCallback<int> OnCountChanged { get; set; }
    
    private async Task IncrementCounter()
    {
        await OnCountChanged.InvokeAsync(CurrentCount + 1);
    }
}
```

### Cascading Parameters

Share data deep into the component hierarchy without passing through intermediate components:

```razor
<!-- Parent.razor -->
<CascadingValue Value="@theme">
    <ChildComponent />
</CascadingValue>

@code {
    private string theme = "dark";
}

<!-- ChildComponent.razor (can be nested deeply) -->
<div class="@Theme-mode">
    Current theme: @Theme
</div>

@code {
    [CascadingParameter]
    public string Theme { get; set; }
}
```

### Application State with Services

For global state, create a service:

```csharp
public class AppState
{
    // Event that will be triggered when CounterValue is changed
    public event Action OnChange;
    
    private int counterValue;
    
    public int CounterValue
    {
        get => counterValue;
        set
        {
            counterValue = value;
            NotifyStateChanged();
        }
    }
    
    private void NotifyStateChanged() => OnChange?.Invoke();
}
```

Register as a service:

```csharp
builder.Services.AddScoped<AppState>();
```

Use in components:

```razor
@page "/counter-state"
@inject AppState State
@implements IDisposable

<h1>Counter: @State.CounterValue</h1>
<button @onclick="IncrementCounter">Increment</button>

@code {
    protected override void OnInitialized()
    {
        // Subscribe to state changes
        State.OnChange += StateHasChanged;
    }
    
    private void IncrementCounter()
    {
        State.CounterValue++;
    }
    
    public void Dispose()
    {
        // Unsubscribe to prevent memory leaks
        State.OnChange -= StateHasChanged;
    }
}
```

#### Best Practices for State Management

1. **Keep state close** to where it's used
2. **Use cascading parameters sparingly** - they can make code harder to understand
3. **Consider state management libraries** for complex applications (Fluxor, Blazor-State)
4. **Be cautious of services with events** - always unsubscribe in `Dispose()`
5. **Use immutable patterns** when possible to avoid unexpected state changes

---
<div style="page-break-after:always"></div>

## 🖥️ Render Modes (.NET 8+)

Blazor Web Apps introduce render modes that determine how and where a component is rendered:

### Static Rendering

Components are rendered on the server as static HTML with no interactivity:

```razor
@rendermode RenderMode.Static

<h1>Static Component</h1>
<p>This component is rendered once on the server.</p>
```

### Interactive Server Rendering

Components are rendered on the server and made interactive via SignalR:

```razor
@rendermode RenderMode.InteractiveServer

<h1>Interactive Server Component</h1>
<button @onclick="HandleClick">Click count: @clickCount</button>

@code {
    private int clickCount = 0;
    
    private void HandleClick()
    {
        clickCount++;
    }
}
```

### Interactive WebAssembly Rendering

Components are rendered on the server then interactive via WebAssembly:

```razor
@rendermode RenderMode.InteractiveWebAssembly

<h1>Interactive WebAssembly Component</h1>
<button @onclick="HandleClick">Click count: @clickCount</button>

@code {
    private int clickCount = 0;
    
    private void HandleClick()
    {
        clickCount++;
    }
}
```

### Auto Rendering

Automatically chooses between Server and WebAssembly:

```razor
@rendermode RenderMode.InteractiveAuto

<h1>Auto Mode Component</h1>
<button @onclick="HandleClick">Click count: @clickCount</button>

@code {
    private int clickCount = 0;
    
    private void HandleClick()
    {
        clickCount++;
    }
}
```

#### Best Practices for Render Modes

1. **Use static rendering** for content that doesn't need interactivity
2. **Use Server mode** for components that need quick initial load
3. **Use WebAssembly mode** for components that need offline support
4. **Use Auto mode** for components that should adapt to the user's environment

---
<div style="page-break-after:always"></div>

## 📱 Building Progressive Web Apps (PWAs)

Blazor WebAssembly apps can be configured as PWAs for offline capabilities and native-like experiences.

### Setup

1. Add these NuGet packages:
   * `Microsoft.AspNetCore.Components.WebAssembly.PWA`

2. Configure the manifest in `wwwroot/manifest.json`:

```json
{
  "name": "My Blazor PWA",
  "short_name": "BlazorPWA",
  "start_url": "./",
  "display": "standalone",
  "background_color": "#ffffff",
  "theme_color": "#03173d",
  "icons": [
    {
      "src": "icon-512.png",
      "type": "image/png",
      "sizes": "512x512"
    }
  ]
}
```

3. Configure the service worker in `wwwroot/service-worker.js` (or use the default one).

### Offline Support

The service worker can cache resources for offline use:

```javascript
// Example service-worker.js
self.addEventListener('fetch', event => {
    event.respondWith(
        caches.match(event.request)
            .then(response => response || fetch(event.request))
    );
});
```

### Installation Experience

Prompt users to install the app:

```razor
@page "/install"
@inject IJSRuntime JS

<button @onclick="InstallPwa" style="display: @(isInstallable ? "block" : "none")">
    Install App
</button>

@code {
    private bool isInstallable = false;
    private object deferredPrompt;
    
    protected override async Task OnInitializedAsync()
    {
        // Set up event listener for beforeinstallprompt
        await JS.InvokeVoidAsync("setupPwaInstall", DotNetObjectReference.Create(this));
    }
    
    [JSInvokable]
    public void OnBeforeInstallPrompt(object prompt)
    {
        isInstallable = true;
        deferredPrompt = prompt;
        StateHasChanged();
    }
    
    private async Task InstallPwa()
    {
        if (deferredPrompt != null)
        {
            await JS.InvokeVoidAsync("showInstallPrompt", deferredPrompt);
            isInstallable = false;
        }
    }
}
```

JavaScript:

```javascript
let savedPrompt;

window.setupPwaInstall = (dotNetHelper) => {
    window.addEventListener('beforeinstallprompt', (e) => {
        e.preventDefault();
        savedPrompt = e;
        dotNetHelper.invokeMethodAsync('OnBeforeInstallPrompt', {});
    });
};

window.showInstallPrompt = async () => {
    if (savedPrompt) {
        savedPrompt.prompt();
        const result = await savedPrompt.userChoice;
        savedPrompt = null;
    }
};
```

---
<div style="page-break-after:always"></div>

## 🔒 Authentication and Authorization

### Authentication Options

1. **Blazor Server**:
   * Uses ASP.NET Core Identity
   * Server-side authentication with cookies

2. **Blazor WebAssembly**:
   * OIDC providers (Microsoft Entra ID, Auth0, etc.)
   * Backend API authentication with tokens

### Authentication Example

```razor
@page "/login"
@using Microsoft.AspNetCore.Components.Authorization
@inject AuthenticationStateProvider AuthStateProvider
@inject NavigationManager Navigation

<h1>Login</h1>

@if (errorMessage != null)
{
    <div class="alert alert-danger">@errorMessage</div>
}

<EditForm Model="loginModel" OnValidSubmit="HandleLogin">
    <DataAnnotationsValidator />
    
    <div class="form-group">
        <label>Email:</label>
        <InputText @bind-Value="loginModel.Email" class="form-control" />
        <ValidationMessage For="@(() => loginModel.Email)" />
    </div>
    
    <div class="form-group">
        <label>Password:</label>
        <InputText type="password" @bind-Value="loginModel.Password" class="form-control" />
        <ValidationMessage For="@(() => loginModel.Password)" />
    </div>
    
    <button type="submit" class="btn btn-primary">Login</button>
</EditForm>

@code {
    private LoginModel loginModel = new();
    private string errorMessage;
    
    private async Task HandleLogin()
    {
        // Example implementation - in real app, call auth service
        try {
            // Perform authentication logic
            await ((CustomAuthStateProvider)AuthStateProvider).LoginAsync(loginModel);
            Navigation.NavigateTo("/");
        }
        catch (Exception ex) {
            errorMessage = ex.Message;
        }
    }
    
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }
    }
}
```

### Authorization

Use `[Authorize]` attribute to protect components:

```razor
@page "/protected"
@attribute [Authorize]

<h1>Protected Page</h1>

<AuthorizeView>
    <Authorized>
        <p>Hello, @context.User.Identity.Name!</p>
    </Authorized>
    <NotAuthorized>
        <p>You're not authorized to view this content.</p>
    </NotAuthorized>
</AuthorizeView>
```

#### Using Role-based Authorization

```razor
@page "/admin-dashboard"
@attribute [Authorize(Roles = "Admin")]

<h1>Admin Dashboard</h1>
```

#### Using Policy-based Authorization

```razor
@page "/premium-content"
@attribute [Authorize(Policy = "PremiumUser")]

<h1>Premium Content</h1>
```

#### Best Practices for Authentication

1. **Always validate claims on the server** for secure operations
2. **Use HTTPS** for all authentication traffic
3. **Implement proper token storage** in WebAssembly apps
4. **Consider refresh tokens** for better user experience
5. **Provide clear feedback** when authentication fails

---
<div style="page-break-after:always"></div>

## 📊 Performance Optimization

### Component Optimization

1. **Avoid unnecessary renders** by minimizing state changes
2. **Use `@key` directive** when rendering lists to help Blazor track items:

```razor
@foreach (var item in items)
{
    <ItemComponent @key="item.Id" Item="item" />
}
```

3. **Implement `ShouldRender()`** to avoid unnecessary rendering:

```razor
@code {
    private int lastCount;
    private int currentCount;
    
    protected override bool ShouldRender()
    {
        return lastCount != currentCount;
    }
    
    private void UpdateCount(int newCount)
    {
        lastCount = currentCount;
        currentCount = newCount;
    }
}
```

### Virtualization

For large lists, use the `Virtualize` component:

```razor
<Virtualize Items="@largeList" Context="item" OverscanCount="10">
    <p>@item.Text</p>
</Virtualize>

@code {
    private List<DataItem> largeList = Enumerable.Range(0, 10000)
        .Select(i => new DataItem { Id = i, Text = $"Item {i}" })
        .ToList();
    
    public class DataItem
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }
}
```

### Lazy Loading

For large applications, use lazy loading with the router:

```razor
<Router AppAssembly="@typeof(App).Assembly">
    <Found Context="routeData">
        <RouteView RouteData="@routeData" DefaultLayout="@typeof(MainLayout)" />
    </Found>
    <NotFound>
        <LayoutView Layout="@typeof(MainLayout)">
            <p>Sorry, there's nothing at this address.</p>
        </LayoutView>
    </NotFound>
</Router>
```

And in `AssemblyInfo.cs`:

```csharp
[assembly: WebElementModule("Admin", typeof(AdminModule))]
```

### Code Size Optimization

1. **Trim unused assemblies** with IL trimming
2. **Use AOT compilation** for WebAssembly (requires .NET 7+)
3. **Compress responses** with Brotli or Gzip
4. **Implement code splitting** for large applications

### Prerendering

Improve initial load times with prerendering:

```csharp
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
```

In `_Host.cshtml`:

```html
<component type="typeof(App)" render-mode="ServerPrerendered" />
```

#### Best Practices for Performance

1. **Profile before optimizing** - use browser DevTools to identify bottlenecks
2. **Minimize component re-rendering** - use `ShouldRender()` and smart state management
3. **Use virtualization** for long lists
4. **Implement lazy loading** for large applications
5. **Consider server vs. client rendering** based on component needs

---
<div style="page-break-after:always"></div>

## 🐞 Debugging and Error Handling

### Error Boundaries

Catch and display errors in components:

```razor
<ErrorBoundary>
    <ChildContent>
        <ComponentThatMightError />
    </ChildContent>
    <ErrorContent Context="exception">
        <p class="error">An error occurred: @exception.Message</p>
    </ErrorContent>
</ErrorBoundary>
```

### Logging

Configure logging in WebAssembly:

```csharp
builder.Logging.SetMinimumLevel(LogLevel.Information);
builder.Logging.AddProvider(new BrowserConsoleLoggerProvider());
```

Use in components:

```razor
@inject ILogger<MyComponent> Logger

@code {
    private void HandleSomething()
    {
        try
        {
            // Some operation
            Logger.LogInformation("Operation succeeded");
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Operation failed");
        }
    }
}
```

### Browser Developer Tools

1. Use browser developer tools to:
   * Debug JavaScript interop
   * Monitor network requests
   * Check for errors in the console
   * Profile performance

2. Use the Blazor debugging extension in Chrome for WebAssembly debugging

#### Best Practices for Debugging

1. **Use error boundaries** around complex components
2. **Implement proper logging** with appropriate log levels
3. **Include environment-specific error handling** (detailed in development, friendly in production)
4. **Use browser dev tools** to inspect network traffic and component state

---
<div style="page-break-after:always"></div>

## 🚀 Deployment Options

### Blazor Server Deployment

1. **Host on any ASP.NET Core hosting platform**:
   * Azure App Service
   * IIS on Windows Server
   * Linux with Nginx/Apache
   * Docker containers

2. Example Azure deployment:

```powershell
dotnet publish -c Release
az webapp up --name MyBlazorApp --resource-group MyResourceGroup --plan MyAppServicePlan --sku F1
```

### Blazor WebAssembly Deployment

1. **Static file hosting options**:
   * Azure Static Web Apps
   * Azure Storage with CDN
   * GitHub Pages
   * Netlify, Vercel, etc.

2. Example GitHub Pages deployment:

```yml
# .github/workflows/deploy.yml
name: Deploy to GitHub Pages

on:
  push:
    branches: [ main ]

jobs:
  build:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v2
      - name: Setup .NET
        uses: actions/setup-dotnet@v1
        with:
          dotnet-version: 8.0.x
      - name: Publish
        run: dotnet publish -c Release -o release --nologo
      - name: Deploy to GitHub Pages
        uses: JamesIves/github-pages-deploy-action@4.1.5
        with:
          branch: gh-pages
          folder: release/wwwroot
```

### Continuous Integration/Continuous Deployment (CI/CD)

1. **GitHub Actions** for automated builds and deployments
2. **Azure DevOps Pipelines** for enterprise CI/CD
3. **Docker containers** for consistent deployment environments

#### Best Practices for Deployment

1. **Use environment configurations** for different deployment targets
2. **Implement health checks** to monitor application status
3. **Set up proper logging and monitoring**
4. **Use CI/CD pipelines** for reliable deployments
5. **Consider containerization** for consistent environments

---
<div style="page-break-after:always"></div>

## 🧪 Testing Blazor Applications

### Unit Testing

Test individual components with bUnit:

```csharp
// Install: dotnet add package bunit
using Bunit;
using Xunit;

public class CounterTests
{
    [Fact]
    public void CounterShouldIncrementWhenClicked()
    {
        // Arrange
        using var ctx = new TestContext();
        var cut = ctx.RenderComponent<Counter>();
        var initialCount = cut.Find("p").TextContent;
        
        // Act
        cut.Find("button").Click();
        
        // Assert
        var newCount = cut.Find("p").TextContent;
        Assert.NotEqual(initialCount, newCount);
    }
}
```

### Integration Testing

Test component interactions and navigation:

```csharp
[Fact]
public void NavigationShouldWorkCorrectly()
{
    // Arrange
    using var ctx = new TestContext();
    ctx.Services.AddSingleton<NavigationManager>(new TestNavigationManager());
    var cut = ctx.RenderComponent<NavMenu>();
    
    // Act
    cut.Find("a[href='/counter']").Click();
    
    // Assert
    var navMan = ctx.Services.GetRequiredService<NavigationManager>();
    Assert.EndsWith("/counter", navMan.Uri);
}
```

### End-to-End Testing

Use tools like Playwright or Selenium for full end-to-end tests:

```csharp
// Using Playwright
[Fact]
public async Task CounterShouldIncrementInBrowser()
{
    using var playwright = await Playwright.CreateAsync();
    await using var browser = await playwright.Chromium.LaunchAsync();
    var page = await browser.NewPageAsync();
    
    await page.GotoAsync("https://localhost:5001/counter");
    await page.ClickAsync("button");
    
    var countText = await page.TextContentAsync("p");
    Assert.Contains("Current count: 1", countText);
}
```

#### Best Practices for Testing

1. **Test business logic** separately from UI when possible
2. **Use dependency injection** to mock services
3. **Focus on critical user paths** for end-to-end tests
4. **Implement CI testing** to catch regressions early
5. **Test multiple browsers** for WebAssembly applications

---
<div style="page-break-after:always"></div>

## 🧠 Summary

Blazor represents a powerful paradigm shift in web development, allowing C# developers to create full-stack web applications with a unified language and framework. Whether you choose Blazor Server for its small download size and immediate startup, Blazor WebAssembly for its client-side execution capabilities, or the newer Blazor Web App model for flexibility, Blazor provides a modern approach to building interactive web UIs.

### Learning Path Recommendation

1. **Start with small components**: Build simple interactive components before complex applications
2. **Master the component lifecycle**: Understand when and why components render
3. **Practice data flow patterns**: Learn parameter passing, events, and state management
4. **Build forms with validation**: Create forms with proper validation approaches
5. **Explore advanced scenarios**: Authentication, optimization, and testing

### Common Pitfalls to Avoid

1. **Overusing JavaScript interop** when Blazor provides native solutions
2. **Creating overly complex components** instead of breaking them down
3. **Ignoring component lifecycle methods** for initialization and cleanup
4. **Neglecting proper error handling** in asynchronous operations
5. **Failing to implement proper state management** in larger applications

---
<div style="page-break-after:always"></div>

## 📚 Learning Resources

### Official Documentation

* [Blazor Documentation](https://learn.microsoft.com/en-us/aspnet/core/blazor/)
* [ASP.NET Core Documentation](https://learn.microsoft.com/en-us/aspnet/core/)

### Community Resources

* [Blazor University](https://blazor-university.com) - In-depth tutorials and explanations
* [Awesome Blazor](https://github.com/AdrienTorris/awesome-blazor) - Curated list of Blazor resources
* [Blazor School](https://blazorschool.com) - Tutorials and examples

### Courses and Books

* [Microsoft Learn Blazor Path](https://learn.microsoft.com/en-us/training/paths/build-web-apps-with-blazor/)
* [Pluralsight Blazor Courses](https://www.pluralsight.com/search?q=blazor)
* [Blazor in Action](https://www.manning.com/books/blazor-in-action) (Manning Publications)

### Component Libraries

* [MudBlazor](https://mudblazor.com/) - Material Design components
* [Radzen Blazor Components](https://blazor.radzen.com/) - 70+ UI components
* [Blazorise](https://blazorise.com/) - UI component library supporting multiple CSS frameworks

### Sample Applications

* [eShopOnBlazor](https://github.com/dotnet-architecture/eShopOnBlazor) - Sample e-commerce application
* [Blazor Samples](https://github.com/dotnet/blazor-samples) - Official Microsoft samples
