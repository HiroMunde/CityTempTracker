import { defineConfig } from 'vitest/config';

export default defineConfig({
    test: {
        setupFiles: './src/components/CitySelector.test.tsx',
        globals: true,
        environment: 'jsdom',
    },
});