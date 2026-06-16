document.addEventListener('DOMContentLoaded', () => {
    // Add hover event listeners to all nav links that trigger animation on burritos
    const setupBurritoAnimations = () => {
        const navLinks = document.querySelectorAll('.nav-link-item');
        const burritos = document.querySelectorAll('.nav-burrito-icon');

        navLinks.forEach(link => {
            // Because Blazor recreates DOM elements, we store whether we've already attached the listener
            // to avoid resetting the link node which destroys Blazors own event bindings
            if (!link.dataset.hasHoverListener) {
                link.dataset.hasHoverListener = 'true';

                link.addEventListener('mouseenter', () => {
                    burritos.forEach(burrito => {
                        burrito.classList.remove('spin-burrito', 'animate-spin-bounce');
                        // Force a reflow so the browser registers the removal
                        void burrito.offsetWidth;
                        // Add the animation class back
                        burrito.classList.add('spin-burrito');
                    });
                });

                link.addEventListener('mouseleave', () => {
                    // Wait for the animation (1.2s now) then clear
                    setTimeout(() => {
                        burritos.forEach(burrito => {
                            burrito.classList.remove('spin-burrito');
                        });
                    }, 1200); 
                });
            }
        });
    };

    // Need to handle Blazor changes
    const observer = new MutationObserver((mutations) => {
        setupBurritoAnimations();
    });

    observer.observe(document.body, { childList: true, subtree: true });

    // Initial setup
    setTimeout(setupBurritoAnimations, 100);
});