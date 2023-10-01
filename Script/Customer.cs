using Godot;
using System;
using System.Collections.Generic;
using System.Runtime;

public class Customer : KinematicBody2D
{
    [Export]
    private int customerSpeed = 10;
    private int currentPath;
    private PathFollow2D customerPath; 
    private Timer customerPatienceTimer;
    private bool isPatienceTimerStarted = false, isCycleCompleted = false, isCustomerAngry = false;
    private Master master;
    private Label lblCustomerOrder, lblPatienceTimer;
    private List<Coffee> coffees;
    private Coffee order;
    private AnimatedSprite customerSprite, customerBubble;
    private AudioStreamPlayer audio;
    
    public override void _Ready()
    {
        master = (Master)GetTree().Root.GetNode<Node2D>("Main");
        coffees = master.RetrieveAvailableCoffee();
        customerSprite = GetNode<AnimatedSprite>("AnimatedSprite");
        customerBubble = GetNode<AnimatedSprite>("Bubble");
        customerPath = (PathFollow2D)GetParent();
        customerPatienceTimer = GetNode<Timer>("PatienceTimer");
        customerPatienceTimer.WaitTime = new Random().Next(7,16);
        lblCustomerOrder = GetNode<Label>("Bubble/CustomerOrder");
        lblPatienceTimer = GetNode<Label>("Bubble/Timer");
        audio = GetNode<AudioStreamPlayer>("Audio");
    }

    public override void _Process(float delta)
    {
        lblPatienceTimer.Text = ((int)customerPatienceTimer.TimeLeft).ToString();
        if(!isCycleCompleted)
        {
            EnterStore(delta);
        }

        if(isCycleCompleted)
        {
            LeaveStore(delta);
        }

        if(master.IsGameOver())
        {
            QueueFree();
        }
    }

    private void EnterStore(float delta)
    {
        if(customerPath.UnitOffset < 1)
        {
            customerPath.Offset += customerSpeed * delta;
        }
        else if(customerPath.UnitOffset >= 1)
        {
            StartOrdering();
        }
    }

    private void LeaveStore(float delta)
    {
        customerSprite.FlipH = true;
        customerBubble.FlipH = true;

        if(customerPath.UnitOffset > 0)
        {
            customerPath.Offset -= customerSpeed * delta;
        }
        else if(customerPath.UnitOffset <= 0)
        {
            master.TogglePathAvailability(currentPath);
            QueueFree();
        }
    }

    public void StartOrdering()
    {
        if(!isPatienceTimerStarted)
        {
            order = coffees[new Random().Next(0, coffees.Count)];
            lblCustomerOrder.Text = order.RecipeName;
            customerBubble.Visible = true;
            customerPatienceTimer.Start();
            isPatienceTimerStarted = true;
        }
    }

    public void AcceptOrder(string coffee)
    {
        if(!isCustomerAngry)
        {
            lblCustomerOrder.Visible = false;
            lblPatienceTimer.Visible = false;
            if(coffee == order.RecipeName)
            {
                isCycleCompleted = true;
                master.PlayerGetPoint(10);
                customerPatienceTimer.Stop();
                customerBubble.Animation = "happy";
            }
            else
            {
                StartAngry();
            }
            customerBubble.Visible = true;
        }
    }

    private void StartAngry()
    {
        customerBubble.Animation = "angry";
        customerSprite.Animation = "angry";
        audio.Play();
        isCustomerAngry = true;
    }

    public void OnPatienceTimerTimeout()
    {
        lblCustomerOrder.Visible = false;
        lblPatienceTimer.Visible = false;
        StartAngry();
    }

    public void SetCurrentPath(int pathIndex)
    {
        currentPath = pathIndex;
    }

    public void OnAnimatedSpriteAnimationFinished()
    {
        if(customerSprite.Animation == "angry")
        {
            customerSprite.Animation = "angry-idle";
            customerPatienceTimer.Stop();
            customerBubble.Animation = "angry";
            customerBubble.Visible = true;
            isCycleCompleted = true;
            master.DeductHealth(1);
        }
    }
}
