const previewImages = (c) => {
    console.log(event + c)
    const imageFiles = event.target.files;
    const imageFilesLength = imageFiles.length;
    const previewContainer = document.querySelector(`.${c} #preview-selected-images`);
    previewContainer.innerHTML = ""; // Clear previous previews

    for (let i = 0; i < imageFilesLength; i++) {
        const imageSrc = URL.createObjectURL(imageFiles[i]);
        const imagePreviewElement = document.createElement("img");
        imagePreviewElement.src = imageSrc;
        imagePreviewElement.classList.add("preview-image");
        previewContainer.appendChild(imagePreviewElement);
    }
};