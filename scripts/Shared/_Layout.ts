let arrowTop = document.getElementById("arrowTop");
let arrowBack = document.getElementById("arrowBack");
let lastYinpage : number = 0;

window.addEventListener("scroll", function(){
    arrowTop.hidden = (pageYOffset < document.documentElement.clientHeight);
    if(pageYOffset >= document.documentElement.clientHeight)
    {
        arrowBack.hidden = true;
    }
});

arrowTop.addEventListener('click', function(){
    lastYinpage = pageYOffset;
    window.scrollTo(pageXOffset, 0);
    arrowBack.hidden = false;
});


arrowBack.addEventListener('click', function(){
    arrowBack.hidden = true;
    window.scrollTo(pageXOffset, lastYinpage);
});