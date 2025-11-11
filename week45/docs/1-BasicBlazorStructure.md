# 👋 Hello Components - Basic Blazor Structure

## 🎯 What You'll Learn

- How to create a basic Blazor component
- Understanding Razor syntax (mixing HTML and C#)
- Using `@page` directive for routing
- Basic component structure and organization

## 📖 Concept: Components

### What is a Blazor Component?

A Blazor component is a piece of UI that can be reused throughout your application. Think of it like a LEGO block - you can combine many components to build a complete application.

**Key Parts of Every Component:**

1. **HTML Markup** - What the user sees
2. **C# Code** - The logic and behavior
3. **CSS (optional)** - How it looks

### Component Structure

```razor
@page "/hello"              <!-- 1. Route (URL) -->

<h1>Hello World!</h1>       <!-- 2. HTML markup -->

@code {                     <!-- 3. C# code block -->
    private string message = "Welcome to Blazor!";
}
```
<div style="page-break-after:always;"></div>

## 🔍 Breaking Down the Hello Component

### The `@page` Directive

```razor
@page "/01/Hello"
```

- This tells Blazor what URL should show this component
- When someone visits `/01/Hello`, this component will be displayed
- You can have multiple `@page` directives for the same component

### HTML with Razor Syntax

```razor
<h1>Hello from Blazor!</h1>
<p>Current time: @DateTime.Now</p>
```

- Write regular HTML
- Use `@` to include C# expressions
- `@DateTime.Now` runs C# code and shows the result

### The `@code` Block

```razor
@code {
    private string greeting = "Hello";
    
    private string GetMessage() 
    {
        return $"{greeting} from Blazor!";
    }
}
```

- Contains all your C# code
- Private fields and methods
- Component logic and state

<div style="page-break-after:always;"></div>

## 🎓 Key Learning Points

### 1. **File Organization**

- `.razor` files contain components
- File name usually matches component name
- Organized in folders by feature/topic

### 2. **Razor Syntax Rules**

- `@` switches from HTML to C#
- `@{ }` for code blocks
- `@( )` for complex expressions
- `@* *@` for comments

### 3. **Component Lifecycle**

- Component is created when route is accessed
- HTML is generated on server (Blazor Server)
- Sent to browser and displayed

## 💡 Try This

1. **Change the Message**: Edit the text and see it update
2. **Add More HTML**: Try adding paragraphs, lists, or images
3. **Add C# Variables**: Create new variables and display them
4. **Experiment with Time**: Show different date formats

## 🔗 What's Next?

After understanding basic components, you'll learn about:

- **Data Binding** - Making your UI interactive
- **Events** - Responding to user actions
- **Parameters** - Passing data between components

The Hello component is your foundation - everything else builds on these concepts!

## 🚀 Common Beginner Questions

**Q: Why use `@` before C# code?**
A: It tells Razor "this is C# code, not HTML text"

**Q: Where does the C# code run?**
A: On the server (with Blazor Server) - the HTML result is sent to the browser

**Q: Can I use any C# feature?**
A: Yes! You have access to the full .NET ecosystem

**Q: How is this different from HTML?**
A: HTML is static, Blazor components are dynamic and can change based on data and user interaction
