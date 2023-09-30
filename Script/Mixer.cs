using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class Mixer : StaticBody2D
{
    private Label lblMixerName;
    private List<Coffee> coffees;
    private Master master;

    public override void _Ready()
    {
        lblMixerName = GetNode<Label>("MixerName");
        Menu menu = new Menu();
        coffees = menu.RetrieveMenu();
    }

    public void DisplayMixerName()
    {
        lblMixerName.Visible = true;
    }

    public Coffee StartMixing(List<string> ingredients)
    {
        Coffee result = coffees.FirstOrDefault(cf => cf.IngredientList.OrderBy(i => i).SequenceEqual(ingredients.OrderBy(i => i))); 
        if(result != null)
        {
            return result;
        }
        return null;
    }

    public void HideMixerName()
    {
        lblMixerName.Visible = false;
    }
}
