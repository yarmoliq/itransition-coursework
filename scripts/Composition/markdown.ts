$(".chapter-contents").each((index, element) => {
    let contents = element.innerHTML;
    contents = marked(contents);
    element.innerHTML = contents;
});