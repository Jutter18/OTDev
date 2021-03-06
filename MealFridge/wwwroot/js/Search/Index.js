var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
window.onload = () => {
    const prevSearch = window.sessionStorage.getItem("prevSearch");
    if (prevSearch !== null) {
        let newSearch = document.getElementById("sbn");
        newSearch.value = prevSearch;
        window.sessionStorage.clear();
        searchByName();
    }
};
class Search {
    constructor(query, type) {
        this.URL = "/api/SearchByName/";
        this.query = query;
        this.type = type;
    }
    getPossibleRecipes() {
        return __awaiter(this, void 0, void 0, function* () {
            return Promise.resolve(this.fetchAPI(this.query, this.type).then(data => {
                this.showRecipes(data);
            }));
        });
    }
    fetchAPI(query, type) {
        return __awaiter(this, void 0, void 0, function* () {
            const response = fetch(this.URL + query + "/" + type, {
                method: 'GET'
            });
            return (yield response).json();
        });
    }
    showRecipes(recipes) {
        let main = document.getElementById("main");
        main.innerHTML = "";
        if (recipes.length < 1) {
            main.innerHTML += "<p> No results found! </p>";
            return;
        }
        recipes.forEach(r => {
            main.innerHTML +=
                `
                <div class="card shadow-lg">
                    <img class="card-img-top" src="${r["image"]}" alt="Recipe Image">
                    <div class="card-body">
                        <h4 class="card-title">${r["title"]}</h4>
                        <a href="#!" class="btn btn-primary">Recipe Details</a>
                    </div>
                </div>
            `;
        });
    }
}
function searchByName() {
    let search = document.getElementById("sbn");
    let type = document.getElementById("searchType");
    if (!search.value) {
        alert("Search can not be empty!");
        return;
    }
    let searcher = new Search(search.value, type.value);
    searcher.getPossibleRecipes();
}
const inputSearch = document.getElementById("sbn");
inputSearch.addEventListener("keydown", (e) => {
    if (e.keyCode === 13) {
        searchByName();
    }
});
//# sourceMappingURL=Index.js.map