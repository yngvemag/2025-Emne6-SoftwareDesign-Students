# 🧭 Routing Lab - Navigation & URL Parameters

## 🎯 What You'll Learn

- Multiple route templates for one component
- Route parameters and constraints
- Optional parameters
- Query string handling
- Programmatic navigation with NavigationManager
- Route constraints and validation

## 📖 Concept: Blazor Routing

### What is Routing?

Routing determines which component to show based on the URL in the browser. It's like a GPS for your web application - different URLs lead to different "destinations" (components).

**Example URLs:**

- `/product/123` → Show product with ID 123
- `/search/blazor` → Search for "blazor"
- `/users?page=2` → Show users page 2

### Route Templates

```razor
@page "/03/RoutingLab"                    // Base route
@page "/03/RoutingLab/home"               // Static route
@page "/03/RoutingLab/product/{id:int}"   // Parameter with constraint
@page "/03/RoutingLab/search/{term?}"     // Optional parameter
```

- One component can have multiple routes
- `{}` indicates parameters
- `:int` adds type constraints
- `?` makes parameters optional

<div style="page-break-after:always;"></div>

## 🔍 Understanding Route Parameters

### Required Parameters

```razor
@page "/product/{id:int}"

@code {
    [Parameter] public int Id { get; set; }
}
```

**URL:** `/product/42` → `Id = 42`

- Parameter MUST be in URL
- If missing, route won't match
- Type conversion happens automatically

### Optional Parameters

```razor
@page "/search/{term?}"

@code {
    [Parameter] public string? Term { get; set; }
}
```

**URLs:**

- `/search/blazor` → `Term = "blazor"`
- `/search/` → `Term = null`

### Route Constraints

Common constraints:

- `{id:int}` - Must be integer
- `{id:guid}` - Must be GUID format
- `{date:datetime}` - Must be valid date
- `{value:decimal}` - Must be decimal number

<div style="page-break-after:always;"></div>

## 🌐 NavigationManager Service

### What is NavigationManager?

A service that lets you navigate programmatically (via C# code instead of clicking links).

```csharp
@inject NavigationManager Nav
```

### Navigation Methods

```csharp
// Simple navigation
Nav.NavigateTo("/product/42");

// With query parameters
Nav.NavigateTo("/search?q=blazor&category=tech");

// Force reload
Nav.NavigateTo("/current-page", true);
```

### URL Information

```csharp
Nav.Uri         // Full current URL
Nav.BaseUri     // Application base URL
```
<div style="page-break-after:always;"></div>

## 🔍 Query Strings vs Route Parameters

### Route Parameters (Part of URL Path)

```
/product/42
         ^^^ Route parameter
```

**Characteristics:**

- Part of the URL structure
- Required for route matching
- Clean, SEO-friendly URLs
- Use for essential data (IDs, categories)

### Query Parameters (After ? in URL)

```
/search?q=blazor&category=tech
        ^^^^^^^^^^^^^^^^^^^^^^ Query parameters
```

**Characteristics:**

- Optional additional data
- Don't affect route matching
- Good for filters, pagination, search terms
- Can be easily added/removed

<div style="page-break-after:always;"></div>

## 💡 Practical Examples from the Lab

### Current URL Analysis

The lab shows real-time information about:

- Full current URL
- Route template being used
- Extracted parameters
- Query string values

### Navigation Buttons

Different ways to navigate:

```csharp
// Direct route navigation
Nav.NavigateTo("/03/RoutingLab/product/42");

// Building URLs with parameters
var productUrl = $"/03/RoutingLab/product/{productId}";
Nav.NavigateTo(productUrl);

// Adding query parameters
Nav.NavigateTo("/03/RoutingLab?q=test&category=electronics");
```
<div style="page-break-after:always;"></div>

## 🎓 Key Learning Points

### 1. **Route Design Best Practices**

- Use route parameters for essential data
- Use query parameters for optional filters
- Keep URLs readable and meaningful
- Use constraints to validate input

### 2. **Parameter Binding**

```csharp
[Parameter] public int? Id { get; set; }      // Can be null
[Parameter] public string? Term { get; set; }  // Can be null
```

- Parameters are automatically bound from URL
- Use nullable types for optional parameters
- Type conversion happens automatically

### 3. **Query String Parsing**

```csharp
var uri = Nav.ToAbsoluteUri(Nav.Uri);
var queryParams = QueryHelpers.ParseQuery(uri.Query);
```

- Parse query strings manually for complex scenarios
- `QueryHelpers` from Microsoft.AspNetCore.WebUtilities
- Convert to dictionary for easy access

<div style="page-break-after:always;"></div>

## 🚨 Common Pitfalls

### Route Conflicts

```razor
<!-- ❌ These conflict! -->
@page "/user/{id}"
@page "/user/profile"

<!-- ✅ More specific routes first -->
@page "/user/profile"
@page "/user/{id:int}"
```

### Parameter Type Mismatches

```razor
<!-- ❌ Will fail if URL has non-integer -->
@page "/product/{id:int}"

<!-- URL: /product/abc → 404 error -->
```

### Missing Parameter Attributes

```razor
@page "/product/{id:int}"

@code {
    // ❌ Missing [Parameter]
    public int Id { get; set; }
    
    // ✅ Correct
    [Parameter] public int Id { get; set; }
}
```
<div style="page-break-after:always;"></div>

## 💡 Try This

1. **Change URLs manually** in the browser address bar
2. **Use navigation buttons** to see programmatic navigation
3. **Add query parameters** and observe how they're parsed
4. **Test invalid URLs** (like letters where numbers expected)

## 🔗 Real-World Applications

Routing is used in every web application:

- **E-commerce**: `/product/{id}`, `/category/{name}`
- **Social media**: `/user/{username}`, `/post/{id}`
- **Admin panels**: `/admin/users?page={n}&filter={type}`
- **Documentation**: `/docs/{section}/{topic}`

## 🚀 What's Next?

After understanding routing, you'll learn about:

- **Component Parameters** - Passing data between components
- **Component Communication** - Parent-child relationships
- **State Management** - Sharing data across components

Routing is the foundation for creating multi-page applications in Blazor!
