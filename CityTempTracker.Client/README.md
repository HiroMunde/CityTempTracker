# CityTempTracker.Client

This document describes the tests implemented in the [CityTempTracker.Client](https://github.com/HiroMunde/CityTempTracker/tree/master/CityTempTracker.Client) project.

## Overview

The project uses [Vitest](https://vitest.dev/) as the testing framework to verify the functionality of React components. Tests are located in the `src/pages` directory and follow the naming convention `ComponentName.test.tsx`.

## Tests

### Dashboard.test.tsx

This test file contains unit tests for the `Dashboard` component. The tests verify the following functionalities:

1. **Renders dashboard heading and city selector:**
   - Ensures the `Dashboard` component renders a `combobox` (dropdown) for selecting a city.

   **Test Implementation:**
   ```tsx
   test("Renders dashboard heading and city selector", async () => {
       render(<Dashboard />);
       await waitFor(() => {
           expect(screen.getByRole("combobox")).toBeInTheDocument();
       });
   });
   ```

2. **Updates chart when a city is selected:**
   - Verifies that when a city is selected from the dropdown, the chart updates with temperature data for the selected city.
   - The test mocks API calls to fetch weather data and uses `fireEvent` to simulate user interaction.

   **Test Implementation:**
   ```tsx
   test("Updates chart when a city is selected", async () => {
       render(<Dashboard />);

       const select = await screen.findByRole("combobox");
       fireEvent.change(select, { target: { value: "1" } });

       const maxLine = await screen.findByTestId("max-line");
       expect(maxLine).toBeInTheDocument();
   });
   ```

## Module Mocking

To isolate the tests and avoid dependencies on external APIs, module mocking is used:

- **Mocking `fetchCities` from `cityApi`:**
  - Returns a list of sample cities to simulate the API response.

  **Mock Implementation:**
  ```tsx
  vi.mock("../api/cityApi", () => ({
      fetchCities: () =>
          Promise.resolve([
              { id: 1, name: "Stockholm", country: "SE" },
              { id: 2, name: "Paris", country: "FR" },
          ]),
  }));
  ```

- **Mocking `fetchWeatherData` from `weatherApi`:**
  - Returns sample weather data to simulate the API response.

  **Mock Implementation:**
  ```tsx
  vi.mock("../api/weatherApi", () => ({
      fetchWeatherData: vi.fn(() =>
          Promise.resolve([
              {
                  timestamp: new Date().toISOString(),
                  temperature: 12.3,
                  name: "Stockholm",
                  country: "SE",
              },
              {
                  timestamp: new Date(Date.now() - 60000).toISOString(),
                  temperature: 14.7,
                  name: "Stockholm",
                  country: "SE",
              },
          ])
      ),
  }));
  ```

## Test Configuration

The project uses `vitest.config.ts` to configure the test environment. Any specific test setup, such as module mocks or global variables, is handled in this file.

## Running the Tests

To run the tests, use the following command in the project root:

```bash
npm test
```

This command will execute all test files and display the results in the terminal.

---

This documentation aims to provide an overview of the existing tests in the project and help developers understand their purpose and implementation.
