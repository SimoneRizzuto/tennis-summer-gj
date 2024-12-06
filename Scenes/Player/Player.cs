using System;
using System.Diagnostics;
using Godot;
using TennisSummerGJ2024.UtilityClasses.Extensions;
using TennisSummerGJ2024.UtilityClasses.Helpers;
using TennisSummerGJ2024.UtilityClasses.Shared;

namespace TennisSummerGJ2024.Scenes.Player;
public partial class Player : CharacterBody2D
{
    private Ball Ball => GetNodeHelper.GetBall(GetTree());
    private Shadow Shadow => GetNodeHelper.GetShadow(GetTree());
    private Area2D SwingArea => GetNode<Area2D>("SwingArea");
    private ColorRect PlayerRect => GetNode<ColorRect>("PlayerRect");
    private AnimatedSprite2D Animation => GetNode<AnimatedSprite2D>("AnimatedSprite2D");

    
    
    private SwingDirection swingDirection = 0;
    private Direction lastMovedDirection = Direction.None;

    private bool IsSwinging => swingDurationTimer.IsRunning;

    private bool isInRange;

    private float moveSpeed = 7500f;
    
    private readonly Stopwatch swingDurationTimer = new();
    private int swingDuration = 2000;

    public override void _Ready()
    {
        // signals to subscribe
        SwingArea.BodyShapeEntered += OnBodyShapeEntered;
        SwingArea.BodyShapeExited += OnBodyShapeExited;
    }
    
    public override void _PhysicsProcess(double delta)
    {
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
            if (isInRange && Shadow.IsReachableHeight)
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
            
            PlayerRect.Color = Colors.Red;
        }
        else if (Input.IsActionJustPressed(InputMapAction.SwingUp))
        {
            swingDirection = SwingDirection.Up;
            swingDurationTimer.Restart();
            
            PlayerRect.Color = Colors.Blue;
        }
    }

    private void ApplyForce()
    {
        var height = Ball.Height;

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
        
        Shadow.LinearVelocity = new(shadowX, -shadowY);
        Ball.LinearVelocity = new(shadowX, -ballY);

        Shadow.PlayerBall = true;
        Shadow.BouncedOnce = false;

        //GD.Print($"Ball Height: {height}, {Enum.GetName(swingDirection)}");
    }
    
    private void StopSwing()
    {
        swingDurationTimer.Reset();
        
        PlayerRect.Color = Colors.White;
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

public enum PlayerState
{
    Idle = 0,
    
}