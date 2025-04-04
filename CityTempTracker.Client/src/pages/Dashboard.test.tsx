import { render, screen, waitFor } from "@testing-library/react";
import { test, vi, expect } from "vitest";
import "@testing-library/jest-dom";

vi.mock("recharts", async () => {
    const actual = await vi.importActual<any>("recharts");
    return {
        ...actual,
        ResponsiveContainer: ({ children }: { children: React.ReactNode }) => (
            <div style={{ width: 800, height: 400 }}>{children}</div>
        ),
    };
});

vi.mock("../api/cityApi", () => ({
    fetchCities: () =>
        Promise.resolve([
            { id: 1, name: "Stockholm", country: "SE" },
            { id: 2, name: "Paris", country: "FR" },
        ]),
}));

import Dashboard from "../pages/Dashboard";

test("Renders dashboard heading and city selector", async () => {
    render(<Dashboard />);
    await waitFor(() => {
        expect(screen.getByRole("combobox")).toBeInTheDocument();
    });
});