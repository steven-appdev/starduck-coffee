using Godot;
using System;
using System.Collections.Generic;

public class Menu : Coffee
{
    Coffee plain, latte, americano;
    public Menu()
    {
        plain = new Coffee
        {
            RecipeName = "Plain",
            IngredientList = new List<string> {"Coffee"}
        };

        latte = new Coffee
        {
            RecipeName = "Latte",
            IngredientList = new List<string> {"Coffee", "Milk"}
        };

        americano = new Coffee
        {
            RecipeName = "Americano",
            IngredientList = new List<string> {"Coffee", "Hot Water"}
        };
    }

    public List<Coffee> RetrieveMenu()
    {
        return new List<Coffee> {plain, latte, americano};
    }
}