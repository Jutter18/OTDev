var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
class Search {
    constructor(query) {
        this.URL = "https://api.spoonacular.com/recipes/complexSearch";
        this.query = query;
    }
    getPossibleRecipes() {
        return __awaiter(this, void 0, void 0, function* () {
            return Promise.resolve(this.fetchAPI(this.query).then(data => {
                this.showRecipes(data["results"]);
            }));
        });
    }
    fetchAPI(query) {
        return __awaiter(this, void 0, void 0, function* () {
            const response = fetch(this.URL + "?apiKey=&query=" + query, {
                method: 'GET'
            });
            return (yield response).json();
        });
    }
    showRecipes(recipes) {
        let main = document.getElementById("main");
        if (recipes === null) {
            main.append("<p> No results found! </p>");
            return;
        }
        recipes.forEach(r => {
            const imagePath = "https://spoonacular.com/recipeImages/" + r["id"] + "-556x370." + r["imageType"];
            main.innerHTML +=
                `
                <div class="card">
                    <img class="card-img-top" src="${imagePath}" alt="Recipe Image">
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
    if (!search.value) {
        alert("Search can not be empty!");
        return;
    }
    let searcher = new Search(search.value);
    searcher.getPossibleRecipes();
}
//# sourceMappingURL=Index.js.map