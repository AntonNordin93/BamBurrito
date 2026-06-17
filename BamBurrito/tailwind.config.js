/** @type {import('tailwindcss').Config} */
module.exports = {
    content: [
        './**/*.{razor,html,cshtml}',
        '../BamBurrito.Web.Client/**/*.razor'
    ],
    theme: {
        extend: {
            colors: {
                bamorange: '#FF8400', // Din exakta, krispiga orangea signaturfärg!
            },
            fontFamily: {
                bangers: ['Bangers', 'system-ui'],
                trueno: ['Trueno', 'orig_trueno_round_bold', 'system-ui'],
                anton: ['Anton', 'system-ui'],
            },
            keyframes: {
                spinbounce: {
                    '0%, 100%': { transform: 'rotate(0deg) scale(1)' },
                    '50%': { transform: 'rotate(180deg) scale(1.3)' },
                    '100%': { transform: 'rotate(360deg) scale(1)' }
                }
            },
            animation: {
                'spin-bounce': 'spinbounce 0.6s ease-in-out'
            }
        },
    },
    plugins: [],
}