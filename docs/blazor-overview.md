# Overview of ASP.NET Blazor

## 🧭 What is Blazor?

**Blazor** is a framework for building **interactive web UIs** using **C# instead of JavaScript**. It's part of ASP.NET Core and allows developers to build **rich client-side web apps** using .NET.

---

## 🚀 Blazor Hosting Models

### 1. Blazor WebAssembly (WASM)

- Runs **C# code directly in the browser** via WebAssembly.
- Static files (DLLs, .NET runtime) are downloaded to the client.
- No server dependency after loading.
- Suitable for offline or client-heavy apps.

### 2. Blazor Server

- Runs on the **server**.
- Uses **SignalR** to maintain a real-time connection between client and server.
- Very thin client; fast startup, but requires constant connection.

### 3. Blazor Hybrid

- Uses Blazor in **desktop apps** via .NET MAUI or WPF.
- UI rendering in a WebView, logic written in C#.

<div style="page-break-after:always;"></div>

## 🧩 Key Concepts and Components

### 1. Razor Components (.razor files)

- The building blocks of Blazor apps.
- Contain **C# code + HTML markup**.
- Can be nested and reused.

```razor
<h3>Hello, @name!</h3>
@code {
    private string name = "Blazor";
}
```

### 2. Routing

- Blazor uses the `@page` directive to define routes:

```razor
@page "/home"
```

### 3. Data Binding

- **One-way binding**: `@variable`
- **Two-way binding**: `bind-` syntax

```razor
<input @bind="name" />
```

### 4. Event Handling

- Blazor handles events with C# methods:

```razor
<button @onclick="HandleClick">Click me</button>

@code {
    void HandleClick() => Console.WriteLine("Clicked!");
}
```

### 5. Dependency Injection

- Built-in support for DI:

```razor
@inject WeatherService Weather
```

### 6. Component Parameters

- Components can accept parameters via `[Parameter]`:

```razor
<Greeting Name="Yngve" />

// Greeting.razor
@code {
    [Parameter] public string Name { get; set; }
}
```

### 7. Lifecycle Methods

- Similar to component lifecycles in React/Angular:
  - `OnInitializedAsync()`
  - `OnParametersSetAsync()`
  - `ShouldRender()`
  - `Dispose()`

### 8. JavaScript Interop

- Call JS from C# and vice versa:

```csharp
await JS.InvokeVoidAsync("alert", "Hello from C#!");
```

<div style="page-break-after:always;"></div>

## 🧱 Project Structure

- `App.razor`: The root component, usually contains the router.
- `MainLayout.razor`: Defines the layout for pages.
- `Pages/`: Contains `.razor` files for each route.
- `Shared/`: Reusable components (e.g., `NavMenu`, `Header`).
- `Program.cs`: Entry point for configuring services and app behavior.

---

## 🔐 Authentication & Authorization

- Blazor supports authentication via:
  - ASP.NET Core Identity
  - OAuth / OpenID Connect
- Use `[Authorize]` attribute on components.
- Access user info via `AuthenticationStateProvider`.

---

## 🔧 Tools & Ecosystem

- Supported in Visual Studio, VS Code, and JetBrains Rider.
- Integrates with .NET backend APIs (Web API, SignalR).
- Can be deployed to static hosts (WASM) or traditional ASP.NET hosts (Blazor Server).

---

## ✅ When to Use Blazor

| Use Case                        | Recommended Model    |
|--------------------------------|----------------------|
| High interactivity, offline    | Blazor WebAssembly   |
| Fast load, real-time sync      | Blazor Server        |
| Desktop + Web combined         | Blazor Hybrid (MAUI) |
