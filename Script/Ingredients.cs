using Godot;
using System;

public class Ingredients : StaticBody2D
{
    [Export]
    private string ingredientName = "placeholder";
    [Export]
    private string ingredientItem = "null";
    private Label lblIngredientName;

    public override void _Ready()
    {
        lblIngredientName = GetNode<Label>("ObjectiveName");
    }

    public void DisplayIngredientName()
    {
        lblIngredientName.Text = ingredientName;
        lblIngredientName.Visible = true;
    }

    public string GetIngredientItem()
    {
        return ingredientItem;
    }

    public void HideIngredientName()
    {
        lblIngredientName.Visible = false;
    }
}
