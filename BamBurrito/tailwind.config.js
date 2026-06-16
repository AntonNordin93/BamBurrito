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
            },
            fontFamily: {
                bangers: ['Bangers', 'system-ui'],
                trueno: ['Trueno', 'orig_trueno_round_bold', 'system-ui'],
                anton: ['Anton', 'system-ui'],
            }
        },
    },
    plugins: [],
}