let tabMD: HTMLDivElement                       = <HTMLDivElement>document.getElementById("tab-md");
const inputTitle: HTMLInputElement              = <HTMLInputElement>document.getElementById("input-title");
const textareaContents: HTMLTextAreaElement     = <HTMLTextAreaElement>document.getElementById("textarea-contents");

let previewTitle: HTMLParagraphElement          = <HTMLParagraphElement>document.getElementById("md-preview-title");
let previewContents: HTMLDivElement             = <HTMLParagraphElement>document.getElementById("md-preview-contents");

tabMD.addEventListener('click', () => {
    const title     : string = inputTitle.value;
    const contents  : string = textareaContents.value;

    previewTitle.innerHTML = title;
    previewContents.innerHTML = marked(contents);
});