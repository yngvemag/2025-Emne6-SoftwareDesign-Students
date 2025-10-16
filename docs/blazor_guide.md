# Blazor Guide (Comprehensive and Extended English Edition)

Blazor is a modern web framework from Microsoft that allows developers to build **interactive, full-stack web applications** using C# and .NET instead of JavaScript. It leverages **WebAssembly** and **SignalR** to create highly responsive UIs and enables developers to share code between the client and server.

This expanded guide not only explains *how* to use Blazor but also *why* it works the way it does — giving you a deep, conceptual understanding of its mechanics and design.

---

## 🌐 What is Blazor?

Blazor enables you to use C# instead of JavaScript for building both client and server-side web logic. It unifies the development model and eliminates the need for switching between languages.

### Hosting Models

1. **Blazor Server**
   * Logic runs on the server.
   * UI updates and events are sent via **SignalR** in real time.
   * Minimal download size and very fast initial load.
   * Requires continuous connection to the server.
   * Ideal for internal or enterprise apps with reliable network access.

   🔍 **How it works:**  
   The UI lives in the browser, but the event handling and rendering logic live on the server. The browser connects through a persistent WebSocket channel. When an event occurs (e.g., a button click), it’s sent to the server, which processes it and sends back a DOM diff to update the interface.

2. **Blazor WebAssembly (WASM)**
   * The app runs fully inside the browser.
   * Uses WebAssembly to execute .NET code.
   * Can work offline after initial download.
   * Slightly larger initial payload (5–10 MB), but cached after the first run.
   * Perfect for public-facing or PWA-style applications.

   🔍 **How it works:**  
   The .NET runtime and your app’s assemblies are downloaded into the browser sandbox. Code executes locally in a managed WebAssembly environment, providing near-native performance.

3. **Blazor Web App (introduced in .NET 8)**
   * A hybrid approach combining the advantages of Server and WebAssembly.
   * Allows per-component render modes: static, interactive server, or interactive WebAssembly.
   * Excellent for modern applications requiring flexible rendering and partial offline support.

4. **Blazor Hybrid**
   * Uses Blazor components inside native desktop or mobile apps (e.g., .NET MAUI, WPF, WinForms).
   * Shares logic and UI components between platforms.
   * Can directly call native APIs via .NET.

---

### Choosing the Right Model

| Factor | Blazor Server | Blazor WebAssembly | Blazor Web App |
|--------|----------------|--------------------|----------------|
| Initial Load | Very fast ✅ | Slower ❌ | Fast ✅ |
| Offline Support | No ❌ | Yes ✅ | Configurable ✅ |
| Server Dependency | Continuous ✅ | None ✅ | Configurable ✅ |
| Security-sensitive Code | Hidden ✅ | Exposed ❌ | Configurable ✅ |

**In short:**  
Blazor Server gives you quick startup and easy server-side security but needs constant connectivity.  
Blazor WebAssembly is fully client-side and works offline but loads slower initially.  
Blazor Web App offers a hybrid model that intelligently uses both worlds.

<div style="page-break-after:always;"></div>

## 🧩 Core Concepts and Syntax

### Razor Components

Blazor applications are composed of **Razor components** — reusable building blocks that combine C# and HTML.

* Each component resides in a `.razor` file.
* Supports markup and C# code within the same file.
* Components can be nested, parameterized, and reused.

#### Example

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

🧠 **Explanation:**  
Blazor uses a diffing algorithm to track UI changes. When `IncrementCount()` runs, Blazor determines which parts of the virtual DOM changed and only updates those parts, ensuring high performance.

---

### Data Binding

Data binding connects UI elements with component fields or properties.

#### One-Way Binding

```razor
<p>Hello, @name!</p>

@code {
    private string name = "World";
}
```

🧩 **Mechanics:**  
One-way binding sends data **from C# ➜ UI**. When `name` changes in code, the updated value appears in the UI after a re-render. However, user edits do not affect `name`.

#### Two-Way Binding

```razor
<input @bind="name" @bind:event="oninput" />
<p>Hello, @name!</p>

@code {
    private string name = "World";
}
```

🧩 **Mechanics:**  
Two-way binding connects both directions **C# ⇄ UI**.  
The `@bind` directive pairs the element’s `value` attribute and `onchange` (or `oninput`) event. When the user types, Blazor automatically updates the variable and triggers a UI refresh.

| Type | Direction | Example | Use Case |
|------|------------|----------|-----------|
| One-way | C# ➜ UI | `@name` | Display-only data |
| Two-way | C# ⇄ UI | `@bind="name"` | Interactive forms |

---

### Event Handling

Blazor can handle events directly in C#:

```razor
<button @onclick="HandleClick">Click Me</button>
<button @onclick="() => HandleClickWithParam('Hello')">Click With Param</button>

@code {
    private void HandleClick() => Console.WriteLine("Clicked!");
    private void HandleClickWithParam(string msg) => Console.WriteLine(msg);
}
```

🔍 **How it works:**  
Events are captured by Blazor’s runtime and dispatched to your component logic.  

* In Blazor Server, the event is sent to the server via SignalR.  
* In WebAssembly, the handler runs directly in the browser without any network overhead.

<div style="page-break-after:always;"></div>

### Component Parameters

```razor
<!-- ParentComponent.razor -->
<ChildComponent Title="Product Details" Count="5" />

<!-- ChildComponent.razor -->
<div class="card">
  <h4>@Title</h4>
  <p>Count: @Count</p>
</div>

@code {
  [Parameter]
  public string Title { get; set; } = string.Empty;

  [Parameter]
  public int Count { get; set; }
}
```

🧠 **Explanation:**  
Parameters let data flow **from parent ➜ child**. The `[Parameter]` attribute makes the property bindable. For communication in the opposite direction (child ➜ parent), use `EventCallback<T>`.

<div style="page-break-after:always;"></div>

### Lifecycle Methods

```razor
@implements IDisposable

@code {
    protected override void OnInitialized() => Console.WriteLine("Init");
    protected override async Task OnInitializedAsync() => await LoadDataAsync();
    protected override void OnParametersSet() => Console.WriteLine("Params updated");
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
            await SetupJsInteropAsync();
    }
    public void Dispose() => Console.WriteLine("Disposed");
}
```

| Method | Trigger | Common Use |
|---------|----------|-------------|
| `OnInitialized` | Component creation | Setup local state |
| `OnInitializedAsync` | Async initialization | Load data from APIs |
| `OnParametersSet` | Parent re-renders | React to new parameters |
| `OnAfterRender` | After rendering | DOM/JS initialization |
| `Dispose` | Component removed | Cleanup events/resources |

Understanding these ensures predictable rendering and correct resource management.

<div style="page-break-after:always;"></div>

## 💉 Dependency Injection (DI)

Blazor integrates tightly with .NET’s DI system.

#### Example

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
        <thead><tr><th>Date</th><th>Temp (°C)</th><th>Summary</th></tr></thead>
        <tbody>
        @foreach (var f in forecasts)
        {
            <tr><td>@f.Date</td><td>@f.TemperatureC</td><td>@f.Summary</td></tr>
        }
        </tbody>
    </table>
}

@code {
    private WeatherForecast[] forecasts;
    protected override async Task OnInitializedAsync()
        => forecasts = await WeatherService.GetForecastAsync();
}
```

**Service registration (Program.cs):**

```csharp
builder.Services.AddScoped<IWeatherService, WeatherService>();
```

| Lifetime | Description | Example Use |
|-----------|-------------|--------------|
| Transient | Created each request | Lightweight stateless services |
| Scoped | Shared per connection/session | User data, API calls |
| Singleton | Created once per app | Caching, configuration |

<div style="page-break-after:always;"></div>

## 🔄 State Management

Blazor’s component model keeps local state automatically, but managing shared or global state requires strategy.

### Component State

```razor
@code {
  private int counter;
  private void Increment() => counter++;
}
```

Each component tracks its own fields automatically. When values change, Blazor rerenders only the affected parts.

### Parent-Child Communication

```razor
<!-- Parent -->
<ChildCounter CurrentCount="count" OnCountChanged="UpdateCount" />
@code {
  private int count;
  private void UpdateCount(int val) => count = val;
}

<!-- Child -->
<div>
  <p>@CurrentCount</p>
  <button @onclick="Increment">Increment</button>
</div>
@code {
  [Parameter] public int CurrentCount { get; set; }
  [Parameter] public EventCallback<int> OnCountChanged { get; set; }
  private async Task Increment() => await OnCountChanged.InvokeAsync(CurrentCount + 1);
}
```
<div style="page-break-after:always;"></div>

### Cascading Parameters

Used to share data deep in the hierarchy without prop-drilling.

```razor
<CascadingValue Value="@theme">
  <DeepComponent />
</CascadingValue>
@code { private string theme = "dark"; }
```

```razor
@code {
  [CascadingParameter] public string Theme { get; set; }
}
```

---

### Global Application State

```csharp
public class AppState
{
    public event Action OnChange;
    private int counter;
    public int Counter
    {
        get => counter;
        set { counter = value; OnChange?.Invoke(); }
    }
}
```

Registered as:

```csharp
builder.Services.AddScoped<AppState>();
```

Used in a component:

```razor
@inject AppState State
<h1>@State.Counter</h1>
<button @onclick="() => State.Counter++">Increment</button>
```

<div style="page-break-after:always;"></div>

## ⚙️ Render Modes (.NET 8+)

Render modes define **where** and **how** components execute.

| Mode | Location | Interactivity |
|-------|-----------|---------------|
| Static | Server prerender | No |
| InteractiveServer | Server | Yes |
| InteractiveWebAssembly | Browser | Yes |
| InteractiveAuto | Dynamic | Auto |

**Examples:**

```razor
@rendermode RenderMode.Static
<h1>Static Component</h1>
```

```razor
@rendermode RenderMode.InteractiveWebAssembly
<button @onclick="Handle">Click: @count</button>
```

<div style="page-break-after:always;"></div>

## 🧠 Forms and Validation

Blazor’s form system uses `EditForm` with data annotation validation.

```razor
<EditForm Model="@person" OnValidSubmit="HandleSubmit">
  <DataAnnotationsValidator />
  <ValidationSummary />
  <InputText @bind-Value="person.Name" />
  <InputNumber @bind-Value="person.Age" />
  <button type="submit">Submit</button>
</EditForm>

@code {
  private Person person = new();
  private void HandleSubmit() => Console.WriteLine("Valid form");
  public class Person {
    [Required] public string Name { get; set; }
    [Range(18,100)] public int Age { get; set; }
  }
}
```

Blazor’s `Input*` components automatically handle validation feedback. The `EditContext` tracks field states and validation results.

<div style="page-break-after:always;"></div>

## 📡 JavaScript Interop

Blazor lets you call JS from C# and vice versa.

```razor
@inject IJSRuntime JS
<button @onclick="ShowAlert">Alert</button>
@code {
  private async Task ShowAlert() => await JS.InvokeVoidAsync("alert", "Hello!");
}
```

**JS → C#:**

```javascript
window.callDotNet = (ref) => ref.invokeMethodAsync('ReceiveMsg', 'Hi from JS');
```

```csharp
[JSInvokable] public void ReceiveMsg(string msg) => Console.WriteLine(msg);
```

🧠 **Best practices:**

* Keep JS minimal.
* Use JS isolation modules (`.js` files imported per component).
* Always dispose `DotNetObjectReference` to avoid leaks.

<div style="page-break-after:always;"></div>

## 🔒 Authentication and Authorization

Blazor supports both server-based and token-based authentication.

```razor
@attribute [Authorize]
<AuthorizeView>
  <Authorized>
    <p>Hello @context.User.Identity.Name!</p>
  </Authorized>
  <NotAuthorized>
    <p>You are not authorized.</p>
  </NotAuthorized>
</AuthorizeView>
```

Roles and policies can also be used:

```razor
@attribute [Authorize(Roles = "Admin")]
```

<div style="page-break-after:always;"></div>

## ⚡ Performance Optimization

1. Use `@key` when rendering lists to track items efficiently.
2. Implement `ShouldRender()` to skip unnecessary renders.
3. Use `Virtualize` for large data lists.
4. Use prerendering for faster perceived load.

Example:

```razor
<Virtualize Items="@items" Context="item">
  <p>@item.Text</p>
</Virtualize>
```

<div style="page-break-after:always;"></div>

## 🧪 Testing Blazor

**Unit Tests with bUnit:**

```csharp
using Bunit;
using Xunit;
public class CounterTest
{
  [Fact]
  public void Increment_Works()
  {
    using var ctx = new TestContext();
    var cut = ctx.RenderComponent<Counter>();
    cut.Find("button").Click();
    cut.Markup.Contains("1");
  }
}
```

**E2E with Playwright:**

```csharp
await page.GotoAsync("/counter");
await page.ClickAsync("button");
await Expect(page.Locator("p")).ToContainTextAsync("1");
```

---

## 🧭 Summary

Blazor represents a unification of client and server development with C#.  
It bridges gaps between backend and frontend, offering:

* Reusable components
* Strong typing and safety
* Minimal JavaScript
* Full .NET ecosystem access

### Key Takeaways

* Start small: build a simple component.
* Understand the component lifecycle.
* Learn data binding and event handling deeply.
* Use DI and state management responsibly.
* Test your UI logic with bUnit or Playwright.

