export async function fetchCities() {
    const response = await fetch("/api/city");
    if (!response.ok) throw new Error("Failed to fetch cities");
    return await response.json();
}