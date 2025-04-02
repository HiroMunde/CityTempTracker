import React, { useState, useEffect } from "react";
import CitySelector from "../components/CitySelector";
import TemperatureChart from "../components/TemperatureChart";
import { fetchWeatherData } from "../api/weatherApi";
import type { WeatherPoint } from "../types";

const Dashboard: React.FC = () => {
    const [cityId, setCityId] = useState<number | null>(null);
    const [weatherData, setWeatherData] = useState<WeatherPoint[]>([]);
    const [lastUpdated, setLastUpdated] = useState<Date | null>(null);

    const loadData = (id: number) => {
        fetchWeatherData(id)
            .then((data: WeatherPoint[]) => {
                const sorted = data.sort(
                    (a: WeatherPoint, b: WeatherPoint) =>
                        new Date(a.timestamp).getTime() - new Date(b.timestamp).getTime()
                );
                setWeatherData(sorted);
                setLastUpdated(new Date());
            })
            .catch(console.error);
    };


    useEffect(() => {
        if (cityId !== null) {
            loadData(cityId);
        }
    }, [cityId]);

    useEffect(() => {
        if (cityId === null) return;

        const intervalId = setInterval(() => {
            loadData(cityId);
        }, 60 * 1000);

        return () => clearInterval(intervalId);
    }, [cityId]);

    return (
        <div style={{ display: "flex", flexDirection: "column", alignItems: "center" }}>
            <h2>CityTempTracker</h2>
            <div style={{ marginBottom: "1rem" }}>
                <CitySelector onSelect={setCityId} />
            </div>
            <div style={{ width: "90vw", maxWidth: "1200px" }}>
                {weatherData.length > 0 && <TemperatureChart data={weatherData} />}
                <p style={{ marginTop: "1rem", fontStyle: "italic", textAlign: "right", width: "100%" }}>
                    Last updated: {lastUpdated?.toLocaleTimeString("sv-SE")} (Local time)
                </p>
            </div>
        </div>
    );
};

export default Dashboard;
