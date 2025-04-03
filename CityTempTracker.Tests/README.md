# CityTempTracker.Tests

This project contains all tests for the [CityTempTracker](https://github.com/HiroMunde/CityTempTracker) backend.

---

## Test Structure

```plaintext
CityTempTracker.Tests/
├── Mocks/
│   ├── EmptyWeatherResponseHandler.cs  # Mocks empty API responses
│   ├── ErrorResponseHandler.cs         # Mocks API failure responses (500)
│   └── MockHttpMessageHandler.cs       # Basic mock handler for simulating API responses
├── CityControllerTests.cs              # Tests the city API controller
├── WeatherControllerTests.cs           # Tests the weather API controller
├── WeatherServiceTests.cs              # Unit tests for WeatherService (with mock HTTP)
├── WeatherUpdaterTests.cs              # Background task scheduler tests
├── SeedDataTests.cs                    # Tests the initial seeding logic
```

---

## Test Categories

### `CityControllerTests.cs`

- Verifies that all cities are returned
- Uses in-memory SQLite
- Supports validation of API return shape (anonymous objects)

### `WeatherControllerTests.cs`

- Returns weather history for a city
- Handles unknown city ID (returns empty list)
- Uses in-memory SQLite and controller instance directly

### `WeatherServiceTests.cs`

- Tests that weather data is saved to the database
- Handles null/invalid API responses
- Includes:
  - `SavesWeatherData`
  - `DoesNotSave_WhenApiReturnsEmptyMain`
  - `UpdateWeatherAsync_DoesNotThrow_OnApiFailure`

### `WeatherUpdaterTests.cs`

- Tests that the background service:
  - Triggers weather updates regularly
  - Does not crash when `IWeatherService` throws
- Uses mock of `IServiceScopeFactory`

### `SeedDataTests.cs`

- Ensures seed logic:
  - Adds cities only when the DB is empty
  - Avoids duplicate insertions

---

## Running tests

From the root of the repo:

```bash
cd CityTempTracker.Tests
dotnet test
```

To run a single file:

```bash
dotnet test --filter FullyQualifiedName~WeatherServiceTests
```

---

## Notes

- All tests use the in-memory SQLite provider (`UseInMemoryDatabase`)
- No external dependencies (e.g. real API calls) are invoked
- Mocks are provided inline in the test project
