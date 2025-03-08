function setSectionHeight() {
    const sliderImg = document.querySelector('.image');
    const sliderSection = document.querySelector('.slider-container');
    const noticeSection = document.querySelector('.notice-section');

    if (!sliderImg || !sliderSection || !noticeSection) return;

    const setHeights = () => {
        const sliderImgHeight = sliderImg.offsetHeight;
        if (sliderImgHeight > 0 && window.innerWidth >= 768) {
            noticeSection.style.height = `${sliderSection.offsetHeight}px`;
        }
    };

    if (sliderImg.complete) {
        setHeights();
    } else {
        sliderImg.addEventListener('load', setHeights);
    }
}

function setMarqueeHeight() {
    const marquee = document.querySelector('#marq');
    const noticeSection = document.querySelector('.notice-section');

    if (marquee && noticeSection) {
        let calculatedHeight;
        if (window.innerWidth >= 1200) {
            calculatedHeight = noticeSection.offsetHeight - 70;
        } else {
            calculatedHeight = noticeSection.offsetHeight - 100;
        }
        marquee.style.height = calculatedHeight > 0 ? `${calculatedHeight}px` : 'auto';
    }
}

function setBoardHeight() {
    const board = document.querySelector('.board');
    if (!board) return;

    const newHeight = board.offsetHeight - 70;
    ['#marq1', '#marq2'].forEach(selector => {
        const element = document.querySelector(selector);
        if (element) {
            element.style.height = newHeight > 0 ? `${newHeight}px` : 'auto';
        }
    });
}

function adjustHeights() {
    const boxes = document.querySelectorAll('.box');
    if (!boxes.length) return;

    if (window.innerWidth > 768) {
        boxes.forEach(box => (box.style.height = 'auto')); // Reset heights

        const maxBoxHeight = Array.from(boxes).reduce((maxHeight, box) =>
            Math.max(maxHeight, box.offsetHeight), 0);

        boxes.forEach(box => (box.style.height = `${maxBoxHeight}px`));
    } else {
        boxes.forEach(box => (box.style.height = 'auto'));
    }
}

function initialize() {
    setSectionHeight();
    setMarqueeHeight();
    setBoardHeight();
    adjustHeights();
}

// Debounce Function for Resize Events
function debounce(func, delay) {
    let timeout;
    return function () {
        clearTimeout(timeout);
        timeout = setTimeout(func, delay);
    };
}

// Add Event Listeners for Load and Resize
window.addEventListener('load', initialize);
window.addEventListener('resize', debounce(initialize, 150));
