export async function fetchWeatherData(cityId: number) {
    const response = await fetch(`/api/weather/city/${cityId}`);
    if (!response.ok) throw new Error("Failed to fetch weather data");
    return await response.json();
}