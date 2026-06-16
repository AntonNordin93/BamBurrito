/** @type {import('tailwindcss').Config} */
module.exports = {
    content: [
        './**/*.{razor,html,cshtml}',
        '../BamBurrito.Web.Client/**/*.razor'
    ],
    theme: {
        extend: {
            colors: {
                bamorange: '#e64e1c',
            }
        },
    },
    plugins: [],
}