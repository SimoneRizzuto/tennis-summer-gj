using System;
using System.Diagnostics;
using Godot;
using TennisSummerGJ2024.UtilityClasses.Extensions;
using TennisSummerGJ2024.UtilityClasses.Helpers;
using TennisSummerGJ2024.UtilityClasses.Shared;

namespace TennisSummerGJ2024.Scenes.Player;
public partial class Player : CharacterBody2D
{
    private Ball ball;
    private Shadow shadow;
    private Area2D swingArea;
    private ColorRect playerRect;

    private SwingDirection swingDirection = 0;
    private Direction lastMovedDirection = Direction.None;

    private bool IsSwinging => swingDurationTimer.IsRunning;

    private bool isInRange;

    private float moveSpeed = 7500f;
    
    private readonly Stopwatch swingDurationTimer = new();
    private int swingDuration = 2000;

    public override void _Ready()
    {
        var tree = GetTree();
        
        ball ??= GetNodeHelper.GetBall(tree);
        shadow ??= GetNodeHelper.GetShadow(tree);
        swingArea ??= GetNode<Area2D>("SwingArea");
        playerRect ??= GetNode<ColorRect>("PlayerRect");
        
        // signals to subscribe
        swingArea.BodyShapeEntered += OnBodyShapeEntered;
        swingArea.BodyShapeExited += OnBodyShapeExited;
    }
    
    public override void _PhysicsProcess(double delta)
    {
        ball = GetNodeHelper.GetBall(GetTree());
        shadow = GetNodeHelper.GetShadow(GetTree());
        swingArea = GetNode<Area2D>("SwingArea");
        playerRect = GetNode<ColorRect>("PlayerRect");
        
        ProcessMovement(delta);

        ProcessSwinging(delta);
    }

    private void ProcessMovement(double delta)
    {
        var movementVector = Input.GetVector(InputMapAction.MoveLeft, InputMapAction.MoveRight, InputMapAction.MoveUp, InputMapAction.MoveDown);
        var calculatedVelocity = movementVector * moveSpeed;
        
        Velocity = calculatedVelocity * (float)delta;
        MoveAndSlide();
        
        var roundedPosition = Position.RoundToNearestValue(0.25f);
        Position = roundedPosition;
        
        lastMovedDirection = Direction.None;

        if (movementVector != Vector2.Zero)
        {
            var direction = DirectionHelper.GetSnappedDirection(movementVector);
            if (direction == Direction.Left || direction == Direction.Right)
            {
                lastMovedDirection = direction;
            }
        }
    }

    private void ProcessSwinging(double delta)
    {
        if (IsSwinging)
        {
            if (isInRange && shadow.IsReachableHeight)
            {
                if (swingDirection == SwingDirection.Down)
                {
                    // play animation
                }
                else if (swingDirection == SwingDirection.Up)
                {
                    // play animation
                }
                
                ApplyForce();
                StopSwing();
            }
            
            var swingDurationHasFinished = swingDurationTimer.ElapsedMilliseconds >= swingDuration/* * delta*/;
            if (swingDurationHasFinished)
            {
                StopSwing();
            }
        }
        else if (Input.IsActionJustPressed(InputMapAction.SwingDown))
        {
            swingDirection = SwingDirection.Down;
            swingDurationTimer.Restart();
            
            playerRect.Color = Colors.Red;
        }
        else if (Input.IsActionJustPressed(InputMapAction.SwingUp))
        {
            swingDirection = SwingDirection.Up;
            swingDurationTimer.Restart();
            
            playerRect.Color = Colors.Blue;
        }
    }

    private void ApplyForce()
    {
        shadow = GetNodeHelper.GetShadow(GetTree());
        ball = GetNodeHelper.GetBall(GetTree());
        
        var height = ball.Height;

        int ballY = 0;
        int shadowY = 0;

        switch (swingDirection)
        {
            case SwingDirection.Up:
                if (height < 10) // big hit
                {
                    ballY = 400;
                    shadowY = 125;
                }
                else if (height < 30)
                {
                    ballY = 350;
                    shadowY = 125;
                }
                else if (height < 40)
                {
                    ballY = 200;
                    shadowY = 125;
                }
                break;
            case SwingDirection.Down:
                if (height < 15)
                {
                    ballY = 250;
                    shadowY = 75;
                }
                else if (height < 30)
                {
                    ballY = 250;
                    shadowY = 125;
                }
                else if (height < 40)
                {
                    ballY = 250;
                    shadowY = 175;
                }
                else if (height < 50) // big hit
                {
                    ballY = 250;
                    shadowY = 250;
                }
                break;
        }

        var shadowX = 0;
        
        if (lastMovedDirection == Direction.Left)
        {
            shadowX = -100;
        }
        else if (lastMovedDirection == Direction.Right)
        {
            shadowX = 100;
        }
        
        shadow.LinearVelocity = new(shadowX, -shadowY);
        ball.LinearVelocity = new(shadowX, -ballY);

        shadow.LastHitByPlayer = true;

        //GD.Print($"Ball Height: {height}, {Enum.GetName(swingDirection)}");
    }
    
    private void StopSwing()
    {
        swingDurationTimer.Reset();
        
        playerRect.Color = Colors.White;
    }
    
    // signals
    private void OnBodyShapeEntered(Rid bodyRid, Node2D body, long bodyShapeIndex, long localShapeIndex)
    {
        if (body is Shadow)
        {
            isInRange = true;
        }
    }
    
    private void OnBodyShapeExited(Rid bodyRid, Node2D body, long bodyShapeIndex, long localShapeIndex)
    {
        if (body is Shadow)
        {
            isInRange = false;
        }
    }
}