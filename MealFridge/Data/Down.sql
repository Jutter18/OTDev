ALTER TABLE [FRIDGE] DROP CONSTRAINT [Fridge_FK_Ingred];
ALTER TABLE [RESTRICTIONS] DROP CONSTRAINT [Restrictions_FK_Ingred];
ALTER TABLE [RECIPEINGRED] DROP CONSTRAINT [RecipeIngred_FK_Recipe];
ALTER TABLE [RECIPEINGRED] DROP CONSTRAINT [RecipeIngred_FK_Ingred];
ALTER TABLE [SAVEDRECIPES] DROP CONSTRAINT [SavedRecipes_FK_Recipes];
ALTER TABLE [MEAL] DROP CONSTRAINT [Meal_FK_Recipe];


DROP TABLE [DIET];
DROP TABLE [FRIDGE];
DROP TABLE [INGREDIENTS];
DROP TABLE [RESTRICTIONS];
DROP TABLE [RECIPES];
DROP TABLE [RECIPEINGRED];
DROP TABLE [SAVEDRECIPES];

DROP TABLE [MEAL];
