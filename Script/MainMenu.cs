using Godot;
using System;

public class MainMenu : CanvasLayer
{
    private AudioStreamPlayer click;

    public override void _Ready()
    {
        click = GetNode<AudioStreamPlayer>("Click");
    }

    public void OnButtonPressed()
    {
        click.Play();
        var dissolve = GetNode<Dissolve>("/root/Dissolve");
        dissolve.ChangeScene("res://Scene/Instruction.tscn");
    }

    public void OnQuitPressed()
    {
        GetTree().Quit();
    }
}
