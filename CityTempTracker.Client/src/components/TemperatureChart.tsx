import React from "react";
import {
    LineChart,
    Line,
    XAxis,
    YAxis,
    CartesianGrid,
    Tooltip,
    Legend,
    ResponsiveContainer,
    ReferenceLine
} from "recharts";
import type { WeatherPoint } from "../types";

interface Props {
    data: WeatherPoint[];
}


const TemperatureChart: React.FC<Props> = ({ data }) => {
    const temps = data.map((d) => d.temperature);
    const maxTemp = Math.max(...temps);
    const minTemp = Math.min(...temps);
    return (
        <ResponsiveContainer width="100%" height={400}>
            <LineChart data={data} margin={{ top: 20, right: 30, left: 0, bottom: 5 }}>
                <CartesianGrid strokeDasharray="3 3" />
                <XAxis
                    dataKey="timestamp"
                    tickFormatter={(t) =>
                        new Date(t).toLocaleTimeString("sv-SE", {
                            hour: "2-digit",
                            minute: "2-digit",
                            timeZone: "Europe/Stockholm",
                        })
                    }
                    label={{ value: "Time", position: "bottom", offset: 10 }}
                />
                <YAxis
                    domain={["auto", "auto"]}
                />
                <ReferenceLine y={maxTemp} stroke="red" label="Max temp" />
                <ReferenceLine y={minTemp} stroke="blue" label="Min temp" />
                <Tooltip />
                <Line type="monotone" dataKey="temperature" />
            </LineChart>
        </ResponsiveContainer>
    );
};

export default TemperatureChart;