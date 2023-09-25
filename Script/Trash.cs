using Godot;
using System;

public class Trash : StaticBody2D
{
    private Label lblTrashName;

    public override void _Ready()
    {
        lblTrashName = GetNode<Label>("TrashName");
    }

    public void DisplayTrashName()
    {
        lblTrashName.Visible = true;
    }

    public void HideTrashName()
    {
        lblTrashName.Visible = false;
    }
}
