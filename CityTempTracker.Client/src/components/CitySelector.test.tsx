import { render, screen, waitFor } from "@testing-library/react";
import CitySelector from "./CitySelector";
import {expect, test, vi } from "vitest";
import '@testing-library/jest-dom';

vi.mock("../api/cityApi", () => ({
  fetchCities: () =>
    Promise.resolve([
      { id: 1, name: "Stockholm", country: "SE" },
      { id: 2, name: "Paris", country: "FR" },
    ]),
}));

test("renders city options", async () => {
  render(<CitySelector onSelect={() => {}} />);
  await waitFor(() => {
    expect(screen.getByText("Stockholm, SE")).toBeInTheDocument();
    expect(screen.getByText("Paris, FR")).toBeInTheDocument();
  });
});

test("calls onSelect with selected city id", async () => {
    const handleSelect = vi.fn();
    render(<CitySelector onSelect={handleSelect} />);
    const select = (await screen.findByRole("combobox")) as HTMLSelectElement;
    await waitFor(() => screen.getByText("Paris, FR"));

    select.value = "2";
    select.dispatchEvent(new Event("change", { bubbles: true }));

    expect(handleSelect).toHaveBeenCalledWith(2);
});
