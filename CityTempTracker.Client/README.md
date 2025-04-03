# Testdokumentation för CityTempTracker.Client

Detta dokument beskriver de olika testerna som finns i projektet [CityTempTracker.Client](https://github.com/HiroMunde/CityTempTracker/tree/master/CityTempTracker.Client).

## Översikt

Projektet använder [Vitest](https://vitest.dev/) som testverktyg för att säkerställa funktionaliteten hos React-komponenterna. Tester är placerade i `src/pages`-katalogen och följer namngivningskonventionen `KomponentNamn.test.tsx`.

## Tester

### Dashboard.test.tsx

Denna testfil innehåller enhetstester för `Dashboard`-komponenten. Testerna verifierar följande funktionaliteter:

1. **Renderar dashboard-rubrik och stadsvalskomponent:**
   - Säkerställer att `Dashboard`-komponenten renderar en `combobox` (dropdown) för att välja stad.

   **Testimplementering:**
   ```tsx
   test("Renders dashboard heading and city selector", async () => {
       render(<Dashboard />);
       await waitFor(() => {
           expect(screen.getByRole("combobox")).toBeInTheDocument();
       });
   });
   ```

2. **Uppdaterar diagrammet när en stad väljs:**
   - Verifierar att när en stad väljs från dropdown-menyn, uppdateras diagrammet med temperaturdata för den valda staden.
   - Testet mockar API-anrop för att hämta väderdata och använder `fireEvent` för att simulera användarinteraktion.

   **Testimplementering:**
   ```tsx
   test("Updates chart when a city is selected", async () => {
       render(<Dashboard />);

       const select = await screen.findByRole("combobox");
       fireEvent.change(select, { target: { value: "1" } });

       const maxLine = await screen.findByTestId("max-line");
       expect(maxLine).toBeInTheDocument();
   });
   ```

## Mockning av moduler

För att isolera testerna och undvika beroenden till externa API:er används mockning:

- **Mockning av `fetchCities` från `cityApi`:**
  - Returnerar en lista med exempelstäder för att simulera API-svaret.

  **Mockimplementering:**
  ```tsx
  vi.mock("../api/cityApi", () => ({
      fetchCities: () =>
          Promise.resolve([
              { id: 1, name: "Stockholm", country: "SE" },
              { id: 2, name: "Paris", country: "FR" },
          ]),
  }));
  ```

- **Mockning av `fetchWeatherData` från `weatherApi`:**
  - Returnerar exempeldata för väder för att simulera API-svaret.

  **Mockimplementering:**
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

## Testkonfiguration

Projektet använder `vitest.config.ts` för att konfigurera testmiljön. Eventuella specifika inställningar för testning, såsom mockning av moduler eller globala variabler, hanteras i denna fil.

## Körning av tester

För att köra testerna, använd följande kommando i projektets rotkatalog:

```bash
npm test
```

Detta kommando kör alla testfiler och visar resultatet i terminalen.

---

Denna dokumentation syftar till att ge en översikt över de befintliga testerna i projektet och underlätta förståelsen för deras syfte och implementering.