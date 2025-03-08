let slideIndex = 0;
let slideInterval = setInterval(function () {
    changeSlide(1);
}, 3000); // Automatically change slides every 3 seconds

showSlides(slideIndex);

function changeSlide(n) {
    clearInterval(slideInterval); // Clear interval to avoid double increments
    showSlides(slideIndex += n);
    slideInterval = setInterval(function () {
        changeSlide(1);
    }, 3000); // Restart the interval after manual navigation
}

function currentSlide(n) {
    clearInterval(slideInterval);
    showSlides(slideIndex = n);
    slideInterval = setInterval(function () {
        changeSlide(1);
    }, 3000); // Restart the interval after dot navigation
}

function showSlides(n) {
    let slides = document.getElementsByClassName("slide");
    let animations = ['slide-left', 'slide-right', 'zoom-in', 'zoom-out', 'bounce']; // List of animations

    if (n >= slides.length) {
        slideIndex = 0;
    } else if (n < 0) {
        slideIndex = slides.length - 1;
    }

    // Hide all slides and remove animation classes
    for (let i = 0; i < slides.length; i++) {
        slides[i].style.display = "none";
        slides[i].classList.remove(...animations); // Remove all animation classes
    }

    // Display the current slide
    slides[slideIndex].style.display = "block";

    // Choose a random animation for the current slide
    let randomAnimation = animations[Math.floor(Math.random() * animations.length)];
    slides[slideIndex].classList.add(randomAnimation);
}