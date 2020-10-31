class Chapter {
    public title: string;
    public contents: string;
}

class HTML_Chapter {
    private _chapterElement: HTMLDivElement;
    
    private _title: HTMLInputElement;
    private _text: HTMLTextAreaElement;
    
    constructor() {
        this._chapterElement = document.createElement("div");

        let div1 = document.createElement("div");
        div1.classList.add("form-group");
        div1.classList.add("input-group");

        this._title = document.createElement("input");
        this._title.type = "text";
        this._title.classList.add("form-control");
        this._title.placeholder = "Chapter title";
        this._title.required = true;
        div1.appendChild(this._title);
        
        let dropDownMenu = document.createElement("div");
        dropDownMenu.classList.add("input-group-append");
        dropDownMenu.innerHTML = `
        <button class="btn btn-outline-secondary dropdown-toggle" type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"></button>
        <div class="dropdown-menu">
        <a class="dropdown-item" href="#">Delete chapter</a>
        <a class="dropdown-item" href="#">Add chapter before</a>
        <a class="dropdown-item" href="#">Add chapter after</a>
        </div>
        `;
        div1.appendChild(dropDownMenu);
        
        this._chapterElement.appendChild(div1);
        
        let div2 = document.createElement("div");
        div2.classList.add("form-group");
        
        this._text = document.createElement("textarea");
        this._text.classList.add("form-control");
        this._text.rows = 10;
        div2.appendChild(this._text);
        
        this._chapterElement.appendChild(div2);
    }
    
    get title(): string {
        return this._title.value;
    }
    
    get text(): string {
        return this._text.value;
    }
    
    get HTMLElement(): HTMLDivElement {
        return this._chapterElement;
    }
}

let chapters: HTML_Chapter[] = [];

const appendNewChapter = function () {
    let newChapter = new HTML_Chapter();
    chapters.push(newChapter);
    document.getElementById("append-new-chapter").before(newChapter.HTMLElement);
};

const addNewChapterAfter = function () {
    
}