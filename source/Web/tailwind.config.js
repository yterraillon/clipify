module.exports = {
    theme: {
        extend: {
            colors: {
                palespringbud: '#E2EDB0',
                mediumaquamarine: '#84DC9E',
                emerald: '#49CF6B',
                mediumseagreen: '#31BA6E',
                yinblue: '#2F4B98',
                defaultgrey: '#9CA3AF',
                lightgrey: '#D1D5DB',
            }
        },
        inset: {
            '0': 0,
            '5': '5rem',
            '9': '9rem',
            '10': '10rem',
            '20': '20rem',
            '30': '30rem',
            '40': '40rem',
            auto: 'auto',
            '1/2': '50%',
        },
        cursor: {
            auto: 'auto',
            default: 'default',
            pointer: 'pointer',
            text: 'text',
            'not-allowed': 'not-allowed',
            grab: 'grab',
            grabbing: 'grabbing',
        }
    },
    variants: {
        textColor: ['hover', 'focus', 'visited'],
        cursor: ['active'],
    },
};