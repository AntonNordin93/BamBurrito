document.addEventListener('DOMContentLoaded', () => {
    // Add hover event listeners to all nav links that trigger animation on burritos
    const setupBurritoAnimations = () => {
        const navLinks = document.querySelectorAll('.nav-link-item');
        const burritos = document.querySelectorAll('.nav-burrito-icon');

        navLinks.forEach(link => {
            link.addEventListener('mouseenter', () => {
                burritos.forEach(burrito => {
                    // Add animation class
                    burrito.classList.add('animate-spin-bounce');
                });
            });

            link.addEventListener('mouseleave', () => {
                burritos.forEach(burrito => {
                    // The CSS animation will finish its iteration or we can stop it
                    setTimeout(() => {
                        burrito.classList.remove('animate-spin-bounce');
                    }, 500); // Give it time to finish spinning if we want
                });
            });
        });
    };

    // Need to handle Blazor changes
    const observer = new MutationObserver((mutations) => {
        setupBurritoAnimations();
    });

    observer.observe(document.body, { childList: true, subtree: true });

    // Initial setup
    setupBurritoAnimations();
});