# Store Project
ASP.NET Core MVC Modular E-Commerce App (In Progress)

# Overview
Store Project is an ASP.NET Core MVC web application structured using the Feature Folder Architecture (modular pattern). This project is built with scalability and maintainability in mind, making it suitable for medium to large-sized web applications.

The goal is to build a modern, extensible online store using:

- ASP.NET Core MVC (.NET 8)

- ASP.NET Core Identity for authentication

- Clean architecture practices (DTOs, services, etc.)

- Modals and SweetAlert2 for dynamic admin UX

- HTML5-based admin and front-end templates

- The project is currently under development.

# Features
#### ✅ Modular Feature Folder Structure
Each domain (User, Product, Category, etc.) is separated into its own folder with its own controllers, services, views, models, and DTOs.

#### ✅ ASP.NET Core Identity Integration
Secure user authentication and role management via Identity.

#### ✅ DTO-based Input/Output Models
All data passed between UI and logic layers are secured and shaped via Data Transfer Objects.

#### ✅ Admin Area
   - User management via modals and SweetAlert2
   - Layouts and partial views optimized for reusability
   - HTML-based AdminLTE UI templates

#### ✅ Responsive Front-End
The UI uses responsive HTML5/CSS3 templates for the public-facing store.

#### 🔒 Validation & Security
   - Email validation attribute
   - Anti-forgery protection
   - Secure password reset workflows

#### 📦 Entity & Domain Models
Users, Products, Orders, Carts, Categories, Posts, Messages, etc.

#### 🗂️ Architecture & Structure
The solution follows a clean separation of concerns via:
   - `Features/` (modular areas)
   - `Entities/` (domain models)
   - `DTOs/` (for secure communication)
   - `Common/` (shared helpers, services, attributes)
   - `Infrastructure/Data/` (DbContext, initializers, EF migrations)
   - `wwwroot/` (static assets: JS, CSS, images)


# Tech Stack
|Technology                |	Purpose                        |
| ------------------------ |:-------------------------------:|
|ASP.NET Core MVC (.NET 8) |	Web Framework                  | 
|ASP.NET Core Identity	   |  Authentication & Authorization |
|Entity Framework Core	   |  Data Access                    |
|SQL Server	               |  Database                       |
|HTML/CSS + Bootstrap	   |  Front-End UI                   |
|AdminLTE Template         |	Admin Panel                    |
|jQuery, SweetAlert2       |	UI Enhancements                |
|DTOs                      |	Data Encapsulation             |
|Feature Folders           |	Modular Architecture           |

# Getting Started
⚠️ This project is not yet complete, but to run the current version:

####  Prerequisites
   - .NET 8 SDK
   - SQL Server LocalDB or SQL Server Express
   - Visual Studio 2022 or later

####  Run Locally
1. Clone the repo:
   - `git clone https://github.com/fabianader/StoreProject.git`

2. Navigate to the directory and build:
   - `cd StoreProject`
   - `dotnet build`

3. Update the database:
   - `dotnet ef database update`

4. Run the app:
   - `dotnet run`


# Status
This project is actively under development and may change rapidly.
- Basic Identity and User Management
- DTO integration
- Front and Admin HTML Template Integration
- Product Catalog, Cart & Order functionality
- Role-based Authorization
- Unit & Integration Tests

# 🤝 Contributions
Contributions are welcome once the core functionalities are stable. Until then, suggestions and code reviews are appreciated!
