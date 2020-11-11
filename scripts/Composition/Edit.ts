import { Chapter }     from "./../Models/chapter.js"
import { Composition } from "./../Models/composition.js"
import { sendRequest } from "./../request.js"

const compositionID : string = location.pathname.split("/")[3];
const returnUrl     : string = decodeURIComponent(location.pathname.split("/")[4]);

let titleInput      = <HTMLInputElement>    document.getElementById("input-title");
let genreSelect     = <HTMLSelectElement>   document.getElementById("select-genre");
let summaryTA       = <HTMLTextAreaElement> document.getElementById("textarea-summary");
let tableOfChapters =                       document.getElementById("tbody-chapters");

let original: Composition;
let composition: Composition;

sendRequest<Composition>("Composition", "Get", "POST", compositionID)
    .then(comp => {
        comp.chapters.sort((c1, c2) => c1.order - c2.order);
        original    = new Composition(comp);
        composition = new Composition(comp);
        
        titleInput.value    = composition.title;
        genreSelect.value   = composition.genre;
        summaryTA.value     = composition.summary;
    
        composition.chapters.forEach((chapter: Chapter) => {
            let tr = document.createElement("tr");

            let td = document.createElement("td");
            td.innerHTML = chapter.title;
            tr.appendChild(td);
            
            tr.id = chapter.id;
            tr.setAttribute("data-order", chapter.order.toString());
            
            tableOfChapters.appendChild(tr);
        });

        (<HTMLButtonElement>document.getElementById("btn-add-chapter")) .disabled = false;
        (<HTMLButtonElement>document.getElementById("btn-delete"))      .disabled = false;
    })
    .catch(err => showAlert('ERROR LOADING COMPOSITION!', false, err));
    



function updateComposition() {
    composition.title = titleInput.value;
    composition.genre = genreSelect.value;
    composition.summary = summaryTA.value;

    let newOrder: string[] = [];

    tableOfChapters.childNodes.forEach(tr => newOrder.push((<HTMLElement>tr).id));

    newOrder.forEach((id: string, i: number) => {
        composition.chapters.find(c => c.id == id).order = i;
    });
}

let formHasChanges: boolean = false;
    
function formChanged() {
    updateComposition();

    if (Composition.equal(composition, original)) {
        formHasChanges = false;
        (<HTMLButtonElement>document.getElementById("btn-save")).disabled = true;
    }
    else {
        formHasChanges = true;
        (<HTMLButtonElement>document.getElementById("btn-save")).disabled = false;
    }
}

titleInput  .addEventListener("change", formChanged);
genreSelect .addEventListener("change", formChanged);
summaryTA   .addEventListener("change", formChanged);

$("#tbody-chapters").sortable({
    items: "> tr",
    appendTo: "parent",
    helper: "clone",
    stop: formChanged
}).disableSelection();



document.getElementById("btn-back").addEventListener("click", () => {
    if (formHasChanges && !confirm("You have unsaved changes. Are you sure you want to leave this page?"))
        return;

    if (returnUrl != 'undefined')
        location.href = location.origin;

    location.href = decodeURIComponent(returnUrl);
});

document.getElementById("btn-add-chapter").addEventListener("click", () => {
    location.href = location.origin + "/Chapter/New/" + compositionID + "/" + encodeURIComponent(encodeURI(location.href));
});

document.getElementById("btn-save").addEventListener("click", () => {
    sendRequest<string>("Composition", "Update", "POST", composition)
    .then(response => {
        if (response == 'Success') {
            formHasChanges = false;
            original = new Composition(composition);
            (<HTMLButtonElement>document.getElementById("btn-save")).disabled = true;
            showAlert('Your changes have been successfully saved :)', true);
        }
        else showAlert('There was an error saving your changes :(', false, response);
    })
    .catch(err => showAlert('There was an error saving your changes :(', false, err));
});

document.getElementById("btn-delete").addEventListener("click", () => {
    if (!confirm('Are you sure you want to delete this composition?'))
        return;

    sendRequest<string>("Composition", "Delete", "POST", compositionID)
        .then(response => {
            if (response == 'Success') {
                if (returnUrl != 'undefined') {
                    location.href = location.origin;
                }

                location.href = decodeURIComponent(returnUrl);
            }

            showAlert('There was an error deleteing your composition :(', false, response);
        })
        .catch(err => showAlert('There was an error deleteing your composition :(', false, err));
});



function showAlert(message: string, good: boolean, moreData = null) {
    $('.alert').text(message);
    $('.alert').switchClass(good ? 'alert-warning' : 'alert-info', good ? 'alert-info' : 'alert-warning');
    $('.alert').switchClass('hide', 'show');

    setTimeout(() => {
        $('.alert').switchClass('show', 'hide');
    }, 5000);
    
    console.log("Alert: ", moreData);
}