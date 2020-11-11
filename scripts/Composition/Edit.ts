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
            
            let td1 = document.createElement("td");
            let a = document.createElement("a");
            a.href = location.origin + "/Chapter/Edit/" + chapter.id + "/" + encodeURIComponent(encodeURI(location.href));
            a.type = "button";
            a.classList.add("btn");
            a.classList.add("btn-secondary");
            a.innerHTML = '<i class="fa fa-edit"></i>';
            td1.appendChild(a);
            tr.appendChild(td1);

            let td2 = document.createElement("td");
            td2.innerHTML = chapter.title;
            tr.appendChild(td2);
            
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
        window.onbeforeunload = null;
        (<HTMLButtonElement>document.getElementById("btn-save")).disabled = true;
    }
    else {
        formHasChanges = true;
        window.onbeforeunload = () => 'You have unsaved changes';
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
            original = new Composition(composition);
            formChanged();
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