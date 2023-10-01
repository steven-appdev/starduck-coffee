using Godot;
using System;
using System.Collections.Generic;

public class Player : KinematicBody2D
{
    private AnimatedSprite playerSprite;
    private Sprite bubble;
    [Export]
    private int playerSpeed = 10;
    private Area2D interactPoint;
    private List<string> playerCurrentHolding = new List<string>();
    private bool isLookingAtIngredients = false, isHoldingCoffee = false, isLookingAtMixer = false, isLookingAtTrash = false, isLookingAtCust = false;
    private Master master;
    private Mixer mixer;
    private Trash trash;
    private Customer customer;
    private Ingredients ingredient;
    private CanvasLayer recipeInfo;
    private AudioStreamPlayer quack;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        master = (Master)GetParent();
        playerSprite = GetNode<AnimatedSprite>("AnimatedSprite");
        bubble = GetNode<Sprite>("Bubble");
        interactPoint = GetNode<Area2D>("InteractPoint");
        recipeInfo = GetParent().GetNode<CanvasLayer>("RecipeMenu");
        quack = GetNode<AudioStreamPlayer>("Audio");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if(!master.IsGameOver())
        {
            PlayerMovement((float)delta);
            PlayerInteract();
            HelpMenu();
            bubble.Visible = isHoldingCoffee;
        }
        else
        {
            playerSprite.Animation = "dead";
        }
    }

    private void PlayerMovement(float frameLength)
    {
        Vector2 direction = Vector2.Zero;
        if(Input.IsActionPressed("player_up"))
        {
            direction.y -= 1;
        }

        if(Input.IsActionPressed("player_down"))
        {
            direction.y += 1;
        }

        if(Input.IsActionPressed("player_left"))
        {
            direction.x -= 1;
            interactPoint.RotationDegrees = 270;
            playerSprite.FlipH = false;
        }

        if(Input.IsActionPressed("player_right"))
        {
            direction.x += 1;
            interactPoint.RotationDegrees = 90;
            playerSprite.FlipH = true;
        }

        if(direction.Length() > 0)
        {
            direction = direction.Normalized();
        }

        if(direction != Vector2.Zero)
        {
            playerSprite.Animation = "walk";
        }
        else
        {
            playerSprite.Animation = "idle";
        }

        Position += direction * playerSpeed * frameLength;
        MoveAndCollide(direction);
    }

    private void PlayerInteract()
    {
        if(Input.IsActionJustPressed("player_interact"))
        {
            if(!isHoldingCoffee)
            {
                if(isLookingAtIngredients && playerCurrentHolding.Count < 4)
                {
                    playerCurrentHolding.Add(ingredient.GetIngredientItem());
                    ingredient.PlayAudio();
                    master.UpdateCurrentItem(playerCurrentHolding);
                }

                if(isLookingAtMixer)
                {
                    Coffee coffee = mixer.StartMixing(playerCurrentHolding);
                    if(coffee != null)
                    {
                        mixer.PlayAudio();
                        isHoldingCoffee = true;
                        playerCurrentHolding.Clear();
                        playerCurrentHolding.Add(coffee.RecipeName);
                        master.UpdateCurrentItem(playerCurrentHolding);
                    }
                }
            }
            else
            {
                if(isLookingAtCust)
                {
                    customer.AcceptOrder(playerCurrentHolding[0]);
                    quack.Play();
                    ClearInventory();
                }
            }

            if(isLookingAtTrash && playerCurrentHolding.Count > 0)
            {
                ClearInventory();
                trash.PlayAudio();
                isHoldingCoffee = false;
            }
        }
    }

    private void HelpMenu()
    {
        if(Input.IsActionJustPressed("player_help"))
        {
            recipeInfo.Visible = !recipeInfo.Visible; 
        }
    }

    public void ClearInventory()
    {
        isHoldingCoffee = false;
        playerCurrentHolding.Clear();
        master.ClearCurrentItem();
    }

    public void OnInteractPointBodyEntered(Node body)
    {
        if(body is Ingredients)
        {
            ingredient = (Ingredients)body;
            ingredient.DisplayIngredient();
            isLookingAtIngredients = true;
        }

        if(body is Mixer)
        {
            mixer = (Mixer)body;
            mixer.DisplayMixer();
            isLookingAtMixer = true;
        }

        if(body is Trash)
        {
            trash = (Trash)body;
            trash.DisplayTrash();
            isLookingAtTrash = true;
        }

        if(body is Customer)
        {
            customer = (Customer)body;
            isLookingAtCust = true;
        }
    }

    public void OnInteractPointBodyExited(Node body)
    {
        if(body is Ingredients)
        {
            ingredient.HideIngredient();
            ingredient = null;
            isLookingAtIngredients = false;
        }

        if(body is Mixer)
        {
            mixer.HideMixer();
            mixer = null;
            isLookingAtMixer = false;
        }

        if(body is Trash)
        {
            trash.HideTrash();
            trash = null;
            isLookingAtTrash = false;
        }

        if(body is Customer)
        {
            customer = null;
            isLookingAtCust = false;
        }
    }
}
