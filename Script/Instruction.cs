using Godot;
using System;

public class Instruction : CanvasLayer
{
    private AudioStreamPlayer click;

    public override void _Ready()
    {
        click = GetNode<AudioStreamPlayer>("Click");
    }

    public void OnNextPressed()
    {
        click.Play();
        var dissolve = GetNode<Dissolve>("/root/Dissolve");
        dissolve.ChangeScene("res://Scene/Instruction2.tscn");
    }
}
