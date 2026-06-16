document.addEventListener('DOMContentLoaded', () => {
    // Keep track of active hovers so animation continues across buttons seamlessly
    let activeHovers = 0;

    const setupBurritoAnimations = () => {
        const navLinks = document.querySelectorAll('.nav-link-item');
        const burritos = document.querySelectorAll('.nav-burrito-icon');

        navLinks.forEach(link => {
            if (!link.dataset.hasHoverListener) {
                link.dataset.hasHoverListener = 'true';

                link.addEventListener('mouseenter', () => {
                    activeHovers++;
                    burritos.forEach(burrito => {
                        // Only add if not already animated to prevent restarting/stuttering
                        if (!burrito.classList.contains('spin-burrito')) {
                            burrito.classList.add('spin-burrito');
                        }
                    });
                });

                link.addEventListener('mouseleave', () => {
                    activeHovers--;

                    // Give a tiny grace period to see if they moved to another button
                    setTimeout(() => {
                        if (activeHovers <= 0) {
                            activeHovers = 0; // safe guard
                            // Only remove if we really left the nav
                            burritos.forEach(burrito => {
                                burrito.classList.remove('spin-burrito');
                            });
                        }
                    }, 50); 
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