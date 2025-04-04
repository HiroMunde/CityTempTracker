# CityTempTracker.Client

This project contains unit tests for React components in a weather dashboard application. Tests are written using [Vitest](https://vitest.dev/) and [React Testing Library](https://testing-library.com/docs/react-testing-library/intro/).

## Test Structure

```plaintext
CityTempTracker.Client/
└── src/
    ├── components/
    │   ├── CitySelector.test.tsx       # Unit tests for CitySelector component
    │   └── TemperatureChart.test.tsx   # Unit tests for TemperatureChart component
    └── pages/
        └── Dashboard.test.tsx          # Integration tests for the Dashboard page
```

---

## Dashboard.test.tsx

### Purpose

Tests the `Dashboard` page, including integration of `CitySelector` and `TemperatureChart`.

### Mocked Modules

- `recharts`: `ResponsiveContainer` is replaced with a static `<div>` of fixed size.
- `../api/cityApi`: Returns mocked city data.
- `../api/weatherApi`: Returns mocked weather data.

### Tests

- Renders the component and ensures the city dropdown appears.
- Updates the chart when a city is selected and verifies temperature data is rendered.

---

## CitySelector.test.tsx

### Purpose

Tests the `CitySelector` component.

### Mocked Modules

- `../api/cityApi`: Returns mocked city data.

### Tests

- Renders the list of city options.
- Calls `onSelect` with the selected city ID when a selection is made.

---

## TemperatureChart.test.tsx

### Purpose

Tests the `TemperatureChart` component.

### Setup

- A global mock for `ResizeObserver` is defined due to `recharts` dependency on it.

### Tests

- Renders without crashing.

---

## Running the Tests

```bash
npx vitest run
```

---

## Technologies Used

- **Vitest** – test runner
- **React Testing Library** – component rendering and DOM interaction
- **jest-dom** – extended DOM matchers
- **vi.mock** – mocking API calls and third-party components
