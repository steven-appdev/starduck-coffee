using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class Master : Node2D
{
    [Export]
    private PackedScene customer;
    private Label lblCurrentItem, lblMoney, lblHealth;
    private List<Coffee> coffeeMenu;
    private List<bool> pathList = new List<bool> {false, false, false, false, false};
    private bool isGameOver = false;
    private Timer customerSpawnTimer;
    private int playerPoint = 0;
    private int playerHealth = 1;
    private AnimationPlayer gameOverAnimation;

    public Master()
    {
        Menu menu = new Menu();
        coffeeMenu = menu.RetrieveMenu();
    }

    public override void _Ready()
    {
        customerSpawnTimer = GetNode<Timer>("CustomerSpawnTimer");
        lblCurrentItem = GetNode<Label>("HUD/CurrentItemPanel/CurrentItem");
        lblMoney = GetNode<Label>("HUD/Panel/Money");
        lblHealth = GetNode<Label>("HUD/Panel/TextureRect/Health");
        gameOverAnimation = GetNode<AnimationPlayer>("AnimationPlayer");
        lblHealth.Text = playerHealth.ToString();
        customerSpawnTimer.Start();
    }

    public void UpdateCurrentItem(List<string> items)
    {
        string currentItems = null;
        foreach(string i in items)
        {
            currentItems += "\n"+ i;
        }
        lblCurrentItem.Text = currentItems;
    }

    public void UpdateHealth(int health)
    {
        lblHealth.Text = health.ToString();
    }

    public void DeductHealth(int damage)
    {
        playerHealth -= damage;
        if(playerHealth <= 0)
        {
            GameOver();
        }
        lblHealth.Text = playerHealth.ToString();
    }

    private void GameOver()
    {
        isGameOver = true;
        customerSpawnTimer.Stop();
        var highscore = GetNode<Highscore>("/root/Highscore");
        highscore.PlayerHighscore = playerPoint;
        gameOverAnimation.Play("GameOver");
    }

    public void ClearCurrentItem()
    {
        lblCurrentItem.Text = "";
    }

    public List<Coffee> RetrieveAvailableCoffee()
    {
        return coffeeMenu;
    }

    public void OnCustomerSpawnTimerTimeout()
    {
        var availablePath = pathList.Select((value, index) => new { Value = value, Index = index})
                                    .Where(pl => !pl.Value)
                                    .Select(pl => pl.Index)
                                    .ToList();

        if(availablePath.Count() != 0)
        {
            var customer = (Customer)this.customer.Instance();
            int pathIndex = new Random().Next(0, availablePath.Count());
            PathFollow2D path = GetNode<PathFollow2D>("Paths/CustomerPath"+availablePath[pathIndex]+"/PathFollow2D");
            TogglePathAvailability(availablePath[pathIndex]);
            customer.SetCurrentPath(availablePath[pathIndex]);
            path.AddChild(customer);
        }

        customerSpawnTimer.WaitTime = new Random().Next(1,7);
        customerSpawnTimer.Start();
    }

    public void PlayerGetPoint(int point)
    {
        playerPoint += point;
        lblMoney.Text = "Money Earned: $"+playerPoint;
    }

    public void TogglePathAvailability(int index)
    {
        pathList[index] = !pathList[index];
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }

    public void OnAnimationPlayerAnimationFinished(string animPlayed)
    {
        var dissolve = GetNode<Dissolve>("/root/Dissolve");
        dissolve.LongerChangeScene("res://Scene/GameOver.tscn");
    }
}
