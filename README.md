# CityTempTracker

## Table of Contents

1. [Requirements](#1-requirements)
2. [Install dependencies](#2-install-dependencies)
3. [Environment settings](#3-environment-settings)
4. [Database](#4-database)
5. [Testing the application](#5-testing-the-application)
6. [Running the application](#6-running-the-application)
7. [Project structure](#7-project-structure)

---

## 1. Requirements

Make sure you have the following installed:

| Tool                                                                 | Version               |
|----------------------------------------------------------------------|------------------------|
| [.NET 6 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) | 6.0.x                 |
| [Node.js](https://nodejs.org/)                                       | ≥ 18                  |
| [npm](https://www.npmjs.com/)                                        | ≥ 8                   |
| [SQLite](https://www.sqlite.org/index.html) *(optional CLI)*         | optional              |
| **Any IDE**                                                          | Visual Studio / Rider / VS Code |

---

## 2. Install dependencies

### Backend (ASP.NET Core)

```bash
cd CityTempTracker.Server
dotnet restore
```

### Frontend (React + Vite)

```bash
cd CityTempTracker.Client
npm install
```

---

## 3. Environment settings

### API key (OpenWeatherMap)

Create `CityTempTracker.Server/appsettings.json`:

```json
{
    "ConnectionStrings": {
        "DefaultConnection": "Data Source=weather.db"
    },
    "OpenWeatherMap": {
        "ApiKey": "YOUR_API_KEY_HERE"
    },
    "Logging": {
        "LogLevel": {
            "Default": "Information",
            "Microsoft.AspNetCore": "Warning"
        }
    },
    "AllowedHosts": "*"
}
```

---

## 4. Database

- SQLite is used as a data store
- Run the following to migrate and create the DB:

```bash
cd CityTempTracker.Server
dotnet ef migrations add InitialCreate
dotnet ef database update
```

- `Cities` table is seeded automatically
- `WeatherData` is updated every minute

---

## 5. Testing the application

### Backend tests (xUnit)

```bash
cd CityTempTracker.Tests
dotnet test
```

### Frontend tests (Vitest + Testing Library)

```bash
cd citytemptracker.client
npx vitest run
```

or watch mode:

```bash
npx vitest
```

---

## 6. Running the application

### Backend

```bash
cd CityTempTracker.Server
dotnet run
```

Usually starts at `http://localhost:5192`

### Frontend

```bash
cd citytemptracker.client
npm run dev
```

Opens in browser at `https://localhost:52361`

---

## 7. Project structure

```plaintext
CityTempTracker/
├── CityTempTracker.Server/        ⬅ ASP.NET Core backend (.NET 6)
│   ├── Controllers/
│   ├── Services/                  ⬅ WeatherService, background tasks
│   ├── Data/                      ⬅ DbContext, Models
│   └── appsettings.json
│
├── citytemptracker.client/       ⬅ React + TypeScript + Vite
│   ├── src/
│   │   ├── components/
│   │   ├── pages/
│   │   ├── types.ts
│   │   └── main.tsx
│   └── vite.config.js
│
├── CityTempTracker.Tests/        ⬅ xUnit backend tests
└── README.md                 ⬅ This file
```

---
