# 🎯 Blazor Event Handlers - StateHasChanged Oversikt

## 📋 Regel for StateHasChanged

**Hovedregel:** Blazor kaller `StateHasChanged()` automatisk etter **synkrone** event handlers, men **IKKE** etter **asynkrone** event handlers.

---

## 🔄 Automatisk vs Manuell StateHasChanged

### ✅ **Automatisk StateHasChanged (Ingen ekstra kode nødvendig)**

```csharp
// ✅ Synkron onclick - StateHasChanged kalles automatisk
<button @onclick="IncrementCounter">Klikk meg</button>

@code {
    private int counter = 0;
    
    private void IncrementCounter() // Synkron metode
    {
        counter++;
        // StateHasChanged kalles automatisk!
    }
}
```

### ⚠️ **Manuell StateHasChanged (Må kalles eksplisitt)**

```csharp
// ⚠️ Asynkron onclick - MÅ kalle StateHasChanged manuelt
<button @onclick="IncrementCounterAsync">Klikk meg</button>

@code {
    private int counter = 0;
    
    private async Task IncrementCounterAsync() // Asynkron metode
    {
        counter++;
        await InvokeAsync(StateHasChanged); // PÅKREVD!
    }
}
```

<div style="page-break-after:always;"></div>

## 📊 Komplett Event Handler Oversikt

### 🖱️ **Mouse Events**

| Event | Synkron | Asynkron | StateHasChanged |
|-------|---------|----------|-----------------|
| `@onclick` | ✅ Auto | ⚠️ Manuell | `void` = Auto, `Task` = Manuell |
| `@ondblclick` | ✅ Auto | ⚠️ Manuell | `void` = Auto, `Task` = Manuell |
| `@onmousedown` | ✅ Auto | ⚠️ Manuell | `void` = Auto, `Task` = Manuell |
| `@onmouseup` | ✅ Auto | ⚠️ Manuell | `void` = Auto, `Task` = Manuell |
| `@onmouseover` | ✅ Auto | ⚠️ Manuell | `void` = Auto, `Task` = Manuell |
| `@onmouseout` | ✅ Auto | ⚠️ Manuell | `void` = Auto, `Task` = Manuell |

### ⌨️ **Keyboard Events**

| Event | Synkron | Asynkron | StateHasChanged |
|-------|---------|----------|-----------------|
| `@onkeydown` | ✅ Auto | ⚠️ Manuell | `void` = Auto, `Task` = Manuell |
| `@onkeyup` | ✅ Auto | ⚠️ Manuell | `void` = Auto, `Task` = Manuell |
| `@onkeypress` | ✅ Auto | ⚠️ Manuell | `void` = Auto, `Task` = Manuell |

### 📝 **Input Events (Alltid Manuell!)**

| Event | Synkron | Asynkron | StateHasChanged |
|-------|---------|----------|-----------------|
| `@oninput` | ⚠️ **Alltid Manuell** | ⚠️ **Alltid Manuell** | **ALLTID PÅKREVD** |
| `@onchange` | ⚠️ **Alltid Manuell** | ⚠️ **Alltid Manuell** | **ALLTID PÅKREVD** |

### 🎯 **Focus Events**

| Event | Synkron | Asynkron | StateHasChanged |
|-------|---------|----------|-----------------|
| `@onfocus` | ✅ Auto | ⚠️ Manuell | `void` = Auto, `Task` = Manuell |
| `@onblur` | ✅ Auto | ⚠️ Manuell | `void` = Auto, `Task` = Manuell |
| `@onfocusin` | ✅ Auto | ⚠️ Manuell | `void` = Auto, `Task` = Manuell |
| `@onfocusout` | ✅ Auto | ⚠️ Manuell | `void` = Auto, `Task` = Manuell |

<div style="page-break-after:always;"></div>

### 📋 **Form Events**

| Event | Synkron | Asynkron | StateHasChanged |
|-------|---------|----------|-----------------|
| `@onsubmit` | ✅ Auto | ⚠️ Manuell | `void` = Auto, `Task` = Manuell |
| `@onreset` | ✅ Auto | ⚠️ Manuell | `void` = Auto, `Task` = Manuell |

<div style="page-break-after:always;"></div>

## 🧩 Kodeeksempler for Hver Kategori

### 🖱️ **Mouse Events Eksempler**

```csharp
<!-- ✅ Automatisk StateHasChanged -->
<button @onclick="SyncClick">Synkron Click</button>
<button @ondblclick="SyncDoubleClick">Synkron Double Click</button>

<!-- ⚠️ Manuell StateHasChanged -->
<button @onclick="AsyncClick">Asynkron Click</button>
<button @ondblclick="AsyncDoubleClick">Asynkron Double Click</button>

@code {
    private int clickCount = 0;
    
    // ✅ Synkron - StateHasChanged automatisk
    private void SyncClick()
    {
        clickCount++;
        // StateHasChanged kalles automatisk
    }
    
    // ✅ Synkron - StateHasChanged automatisk
    private void SyncDoubleClick()
    {
        clickCount += 2;
        // StateHasChanged kalles automatisk
    }
    
    // ⚠️ Asynkron - MÅ kalle StateHasChanged
    private async Task AsyncClick()
    {
        clickCount++;
        await InvokeAsync(StateHasChanged); // PÅKREVD!
    }
    
    // ⚠️ Asynkron - MÅ kalle StateHasChanged
    private async Task AsyncDoubleClick()
    {
        clickCount += 2;
        await InvokeAsync(StateHasChanged); // PÅKREVD!
    }
}
```
<div style="page-break-after:always;"></div>

### 📝 **Input Events Eksempler (Alltid Manuell)**

```csharp
<!-- ⚠️ ALLTID manuell StateHasChanged for input events -->
<input @oninput="OnInputChanged" value="@inputValue" />
<input @onchange="OnInputChanged" value="@inputValue" />
<select @onchange="OnSelectChanged">
    <option value="1">Option 1</option>
    <option value="2">Option 2</option>
</select>

@code {
    private string inputValue = "";
    private string selectedValue = "";
    
    // ⚠️ Synkron input - MÅ kalle StateHasChanged
    private void OnInputChanged(ChangeEventArgs e)
    {
        inputValue = e.Value?.ToString() ?? "";
        StateHasChanged(); // PÅKREVD selv om synkron!
    }
    
    // ⚠️ Asynkron input - MÅ kalle StateHasChanged
    private async Task OnSelectChanged(ChangeEventArgs e)
    {
        selectedValue = e.Value?.ToString() ?? "";
        await InvokeAsync(StateHasChanged); // PÅKREVD!
    }
}
```
<div style="page-break-after:always;"></div>

### ⌨️ **Keyboard Events Eksempler**

```csharp
<!-- ✅ Synkron keyboard events - automatisk StateHasChanged -->
<input @onkeydown="SyncKeyDown" />
<input @onkeyup="SyncKeyUp" />

<!-- ⚠️ Asynkron keyboard events - manuell StateHasChanged -->
<input @onkeydown="AsyncKeyDown" />
<input @onkeyup="AsyncKeyUp" />

@code {
    private string lastKey = "";
    
    // ✅ Synkron - StateHasChanged automatisk
    private void SyncKeyDown(KeyboardEventArgs e)
    {
        lastKey = e.Key;
        // StateHasChanged kalles automatisk
    }
    
    // ✅ Synkron - StateHasChanged automatisk
    private void SyncKeyUp(KeyboardEventArgs e)
    {
        lastKey = $"Released: {e.Key}";
        // StateHasChanged kalles automatisk
    }
    
    // ⚠️ Asynkron - MÅ kalle StateHasChanged
    private async Task AsyncKeyDown(KeyboardEventArgs e)
    {
        lastKey = e.Key;
        await InvokeAsync(StateHasChanged); // PÅKREVD!
    }
    
    // ⚠️ Asynkron - MÅ kalle StateHasChanged
    private async Task AsyncKeyUp(KeyboardEventArgs e)
    {
        lastKey = $"Released: {e.Key}";
        await InvokeAsync(StateHasChanged); // PÅKREVD!
    }
}
```

<div style="page-break-after:always;"></div>

## 🆚 Sammenligning: @bind vs Manual Event Handling

### ✅ **@bind (Helt Automatisk)**

```csharp
<!-- ✅ Helt automatisk - ingen StateHasChanged nødvendig -->
<input @bind="automaticValue" />
<input @bind="automaticValue" @bind:event="oninput" />

@code {
    private string automaticValue = "";
    // @bind håndterer alt automatisk!
}
```

### ⚠️ **Manual Event Handling (Krever StateHasChanged)**

```csharp
<!-- ⚠️ Manuell - MÅ kalle StateHasChanged -->
<input @oninput="OnManualInput" value="@manualValue" />

@code {
    private string manualValue = "";
    
    private async Task OnManualInput(ChangeEventArgs e)
    {
        manualValue = e.Value?.ToString() ?? "";
        await InvokeAsync(StateHasChanged); // PÅKREVD!
    }
}
```

<div style="page-break-after:always;"></div>

## 🔍 Spesielle Tilfeller

### 🎛️ **EventCallback (Komponent-til-komponent)**

```csharp
<!-- Parent komponent -->
<ChildComponent OnValueChanged="HandleValueChanged" />

@code {
    // ✅ EventCallback håndterer StateHasChanged automatisk
    private void HandleValueChanged(string newValue)
    {
        // Ingen StateHasChanged nødvendig
        parentValue = newValue;
    }
    
    // ⚠️ Asynkron EventCallback - må kalle StateHasChanged
    private async Task HandleValueChangedAsync(string newValue)
    {
        parentValue = newValue;
        await InvokeAsync(StateHasChanged); // PÅKREVD!
    }
}

<!-- Child komponent -->
<input @oninput="OnInput" />

@code {
    [Parameter] public EventCallback<string> OnValueChanged { get; set; }
    
    private async Task OnInput(ChangeEventArgs e)
    {
        var value = e.Value?.ToString() ?? "";
        await OnValueChanged.InvokeAsync(value); // Automatisk StateHasChanged i parent
        await InvokeAsync(StateHasChanged); // Påkrevd for denne komponenten
    }
}
```
<div style="page-break-after:always;"></div>

### ⏰ **Timer og Background Tasks**

```csharp
@code {
    private Timer? timer;
    private int seconds = 0;
    
    protected override void OnInitialized()
    {
        timer = new Timer(OnTimerElapsed, null, 0, 1000);
    }
    
    // ⚠️ Timer callback - MÅ kalle StateHasChanged
    private async void OnTimerElapsed(object? state)
    {
        seconds++;
        await InvokeAsync(StateHasChanged); // PÅKREVD!
    }
}
```
<div style="page-break-after:always;"></div>

## 📚 Sammendrag og Huskeliste

### 🎯 **Når StateHasChanged kalles AUTOMATISK:**

1. ✅ **Synkrone event handlers** (`void` metoder)
2. ✅ **@bind** direktiver
3. ✅ **EventCallback** (i mottaker-komponenten)

### ⚠️ **Når StateHasChanged MÅ kalles MANUELT:**

1. ⚠️ **Asynkrone event handlers** (`Task` metoder)
2. ⚠️ **@oninput og @onchange** (alltid, selv synkrone)
3. ⚠️ **Timer callbacks**
4. ⚠️ **Background tasks**
5. ⚠️ **HttpClient calls**
6. ⚠️ **Database operations**

### 💡 **Huskeregel:**

```csharp
// ✅ AUTOMATISK: void + ikke input event
private void OnClick() { }

// ⚠️ MANUELL: async Task ELLER input event
private async Task OnClickAsync() { }
private void OnInput(ChangeEventArgs e) { }
private async Task OnInputAsync(ChangeEventArgs e) { }
```

### 🔧 **Best Practice:**

```csharp
// Bruk alltid InvokeAsync for thread-safety
await InvokeAsync(StateHasChanged);

// Ikke bare:
StateHasChanged(); // Kan være farlig i async kontekst
```

---

*Denne oversikten hjelper deg å identifisere når StateHasChanged er nødvendig i Blazor-applikasjoner!* 🚀