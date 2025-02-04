/** @type {import('tailwindcss').Config} */
module.exports = {
	content: [
		'./**/*.{razor,html}',
		'./**/(Layout|Pages)/*.{razor,html}',
	],
	mode: "jit",
	purge: false,
	theme: {
		extend: {
			
		}
	},
	plugins: [],
}

