(globalThis as any).ResizeObserver = class {
    observe() {}
    unobserve() {}
    disconnect() {}
};

import { render, screen } from "@testing-library/react";
import TemperatureChart from "../components/TemperatureChart";
import { expect, test } from "vitest";

test("renders TemperatureChart without crashing", () => {
    const data = [
        {
            timestamp: new Date().toISOString(),
            temperature: 10,
            name: "Stockholm",
            country: "SE"
        },
        {
            timestamp: new Date().toISOString(),
            temperature: 15,
            name: "Stockholm",
            country: "SE",
        },
    ];

    render(<TemperatureChart data={data} width={500} height={400} />);
    expect(screen.getByText((content) => content.includes("Max temp"))).toBeInTheDocument();
    expect(screen.getByText((content) => content.includes("Min temp"))).toBeInTheDocument();
});