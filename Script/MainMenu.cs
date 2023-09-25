using Godot;
using System;

public class MainMenu : CanvasLayer
{
    public void OnButtonPressed()
    {
        var dissolve = GetNode<Dissolve>("/root/Dissolve");
        dissolve.ChangeScene("res://Scene/Main.tscn");
    }
}
