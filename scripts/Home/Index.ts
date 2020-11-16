import { Composition } from "../Models/composition.js";
import { sendRequest } from "./../request.js"

let main = document.getElementById("feed");
let start : number = 0;
let end : boolean = false;
let anotherRequest : boolean = true;

function addPost(comp : Composition) : void {
    let card = document.createElement("div");
    card.classList.add("card");
    let card_header = document.createElement("div");
    card_header.classList.add("card-header");
    let card_body = document.createElement("div");
    card_body.addEventListener("click",()=>{
        document.location.href = location.origin + "/Composition/Show/" + comp.id + "/" + encodeURIComponent(encodeURI(location.href));
    });
    card_body.classList.add("card-body");
    let card_footer = document.createElement("div");
    card_footer.classList.add("card-footer");
    
    //header
    let row = document.createElement("div");
    let h3 = document.createElement("h3");
    h3.addEventListener("click",()=>{
        document.location.href = location.origin + "/Composition/Show/" + comp.id + "/" + encodeURIComponent(encodeURI(location.href));
    });
    h3.classList.add("col-10");
    h3.innerText = comp.title;
    row.appendChild(h3);

    let out_dropdown = document.createElement("div");
    out_dropdown.classList.add("col-2", "ml-auto", "d-flex", "align-items-center");
    let dropdown = document.createElement("div");
    dropdown.className = "dropdown";
    
    let dropdown_button = document.createElement("a");
    dropdown_button.classList.add("h4", "mr-3", "dropdown-toggle");
    dropdown_button.id = "dropdownMenuButton";
    dropdown_button.setAttribute("data-toggle","dropdown");
    dropdown_button.setAttribute("aria-haspopup","true");
    dropdown_button.setAttribute("aria-expanded","false");
    dropdown.appendChild(dropdown_button);
    let dropdown_menu = document.createElement("div");
    dropdown_menu.classList.add("dropdown-menu", "dropdown-menu-right");
    dropdown_menu.setAttribute("aria-labelledby", "dropdownMenuButton");
    dropdown_menu.id = "dropdown-hower";
    if(document.getElementById("id-user").innerText == comp.authorID || document.getElementById("admin-user").innerText == "True"){
        let a = document.createElement("a");
        a.classList.add("dropdown-item", "btn");
        a.href = location.origin + "/Composition/Edit/" + comp.id + "/" + encodeURIComponent(encodeURI(location.href));
        a.innerText = "Edit";
        dropdown_menu.appendChild(a);
    }
    dropdown.appendChild(dropdown_menu);

    out_dropdown.appendChild(dropdown);
    row.appendChild(out_dropdown);
    card_header.appendChild(row);
    
    //body
    let h5 = document.createElement("h5");
    h5.innerHTML = comp.authorID;
    card_body.appendChild(h5);
    card_body.append(comp.summary ?? "");
    
    let ol = document.createElement("ol");
    comp.chapters.forEach(chap => {
        let li = document.createElement("li");
        li.innerText = chap.title;
        ol.appendChild(li);
    });
    card_body.appendChild(ol);

    //footer
    card_footer.className = "text-muted";
    card_footer.append(comp.lastEditDT);

    card.appendChild(card_header);
    card.appendChild(card_body);
    card.appendChild(card_footer);
    main.appendChild(card);
    main.appendChild(document.createElement("br"));
}



async function populate() {
    if(!end)
    {
        let windowRelativeBottom = document.documentElement.getBoundingClientRect().bottom;
        if (windowRelativeBottom < document.documentElement.clientHeight + 100) {
            if(anotherRequest){
                anotherRequest = false;
                await sendRequest<Composition[]>("Administrator", "Get", "POST", start).then(comps=>{
                    if(comps.length == 0)
                    {
                        end = true;
                        let oops = document.createElement("p");
                        oops.innerText = "Ooops... Thats all for now";
                        main.appendChild(oops);
                    }
                    else
                    {
                        start = start + comps.length;
                        comps.forEach(addPost);
                        anotherRequest = true;
                    }
                });            
            }
        }
    }
}


window.addEventListener("scroll", populate);

populate();
