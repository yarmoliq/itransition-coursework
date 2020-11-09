import { Chapter } from "./chapter";

export class Composition {
    id: string;
    authorID: string;
    title: string;
    summary: string;
    creationDT: string;
    lastEditDT: string;
    genre: string;
    chapters: Chapter[];
};