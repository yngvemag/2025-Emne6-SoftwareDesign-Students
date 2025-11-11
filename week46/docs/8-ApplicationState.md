# 🏪 Application State with Services (DI) Lab

## 🎯 Mål

Forstå hvordan **Dependency Injection** fungerer i Blazor og hvordan tjenester kan brukes til å dele state mellom komponenter.

## 📚 Konsepter som dekkes

### 🔧 Dependency Injection (DI)

- **Hva er DI?** Et design pattern som lar oss injisere avhengigheter i komponenter i stedet for å opprette dem manuelt
- **Hvorfor DI?** Løsere kobling, bedre testbarhet, og delt state management
- **Blazor DI:** Innebygd støtte gjennom `@inject` direktiv

### ⏰ Service Lifetimes

#### 🔵 Singleton

```csharp
builder.Services.AddSingleton<MyService>();
```

- **Levetid:** Hele applikasjonens levetid
- **Bruksområde:** Global konfigurasjon, caching, logging
- **Risiko:** Memory leaks, concurrency issues i web apps
- **Eksempel:** Configuration services, global counters

#### 🟢 Scoped (Anbefalt for web apps)

```csharp
builder.Services.AddScoped<MyService>();
```

- **Levetid:** Per HTTP request / Blazor circuit
- **Bruksområde:** User session state, shopping carts, user preferences
- **Fordeler:** Isolert per bruker, automatisk cleanup
- **Eksempel:** Shopping cart, user authentication state

#### 🟡 Transient

```csharp
builder.Services.AddTransient<MyService>();
```

- **Levetid:** Ny instans hver gang den injiseres
- **Bruksområde:** Stateless utilities, lightweight services
- **Performance:** Overhead ved mange opprettelser
- **Eksempel:** HTTP clients, validators, mappers

## 🛍️ Shopping Cart Eksempel

### Arkitektur

```
┌─────────────────────────────────────────┐
│           CartService (Scoped)          │
│  ┌─────────────────────────────────────┐│
│  │ - Items: List<CartItem>             ││
│  │ - OnChange: Action?                 ││
│  │ + AddToCart()                       ││
│  │ + RemoveFromCart()                  ││
│  │ + ClearCart()                       ││
│  └─────────────────────────────────────┘│
└─────────────────────────────────────────┘
           ↑                    ↑
    @inject │            @inject │
           │                    │
┌──────────────────┐   ┌─────────────────┐
│  ProductList     │   │    MiniCart     │
│  Component       │   │   Component     │
└──────────────────┘   └─────────────────┘
```

### Event-drevet oppdatering

1. **ProductList** legger til produkt → CartService.AddToCart()
2. **CartService** trigger OnChange event
3. **MiniCart** mottar event → StateHasChanged()
4. **UI oppdateres automatisk** 🎉

<div style="page-break-after:always;"></div>

## 🔍 Implementasjonsdetaljer

### CartService Registration

```csharp
// Program.cs
builder.Services.AddScoped<CartService>();
```

### Component Injection

```csharp
@inject CartService CartService
@implements IDisposable

@code {
    protected override void OnInitialized()
    {
        // Subscribe to service events
        CartService.OnChange += OnCartChanged;
    }
    
    private void OnCartChanged()
    {
        InvokeAsync(StateHasChanged);
    }
    
    public void Dispose()
    {
        // Important: Unsubscribe to prevent memory leaks
        CartService.OnChange -= OnCartChanged;
    }
}
```

### Event Pattern

```csharp
public class CartService
{
    public Action? OnChange { get; set; }
    
    private void NotifyStateChanged()
    {
        OnChange?.Invoke();
    }
}
```
<div style="page-break-after:always;"></div>

## 🧪 Øvelser

### 1. Grunnleggende oppgave

- ✅ Test å legge til produkter i ProductList
- ✅ Observer at MiniCart oppdateres automatisk
- ✅ Sjekk service hash-verdien i debug info

### 2. Service Lifetime eksperiment

```csharp
// Endre fra Scoped til Singleton i Program.cs
builder.Services.AddSingleton<CartService>();
```

**Resultat:** Samme cart deles mellom alle brukere/tabs!

### 3. Transient eksperiment

```csharp
// Endre til Transient
builder.Services.AddTransient<CartService>();
```

**Resultat:** Hver komponent får sin egen cart-instans!

### 4. Memory leak test

- Fjern `Dispose()` implementasjonen
- Naviger bort og tilbake til siden flere ganger
- Observer potensielle memory leaks

## 🎓 Læringsmål

### Etter denne øvelsen skal du forstå

1. **Forskjell på service lifetimes** og når du skal bruke hver av dem
2. **Hvordan injisere tjenester** i Blazor komponenter
3. **Event-drevet state management** mellom komponenter
4. **Viktigheten av Dispose pattern** for å unngå memory leaks
5. **Hvorfor Scoped er perfekt** for web applications

## 🔗 Relaterte konsepter

- **Component Communication:** Hvordan komponenter kan kommunisere uten prop drilling
- **State Management:** Alternativer til global state (Redux-style)
- **Memory Management:** Dispose pattern og event unsubscription
- **Service Design:** Hvordan designe gode, gjenbrukbare tjenester

## 📖 Videre lesning

- [ASP.NET Core Dependency Injection](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection)
- [Blazor Dependency Injection](https://docs.microsoft.com/en-us/aspnet/core/blazor/fundamentals/dependency-injection)
- [Service Lifetimes](https://docs.microsoft.com/en-us/dotnet/core/extensions/dependency-injection#service-lifetimes)
