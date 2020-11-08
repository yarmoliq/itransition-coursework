import { Chapter } from "./../Models/chapter.js"
import { Composition } from "./../Models/composition.js"
import { sendRequest } from "./../request.js"

let chapters: Chapter[];

const compositionID: string = location.pathname.split("/")[3];

sendRequest<Chapter[]>("Composition", "GetChapters", "POST", compositionID)
    .then((chapters: Chapter[]) => {
        console.log(chapters);
    })
    .catch(error => {
        console.log("FUCK");
    });