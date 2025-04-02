# 📘 CityTempTracker

## 📆 Innehåll

1. [Förutsättningar](#1-förutsättningar)
2. [Installera beroenden](#2-installera-beroenden)
3. [Köra applikationen](#3-köra-applikationen)
4. [Miljöinställningar](#4-miljöinställningar)
5. [Testa applikationen](#5-testa-applikationen)
6. [Databas](#6-databas)
7. [Strukturöversikt](#7-strukturöversikt)

---

## 1. ✅ Förutsättningar

Se till att du har följande installerat:

| Program                                                              | Version                              |
| -------------------------------------------------------------------- | ------------------------------------ |
| [.NET 6 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) | 6.0.x                                |
| [Node.js](https://nodejs.org/)                                       | ≥ 18                                 |
| [npm](https://www.npmjs.com/)                                        | ≥ 8                                  |
| [SQLite](https://www.sqlite.org/index.html) *(valfritt CLI)*         | valfri                               |
| **Valfri IDE**                                                       | Visual Studio 2022 / Rider / VS Code |

---

## 2. 📥 Installera beroenden

### 💥 Backend (ASP.NET Core)

```bash
cd CityTempTracker.Server
dotnet restore
```

### 🌐 Frontend (React + Vite)

```bash
cd citytemptracker.client
npm install
```

---

## 3. ▶️ Köra applikationen

### 📦 Backend

```bash
cd CityTempTracker.Server
dotnet run
```

Startar på t.ex. `http://localhost:5192`

### 💻 Frontend

```bash
cd citytemptracker.client
npm run dev
```

Frontend öppnas på t.ex. `https://localhost:52361`

---

## 4. ⚙️ Miljöinställningar

### 🔐 API-nyckel (OpenWeatherMap)

Lägg till i `CityTempTracker.Server/appsettings.json`:

```json
"OpenWeatherMap": {
  "ApiKey": "DIN_API_NYCKEL_HÄR"
}
```

---

## 5. 🧪 Testa applikationen

### ↻ Backendtester (xUnit)

```bash
cd CityTempTracker.Tests
dotnet test
```

### 🔬 Frontendtester (Vitest + Testing Library)

```bash
cd citytemptracker.client
npx vitest run
```

eller watch-läge:

```bash
npx vitest
```

---

## 6. 🗄️ Databas (SQLite)

- SQLite används som datalager
- Databasen migreras med:

```bash
cd CityTempTracker.Server
dotnet ef migrations add InitialCreate
dotnet ef database update
```

- `Cities` fylls initialt automatiskt
- `WeatherData` uppdateras var minut

---

## 7. 📂 Strukturöversikt

```plaintext
CityTempTracker/
├── CityTempTracker.Server/        ⬅ ASP.NET Core-backend (.NET 6)
│   ├── Controllers/
│   ├── Services/                  ⬅ WeatherService, scheduler
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
├── CityTempTracker.Tests/        ⬅ xUnit-testprojekt för backend
└── README.md                     ⬅ Denna fil
```

---

> ✉ Tips: Använd `npm run test` eller `dotnet test` för att verifiera kodkvalitet innan du deployar.
