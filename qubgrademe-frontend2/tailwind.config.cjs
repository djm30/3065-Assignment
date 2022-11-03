/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./index.html", "./src/**/*.{js,ts,jsx,tsx}"],
  theme: {
    extend: {
      colors: {
        accentBlue: "#1768AC",
        accentHoverBlue: "#06BEE1",
      },
    },
  },
  plugins: [],
};
