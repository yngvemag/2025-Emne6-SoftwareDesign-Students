# 📝 Local State & Re-render Lab - State Management & @key Directive

## 🎯 What You'll Learn

- Component local state management
- Understanding re-render triggers
- The `@key` directive and DOM stability
- "Single source of truth" principle
- Manual state updates with `StateHasChanged()`
- "Lifting state up" pattern

## 📖 Concept: Local State Management

### What is Component State?

Component state is data that belongs to and is managed by a specific component. It determines what the component displays and how it behaves.

**Think of it like a person's memory:**

- **Short-term memory** (UI state): What you're currently thinking about
- **Long-term memory** (Business state): Important facts you need to remember

### Types of State

#### Business State (Important Data)

```csharp
// ✅ This is business state - the actual todo items
private List<TodoItem> todos = new();
```

**Characteristics:**

- Contains important application data
- Multiple components might need access
- Should be owned by parent components
- Changes trigger re-renders

#### UI State (Temporary Data)

```csharp
// ✅ This is UI state - temporary input text
private string newTodoText = "";
private string lastAction = "Component initialized";
```

**Characteristics:**

- Temporary, for display purposes only
- Local to one component
- Can be lost without data loss
- Helps with user experience

## 🔄 Re-render Triggers

### What Causes Re-renders?

Blazor automatically re-renders components when certain things happen:

#### 1. **Property Changes** (Automatic)

```csharp
private int count = 0;

private void Increment()
{
    count++; // This triggers automatic re-render
}
```

#### 2. **Event Handlers** (Automatic)

```razor
<button @onclick="Increment">Click me</button>
```

When user clicks, Blazor automatically re-renders after the event handler runs.

#### 3. **Parent Re-renders** (Automatic)

When a parent component re-renders, all child components also re-render by default.

#### 4. **Manual StateHasChanged()** (Explicit)

```csharp
private void UpdateData()
{
    // Some complex update that Blazor might not detect
    StateHasChanged(); // Force re-render
}
```
<div style="page-break-after:always;"></div>

## 🔑 The @key Directive

### What is @key?

The `@key` directive tells Blazor how to identify specific elements in the DOM. It's like giving each element a unique ID badge.

### Without @key (Potentially Problematic)

```razor
@foreach (var todo in todos)
{
    <div class="todo-item">
        <input type="checkbox" @bind="todo.IsCompleted" />
        <span>@todo.Text</span>
    </div>
}
```

**Problem:** When the list order changes, Blazor might reuse DOM elements in confusing ways.

### With @key (Stable DOM)

```razor
@foreach (var todo in todos)
{
    <div @key="todo.Id" class="todo-item">
        <input type="checkbox" @bind="todo.IsCompleted" />
        <span>@todo.Text</span>
    </div>
}
```

**Benefit:** Each todo item keeps its DOM element, even when the list is reordered.

### When to Use @key

#### ✅ **Use @key when:**

- Rendering lists that can change order
- Items have important UI state (checkboxes, input focus)
- Performance is critical for large lists
- Items can be added/removed from middle of list

#### ❌ **Don't need @key when:**

- Simple, static lists
- Items are always added to end
- No important UI state to preserve
- Small lists with good performance

## 🏗️ "Lifting State Up" Pattern

### The Problem: Scattered State

```csharp
// ❌ BAD: Multiple components own the same type of data
Component A: private List<Todo> myTodos = new();
Component B: private List<Todo> myTodos = new();
Component C: private List<Todo> myTodos = new();
```

**Issues:**

- Data gets out of sync
- Hard to coordinate changes
- Duplicate state everywhere
- Complex to debug

### The Solution: Single Source of Truth

```csharp
// ✅ GOOD: Parent owns all business state
Parent Component:
    private List<Todo> todos = new(); // Single source of truth

Child Component A:
    [Parameter] public List<Todo> Todos { get; set; } // Receives data
    [Parameter] public EventCallback<Todo> OnTodoAdded { get; set; } // Reports changes

Child Component B:
    [Parameter] public List<Todo> Todos { get; set; } // Same data
    [Parameter] public EventCallback<Todo> OnTodoDeleted { get; set; } // Reports changes
```

**Benefits:**

- All components show consistent data
- Changes flow through one place
- Easy to debug and test
- Clear responsibility boundaries

<div style="page-break-after:always;"></div>

## 🎓 Key Learning Points

### 1. **State Ownership Rules**

```csharp
// Parent component (TodoList)
private List<TodoItem> todos = new(); // ✅ Owns business state

// Child component (TodoInput)  
private string inputText = ""; // ✅ Owns only UI state
[Parameter] public EventCallback<string> OnTodoAdded { get; set; } // ✅ Reports to parent
```

**Guidelines:**

- Business state goes in the lowest common ancestor
- UI state stays in the component that needs it
- Use EventCallback to communicate changes up

### 2. **Performance with @key**

```csharp
// With @key: Efficient updates
<TodoItem @key="todo.Id" Todo="todo" />

// Without @key: Potentially inefficient
<TodoItem Todo="todo" /> // Might recreate unnecessarily
```

### 3. **Manual Re-render Control**

```csharp
private void ComplexUpdate()
{
    // Complex logic that Blazor might not detect
    ProcessDataInBackground();
    
    // Force UI update
    StateHasChanged();
}
```
<div style="page-break-after:always;"></div>

## 🔍 Practical Examples from the Lab

### Toggle @key Demo

The lab shows the same todo list twice:

1. **With @key**: DOM elements stay stable when shuffled
2. **Without @key**: DOM elements might get confused

**Try this:**

1. Check some checkboxes
2. Click "Shuffle Order"  
3. See which version preserves checkbox state better

### Child Component Communication

The `TodoInput` component demonstrates:

- Taking parameters from parent
- Managing local UI state (input text)
- Reporting back via EventCallback
- Not owning business state
- 
<div style="page-break-after:always;"></div>

## 🚨 Common Pitfalls

### 1. **Wrong @key Values**

```razor
<!-- ❌ BAD: Index changes when items move -->
@foreach (var (todo, index) in todos.Select((t, i) => (t, i)))
{
    <div @key="index">@todo.Text</div>
}

<!-- ✅ GOOD: Stable unique identifier -->
@foreach (var todo in todos)
{
    <div @key="todo.Id">@todo.Text</div>
}
```

### 2. **State in Wrong Component**

```csharp
// ❌ BAD: Child owning shared business state
// TodoItem.razor
[Parameter] public Todo Todo { get; set; }
private List<Todo> allTodos; // Wrong! This should be in parent

// ✅ GOOD: Child only manages UI concerns
// TodoItem.razor
[Parameter] public Todo Todo { get; set; }
[Parameter] public EventCallback<Todo> OnChanged { get; set; }
private bool isEditing = false; // UI state only
```

### 3. **Overusing StateHasChanged()**

```csharp
// ❌ BAD: Unnecessary manual re-renders
private void UpdateTodo()
{
    todo.Text = newText;
    StateHasChanged(); // Not needed! Blazor detects this automatically
}

// ✅ GOOD: Let Blazor handle automatic re-renders
private void UpdateTodo()
{
    todo.Text = newText; // Blazor re-renders automatically
}
```
<div style="page-break-after:always;"></div>

## 💡 Try This

1. **Add/Remove Todos**: See state management in action
2. **Toggle @key**: Compare DOM stability with/without @key
3. **Shuffle Order**: Test how @key preserves UI state
4. **Use Child Input**: See "lifting state up" pattern
5. **Force Re-renders**: Experiment with manual StateHasChanged()

## 🔗 Real-World Applications

### Shopping Cart

```csharp
// Parent: ShoppingCart component
private List<CartItem> items = new();

// Child: CartItemComponent
[Parameter] public CartItem Item { get; set; }
[Parameter] public EventCallback<CartItem> OnQuantityChanged { get; set; }
```

### Todo App

```csharp
// Parent: TodoList component  
private List<Todo> todos = new();

// Child: TodoItem component
[Parameter] public Todo Todo { get; set; }
[Parameter] public EventCallback<Todo> OnCompleted { get; set; }
```

### Data Tables

```razor
<!-- Preserve row state during sorting -->
@foreach (var row in sortedData)
{
    <TableRow @key="row.Id" Data="row" />
}
```
<div style="page-break-after:always;"></div>

## 🚀 What's Next?

After mastering local state and re-rendering, you'll learn about:

- **Services** - Sharing state across the entire application
- **Dependency Injection** - Managing shared resources
- **State Management Libraries** - Advanced patterns for complex apps

Understanding local state management is fundamental to building maintainable Blazor applications!
