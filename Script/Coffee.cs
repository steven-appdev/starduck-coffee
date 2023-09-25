using Godot;
using System;
using System.Collections.Generic;

public class Coffee
{
    private string recipeName;
    private List<string> ingredientList;

    public string RecipeName 
    {
        set { recipeName = value; }
        get { return recipeName; }
    }

    public List<string> IngredientList
    {
        set { ingredientList = value; }
        get { return ingredientList; }
    }
}
