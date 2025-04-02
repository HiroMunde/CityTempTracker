import React, { useEffect, useState } from "react";
import { fetchCities } from "../api/cityApi";
import { City } from "../types";

interface Props {
    onSelect: (cityId: number) => void;
}

const CitySelector: React.FC<Props> = ({ onSelect }) => {
    const [cities, setCities] = useState<City[]>([]);
    const [selectedId, setSelectedId] = useState<string>("");

    useEffect(() => {
        fetchCities().then(setCities).catch(console.error);
    }, []);

    const handleChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
        const value = e.target.value;
        setSelectedId(value);
        if (value !== "") {
            onSelect(Number(value));
        }
    };

    return (
        <select value={selectedId} onChange={handleChange}>
            <option value="" disabled hidden>Select a city...</option>
            {cities.map((city) => (
                <option key={city.id} value={city.id}>
                    {city.name}, {city.country}
                </option>
            ))}
        </select>
    );
};

export default CitySelector;