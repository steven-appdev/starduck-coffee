using Godot;
using System;

public class Instruction3 : CanvasLayer
{
    private AudioStreamPlayer click;

    public override void _Ready()
    {
        click = GetNode<AudioStreamPlayer>("Click");
    }
    
    public void OnBackPressed()
    {
        click.Play();
        var dissolve = GetNode<Dissolve>("/root/Dissolve");
        dissolve.ChangeScene("res://Scene/Instruction2.tscn");
    }

    public void OnStartPressed()
    {
        click.Play();
        var dissolve = GetNode<Dissolve>("/root/Dissolve");
        dissolve.ChangeScene("res://Scene/Main.tscn");
    }
}
