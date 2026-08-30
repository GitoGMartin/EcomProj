# E-Commerce Backend API

A scalable RESTful backend API for an e-commerce platform built with **ASP.NET Core** and **PostgreSQL**. The system provides user authentication, product and inventory management, shopping carts, orders, reviews, and other core e-commerce functionality.

## 🚀 Tech Stack

* **ASP.NET Core Web API** – Backend framework
* **C#** – Primary programming language
* **PostgreSQL** – Relational database
* **Npgsql / Entity Framework Core** – Database connectivity and data access
* **JWT** – Access-token authentication
* **Refresh Tokens** – Secure session renewal
* **REST API** – API architecture
* **Postman** – API testing
* **Git / GitHub** – Version control

## 📌 Features

### Authentication & Authorization

* User registration and login
* Secure password hashing
* JWT-based authentication
* Access and refresh token system
* Role-based authorization
* Protected API endpoints

### Product Management

* Create, update, and delete products
* Product categories
* Product images
* Product availability and inventory tracking
* Product reviews and ratings

### Shopping & Orders

* Shopping cart management
* Cart items
* Address management
* Order creation and management
* Order status tracking
* Order history

### Inventory

* Inventory tracking
* Inventory transactions
* Stock availability management

### Additional Features

* Wishlist functionality
* Coupons and discounts
* Payment records
* Notifications
* Audit logging

## 🏗️ Architecture

The backend follows a layered architecture to separate responsibilities and make the application easier to maintain and extend.

```text
Controllers
     ↓
Services
     ↓
Repositories
     ↓
PostgreSQL Database
```

### Main Components

**Controllers**
Handle HTTP requests, validation, responses, and API routing.

**Services**
Contain business logic and coordinate operations between controllers and repositories.

**Repositories**
Handle database access and isolate database-specific operations from the rest of the application.

**Models / Entities**
Represent the application's data structures and database entities.

## 🗄️ Database

The application uses **PostgreSQL** as its primary database.

The database contains entities including:

```text
Users
Roles
Permissions
Products
Categories
Inventory
InventoryTransactions
Orders
OrderItems
OrderStatusHistory
ShoppingCart
CartItems
Addresses
Reviews
Wishlists
Coupons
Payments
RefreshTokens
Notifications
AuditLogs
```

Relationships between these entities allow the system to manage the complete purchasing lifecycle from product browsing through to order management.

## 🔐 Authentication Flow

The API uses JWT access tokens together with refresh tokens.

```text
User
 ↓
Login
 ↓
Validate Credentials
 ↓
Generate Access Token
 ↓
Generate Refresh Token
 ↓
Authenticated API Requests
 ↓
Access Token Expires
 ↓
Refresh Token
 ↓
New Access Token
```

Passwords are never stored as plain text. Passwords are securely hashed before being stored in the database.

## ⚙️ Configuration

Create a local configuration containing your PostgreSQL connection string and authentication settings.

Example:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=ECommerce;Username=your_username;Password=your_password"
  }
}
```

**Do not commit passwords, JWT secrets, connection strings, or other credentials to GitHub.**

For development, sensitive configuration should be stored using environment variables or another secure configuration mechanism.

## ▶️ Running the Project

### 1. Clone the repository

```bash
git clone <your-repository-url>
cd <project-folder>
```

### 2. Configure PostgreSQL

Create the required PostgreSQL database and configure the connection string.

### 3. Restore dependencies

```bash
dotnet restore
```

### 4. Apply database migrations

```bash
dotnet ef database update
```

### 5. Run the API

```bash
dotnet run
```

The API will then be available through the configured ASP.NET Core development URL.

## 🧪 Testing

API endpoints can be tested using **Postman**.

Authentication endpoints can be used to obtain a JWT access token, which can then be supplied to protected endpoints using:

```text
Authorization: Bearer <access-token>
```

## 📁 Project Structure

```text
ECommerceBackend/
│
├── Controllers/
├── Services/
├── Repositories/
├── Models/
├── DTOs/
├── Data/
├── Migrations/
├── Middleware/
├── Helpers/
├── Program.cs
└── appsettings.json
```

## 🔒 Security

Security is an important part of the backend design.

The project implements:

* Password hashing
* JWT authentication
* Refresh-token authentication
* Role-based authorization
* Protected endpoints
* Input validation
* Database-level constraints
* Audit logging
* Environment-based configuration for sensitive values

## 🛣️ Future Improvements

Potential future additions include:

* Redis caching
* Rate limiting
* Automated integration testing
* Docker containerization
* CI/CD pipeline
* Payment gateway integration
* Email notifications
* Centralized logging
* API versioning
* Performance and load testing
* Python-based AI services

## 👨‍💻 Project Purpose

This project was developed as a portfolio application to demonstrate practical backend development using **C#, ASP.NET Core, REST APIs, PostgreSQL, authentication, database design, and software architecture**.

The long-term goal is to develop the application into a scalable e-commerce platform while applying production-oriented development practices.
