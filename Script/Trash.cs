using Godot;
using System;

public class Trash : StaticBody2D
{
    private Sprite bubble;
    private AnimationPlayer bubbleAnimation;
    private AudioStreamPlayer audio;

    public override void _Ready()
    {
        bubble = GetNode<Sprite>("Bubble");
        bubbleAnimation = GetNode<AnimationPlayer>("BubbleAnimation");
        audio = GetNode<AudioStreamPlayer>("Audio");
    }

    public void DisplayTrash()
    {
        bubble.Visible = true;
        bubbleAnimation.Play("BubbleDisplay");
    }

    public void HideTrash()
    {
        bubbleAnimation.Play("BubbleHide");
    }

    public void PlayAudio()
    {
        audio.Play();
    }

    public void OnBubbleAnimationAnimationFinished(string anim)
    {
        if(anim == "BubbleHide")
        {
            bubble.Visible = false;
        }
    }
}
