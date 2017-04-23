drop database MenusOfRecipes;

create database MenusOfRecipes;

use MenusOfRecipes;

/* Create menu table contains menus data */
CREATE TABLE `Menus` (
	menuName VARCHAR(50) not null,
    menuID INT(16) not null AUTO_INCREMENT,
    PRIMARY KEY (menuID)
);

/* Create recipe table contains recipes data*/
CREATE TABLE `Recipes` (
	recipeName VARCHAR(50) not null,
    recipeID INT(16) not null AUTO_INCREMENT,
    PRIMARY KEY (recipeID)
);

/* Create junction table for Menu, Recipe relationship, many to many*/
CREATE TABLE `Menu_Recipe` (
	recipeID INT (16) not null,
    menuID INT (16) not null,
	FOREIGN KEY (recipeID) REFERENCES Recipes(recipeID),
	FOREIGN KEY (menuID) REFERENCES Menus(menuID) 
);

/* Create ingredient table contains ingredients data*/
CREATE TABLE `Ingredients` (
	ingredientID INT(16) not null AUTO_INCREMENT,
    ingreName VARCHAR(50) not null,
    PRIMARY KEY (ingredientID)
);

/* Create instruction table contains instructions data
	recipeID is a foreign key to the Recipes table
*/
CREATE TABLE `Instruction` (
	instructionID INT(16) not null AUTO_INCREMENT,
    instructionName VARCHAR(50) not null,
    recipeID INT(16) not null,
	PRIMARY KEY (instructionID),
    FOREIGN KEY (recipeID) REFERENCES Recipes(recipeID) 
);

/* Create junction table for recipe, ingredient relationship, many to many*/
CREATE TABLE `Recipe_Ingredient` (
	recipeID INT(16) not null,
    ingredientID INT(16) not null,
    FOREIGN KEY (recipeID) REFERENCES Recipes(recipeID),
	FOREIGN KEY (ingredientID) REFERENCES Ingredients(ingredientID) 
);