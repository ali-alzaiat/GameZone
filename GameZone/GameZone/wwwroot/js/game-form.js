let cover = document.querySelector("#Cover");
cover.addEventListener("change", () => {
    let coverPreview = document.querySelector("#cover-preview");
    console.log(this);
    coverPreview.setAttribute("src", window.URL.createObjectURL(cover.files[0]));
    coverPreview.classList.remove("d-none");
})