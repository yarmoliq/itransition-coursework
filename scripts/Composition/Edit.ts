import { Chapter } from "./../Models/chapter.js"
import { Composition } from "./../Models/composition.js"
import { sendRequest } from "./../request.js"

let chapters: Chapter[] = [];

const compositionID: string = location.pathname.split("/")[3];
let tableOfChapters = document.getElementById("table-of-chapters");

$("#table-of-chapters").sortable({
    items: "> tr",
    appendTo: "parent",
    helper: "clone",
    stop: (e, i) => {
        console.log("dragged");
    }
}).disableSelection();

sendRequest<Chapter[]>("Composition", "GetChapters", "POST", compositionID)
    .then((_chapters: Chapter[]) => {
        chapters = _chapters.map(obj => ({ ...obj }));
        chapters.sort((c1, c2) => c1.order - c2.order);
        chapters.forEach((chapter: Chapter) => {
            let tr = document.createElement("tr");

            let td = document.createElement("td");
            td.innerHTML = chapter.title;
            tr.appendChild(td);

            tr.id = chapter.id;
            tr.setAttribute("data-order", chapter.order.toString());

            tableOfChapters.appendChild(tr);
        });
    })
    .catch(error => {
        console.log("FUCK");
        console.log(error);
    });

document.getElementById("save-order").addEventListener("click", () => {
    let newOrder: string[] = [];
    
    tableOfChapters.childNodes.forEach(tr => newOrder.push((<HTMLElement>tr).id));

    for (let i = 0; i < newOrder.length; ++i){
        chapters.forEach(c => {
            if (c.id == newOrder[i]) {
                c.order = i;
            }
        });
    }

    
});