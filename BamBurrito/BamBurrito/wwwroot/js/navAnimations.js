window.registerBurritoHoverAnimations = function () {
    const navLinks = document.querySelectorAll('.nav-link-item');
    const burritos = document.querySelectorAll('.nav-burrito-icon');

    navLinks.forEach(link => {
        link.addEventListener('mouseenter', () => {
            burritos.forEach(burrito => {
                // Remove the class first to restart the animation if it was already playing
                burrito.classList.remove('spin-burrito');
                
                // Force a reflow so the browser registers the removal
                void burrito.offsetWidth;
                
                // Add the animation class back
                burrito.classList.add('spin-burrito');
            });
        });
    });
};
