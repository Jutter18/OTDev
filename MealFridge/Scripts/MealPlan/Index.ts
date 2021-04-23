function getMealPlan(): void {
    $.ajax({
        url: "/MealPlan/MealPlan",
        type: "POST",
        error: (err) => { console.log(err); },
        success: (generatedMeals) => {
            $("#meals").html(generatedMeals);
        }
    })
}