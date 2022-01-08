/* eslint-disable @typescript-eslint/no-var-requires */
const autoprefixer = require("autoprefixer")();
const tailwindcss = require("tailwindcss")("./tailwind.config.ts");

module.exports = {
  plugins: [tailwindcss, autoprefixer],
};
