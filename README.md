# Backend Playground 🚀

This repository is a **personal backend playground** created to experiment with, compare, and reference **.NET backend technologies**.

The goal is to build **realistic, clean, and modular examples** that reflect real-world backend development scenarios.

---

## 🎯 Goals

* Practice .NET Web API development
* Experiment with Entity Framework Core
* Integrate Redis caching
* Configure Swagger / OpenAPI properly
* Use Docker for local development
* Try microservice & modular architectures

---

## 🧱 Repository Structure

```
Backend-Playground/
│
├── dbConnections/
│ ├── backEndDbConnection/
│ │ ├── Controllers/
│ │ ├── Data/
│ │ ├── Models/
│ │ ├── Program.cs
│ │ └── appsettings.json
│ │
│ └── PaginationApi/
│ ├── Controllers/
│ ├── Repositories/
│ ├── Models/
│ ├── Program.cs
│ └── appsettings.json
│
├── .gitignore
├── README.md
└── Backend-Playground.sln
```

---

## 🧰 Technologies Used

* **.NET 9 Web API**
* **Entity Framework Core**
* **SQL Server**
* **Redis (StackExchange.Redis)**
* **Swagger / Swashbuckle**
* **Docker (optional)**

---

## ▶️ Running a Project

Each folder under the repository represents an **independent solution/project**.

1. Open `Backend-Playground.sln`
2. Select the project you want to run as **Startup Project**
3. Restore dependencies:

```bash
dotnet restore
```

4. Run the project:

```bash
dotnet run
```

5. Open Swagger:

```
https://localhost:{PORT}/swagger
```

---

## 🔌 Configuration

* Database connection strings are defined in `appsettings.json`
* Sensitive values (connection strings, secrets, env files) are **not committed**
* Redis should be running locally (Docker recommended)

Example Redis container:

```bash
docker run -d -p 6379:6379 redis
```

---

## 📌 Notes

* This repository is **not a production project**
* Each project may use different patterns or approaches intentionally
* Focus is on learning, testing, and comparison

---

## 📬 License

This repository is for **educational and experimental purposes**.

Feel free to fork, explore, and adapt it for your own learning.
