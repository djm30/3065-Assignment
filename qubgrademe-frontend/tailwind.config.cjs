/** @type {import('tailwindcss').Config} */
module.exports = {
    content: ["./index.html", "./src/**/*.{js,ts,jsx,tsx}"],
    theme: {
        extend: {
            colors: {
                accentBlue: "#426cff",
                accentHoverBlue: "#0057ff",
                purple: "#9559d1",
                purupleHover: "#a96fe3",
            },
        },
    },
    plugins: [],
};
