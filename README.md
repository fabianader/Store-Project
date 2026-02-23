# 🛒 StoreProject

StoreProject is a modular **ASP.NET Core MVC (.NET 8)** web application
developed for an online store system.

The project follows a **Feature-Based Modular Architecture** and uses
**SQL Server** as its database management system.

It includes separate sections for:

-   🛍️ Customers (Storefront)
-   🛠️ Admin Management Panel
-   👤 User Panel (Profile & Activities)
-   📩 Contact System
-   💳 Demo Payment Section

------------------------------------------------------------------------

## 🚀 Technologies Used

-   ASP.NET Core MVC (.NET 8)
-   Entity Framework Core
-   SQL Server
-   ASP.NET Core Identity
-   Razor Views
-   Feature Folder Architecture
-   DTO Pattern
-   Manual Mapper Classes (Per Feature)
-   AdminLTE 3.2.0 (Admin Panel Template)
-   Aranoz (User UI Template)

------------------------------------------------------------------------

## 🏗 Architecture Overview

The project follows a **Feature-Based Modular Structure**, where each
feature contains:

-   Controllers\
-   DTOs\
-   Models\
-   Services\
-   Mapper\
-   Views

### 🔁 Data Flow

    Controller → Model → DTO → Service → Entity → Database

Each feature includes its own Mapper class to handle data transformation
between:

-   Entity classes\
-   DTOs\
-   View Models

------------------------------------------------------------------------

## 🔐 Authentication & Authorization

Authentication is implemented using **ASP.NET Core Identity**.

Two predefined roles:

-   `Admin`
-   `User`

### 🔑 Admin Login Credentials

    Username: Admin
    Password: Admin123#

⚠️ Change the default admin password in production.

------------------------------------------------------------------------

# ⚠️ IMPORTANT -- Database & Migration Setup

During development, migration inconsistencies occurred and the database
schema was manually synchronized with the code.

Because of this:

-   Existing migrations may not fully reflect the final schema
-   Running `Update-Database` directly may cause errors

To safely run the project after cloning, follow the steps below.

------------------------------------------------------------------------

## 🛠 How to Clone and Run (Recommended Safe Setup)

### 1️⃣ Clone the Repository

``` bash
git clone https://github.com/yourusername/StoreProject.git
cd StoreProject
```

### 2️⃣ Configure Database Connection

Open `appsettings.json` and update the connection string:

``` json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=StoreProjectDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

### 3️⃣ Remove Existing Migrations (Recommended)

Delete the `Migrations` folder manually.

If needed, use:

``` bash
dotnet ef migrations remove
```

(Repeat until all migrations are removed.)

### 4️⃣ Create Fresh Migration

``` bash
dotnet ef migrations add InitialCreate
```

### 5️⃣ Update Database

``` bash
dotnet ef database update
```

### 6️⃣ Run the Project

``` bash
dotnet run
```

Or run via **Visual Studio (IIS Express)**.

------------------------------------------------------------------------

## 📂 Main Features

### 🏠 Store (Customer Section)

-   Browse Products
-   Search Products
-   Browse Categories
-   Product Details
-   Add to Cart
-   Checkout (Demo Payment)
-   Favorites
-   Contact Us

### 👤 User Panel

-   Dashboard
-   Edit Profile
-   Change Password
-   Order History
-   Cancel Orders
-   Favorites List
-   Contact Messages

### 🛠 Admin Management Panel

-   Manage Products
-   Manage Categories (Tree Structure)
-   Manage Orders
-   Manage Users
-   Contact Message Management
-   Order Change Logs

------------------------------------------------------------------------

## 📦 Project Structure (Simplified)

    StoreProject
    │
    ├── Common
    ├── Entities
    ├── Features
    │   ├── Admin
    │   ├── Product
    │   ├── Category
    │   ├── Cart
    │   ├── Order
    │   ├── User
    │   └── ContactMessage
    │
    ├── Infrastructure
    │   └── Data
    │
    ├── Migrations
    ├── wwwroot
    └── Program.cs

------------------------------------------------------------------------

## ✨ Design Patterns & Concepts Used

-   Feature Folder Architecture
-   Separation of Concerns
-   DTO Pattern
-   Service Layer Pattern
-   Manual Mapping
-   Role-Based Authorization
-   View Components
-   Tag Helpers

------------------------------------------------------------------------

## 📌 Notes

-   The payment section is a demo simulation.
-   Email sending is supported via `EmailSender`.
-   The architecture is modular and scalable.
-   Database should be freshly generated when cloning.

------------------------------------------------------------------------

## 📄 License

This project is developed for educational and demonstration purposes.
