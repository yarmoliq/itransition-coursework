import { Chapter } from "./chapter";

export class Composition {
    ID: string;
    AuthorID: string;
    Title: string;
    Summary: string;
    CreationDT: string;
    LastEditDT: string;
    Genre: string;
    Chapters: Chapter[];
};