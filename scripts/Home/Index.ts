let arrowTop = document.getElementById("arrowTop");
let pol = document.getElementById("pol");

function addPost() {
    let div = document.createElement("div");
    div.innerHTML = "hjcjvhx";
    pol.appendChild(div);
}

function populate() {
    while (true) {
        let windowRelativeBottom = document.documentElement.getBoundingClientRect().bottom;
        if (windowRelativeBottom < document.documentElement.clientHeight + 100) {
            addPost();
        }
        else{
            break;
        }
    }
}

window.addEventListener("scroll", function(){
    arrowTop.hidden = (pageYOffset < document.documentElement.clientHeight);
    populate();
});

populate();

arrowTop.addEventListener('click', function(){
    window.scrollTo(pageXOffset, 0);
});
