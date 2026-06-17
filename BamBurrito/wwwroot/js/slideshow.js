document.addEventListener('DOMContentLoaded', () => {
    let currentIndex = 0;
    let slideshowTimer = null;

    const setupHeroSlideshow = () => {
        const container = document.getElementById('hero-slideshow');
        if (!container) return;

        const slides = container.querySelectorAll('.slide-img');
        if (slides.length === 0) return;

        // Om Blazor renderat om sidan synkar vi bara klasserna så rätt bild visas
        if (slideshowTimer && container.dataset.hasSlideshow === 'true') {
            slides.forEach((slide, idx) => {
                if (idx === currentIndex) {
                    slide.classList.remove('opacity-0');
                    slide.classList.add('opacity-40');
                } else {
                    slide.classList.remove('opacity-40');
                    slide.classList.add('opacity-0');
                }
            });
            return;
        }

        // Initiera timern första gången behållaren dyker upp
        container.dataset.hasSlideshow = 'true';
        if (slideshowTimer) clearInterval(slideshowTimer);

        slideshowTimer = setInterval(() => {
            const activeContainer = document.getElementById('hero-slideshow');
            if (!activeContainer) return;

            const activeSlides = activeContainer.querySelectorAll('.slide-img');
            if (activeSlides.length === 0) return;

            // Tona ut nuvarande bild
            activeSlides[currentIndex].classList.remove('opacity-40');
            activeSlides[currentIndex].classList.add('opacity-0');

            // Byt till nästa index
            currentIndex = (currentIndex + 1) % activeSlides.length;

            // Tona in nästa bild
            activeSlides[currentIndex].classList.remove('opacity-0');
            activeSlides[currentIndex].classList.add('opacity-40');
        }, 5000); // 5000ms = 5 sekunder per bild
    };

    // Lyssna på Blazors DOM-förändringar
    const observer = new MutationObserver(() => {
        setupHeroSlideshow();
    });

    observer.observe(document.body, { childList: true, subtree: true });

    // Kör direkt vid start
    setTimeout(setupHeroSlideshow, 100);
});