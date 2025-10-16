# Blazor vs REST API - Arkitektur og Best Practices

## Innledning

Som erfaren .NET utvikler med bakgrunn fra REST API utvikling, er det naturlig å spørre hvordan etablerte patterns kan overføres til Blazor-applikasjoner. Dette dokumentet sammenligner patterns og best practices mellom disse to teknologiene.

---

## 1. Organize by Feature - Sammenligning

### 1.1 REST API Feature Structure (Kjent pattern)

```text
WebApi/
├── Controllers/
├── Features/                    # 📁 Organize by Feature
│   ├── Products/               # 📁 Product Feature
│   │   ├── ProductsController.cs
│   │   ├── Models/
│   │   │   ├── ProductDto.cs
│   │   │   ├── CreateProductRequest.cs
│   │   │   └── UpdateProductRequest.cs
│   │   ├── Services/
│   │   │   ├── IProductService.cs
│   │   │   └── ProductService.cs
│   │   ├── Validators/
│   │   │   └── ProductValidator.cs
│   │   └── Mappings/
│   │       └── ProductMappingProfile.cs
│   ├── Orders/                 # 📁 Order Feature
│   │   ├── OrdersController.cs
│   │   ├── Models/
│   │   ├── Services/
│   │   └── Validators/
│   └── Users/                  # 📁 User Feature
├── Shared/                     # 📁 Delt på tvers av features
│   ├── Models/
│   ├── Services/
│   └── Extensions/
└── Data/
    ├── Entities/
    └── AppDbContext.cs
```
<div style="page-break-after:always;"></div>

### 1.2 Blazor Feature Structure (Anbefalt tilnærming)

```text
BlazorApp/
├── Components/
│   ├── Layout/                 # Layout komponenter
│   └── Shared/                 # Delte UI komponenter
├── Features/                   # 📁 Organize by Feature (JA - dette fungerer!)
│   ├── Products/              # 📁 Product Feature
│   │   ├── Pages/             # 📁 Blazor Pages (.razor)
│   │   │   ├── ProductList.razor      # ≈ GET /api/products
│   │   │   ├── ProductDetails.razor   # ≈ GET /api/products/{id}
│   │   │   ├── CreateProduct.razor    # ≈ POST /api/products
│   │   │   └── EditProduct.razor      # ≈ PUT /api/products/{id}
│   │   ├── Components/        # 📁 Feature-spesifikke komponenter
│   │   │   ├── ProductCard.razor
│   │   │   ├── ProductForm.razor
│   │   │   └── ProductSearch.razor
│   │   ├── Models/            # 📁 ViewModels og DTOs
│   │   │   ├── ProductViewModel.cs
│   │   │   ├── CreateProductModel.cs
│   │   │   └── ProductSummaryDto.cs
│   │   ├── Services/          # 📁 Feature tjenester
│   │   │   ├── IProductService.cs
│   │   │   └── ProductService.cs
│   │   ├── Validators/        # 📁 Validering
│   │   │   └── ProductValidator.cs
│   │   └── Mappings/          # 📁 Mapping profiles
│   │       └── ProductMappingProfile.cs
│   ├── Orders/               # 📁 Order Feature
│   │   ├── Pages/
│   │   ├── Components/
│   │   ├── Models/
│   │   └── Services/
│   └── Users/                # 📁 User Feature
├── Shared/                   # 📁 Delt på tvers av features
│   ├── Models/              # 📁 Delte modeller
│   ├── Services/            # 📁 Delte tjenester
│   ├── Components/          # 📁 Delte UI komponenter
│   ├── Extensions/          # 📁 Extension methods
│   └── Constants/           # 📁 Konstanter
├── Data/                    # 📁 Data access layer
│   ├── Entities/
│   ├── Repositories/
│   └── AppDbContext.cs
└── Infrastructure/          # 📁 Eksterne avhengigheter
    ├── HttpClients/
    ├── Configuration/
    └── Middleware/
```

---

## 2. Razor Pages som "Endpunkter" - Sammenligning

### 2.1 REST API Endpoints (Controller Actions)

```csharp
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;
    
    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }
    
    [HttpGet]                           // GET /api/products
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
    {
        var products = await _productService.GetAllProductsAsync();
        return Ok(products);
    }
    
    [HttpGet("{id}")]                   // GET /api/products/{id}
    public async Task<ActionResult<ProductDto>> GetProduct(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        return product == null ? NotFound() : Ok(product);
    }
    
    [HttpPost]                          // POST /api/products
    public async Task<ActionResult<ProductDto>> CreateProduct(CreateProductRequest request)
    {
        var product = await _productService.CreateProductAsync(request);
        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
    }
    
    [HttpPut("{id}")]                   // PUT /api/products/{id}
    public async Task<IActionResult> UpdateProduct(int id, UpdateProductRequest request)
    {
        await _productService.UpdateProductAsync(id, request);
        return NoContent();
    }
    
    [HttpDelete("{id}")]                // DELETE /api/products/{id}
    public async Task<IActionResult> DeleteProduct(int id)
    {
        await _productService.DeleteProductAsync(id);
        return NoContent();
    }
}
```

### 2.2 Blazor Pages som "Endpunkt-ekvivalenter"

#### Features/Products/Pages/ProductList.razor (≈ GET /api/products)

```razor
@page "/products"
@page "/products/list"
@inject IProductService ProductService
@inject NavigationManager Navigation
@using BlazorApp.Features.Products.Models
@using BlazorApp.Features.Products.Services

<PageTitle>Produkter</PageTitle>

<div class="product-list-page">
    <div class="d-flex justify-content-between align-items-center mb-4">
        <h1>Produktliste</h1>
        <button class="btn btn-primary" @onclick="NavigateToCreate">
            <i class="fas fa-plus"></i> Nytt Produkt
        </button>
    </div>
    
    <!-- Søk og filter (tilsvarer query parameters i API) -->
    <div class="search-section mb-4">
        <div class="row">
            <div class="col-md-6">
                <input type="text" class="form-control" placeholder="Søk produkter..." 
                       @bind="searchTerm" @bind:event="oninput" />
            </div>
            <div class="col-md-3">
                <select class="form-select" @bind="selectedCategory">
                    <option value="">Alle kategorier</option>
                    @foreach (var category in categories)
                    {
                        <option value="@category">@category</option>
                    }
                </select>
            </div>
            <div class="col-md-3">
                <button class="btn btn-outline-secondary" @onclick="Search">
                    Søk
                </button>
            </div>
        </div>
    </div>
    
    @if (isLoading)
    {
        <div class="text-center">
            <div class="spinner-border" role="status">
                <span class="visually-hidden">Laster...</span>
            </div>
        </div>
    }
    else if (products?.Any() == true)
    {
        <div class="row">
            @foreach (var product in products)
            {
                <div class="col-md-4 mb-3">
                    <ProductCard Product="product" 
                                OnEdit="NavigateToEdit" 
                                OnDelete="DeleteProduct" 
                                OnView="NavigateToDetails" />
                </div>
            }
        </div>
        
        <!-- Paginering (tilsvarer pagination i API) -->
        <nav aria-label="Produkter paginering">
            <ul class="pagination justify-content-center">
                <!-- Paginering logikk her -->
            </ul>
        </nav>
    }
    else
    {
        <div class="alert alert-info">
            Ingen produkter funnet.
        </div>
    }
</div>

@code {
    private List<ProductViewModel>? products;
    private string searchTerm = string.Empty;
    private string selectedCategory = string.Empty;
    private List<string> categories = new();
    private bool isLoading = true;
    
    protected override async Task OnInitializedAsync()
    {
        await LoadProductsAsync();
        await LoadCategoriesAsync();
    }
    
    private async Task LoadProductsAsync()
    {
        isLoading = true;
        try
        {
            products = await ProductService.GetAllProductsAsync();
        }
        catch (Exception ex)
        {
            // Håndter feil (tilsvarer error handling i API controller)
            Console.WriteLine($"Feil ved lasting av produkter: {ex.Message}");
        }
        finally
        {
            isLoading = false;
        }
    }
    
    private async Task Search()
    {
        isLoading = true;
        products = await ProductService.SearchProductsAsync(searchTerm, selectedCategory);
        isLoading = false;
    }
    
    private void NavigateToCreate() => Navigation.NavigateTo("/products/create");
    private void NavigateToEdit(int id) => Navigation.NavigateTo($"/products/edit/{id}");
    private void NavigateToDetails(int id) => Navigation.NavigateTo($"/products/{id}");
    
    private async Task DeleteProduct(int id)
    {
        if (await JSRuntime.InvokeAsync<bool>("confirm", "Er du sikker på at du vil slette dette produktet?"))
        {
            await ProductService.DeleteProductAsync(id);
            await LoadProductsAsync(); // Refresh listen
        }
    }
    
    private async Task LoadCategoriesAsync()
    {
        categories = await ProductService.GetCategoriesAsync();
    }
}
```

#### Features/Products/Pages/ProductDetails.razor (≈ GET /api/products/{id})

```razor
@page "/products/{id:int}"
@inject IProductService ProductService
@inject NavigationManager Navigation
@using BlazorApp.Features.Products.Models

<PageTitle>Produktdetaljer</PageTitle>

@if (isLoading)
{
    <div class="text-center">
        <div class="spinner-border" role="status"></div>
    </div>
}
else if (product == null)
{
    <div class="alert alert-danger">
        <h4>Produkt ikke funnet</h4>
        <p>Produktet med ID @Id finnes ikke.</p>
        <button class="btn btn-secondary" @onclick="NavigateBack">
            Tilbake til produktliste
        </button>
    </div>
}
else
{
    <div class="product-details">
        <div class="d-flex justify-content-between align-items-center mb-4">
            <h1>@product.Name</h1>
            <div class="btn-group">
                <button class="btn btn-primary" @onclick="NavigateToEdit">
                    <i class="fas fa-edit"></i> Rediger
                </button>
                <button class="btn btn-danger" @onclick="DeleteProduct">
                    <i class="fas fa-trash"></i> Slett
                </button>
                <button class="btn btn-secondary" @onclick="NavigateBack">
                    <i class="fas fa-arrow-left"></i> Tilbake
                </button>
            </div>
        </div>
        
        <div class="row">
            <div class="col-md-8">
                <div class="card">
                    <div class="card-header">
                        <h5>Produktinformasjon</h5>
                    </div>
                    <div class="card-body">
                        <dl class="row">
                            <dt class="col-sm-3">Navn:</dt>
                            <dd class="col-sm-9">@product.Name</dd>
                            
                            <dt class="col-sm-3">Beskrivelse:</dt>
                            <dd class="col-sm-9">@product.Description</dd>
                            
                            <dt class="col-sm-3">Pris:</dt>
                            <dd class="col-sm-9">@product.Price.ToString("C")</dd>
                            
                            <dt class="col-sm-3">Kategori:</dt>
                            <dd class="col-sm-9">@product.Category</dd>
                            
                            <dt class="col-sm-3">På lager:</dt>
                            <dd class="col-sm-9">
                                <span class="badge @(product.IsInStock ? "bg-success" : "bg-danger")">
                                    @(product.IsInStock ? "Ja" : "Nei")
                                </span>
                            </dd>
                            
                            <dt class="col-sm-3">Opprettet:</dt>
                            <dd class="col-sm-9">@product.CreatedAt.ToString("dd.MM.yyyy HH:mm")</dd>
                        </dl>
                    </div>
                </div>
            </div>
            
            <div class="col-md-4">
                @if (!string.IsNullOrEmpty(product.ImageUrl))
                {
                    <div class="card">
                        <div class="card-header">
                            <h6>Produktbilde</h6>
                        </div>
                        <div class="card-body">
                            <img src="@product.ImageUrl" alt="@product.Name" 
                                 class="img-fluid rounded" />
                        </div>
                    </div>
                }
            </div>
        </div>
    </div>
}

@code {
    [Parameter] public int Id { get; set; }
    
    private ProductViewModel? product;
    private bool isLoading = true;
    
    protected override async Task OnInitializedAsync()
    {
        await LoadProductAsync();
    }
    
    protected override async Task OnParametersSetAsync()
    {
        // Kjøres når route parameters endres (tilsvarer route parameter binding i API)
        if (Id > 0)
        {
            await LoadProductAsync();
        }
    }
    
    private async Task LoadProductAsync()
    {
        isLoading = true;
        try
        {
            product = await ProductService.GetProductByIdAsync(Id);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Feil ved lasting av produkt: {ex.Message}");
            product = null;
        }
        finally
        {
            isLoading = false;
        }
    }
    
    private void NavigateToEdit() => Navigation.NavigateTo($"/products/edit/{Id}");
    private void NavigateBack() => Navigation.NavigateTo("/products");
    
    private async Task DeleteProduct()
    {
        if (await JSRuntime.InvokeAsync<bool>("confirm", "Er du sikker på at du vil slette dette produktet?"))
        {
            try
            {
                await ProductService.DeleteProductAsync(Id);
                Navigation.NavigateTo("/products");
            }
            catch (Exception ex)
            {
                // Håndter feil
                Console.WriteLine($"Feil ved sletting: {ex.Message}");
            }
        }
    }
}
```

<div style="page-break-after:always;"></div>

## 3. Mappere i Blazor vs REST API

### 3.1 REST API Mapping Pattern

```csharp
// AutoMapper Profile for REST API
public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        // Entity ↔ DTO mappings
        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
            
        CreateMap<CreateProductRequest, Product>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));
            
        CreateMap<UpdateProductRequest, Product>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());
    }
}

// REST API Service
public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;
    
    public ProductService(IProductRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<ProductDto> GetProductByIdAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return _mapper.Map<ProductDto>(entity); // Entity → DTO
    }
    
    public async Task<ProductDto> CreateProductAsync(CreateProductRequest request)
    {
        var entity = _mapper.Map<Product>(request); // Request → Entity
        var saved = await _repository.AddAsync(entity);
        return _mapper.Map<ProductDto>(saved); // Entity → DTO
    }
}
```

### 3.2 Blazor Mapping Pattern (JA - du trenger mappere!)

#### Features/Products/Mappings/ProductMappingProfile.cs

```csharp
using AutoMapper;
using BlazorApp.Features.Products.Models;
using BlazorApp.Data.Entities;

namespace BlazorApp.Features.Products.Mappings
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            // Entity ↔ ViewModel mappings (tilsvarende Entity ↔ DTO i API)
            CreateMap<Product, ProductViewModel>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.IsInStock, opt => opt.MapFrom(src => src.Stock > 0));
                
            // Form Models ↔ Entity mappings (tilsvarende Request DTOs i API)
            CreateMap<CreateProductModel, Product>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));
                
            CreateMap<ProductViewModel, EditProductModel>();
            
            CreateMap<EditProductModel, Product>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));
                
            // DTO mappings for external APIs (hvis du kaller eksterne tjenester)
            CreateMap<ExternalProductDto, Product>();
            CreateMap<Product, ExternalProductDto>();
        }
    }
}
```

#### Features/Products/Models/ProductViewModel.cs

```csharp
using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Features.Products.Models
{
    /// <summary>
    /// ViewModel for visning av produkter (tilsvarer ProductDto i API)
    /// </summary>
    public class ProductViewModel
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Produktnavn er påkrevd")]
        [StringLength(100, ErrorMessage = "Produktnavn kan ikke være lengre enn 100 tegn")]
        public string Name { get; set; } = string.Empty;
        
        [StringLength(500, ErrorMessage = "Beskrivelse kan ikke være lengre enn 500 tegn")]
        public string? Description { get; set; }
        
        [Required(ErrorMessage = "Pris er påkrevd")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Pris må være større enn 0")]
        public decimal Price { get; set; }
        
        public string? ImageUrl { get; set; }
        
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        
        public bool IsInStock { get; set; }
        public int Stock { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        
        // UI-spesifikke properties (ikke i Entity)
        public bool IsSelected { get; set; }
        public string DisplayPrice => Price.ToString("C");
        public string StatusBadgeClass => IsInStock ? "badge bg-success" : "badge bg-danger";
        public string StatusText => IsInStock ? "På lager" : "Ikke på lager";
    }
}
```

#### Features/Products/Models/CreateProductModel.cs

```csharp
using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Features.Products.Models
{
    /// <summary>
    /// Model for opprettelse av produkter (tilsvarer CreateProductRequest i API)
    /// </summary>
    public class CreateProductModel
    {
        [Required(ErrorMessage = "Produktnavn er påkrevd")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Produktnavn må være mellom 2 og 100 tegn")]
        public string Name { get; set; } = string.Empty;
        
        [StringLength(500, ErrorMessage = "Beskrivelse kan ikke være lengre enn 500 tegn")]
        public string? Description { get; set; }
        
        [Required(ErrorMessage = "Pris er påkrevd")]
        [Range(0.01, 999999.99, ErrorMessage = "Pris må være mellom 0,01 og 999 999,99")]
        public decimal Price { get; set; }
        
        [Url(ErrorMessage = "Ugyldig URL-format")]
        public string? ImageUrl { get; set; }
        
        [Required(ErrorMessage = "Kategori må velges")]
        [Range(1, int.MaxValue, ErrorMessage = "Velg en gyldig kategori")]
        public int CategoryId { get; set; }
        
        [Required(ErrorMessage = "Antall på lager er påkrevd")]
        [Range(0, int.MaxValue, ErrorMessage = "Antall på lager kan ikke være negativt")]
        public int Stock { get; set; }
    }
}
```

#### Features/Products/Services/ProductService.cs

```csharp
using AutoMapper;
using BlazorApp.Features.Products.Models;
using BlazorApp.Data.Entities;
using BlazorApp.Data.Repositories;

namespace BlazorApp.Features.Products.Services
{
    public interface IProductService
    {
        Task<List<ProductViewModel>> GetAllProductsAsync();
        Task<ProductViewModel?> GetProductByIdAsync(int id);
        Task<List<ProductViewModel>> SearchProductsAsync(string searchTerm, string category);
        Task<ProductViewModel> CreateProductAsync(CreateProductModel model);
        Task<ProductViewModel> UpdateProductAsync(int id, EditProductModel model);
        Task DeleteProductAsync(int id);
        Task<List<string>> GetCategoriesAsync();
    }
    
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        
        public ProductService(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        public async Task<List<ProductViewModel>> GetAllProductsAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<List<ProductViewModel>>(entities);
        }
        
        public async Task<ProductViewModel?> GetProductByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity == null ? null : _mapper.Map<ProductViewModel>(entity);
        }
        
        public async Task<List<ProductViewModel>> SearchProductsAsync(string searchTerm, string category)
        {
            var entities = await _repository.SearchAsync(searchTerm, category);
            return _mapper.Map<List<ProductViewModel>>(entities);
        }
        
        public async Task<ProductViewModel> CreateProductAsync(CreateProductModel model)
        {
            var entity = _mapper.Map<Product>(model); // Model → Entity
            var saved = await _repository.AddAsync(entity);
            return _mapper.Map<ProductViewModel>(saved); // Entity → ViewModel
        }
        
        public async Task<ProductViewModel> UpdateProductAsync(int id, EditProductModel model)
        {
            var existingEntity = await _repository.GetByIdAsync(id);
            if (existingEntity == null)
                throw new ArgumentException($"Produkt med ID {id} finnes ikke");
                
            _mapper.Map(model, existingEntity); // Model → Entity (oppdaterer eksisterende)
            var updated = await _repository.UpdateAsync(existingEntity);
            return _mapper.Map<ProductViewModel>(updated); // Entity → ViewModel
        }
        
        public async Task DeleteProductAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
        
        public async Task<List<string>> GetCategoriesAsync()
        {
            return await _repository.GetCategoriesAsync();
        }
    }
}
```

---

## 4. Dependency Injection Setup

### 4.1 REST API Program.cs

```csharp
var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// Repositories
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// Services
builder.Services.AddScoped<IProductService, ProductService>();

// Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();
```

### 4.2 Blazor Program.cs

```csharp
using BlazorApp.Components;
using BlazorApp.Data;
using BlazorApp.Data.Repositories;
using BlazorApp.Features.Products.Services;
using BlazorApp.Features.Orders.Services;
using BlazorApp.Features.Users.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Blazor
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// AutoMapper (samme som API!)
builder.Services.AddAutoMapper(typeof(Program));

// Database (samme som API!)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositories (samme som API!)
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Feature Services (tilsvarende API services)
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IUserService, UserService>();

// HTTP Clients (for eksterne API-kall)
builder.Services.AddHttpClient();

// Shared Services (tjenester på tvers av features)
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IFileService, FileService>();

var app = builder.Build();

// Configure pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
```

---

## 5. Forskjeller og likheter - Sammendrag

### 5.1 Likheter (Samme patterns fungerer!)

| Konsept | REST API | Blazor | Kommentar |
|---------|----------|---------|-----------|
| **Organize by Feature** | ✅ Controllers per feature | ✅ Pages per feature | Samme struktur fungerer utmerket |
| **Dependency Injection** | ✅ Services, Repositories | ✅ Services, Repositories | Identisk tilnærming |
| **AutoMapper** | ✅ Entity ↔ DTO | ✅ Entity ↔ ViewModel | Samme bibliotek og patterns |
| **Validation** | ✅ DataAnnotations | ✅ DataAnnotations | Samme validering, men også client-side |
| **Repository Pattern** | ✅ Data access abstraction | ✅ Data access abstraction | Identisk implementasjon |
| **Service Layer** | ✅ Business logic | ✅ Business logic | Samme ansvar og struktur |

### 5.2 Forskjeller

| Aspekt | REST API | Blazor | Forklaring |
|--------|----------|---------|------------|
| **"Endpunkter"** | Controller Actions | Razor Pages | Pages har `@page` directive istedenfor `[Route]` |
| **Input Binding** | Model Binding fra HTTP | Parameter Binding + Form Binding | Blazor har `@bind` for two-way binding |
| **Response Format** | JSON/XML | HTML + C# | Blazor genererer HTML dynamisk |
| **State Management** | Stateless | Stateful | Blazor kan holde state mellom interactions |
| **Navigation** | HTTP redirects | NavigationManager | Programmatisk navigering i Blazor |
| **Error Handling** | HTTP status codes | Exception handling + UI feedback | Blazor må vise feil i UI |

---

## 6. Praktisk implementasjon - Feature struktur

### 6.1 Komplett Products Feature

```text
Features/Products/
├── Pages/                          # 📄 "Endpunkter" (Razor Pages)
│   ├── ProductList.razor          # ≈ GET /api/products
│   ├── ProductDetails.razor       # ≈ GET /api/products/{id}
│   ├── CreateProduct.razor        # ≈ POST /api/products
│   └── EditProduct.razor          # ≈ PUT /api/products/{id}
├── Components/                     # 🧩 Feature-spesifikke UI komponenter
│   ├── ProductCard.razor          # Produkt kort i listen
│   ├── ProductForm.razor          # Skjema for create/edit
│   ├── ProductSearch.razor        # Søkefunksjonalitet
│   └── ProductImageUpload.razor   # Bildeopplasting
├── Models/                         # 📋 ViewModels og DTOs
│   ├── ProductViewModel.cs        # ≈ ProductDto (for visning)
│   ├── CreateProductModel.cs      # ≈ CreateProductRequest
│   ├── EditProductModel.cs        # ≈ UpdateProductRequest
│   ├── ProductSearchModel.cs      # Søkeparametere
│   └── DTOs/                      # For eksterne API-kall
│       └── ExternalProductDto.cs
├── Services/                       # ⚙️ Business logic (identisk med API)
│   ├── IProductService.cs
│   ├── ProductService.cs
│   └── IProductValidationService.cs
├── Validators/                     # ✅ Validering (samme som API)
│   ├── ProductValidator.cs
│   └── CreateProductValidator.cs
└── Mappings/                      # 🔄 AutoMapper profiles (samme som API)
    └── ProductMappingProfile.cs
```

### 6.2 Example: CreateProduct.razor (≈ POST /api/products)

```razor
@page "/products/create"
@inject IProductService ProductService
@inject NavigationManager Navigation
@inject IJSRuntime JSRuntime
@using BlazorApp.Features.Products.Models
@using BlazorApp.Features.Products.Components

<PageTitle>Opprett nytt produkt</PageTitle>

<div class="create-product-page">
    <div class="d-flex justify-content-between align-items-center mb-4">
        <h1>Opprett nytt produkt</h1>
        <button class="btn btn-outline-secondary" @onclick="NavigateBack">
            <i class="fas fa-arrow-left"></i> Tilbake
        </button>
    </div>
    
    <div class="row">
        <div class="col-lg-8 mx-auto">
            <div class="card">
                <div class="card-header">
                    <h5>Produktinformasjon</h5>
                </div>
                <div class="card-body">
                    <EditForm Model="model" OnValidSubmit="CreateProduct" FormName="CreateProductForm">
                        <DataAnnotationsValidator />
                        
                        <!-- Gjenbruk ProductForm komponent -->
                        <ProductForm Model="model" 
                                   Categories="categories"
                                   IsLoading="isCreating" />
                        
                        <div class="form-actions mt-4">
                            <button type="submit" class="btn btn-primary" disabled="@isCreating">
                                @if (isCreating)
                                {
                                    <span class="spinner-border spinner-border-sm me-2" role="status"></span>
                                }
                                <i class="fas fa-save"></i> Opprett produkt
                            </button>
                            <button type="button" class="btn btn-secondary ms-2" @onclick="ResetForm">
                                <i class="fas fa-undo"></i> Nullstill
                            </button>
                        </div>
                    </EditForm>
                </div>
            </div>
        </div>
    </div>
</div>

@code {
    [SupplyParameterFromForm]
    private CreateProductModel model { get; set; } = new();
    
    private List<CategoryOption> categories = new();
    private bool isCreating = false;
    
    protected override async Task OnInitializedAsync()
    {
        await LoadCategoriesAsync();
    }
    
    private async Task CreateProduct()
    {
        isCreating = true;
        
        try
        {
            // Samme business logic som i API controller
            var created = await ProductService.CreateProductAsync(model);
            
            await JSRuntime.InvokeVoidAsync("alert", "Produktet ble opprettet!");
            Navigation.NavigateTo($"/products/{created.Id}"); // Naviger til detaljer
        }
        catch (Exception ex)
        {
            // Feilhåndtering (tilsvarende try-catch i API controller)
            await JSRuntime.InvokeVoidAsync("alert", $"Feil ved opprettelse: {ex.Message}");
        }
        finally
        {
            isCreating = false;
        }
    }
    
    private void ResetForm()
    {
        model = new CreateProductModel();
    }
    
    private void NavigateBack()
    {
        Navigation.NavigateTo("/products");
    }
    
    private async Task LoadCategoriesAsync()
    {
        var categoryNames = await ProductService.GetCategoriesAsync();
        categories = categoryNames.Select((name, index) => new CategoryOption 
        { 
            Id = index + 1, 
            Name = name 
        }).ToList();
    }
    
    private class CategoryOption
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
```

---

## 7. Best Practices sammendrag

### ✅ DO (Anbefalte patterns fra REST API som fungerer i Blazor)

1. **Organize by Feature** - Samme struktur fungerer perfekt
2. **Dependency Injection** - Bruk samme DI patterns som i API
3. **AutoMapper** - Bruk samme mapping-bibliotek og patterns  
4. **Repository Pattern** - Identisk implementasjon for data access
5. **Service Layer** - Samme business logic lag
6. **Validation** - DataAnnotations fungerer både server og client-side
7. **DTO/ViewModel pattern** - Separér UI-modeller fra database-entiteter

### 📋 Blazor-spesifikke tilpasninger

1. **Pages som "Endpunkter"** - Bruk `@page` directive for routing
2. **Parameter Binding** - Bruk `[Parameter]` for route parameters  
3. **Form Handling** - Bruk `EditForm` og `@bind` for two-way binding
4. **Navigation** - Bruk `NavigationManager` istedenfor redirects
5. **State Management** - Håndter komponent-state og lifecycle
6. **Error Handling** - Vis feil i UI, ikke bare logg dem

### ⚠️ Forskjeller å være obs på

| REST API | Blazor | Hvorfor forskjell |
|----------|---------|-------------------|
| HTTP status codes | UI feedback | Blazor må vise feil visuelt |
| JSON responses | C# objects | Blazor jobber direkte med C# |
| Stateless requests | Stateful components | Blazor holder state mellom interactions |
| Model validation on server | Client + server validation | Blazor kan validere real-time |

---

## 8. Konklusjon

**JA** - Du kan bruke mange av de samme patterns fra REST API utvikling i Blazor:

✅ **Organize by Feature** struktur fungerer utmerket  
✅ **Razor Pages** kan tenkes på som "endpunkter" med `@page` routing  
✅ **AutoMapper** og DTO/ViewModel patterns er like viktige  
✅ **Service Layer** og **Repository Pattern** er identiske  
✅ **Dependency Injection** setup er nesten identisk  

Den største forskjellen er at Blazor genererer HTML dynamisk og håndterer UI-interaksjoner, mens REST API returnerer data som JSON. Men de underliggende arkitektur-patterns er overraskende like!

**Start med det du kjenner fra REST API utvikling** - strukturen vil føles kjent og naturlig i Blazor også.