import * as cookies from "./cookies.js"

let themeSwitcher = document.getElementById("switch-theme");
if (themeSwitcher) {
    themeSwitcher.addEventListener("click", () => {
        if (cookies.getCookie("theme") == "dark") {
            cookies.setCookie("theme", "light");
        }
        else {
            cookies.setCookie("theme", "dark");
        }
        location.reload();
    });
}