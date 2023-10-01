using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class Mixer : StaticBody2D
{
    private List<Coffee> coffees;
    private Master master;
    private Sprite bubble;
    private AnimationPlayer bubbleAnimation;
    private AudioStreamPlayer audio;

    public override void _Ready()
    {
        bubble = GetNode<Sprite>("Bubble");
        bubbleAnimation = GetNode<AnimationPlayer>("BubbleAnimation");
        Menu menu = new Menu();
        coffees = menu.RetrieveMenu();
        audio = GetNode<AudioStreamPlayer>("Audio");
    }

    public void DisplayMixer()
    {
        bubble.Visible = true;
        bubbleAnimation.Play("BubbleDisplay");
    }

    public Coffee StartMixing(List<string> ingredients)
    {
        Coffee result = coffees.FirstOrDefault(cf => cf.IngredientList.OrderBy(i => i).SequenceEqual(ingredients.OrderBy(i => i))); 
        if(result != null)
        {
            return result;
        }
        return null;
    }

    public void HideMixer()
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
