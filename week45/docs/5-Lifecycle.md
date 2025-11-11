# 🔄 Lifecycle Lab - Component Lifecycle Methods

## 🎯 What You'll Learn

- Understanding component lifecycle phases
- Order of lifecycle method execution
- When to use each lifecycle method
- Async vs sync lifecycle methods
- `ShouldRender()` optimization
- Manual state updates with `StateHasChanged()`

## 📖 Concept: Component Lifecycle

### What is Component Lifecycle?

Component lifecycle is the series of phases a component goes through from creation to destruction. Think of it like a person's life stages: birth, growth, changes, and eventually death.

**Why is this important?**

- Know when to load data
- Understand performance implications  
- Avoid memory leaks
- Optimize rendering

### The Lifecycle Phases

```
Creation → Initialization → Parameter Updates → Rendering → Disposal
```
Each phase has specific methods you can override to add custom behavior.

![1762357168863](image/README/1762357168863.png)

<div style="page-break-after:always;"></div>

## 🏗️ Lifecycle Methods in Order

### 1. Constructor

```csharp
public LifeCycle()
{
    // ❌ DON'T do async operations here
    // ✅ DO basic initialization only
}
```

**When:** Component instance is created  
**Use for:** Basic field initialization  
**Avoid:** HTTP calls, database access, any async work

### 2. OnInitialized()

```csharp
protected override void OnInitialized()
{
    // ✅ Component is created, but parameters not set yet
    LogEvent("Component created");
}
```

**When:** Once, after component is constructed  
**Use for:** Setup that doesn't depend on parameters  
**Note:** Parameters are not available yet!

### 3. OnInitializedAsync()

```csharp
protected override async Task OnInitializedAsync()
{
    // ✅ Good place for initial data loading
    userData = await UserService.GetCurrentUser();
}
```

**When:** Once, after OnInitialized()  
**Use for:** Initial async operations, data loading  
**Best for:** API calls that don't depend on parameters

<div style="page-break-after:always;"></div>

### 4. OnParametersSet()

```csharp
protected override void OnParametersSet()
{
    // ✅ Parameters are now available
    if (UserId != previousUserId)
    {
        // Parameter changed, react accordingly
    }
}
```

**When:** After parameters are set (every time they change)  
**Use for:** Reacting to parameter changes  
**Note:** Called after every parameter update!

### 5. OnParametersSetAsync()

```csharp
protected override async Task OnParametersSetAsync()
{
    // ✅ Load data based on new parameters
    if (ProductId != 0)
    {
        product = await ProductService.GetProduct(ProductId);
    }
}
```

**When:** After OnParametersSet()  
**Use for:** Async operations based on parameters  
**Best for:** Loading data when parameters change

### 6. ShouldRender()

```csharp
protected override bool ShouldRender()
{
    // ✅ Return false to skip rendering for performance
    return hasDataChanged;
}
```

**When:** Before every render attempt  
**Use for:** Performance optimization  
**Return:** `true` to render, `false` to skip

<div style="page-break-after:always;"></div>

### 7. OnAfterRender(bool firstRender)

```csharp
protected override void OnAfterRender(bool firstRender)
{
    if (firstRender)
    {
        // ✅ DOM is ready, safe for JavaScript interop
        SetupJavaScript();
    }
}
```

**When:** After component renders to DOM  
**Use for:** JavaScript interop, DOM manipulation  
**firstRender:** `true` only on first render

### 8. OnAfterRenderAsync(bool firstRender)

```csharp
protected override async Task OnAfterRenderAsync(bool firstRender)
{
    if (firstRender)
    {
        // ✅ Async work after DOM is ready
        await JSRuntime.InvokeVoidAsync("initializeChart");
    }
}
```

**When:** After OnAfterRender()  
**Use for:** Async work after rendering  
**Common:** JavaScript library initialization

<div style="page-break-after:always;"></div>

## 🎓 Key Learning Points

### 1. **Sync vs Async Methods**

**Rule:** Always prefer async versions for I/O operations

```csharp
// ❌ BAD: Blocking the UI thread
protected override void OnInitialized()
{
    // This blocks the UI!
    data = HttpClient.GetStringAsync("/api/data").Result;
}

// ✅ GOOD: Non-blocking
protected override async Task OnInitializedAsync()
{
    // This doesn't block the UI
    data = await HttpClient.GetStringAsync("/api/data");
}
```

### 2. **Parameter-Dependent Loading**

```csharp
protected override async Task OnParametersSetAsync()
{
    // Only load when parameter actually changes
    if (ProductId != previousProductId)
    {
        product = await LoadProduct(ProductId);
        previousProductId = ProductId;
    }
}
```

### 3. **ShouldRender Optimization**

```csharp
protected override bool ShouldRender()
{
    // Skip expensive re-renders when data hasn't changed
    return dataHasChanged;
}

private void UpdateData()
{
    data = newData;
    dataHasChanged = true;
    StateHasChanged(); // Force render
}
```

## 🚨 Common Pitfalls

### 1. **I/O in Constructor**

```csharp
// ❌ BAD: Never do this
public MyComponent()
{
    data = httpClient.GetAsync("/api/data").Result; // Blocks UI!
}

// ✅ GOOD: Use lifecycle methods
protected override async Task OnInitializedAsync()
{
    data = await httpClient.GetStringAsync("/api/data");
}
```

### 2. **Forgetting Parameter Dependencies**

```csharp
// ❌ BAD: Only loads once
protected override async Task OnInitializedAsync()
{
    product = await LoadProduct(ProductId); // What if ProductId changes?
}

// ✅ GOOD: Reloads when parameter changes
protected override async Task OnParametersSetAsync()
{
    product = await LoadProduct(ProductId);
}
```

### 3. **Infinite Render Loops**

```csharp
// ❌ BAD: Creates infinite loop
protected override void OnAfterRender(bool firstRender)
{
    StateHasChanged(); // This triggers another render!
}

// ✅ GOOD: Conditional updates
protected override void OnAfterRender(bool firstRender)
{
    if (firstRender && needsSetup)
    {
        SetupComponent();
        needsSetup = false; // Prevent future calls
    }
}
```

## 🔍 Practical Examples from the Lab

### Parameter Change Simulation

The lab has a dropdown that changes parameters, triggering:

1. `OnParametersSet()`
2. `OnParametersSetAsync()` with simulated data loading
3. Re-render with new data

### ShouldRender Testing

Toggle the "Allow Render" checkbox to see:

- When `ShouldRender()` returns `false`
- How many render attempts are made
- Performance impact of skipping renders

### Manual StateHasChanged()

Button that calls `StateHasChanged()` to show:

- When you need to force re-renders
- How it fits into the lifecycle
- Performance implications

## 💡 Try This

1. **Change Parameters**: Watch the lifecycle methods execute in order
2. **Disable Rendering**: See how `ShouldRender()` can optimize performance  
3. **Force Re-renders**: Use `StateHasChanged()` and observe the effects
4. **Read the Log**: Every lifecycle method is logged with timestamps

## 🔗 Real-World Applications

### Data Loading Patterns

```csharp
// User profile component
protected override async Task OnParametersSetAsync()
{
    if (UserId != previousUserId)
    {
        isLoading = true;
        StateHasChanged(); // Show loading spinner
        
        userProfile = await UserService.GetProfile(UserId);
        
        isLoading = false;
        previousUserId = UserId;
    }
}
```

### Performance Optimization

```csharp
// Expensive chart component
protected override bool ShouldRender()
{
    // Only re-render if data actually changed
    return dataVersion != lastRenderedVersion;
}
```

### JavaScript Interop

```csharp
// Map component
protected override async Task OnAfterRenderAsync(bool firstRender)
{
    if (firstRender)
    {
        await JSRuntime.InvokeVoidAsync("initializeMap", mapContainer);
    }
}
```

## 🚀 What's Next?

After understanding component lifecycle, you'll learn about:

- **State Management** - Managing complex application state
- **Services** - Sharing data and logic across components
- **Advanced Rendering** - Optimizing performance at scale

Understanding the component lifecycle is crucial for building performant, reliable Blazor applications!
