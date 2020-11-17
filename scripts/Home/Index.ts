import { Composition } from "../Models/composition.js";
import { sendRequest } from "../request.js";

let main = document.getElementById("feed");
let start : number = 0;
let end : boolean = false;
let anotherRequest : boolean = true;

async function addPost(comp : Composition) {
    let card = document.createElement("div");
    card.classList.add("card");
    let card_header = document.createElement("div");
    card_header.classList.add("card-header");
    let card_body = document.createElement("div");
    card_body.classList.add("card-body");
    let card_footer = document.createElement("div");
    card_footer.classList.add("card-footer");
    
    //header
    let row = document.createElement("div");
    row.className = "row";
    let h3 = document.createElement("h3");
    h3.addEventListener("click",()=>{
        document.location.href = location.origin + "/Composition/Show/" + comp.id + "/" + encodeURIComponent(encodeURI(location.href));
    });
    h3.classList.add("col-10");
    h3.innerText = comp.title;
    row.appendChild(h3);

    let out_dropdown = document.createElement("div");
    out_dropdown.classList.add("col-2", "d-flex", "align-items-center");
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
    sendRequest<string>("Home", "GetAuthorName", "POST", comp.authorID).then((authorName)=>{h5.innerHTML = authorName;});
    card_body.appendChild(h5);
    let text = document.createElement("div");
    text.className = "card-text";
    text.addEventListener("click",()=>{
        document.location.href = location.origin + "/Composition/Show/" + comp.id + "/" + encodeURIComponent(encodeURI(location.href));
    });
    let ol = document.createElement("ol");
    card_body.appendChild(text);
    function addReadMore(){
        let readmore = document.createElement("a");
        readmore.className = "card-link"
        readmore.innerText = "Read more ...";
        card_body.appendChild(readmore);
        readmore.addEventListener("click",()=>{
            comp.chapters.forEach(chap => {
                let li = document.createElement("li");
                li.innerText = chap.title;
                ol.appendChild(li);
            });
            readmore.hidden = true;
            text.innerHTML = "";
            text.innerText = comp.summary;
            text.appendChild(ol);
        });
    }
    if(comp.summary.length < 500){
        text.innerText = comp.summary ?? "";
        let count = 500 - comp.summary.length;
        for(var i = 0; i < comp.chapters.length; ++i)
        {
            if(count > comp.chapters[i].title.length){
                count = count - comp.chapters[i].title.length;
                let li = document.createElement("li");
                li.innerText = comp.chapters[i].title;
                ol.appendChild(li);
            }
            else{
                addReadMore();
                break;
            }
        }
        text.appendChild(ol);
    }
    else{
        text.innerText = comp.summary.substr(0,500) + "...";
        addReadMore();
    }
    
    card_footer.className = "text-muted";
    var k = new Date(comp.lastEditDT);
    card_footer.append(k.toString());

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
                await sendRequest<Composition[]>("Home", "Get", "POST", start).then(comps=>{
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
