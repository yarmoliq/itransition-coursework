// there are a few crutches:
// 1) .js in import
// 2) (<any>window) to add functions to window thing
//    so we can add it to onclick="" in html

import { OUR } from "../Models/comment.js";

let compositionEditor = document.getElementById("composition-editor");
let chapterEditor = document.getElementById("chapter-editor");

(<any>window).displayChapterEditor = function () {
    compositionEditor.style.display = "none";
    chapterEditor.style.display = "block";
};

(<any>window).closeChapterEditor = function () {
    // may be show "are you sure" thing ?
    compositionEditor.style.display = "block";
    chapterEditor.style.display = "none";
};

(<any>window).closeAndSaveChapterEditor = function () {
    compositionEditor.style.display = "block";
    chapterEditor.style.display = "none";
    // save chapter  
};