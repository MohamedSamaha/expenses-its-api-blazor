# Expenses API & Blazor Web UI

ITS Technical Assessment Submission

**Author:** Mohamed Samaha  
Senior .NET Engineer

---

## 📌 Overview

This project implements an **Expenses Management API** for a small team along with a **Blazor WebAssembly (WASM) UI**.

The solution includes:

- ASP.NET Core Web API
- EF Core with SQLite
- JWT Bearer Authentication
- CRUD for Expenses
- Validation rules
- Pagination & Filtering
- Optimistic Concurrency (RowVersion)
- Unit Tests
- Production-ready SQL migration script

---

# 🚀 How To Run The Project

## 1️⃣ Restore Packages

From the root folder (`Expenses`):

```bash
dotnet restore
```

---

## 2️⃣ Run API Project

```bash
dotnet run --project src/Expenses.Api --launch-profile https
```

API runs on:

```
https://localhost:7237
```

---

## 3️⃣ Run Blazor Web UI

Open a new terminal from the root:

```bash
dotnet run --project src/ExpensesWeb
```

UI runs on:

```
http://localhost:5108
```

---

# 🗄 Database Initialization

The project uses **SQLite**.

EF Core Migrations are enabled.

To recreate the database (PowerShell):

```bash
dotnet ef database update --project src/Expenses.Api --startup-project src/Expenses.Api
```

A production-ready SQL script is included:

```
deploy/InitialCreate.sql
```

This fulfills the migration requirement.

## 📦 Included SQLite Database

The repository includes a pre-generated SQLite database file (`expenses.db`) for convenience.

This allows the solution to run immediately after cloning.

The database schema is still fully managed via EF Core Migrations and a production-ready SQL script is provided in:

deploy/InitialCreate.sql

---

# 🔐 Authentication (JWT Bearer)

Only authenticated users may:

- Create expenses
- Update expenses
- Delete expenses

JWT Bearer authentication is configured in the API.

---

## 🧪 Test Credentials

Username: `admin`  
Password: `1234`

---

## 🔑 Login Example (API)

POST:

```
https://localhost:7237/api/auth/login
```

Body:

```json
{
  "username": "admin",
  "password": "1234"
}
```

Response:

```json
{
  "token": "eyJhbGciOiJIUzI1NiIs..."
}
```

Use this token in the request header:

```
Authorization: Bearer {token}
```

---

# 📦 API Endpoints

## 🔹 Create Expense

POST:

```
/api/expenses
```

Example:

```json
{
  "title": "Lunch",
  "amount": 100,
  "currency": "USD",
  "category": "Food",
  "occurredOn": "2026-02-23"
}
```

---

## 🔹 Get Expenses (with pagination & filtering)

Basic:

```
/api/expenses?page=1&pageSize=5
```

Filter by category:

```
/api/expenses?category=Food
```

Filter by date range:

```
/api/expenses?from=2026-01-01&to=2026-12-31
```

Combined:

```
/api/expenses?page=1&pageSize=5&category=Food&from=2026-01-01&to=2026-12-31
```

---

## 🔹 Update Expense

PUT:

```
/api/expenses/{id}
```

---

## 🔹 Delete Expense

DELETE:

```
/api/expenses/{id}
```

---

# ✅ Validation Rules

Validation is implemented using DataAnnotations in the Domain model.

### Title

- Required.

### Amount

- Must be greater than 0.

Invalid example:

```json
{
  "title": "Lunch",
  "amount": 0,
  "currency": "USD",
  "category": "Food",
  "occurredOn": "2026-02-23"
}
```

Returns:

```
400 Bad Request
Amount must be greater than 0.
```

---

### Currency

Allowed values:

- EGP
- USD
- EUR

Invalid example:

```json
{
  "currency": "BTC"
}
```

Returns validation error.

---

# ⭐ Bonus Features Implemented

## 1️⃣ Optimistic Concurrency

Implemented using:

```
RowVersion (Timestamp)
```

If two users update the same expense simultaneously:

- API returns 409 Conflict
- Prevents data overwrite

---

## 2️⃣ Pagination & Filtering

Supports:

- Pagination (page & pageSize)
- Filtering by Category
- Filtering by OccurredOn date range

Implemented directly at API query level.

---

## 3️⃣ Unit Tests

Includes tests for:

- Validation logic (Amount > 0)
- Currency validation
- Filtering by category
- Pagination behavior

Run tests:

```bash
dotnet test
```

---

# 🏗 Project Structure

```
Expenses/
│
├── deploy/
│   └── InitialCreate.sql
│
├── src/
│   ├── Expenses.Api/
│   │   ├── Controllers/
│   │   │   ├── AuthController.cs
│   │   │   └── ExpensesController.cs
│   │   │
│   │   ├── Data/
│   │   │   └── AppDbContext.cs
│   │   │
│   │   ├── Dtos/
│   │   │   ├── ExpenseDtos.cs
│   │   │   └── LoginRequest.cs
│   │   │
│   │   ├── Migrations/
│   │   │
│   │   ├── Program.cs
│   │   ├── appsettings.json
│   │   └── Expenses.Api.csproj
│   │
│   ├── Expenses.Domain/
│   │   ├── Expense.cs
│   │   └── Expenses.Domain.csproj
│   │
│   ├── Expenses.Tests/
│   │   ├── ValidationTests.cs
│   │   ├── FilteringTests.cs
│   │   ├── PaginationTests.cs
│   │   └── Expenses.Tests.csproj
│   │
│   └── ExpensesWeb/ (Blazor WASM)
│       ├── Pages/
│       │   ├── Index.razor
│       │   ├── Login.razor
│       │   ├── Counter.razor
│       │   └── NotFound.razor
│       │
│       ├── Layout/
│       ├── wwwroot/
│       ├── App.razor
│       └── ExpensesWeb.csproj
│
├── Expenses.sln
└── README.md

---

## 🧠 Notable Decisions & Tradeoffs

- Built using .NET 8 and EF Core.
- SQLite selected for simplicity and portability.
- JWT implemented without ASP.NET Identity to keep solution lightweight.
- CreatedByUserId is automatically set from the authenticated JWT claim.
- Domain validation implemented using DataAnnotations.
- API returns paged response wrapper for scalability.
- Optimistic concurrency handled using EF Core RowVersion.

---

# 📌 Summary

This solution fulfills:

✔ API & Persistence
✔ JWT Authentication
✔ EF Core Migrations
✔ SQL Script for Deployment
✔ Blazor WASM UI
✔ Pagination & Filtering
✔ Optimistic Concurrency
✔ Unit Tests

---
```
