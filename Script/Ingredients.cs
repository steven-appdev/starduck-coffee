using Godot;
using System;

public class Ingredients : StaticBody2D
{
    [Export]
    private string ingredientName = "placeholder";
    [Export]
    private string ingredientItem = "null";
    private Sprite bubble;
    private AnimationPlayer bubbleAnimation;
    private AudioStreamPlayer audio;

    public override void _Ready()
    {
        bubble = GetNode<Sprite>("Bubble");
        bubbleAnimation = GetNode<AnimationPlayer>("BubbleAnimation");
        audio = GetNode<AudioStreamPlayer>("Audio");
    }

    public void DisplayIngredient()
    {
        bubble.Visible = true;
        bubbleAnimation.Play("BubbleDisplay");
    }

    public string GetIngredientItem()
    {
        return ingredientItem;
    }

    public void HideIngredient()
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
