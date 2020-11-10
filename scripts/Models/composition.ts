import { Chapter } from "./chapter.js";

export class Composition {
    id: string;
    authorID: string;
    title: string;
    summary: string;
    creationDT: string;
    lastEditDT: string;
    genre: string;
    chapters: Chapter[];
    static equal(first: Composition, second: Composition): boolean {
        if (first.id          != second.id)          return false;
        if (first.authorID    != second.authorID)    return false;
        if (first.title       != second.title)       return false;
        if (first.summary     != second.summary)     return false;
        if (first.creationDT  != second.creationDT)  return false;
        if (first.lastEditDT  != second.lastEditDT)  return false;
        if (first.genre       != second.genre)       return false;

        if (first.chapters.length != second.chapters.length) return false;

        for (let i = 0; i < first.chapters.length; ++i)
            if (!Chapter.equal(first.chapters[i], second.chapters[i]))
                return false;
        
        return true;
    };

    constructor(other: Composition) {
        this.id          = other.id;
        this.authorID    = other.authorID;
        this.title       = other.title;
        this.summary     = other.summary;
        this.creationDT  = other.creationDT;
        this.lastEditDT  = other.lastEditDT;
        this.genre       = other.genre;

        this.chapters = [];
        other.chapters.forEach(c => {
            this.chapters.push({ ...c });
        });
    }
};