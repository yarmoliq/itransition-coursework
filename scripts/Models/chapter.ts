export class Chapter {
    id: string;
    title: string;
    compositionID: string;
    creationDT: string;
    lastEditDT: string;
    order: number;

    static equal(first: Chapter, second: Chapter): boolean{
        if (first.id             != second.id)            return false;
        if (first.title          != second.title)         return false;
        if (first.compositionID  != second.compositionID) return false;
        if (first.creationDT     != second.creationDT)    return false;
        if (first.lastEditDT     != second.lastEditDT)    return false;
        if (first.order          != second.order)         return false;

        return true;
    };
};