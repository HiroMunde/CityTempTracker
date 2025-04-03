import { render, screen, waitFor } from "@testing-library/react";
import Dashboard from "../pages/Dashboard";
import { expect, test, vi } from "vitest";
import '@testing-library/jest-dom';

vi.mock("../api/cityApi", () => ({
    fetchCities: () =>
        Promise.resolve([
            { id: 1, name: "Stockholm", country: "SE" },
        ]),
}));

vi.mock("../api/weatherApi", () => ({
    fetchWeatherData: () =>
        Promise.resolve([
            { timestamp: new Date().toISOString(), temperature: 12.3 },
        ]),
}));

test("renders dashboard heading and city selector", async () => {
    render(<Dashboard />);
    await waitFor(() => {
        expect(screen.getByText("CityTempTracker")).toBeInTheDocument();
        expect(screen.getByRole("combobox")).toBeInTheDocument();
    });
});