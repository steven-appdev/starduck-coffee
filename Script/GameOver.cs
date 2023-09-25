using Godot;
using System;

public class GameOver : CanvasLayer
{
    private Label score;

    public override void _Ready()
    {
        score = GetNode<Label>("Highscore");
        var highscore = GetNode<Highscore>("/root/Highscore");
        score.Text = "Your highscore is $"+highscore.PlayerHighscore;
    }

    public override void _Process(float delta)
    {
        if(Input.IsActionJustPressed("ui_cancel"))
        {
            GetNode<Dissolve>("/root/Dissolve").ChangeScene("res://Scene/MainMenu.tscn");
        }
    }
}
