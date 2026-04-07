# 📚 LibraCore

**LibraCore** is a full-featured online bookstore web application built with **ASP.NET Core 8** as a final project for the *ASP.NET Advanced* course at SoftUni. It allows users to browse books, manage favorites, write reviews, and place orders. Administrators have a dedicated panel for managing books, authors, orders, reviews, and users.

---

## 🚀 Features

### User-Facing
- Browse and search books with pagination
- View detailed book information (author, genre, price, release date, description)
- Add/remove books to/from a personal Favorites list
- Write and view reviews with star ratings
- Place orders and view order history with detailed receipts
- Register, log in, and log out via ASP.NET Identity

### Admin Panel (`/Admin`)
- Add, edit, and soft-delete books and authors
- View and manage all orders
- Delete inappropriate reviews
- Manage registered users (view list, promote/demote roles)
- Dedicated Admin Home dashboard

---

## 🏗️ Architecture

The solution follows a **multi-layer (N-Tier) architecture** with clear separation of concerns:

```
LibraCore/
├── LibraCore.Web                  → MVC Presentation Layer (Controllers, Views, Areas)
├── LibraCore.Core                 → Business Logic Layer (Services, Service Interfaces)
├── LibraCore.Infrastructure       → Data Access Layer (EF Core, Repositories, Entities, Migrations)
├── LibraCore.ViewModels           → Shared DTOs and View Models
├── LibraCore.GCommon              → Global constants, validation constants, custom exceptions
├── LibraCore.ApplicationConstants → Identity and role constants
├── LibraCore.Controllers.Tests    → Unit tests for MVC controllers (Moq)
├── LibraCore.Integration.Tests    → Integration tests for repositories (InMemory DB)
└── LibraCore.Repositories.Tests   → Unit tests for repository layer (InMemory DB)
```

### Design Patterns Used
- **Repository Pattern** — abstracts data access behind interfaces
- **Service Layer Pattern** — encapsulates business logic away from controllers
- **Soft Delete Pattern** — entities are flagged `IsDeleted = true` instead of being physically removed
- **Dependency Injection** — built-in ASP.NET Core DI container throughout
- **MVC Areas** — `Admin` area fully separated from the public area

---

## 🗃️ Entity Models

| Entity          | Description                                                     |
|-----------------|-----------------------------------------------------------------|
| `ApplicationUser` | Extends `IdentityUser<Guid>` for the auth system             |
| `Book`          | Core entity: title, description, price, release date, image URL |
| `Author`        | Author name; supports soft delete                               |
| `Genre`         | Book genre/category; supports soft delete                       |
| `Review`        | Rating (1–5) + comment, linked to a user and a book            |
| `Order`         | Order header linked to a user, with status tracking             |
| `OrderItem`     | Line item within an order (book, quantity, price snapshot)      |
| `UserBook`      | Join table — maps users' favorited books                        |

---

## ⚙️ Services

| Service           | Responsibilities                                                          |
|-------------------|---------------------------------------------------------------------------|
| `BookService`     | CRUD for books, pagination/filtering logic, detail/form view mapping      |
| `AuthorService`   | Author listing, add, soft delete; deduplication by name (case-insensitive)|
| `GenreService`    | Genre listing for dropdowns                                               |
| `FavoriteService` | Add/remove user favorites, list user's favorited books                    |
| `ReviewService`   | Add and list reviews per book, delete by admin                            |
| `OrderService`    | Create orders, retrieve order history, order details per user             |
| `UserService`     | List all users for admin panel                                            |

---

## 🔐 Security

- **Authentication & Authorization** via ASP.NET Core Identity
- Two roles seeded: `User` and `Administrator`
- Admin area protected with `[Authorize(Roles = "Administrator")]` via `BaseAdminController`
- **Anti-Forgery Tokens** on all mutating forms (POST/DELETE)
- **Input validation** on both client side (jQuery Unobtrusive Validation) and server side (Data Annotations + ModelState)
- Database-level constraints enforced via EF Core Fluent API configurations
- HTML special characters are escaped by Razor by default (XSS prevention)
- Parameterized queries through EF Core (SQL injection prevention)
- Custom **404** and **500** error pages

---

## ✅ Validation

All models are validated with Data Annotations and constants from `LibraCore.GCommon.EntityValidation`:

| Entity    | Constraints                                                          |
|-----------|----------------------------------------------------------------------|
| Book      | Title: 2–200 chars; Description: 10–2000 chars; Price: decimal(10,2)|
| Author    | Name: 2–150 chars                                                    |
| Genre     | Name: 3–80 chars                                                     |
| Review    | Rating: 1–5; Comment: 5–1000 chars                                  |
| OrderItem | Quantity: 1–10; Price: decimal(18,2)                                 |

---

## 🌱 Seeding

The database is seeded via EF Core migrations and includes:

- **Authors** — a set of real-world authors
- **Genres** — common book genres (Fiction, Science, History, etc.)
- **Books** — a curated catalog with prices, descriptions, and cover image URLs
- **ApplicationUser** — a default admin user and sample regular users
- **Roles** — `User` and `Administrator` roles
- **UserRoles** — the admin user is assigned the Administrator role
- **UserBooks** — sample favorites for demo users
- **Reviews** — sample reviews per book

---

## 📄 Views

### Public Area (18 views)
- `Home/Index` — landing page
- `Home/NotFound` — custom 404
- `Home/InternalError` — custom 500
- `Book/Index` — searchable, paginated book listing
- `Book/Details` — full book detail with actions partial
- `Author/Index` — all authors with partial card view
- `Favorite/Index` — current user's favorite books
- `Review/Index` — reviews for a specific book
- `Review/Add` — add a review form
- `Order/Index` — user's order history
- `Order/Details` — detailed order view
- Identity pages: `Login`, `Register`, `Logout`
- Shared: `_Layout`, `_LoginPartial`, `_AuthorCardPartial`, `_BookDetailsActionsPartial`, `_ValidationScriptsPartial`

### Admin Area (12 views)
- `Admin/Home/Index` — admin dashboard
- `Admin/Book/Add`, `Edit`, `Delete`
- `Admin/Author/Index`, `Add`, `Delete`
- `Admin/Order/Index`
- `Admin/Review/Delete`
- `Admin/User/Index`

---

## 🧪 Tests

The project contains three test projects:

### `LibraCore.Controllers.Tests` — Controller Unit Tests (Moq)
Tests MVC controller actions in isolation using mocked services.

| Test Class               | Tests |
|--------------------------|-------|
| `BookControllerTests`    | Index pagination & search, Details found/not found |
| `AuthorControllerTests`  | Index with data, Index empty state |
| `FavoriteControllerTests`| Index, Add success/already exists/not found, Remove success/persist fail |
| `OrderControllerTests`   | Index, Details empty guid/not found, Buy empty guid/success/not found/persist fail |

### `LibraCore.Integration.Tests` — Repository Integration Tests (InMemory EF Core)
Tests repository logic against an in-memory database.

| Test Class                | Tests |
|---------------------------|-------|
| `AuthorRepositoryTests`   | Non-deleted filter, case-insensitive name lookup, soft-delete check, add, soft delete |
| `BookRepositoryTests`     | Non-deleted filter, soft-deleted lookup returns null, add, soft delete, exists-regardless-of-status |

### `LibraCore.Repositories.Tests` — Repository Unit Tests (InMemory EF Core)
Additional repository tests using in-memory database.

| Test Class           | Tests |
|----------------------|-------|
| `BookRepositoryTests`| Non-deleted filter, null-for-deleted lookup, add, soft delete, exists |

---

## 🛠️ Tech Stack

| Layer              | Technology                              |
|--------------------|-----------------------------------------|
| Framework          | ASP.NET Core 8 (MVC + Razor Pages)      |
| ORM                | Entity Framework Core 8                 |
| Database           | Microsoft SQL Server                    |
| Auth               | ASP.NET Core Identity                   |
| UI                 | Razor Views, Bootstrap 5, custom CSS    |
| Client Validation  | jQuery + jQuery Unobtrusive Validation  |
| Testing            | NUnit, Moq, EF Core InMemory            |
| DI                 | Built-in ASP.NET Core DI Container      |

---

## 🔧 Setup Instructions

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server) (LocalDB or full)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or JetBrains Rider

### Steps

1. **Clone the repository**
   ```bash
   git clone https://github.com/<your-username>/LibraCore.git
   cd LibraCore
   ```

2. **Configure the connection string**
   
   Edit `LibraCore.Web/appsettings.Development.json`:
   ```json
   {
     "ConnectionStrings": {
       "DevConnectionString": "Server=(localdb)\\mssqllocaldb;Database=LibraCoreDb;Trusted_Connection=True;"
     }
   }
   ```

3. **Apply migrations and seed the database**
   ```bash
   cd LibraCore.Web
   dotnet ef database update
   ```

4. **Run the application**
   ```bash
   dotnet run
   ```
   Navigate to `https://localhost:7XXX`

5. **Default admin credentials** (seeded)
   - Email: `admin@libracore.com`
   - Password: `Admin123!`

### Running Tests
```bash
dotnet test
```

---

## 📁 Project Structure Summary

```
LibraCore.Web/
├── Areas/
│   ├── Admin/              ← Admin area (controllers + views)
│   └── Identity/           ← Razor Pages for auth
├── Controllers/            ← Public controllers
├── Views/                  ← Razor views per controller
└── wwwroot/                ← Static assets (CSS, JS, images)

LibraCore.Core/
├── Interfaces/             ← Service interfaces (IBookService, etc.)
└── *Service.cs             ← Service implementations

LibraCore.Infrastructure/
├── Data/
│   ├── Configuration/      ← EF Fluent API entity configs
│   ├── Entities/           ← Domain entity classes
│   └── LibraCoreDbContext.cs
├── Migrations/             ← EF Core migrations
└── Repositories/           ← Repository implementations + interfaces

LibraCore.GCommon/
├── EntityValidation.cs     ← Shared validation constants
├── ApplicationConstants.cs ← App-wide constants (date format, image URLs)
├── OutputMessages.cs       ← User-facing success/error messages
└── Exceptions/             ← Custom exception types
```

---

## 📌 Requirements Compliance

| Requirement                               | Status |
|-------------------------------------------|--------|
| ASP.NET Core 8                            | ✅     |
| 10+ Views                                 | ✅ 30+ |
| 5+ Entity Models                          | ✅ 8   |
| 5+ Controllers                            | ✅ 12  |
| Razor + Sections + Partial Views          | ✅     |
| SQL Server + EF Core                      | ✅     |
| MVC Areas (Admin)                         | ✅     |
| Bootstrap Responsive UI                   | ✅     |
| ASP.NET Identity (User + Admin roles)     | ✅     |
| Unit Tests (65%+ business logic coverage) | ✅     |
| Error handling + custom 404/500 pages     | ✅     |
| Client + server-side validation           | ✅     |
| Dependency Injection                      | ✅     |
| Pagination                                | ✅     |
| Search / Filtering                        | ✅     |
| Data Seeding                              | ✅     |
| Security (XSS, CSRF, SQL Injection)       | ✅     |
| Soft Delete                               | ✅     |

---

## 📝 License

This project is developed for educational purposes as part of the SoftUni ASP.NET Advanced course.
