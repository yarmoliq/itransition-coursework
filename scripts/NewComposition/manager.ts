let compositionEditor = document.getElementById("composition-editor");
let chapterEditor = document.getElementById("chapter-editor");

// let currentComposition;

const displayChapterEditor = function () {
    compositionEditor.style.display = "none";
    chapterEditor.style.display = "block";
};

const closeChapterEditor = function () {
    // may be show "are you sure" thing ?
    compositionEditor.style.display = "block";
    chapterEditor.style.display = "none";
};

const closeAndSaveChapterEditor = function () {
    compositionEditor.style.display = "block";
    chapterEditor.style.display = "none";
    // save chapter  
};