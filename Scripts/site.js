function lazyLoadImages() {
    let e = document.querySelectorAll("img")
        , t = (e, t) => {
            e.forEach(e => {
                if (e.isIntersecting) {
                    let a = e.target
                        , r = a.getAttribute("data-src") || a.src;
                    a.src = r,
                        a.onload = () => a.classList.remove("lazy"),
                        t.unobserve(a)
                }
            }
            )
        }
        , a = new IntersectionObserver(t, {
            root: null,
            rootMargin: "0px",
            threshold: .1
        });
    e.forEach(e => {
        e.hasAttribute("data-src") && a.observe(e)
    }
    )
}
document.addEventListener("DOMContentLoaded", lazyLoadImages);

// Show/Hide the "Go to Top" button based on scroll position
const goToTopButton = document.getElementById('goToTop');

window.addEventListener('scroll', () => {
    if (window.scrollY > 200) {
        goToTopButton.style.display = 'flex';
    } else {
        goToTopButton.style.display = 'none';
    }
});

// Smooth scroll to top when the button is clicked
goToTopButton.addEventListener('click', () => {
    window.scrollTo({
        top: 0,
        behavior: 'smooth'
    });
});