const bootstrapDark : string = location.href.replace(location.pathname, "/") + "lib/bootstrap/dist/css/bootstrap-darkly.css";
const bootstrapLight: string = location.href.replace(location.pathname, "/") + "lib/bootstrap/dist/css/bootstrap-flatly.css";

function setCookie(name, value, days = 0) {
    var expires = "";
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        expires = "; expires=" + date.toUTCString();
    }
    document.cookie = name + "=" + (value || "") + expires + "; path=/";
}

function getCookie(name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
    }
    return null;
}

let link = document.createElement('link');

// var file = location.pathname.split("/").pop();

// var link = document.createElement("link");
// link.href = file.substr(0, file.lastIndexOf(".")) + ".css";
// link.type = "text/css";
// link.rel = "stylesheet";
// link.media = "screen,print";

link.rel = 'stylesheet';
link.type = 'text/css';
if (getCookie("theme") == "dark") {
    link.href = bootstrapDark;
}
else {
    link.href = bootstrapLight;
}
link.media = 'all';

document.getElementsByTagName("head")[0].appendChild(link);

let themeSwitcher = document.getElementById("switch-theme");
if (themeSwitcher) {
    themeSwitcher.addEventListener("click", () => {
        if (getCookie("theme") == "dark") {
            setCookie("theme", "light");
        }
        else {
            setCookie("theme", "dark");
        }
        location.reload();
    });
}