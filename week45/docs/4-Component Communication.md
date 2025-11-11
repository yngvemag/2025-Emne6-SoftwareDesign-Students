# 👨‍👧‍👦 Parent-Child Lab - Component Communication

> Also read: [Blazor University - Components Events](https://blazor-university.com/components/component-events/)

## 🎯 What You'll Learn

- Parent-to-child communication via Parameters
- Child-to-parent communication via EventCallback
- Component parameter attributes and validation
- "Lifting state up" pattern
- EventCallback vs regular events
- Component isolation and responsibility

## 📖 Concept: Component Communication

### Why Do Components Need to Communicate?

In real applications, components need to work together. Think of a shopping cart:

- **Product component** shows item details
- **Quantity component** lets user change amount
- **Cart component** shows total items and price

They all need to share and update the same data!

### The Blazor Communication Model

```
    Parent Component (Owns State)
           ↕ Parameters & EventCallbacks
    Child Component (Receives & Reports)
```

**Golden Rule**: State flows DOWN, events flow UP.

<div style="page-break-after:always;"></div>

## 🔄 Parent → Child: Parameters

### What are Parameters?

Parameters are how parent components pass data to child components.

```csharp
// Child component
[Parameter, EditorRequired] 
public int Value { get; set; }

[Parameter] 
public string ProductName { get; set; } = "";
```

```razor
<!-- Parent component usage -->
<Rating Value="@product.Rating" 
        ProductName="@product.Name" />
```

### Parameter Attributes

#### `[Parameter]`

- Marks property as receivable from parent
- Required for all component parameters

#### `[EditorRequired]`

- Forces parent to provide this parameter
- IDE will warn if missing
- Use for essential data

#### Default Values

```csharp
[Parameter] public string Color { get; set; } = "blue";
```
<div style="page-break-after:always;"></div>

## 📤 Child → Parent: EventCallback

### What is EventCallback?

EventCallback is Blazor's way for child components to notify parents when something happens.

```csharp
// Child component
[Parameter] public EventCallback<int> ValueChanged { get; set; }

// When user clicks star
await ValueChanged.InvokeAsync(newRating);
```

```csharp
// Parent component
<Rating ValueChanged="@(value => UpdateRating(product, value))" />

private void UpdateRating(Product product, int newRating)
{
    product.Rating = newRating; // Update parent's state
}
```

### EventCallback Types

#### With Data: `EventCallback<T>`

```csharp
[Parameter] public EventCallback<int> ValueChanged { get; set; }

// Usage: Send data to parent
await ValueChanged.InvokeAsync(newValue);
```

#### Without Data: `EventCallback`

```csharp
[Parameter] public EventCallback OnDelete { get; set; }

// Usage: Just notify parent
await OnDelete.InvokeAsync();
```
<div style="page-break-after:always;"></div>

## 🏗️ Architecture Pattern: "Lifting State Up"

### The Problem

```csharp
// ❌ BAD: Each component has its own state
Component A: rating = 3
Component B: rating = 4  // Out of sync!
Component C: rating = 5  // Different values!
```

### The Solution

```csharp
// ✅ GOOD: Parent owns single source of truth
Parent: product.Rating = 4
   ↓ Pass down via parameters
Child A: displays 4
Child B: displays 4
Child C: displays 4
```

**Benefits:**

- Single source of truth
- No synchronization problems
- Easier debugging and testing
- Clear data ownership

<div style="page-break-after:always;"></div>

## 🎓 Key Learning Points

### 1. **State Ownership Rules**

```csharp
// Parent owns business state
private List<Product> products = new();

// Child only owns UI state (temporary)
private string lastAction = "";  // Just for display
```

**Guidelines:**

- Parent owns data that multiple components need
- Child owns only temporary UI state
- Never mutate parent data directly in child

### 2. **Parameter Validation**

```csharp
[Parameter, EditorRequired] 
public EventCallback<int> ValueChanged { get; set; }

protected override void OnParametersSet()
{
    // Validate required parameters
    if (ValueChanged.HasDelegate == false)
        throw new ArgumentException("ValueChanged is required");
}
```

### 3. **Event Flow Pattern**

```
1. User clicks star in Rating component
2. Rating.OnStarClick() method runs
3. Child calls: await ValueChanged.InvokeAsync(rating)
4. Parent's UpdateRating() method runs
5. Parent updates its state
6. Blazor re-renders both parent and child
7. Child displays new rating from parent
```
<div style="page-break-after:always;"></div>

## 🔍 Practical Examples from the Lab

### Rating Component (Child)

**Responsibilities:**

- Display current rating as stars
- Handle user clicks
- Report changes to parent
- Show debug information

**Does NOT:**

- Store the actual rating value
- Decide business logic
- Manage product data

### ParentChildLab Component (Parent)

**Responsibilities:**

- Own all product data
- Handle rating updates
- Manage product list
- Log all events
- Coordinate between components

<div style="page-break-after:always;"></div>

## 🚨 Common Pitfalls

### 1. **Direct State Mutation**

```csharp
// ❌ BAD: Child modifying parent data directly
[Parameter] public Product Product { get; set; }

private void OnStarClick(int rating)
{
    Product.Rating = rating; // Don't do this!
}

// ✅ GOOD: Child reports to parent
[Parameter] public EventCallback<int> ValueChanged { get; set; }

private async Task OnStarClick(int rating)
{
    await ValueChanged.InvokeAsync(rating);
}
```

### 2. **Missing EventCallback Checks**

```csharp
// ❌ BAD: Might be null
await OnDelete.InvokeAsync();

// ✅ GOOD: Check first
if (OnDelete.HasDelegate)
    await OnDelete.InvokeAsync();
```

### 3. **Too Much State in Child**

```csharp
// ❌ BAD: Child storing business state
[Parameter] public int Value { get; set; }
private int internalValue; // Duplicate state!

// ✅ GOOD: Child is stateless for business data
[Parameter] public int Value { get; set; }
private string lastClickTime = ""; // Only UI state
```
<div style="page-break-after:always;"></div>

## 💡 Try This

1. **Add Products**: See how parent manages the list
2. **Change Ratings**: Watch parent-child communication
3. **Delete Products**: Observe event flow
4. **Check Event Log**: See all communication events

## 🔗 Real-World Applications

This pattern is everywhere in modern web development:

- **Shopping Cart**: Product components report quantity changes
- **Todo Lists**: Todo items report completion status
- **Forms**: Input components report validation results
- **Comments**: Comment components report likes/replies

<div style="page-break-after:always;"></div>

## 🚀 Advanced Concepts

### Component References

```csharp
// Parent can call child methods directly
<Rating @ref="ratingComponent" />

@code {
    private Rating ratingComponent;
    
    private void ResetRating()
    {
        ratingComponent.Reset(); // Direct method call
    }
}
```

### Cascading Parameters

```csharp
// For deeply nested component communication
<CascadingValue Value="currentUser">
    <ParentComponent>
        <ChildComponent />  <!-- Gets currentUser automatically -->
    </ParentComponent>
</CascadingValue>
```

## 🎯 What's Next?

After mastering component communication, you'll learn about:

- **Component Lifecycle** - When components are created/destroyed
- **State Management** - Managing complex application state
- **Services** - Sharing data across the entire application

Understanding parent-child communication is fundamental to building scalable Blazor applications!
