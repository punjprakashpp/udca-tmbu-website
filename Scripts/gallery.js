function openLightbox(title) {
    document.getElementById('galleryLightbox').style.display = 'block';
    loadGalleryImages(title);
}

function closeGalleryLightbox() {
    document.getElementById('galleryLightbox').style.display = 'none';
}

function displayImage(FilePath) {
    const lightboxImage = document.getElementById('lightboxImage');
    lightboxImage.src = FilePath;

    // Trigger the Bootstrap modal
    const lightboxModal = new bootstrap.Modal(document.getElementById('lightboxModal'));
    lightboxModal.show();
}

window.onclick = function (event) {
    const lightboxModal = document.getElementById('lightboxModal');
    const galleryLightbox = document.getElementById('galleryLightbox');
    if (event.target === lightboxModal) {
        lightboxModal.style.display = 'none';
    } else if (event.target === galleryLightbox) {
        galleryLightbox.style.display = 'none';
    }
};

function loadGalleryImages(title) {
    const xhr = new XMLHttpRequest();
    xhr.open("GET", "ImageGallery.aspx?Title=" + encodeURIComponent(title), true);
    xhr.onreadystatechange = function () {
        if (xhr.readyState === 4 && xhr.status === 200) {
            const galleryContainer = document.getElementById('galleryLightbox').getElementsByClassName('gallery')[0];
            galleryContainer.innerHTML = xhr.responseText;
        }
    };
    xhr.send();
}