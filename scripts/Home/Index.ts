function addPost() {
    document.createElement("div").innerHTML = "hjcjvhx";
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
window.addEventListener("scroll", populate);
populate();