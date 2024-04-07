function displayFileName() {
    const fileInput = document.getElementById('file');
    const fileNameDisplay = document.getElementById('vld-image');

    if (fileInput.files.length > 0) {
        const fileName = fileInput.files[0].name;
        fileNameDisplay.textContent = fileName;
    } else {
        fileNameDisplay.textContent = 'No file selected';
    }
}