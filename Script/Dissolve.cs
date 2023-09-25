using Godot;
using System;

public class Dissolve : CanvasLayer
{
    private AnimationPlayer animation;
    private string targetScene;

    public override void _Ready()
    {
        animation = GetNode<AnimationPlayer>("AnimationPlayer");
    }

    public void ChangeScene(string target)
    {
        animation.Play("dissolve-out");
        targetScene = target;
    }

    public void LongerChangeScene(string target)
    {
        animation.Play("long-dissolve-out");
        targetScene = target;
    }

    public void OnAnimationPlayerAnimationFinished(string animName)
    {
        if(animName == "dissolve-out")
        {
            GetTree().ChangeScene(targetScene);
            animation.Play("dissolve-in");
        }

        if(animName == "long-dissolve-out")
        {
            GetTree().ChangeScene(targetScene);
            animation.Play("long-dissolve-in");
        }
    }
}
