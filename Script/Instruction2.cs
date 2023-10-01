using Godot;
using System;

public class Instruction2 : CanvasLayer
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
        dissolve.ChangeScene("res://Scene/Instruction3.tscn");
    }

    public void OnBackPressed()
    {
        click.Play();
        var dissolve = GetNode<Dissolve>("/root/Dissolve");
        dissolve.ChangeScene("res://Scene/Instruction.tscn");
    }
}
