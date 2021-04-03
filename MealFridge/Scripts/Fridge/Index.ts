window.onload = () => {
    const prevSearch = window.sessionStorage.getItem("prevSearch");
    if (prevSearch !== null) {
        let newSearch = <HTMLInputElement>document.getElementById("ingredSearch");
        newSearch.value = prevSearch;
        window.sessionStorage.clear();
        searchByName()
    }
};

function addIngredient(id: string, amount: string): void {
    let current = parseInt($("#current-card-" + id).text(), 10);
    $("#current-card-" + id).empty()
    $("#current-card-" + id).append((current + 1).toString());//Add one for updated value
    //fridge-table-main
    $.ajax({
        url: "/Fridge/AddItem",
        method: "POST",
        data: {
            id: id,
            amount: parseInt(amount, 10)
        },
        success: (data) => {
            $("#fridge-table-main").empty();
            $("#fridge-table-main").html(data);
        },
        error: (err) => { console.log(err); }
    })
}

function updateInventory(id: string, amount: string): void {
    $.ajax({
        url: "/Fridge/AddItem",
        method: "POST",
        data: {
            id: id,
            amount: parseInt(amount)
        },
        success: (data) => {
            $("#fridge-table-main").empty();
            $("#fridge-table-main").html(data);
        },
        error: (err) => { console.log(err); }
    })
}
                        </div>
                        <div class="card-body col-6">
                            <h4 class="card-title">${r["name"]}</h4>
                            <p class="card-text">Cost per Serving: ${r["cost"]}</p>
                            <p class="card-text">Aisle: ${r["aisle"]}</p>
                        </div>
                        <div class="btn-group-vertical col-2" role="group">
                            <button type="button" class="btn btn-primary addIngred" value="${r["id"]}" onclick="AddIngredient(this.value, '${r["name"]}')">Add</button>
                        </div>
                    </div>
                </div>
            `;
        })
    }
}

function AddIngredient(id: string, name: string): void {
    let amount = prompt("Please enter the amount", "1");
    if (amount != '' && !isNaN(+amount)) {
        fetch("Fridge/AddItem?id=" + id + "&amount=" + amount, {
            method: 'GET'
        });
        document.getElementById("fridge").innerHTML += `
                        <tr id="${id}">
                            <td class="w-75"> ${name} </td>
                            <td class="w-25">
                                <button type="button" class="btn btn-primary changeIngred" id="btn-${id}" value="${id}" onclick="ChangeIngredient(this.value)">
                                    ${amount}
                                </button>
                            </td>                           
                        </tr>
        `
    }
}



function SearchByIngredientName(): void {
    let search: HTMLInputElement = <HTMLInputElement>document.getElementById("ingredSearch");
    if (!search.value) {
        alert("Search can not be empty!");
        return;
    }
    $.ajax({
        url: "/Fridge/SearchIngredients",
        method: "POST",
        data: {
            QueryValue: search.value
        },
        success: (data) => {
            $("#fridge_main").empty();
            $("#fridge_main").html(data);
        },
        error: (err) => { console.log(err); }
    });
}

const inputSearchFridge: HTMLInputElement = <HTMLInputElement>document.getElementById("ingredSearch");
inputSearchFridge.addEventListener("keydown", (e) => {
    //checks whether the pressed key is "Enter"
    if (e.keyCode === 13) {
        SearchByIngredientName();
    }

});
