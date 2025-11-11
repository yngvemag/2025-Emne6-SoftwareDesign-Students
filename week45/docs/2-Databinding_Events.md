# ➕ CounterPlus - Data Binding & Events

## 🎯 What You'll Learn

- One-way vs two-way data binding
- Event handling with `@onclick`
- Method groups vs lambda expressions
- Working with InputNumber components
- Manual state updates with `StateHasChanged()`

## 📖 Concept: Data Binding

### What is Data Binding?

Data binding connects your C# variables to the HTML UI. When data changes, the UI automatically updates to reflect those changes.

**Types of Binding:**

1. **One-way binding** - Data flows from C# to HTML (read-only)
2. **Two-way binding** - Data flows both ways (editable)

### One-Way Binding (Display Only)

```razor
<input class="form-control" value="@Count" readonly />
```

- Shows the current value of `Count`
- User cannot change it (readonly)
- Updates automatically when `Count` changes in code

### Two-Way Binding (Interactive)

```razor
<InputNumber @bind-Value="Count" class="form-control" />
```

- Shows current value AND allows user to edit
- `@bind-Value` creates two-way connection
- When user types, `Count` variable updates immediately

<div style="page-break-after:always;"></div>

## 🔍 Understanding Events

### What are Events?

Events happen when users interact with your UI - clicking buttons, typing in inputs, etc. Blazor lets you respond to these events with C# methods.

### Event Handling Approaches

#### 1. Method Groups (Recommended)

```razor
<button @onclick="Increment">+ @Step</button>
```

```csharp
private void Increment() => Count += Step;
```

**Pros:**

- Clean and readable
- Better performance
- Easy to test

#### 2. Lambda Expressions (For Simple Logic)

```razor
<button @onclick="() => Count += Step">+ (lambda)</button>
```

**Pros:**

- Inline logic
- Can pass parameters
- Good for simple operations

**When to use which?**

- **Method groups**: For complex logic, reusable operations
- **Lambdas**: For simple, one-line operations or when you need to pass parameters

<div style="page-break-after:always;"></div>

## 🎓 Key Learning Points

### 1. **InputNumber Component**

```razor
<InputNumber @bind-Value="Step" class="form-control" />
```

- Built-in Blazor component for numeric input
- Automatically handles type conversion (string ↔ int)
- Validates numeric input
- Updates on `onchange` event (when field loses focus)

### 2. **Event Timing**

- `onchange`: Fires when field loses focus or Enter is pressed
- `oninput`: Fires on every keystroke (harder to implement with type conversion)

### 3. **State Management**

```csharp
private int Count { get; set; } = 0;
private int Step { get; set; } = 1;
```

- Private properties hold component state
- Blazor automatically re-renders when these change
- Use `StateHasChanged()` for manual re-renders if needed

<div style="page-break-after:always;"></div>

## 💡 Interactive Features Explained

### Step Control

```razor
<InputNumber @bind-Value="Step" class="form-control" />
```

- Changes how much the counter increases/decreases
- Two-way bound to `Step` property
- Updates immediately affect button behavior

### Multiple Button Styles

```razor
<!-- Method group -->
<button @onclick="Increment">+ @Step</button>

<!-- Lambda with parameter -->
<button @onclick="() => IncrementBy(Step)">+ (lambda)</button>
```

- Different ways to achieve the same result
- Demonstrates coding flexibility
- Shows performance considerations

<div style="page-break-after:always;"></div>

## 🔄 Component Lifecycle in Action

1. **Component loads** → Initial values set
2. **User interacts** → Events fired
3. **State changes** → UI automatically updates
4. **Blazor re-renders** → New HTML sent to browser

## 🚨 Common Pitfalls

### InputNumber with oninput

```razor
<!-- ❌ This causes type conversion errors -->
<InputNumber @bind-Value="Step" @bind-Value:event="oninput" />

<!-- ✅ Use regular input for immediate updates -->
<input type="number" @bind="Step" @bind:event="oninput" />
```

### Event Handler Mistakes

```razor
<!-- ❌ Wrong - calling method immediately -->
<button @onclick="Increment()">Bad</button>

<!-- ✅ Correct - passing method reference -->
<button @onclick="Increment">Good</button>
```
<div style="page-break-after:always;"></div>

## 💡 Try This

1. **Change the Step**: See how it affects all buttons
2. **Add New Buttons**: Create buttons for multiply/divide
3. **Add Validation**: Prevent negative numbers
4. **Style Changes**: Make completed todos look different

## 🔗 What's Next?

After mastering data binding and events, you'll learn about:

- **Routing** - Navigation and URL parameters
- **Component Parameters** - Passing data between components
- **Component Communication** - Parent-child relationships

## 🚀 Real-World Applications

This pattern is used everywhere:

- **Shopping carts** - Quantity selectors
- **Settings panels** - Configuration options
- **Forms** - User input validation
- **Games** - Score counters, timers

Understanding data binding is crucial for building interactive web applications!
