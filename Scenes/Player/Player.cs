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

    private bool IsSwinging => swingDurationTimer.IsRunning;
    private bool IsReachableHeight
    {
        get
        {
            if (shadow.CollisionMask == HeightLevel.Shadow + HeightLevel.Net
                || shadow.CollisionMask == HeightLevel.Shadow + HeightLevel.Eye)
            {
                return true;
            }
            
            return false;
        }
    }

    private bool isInRange;

    private float moveSpeed = 10000f;
    
    private readonly Stopwatch swingDurationTimer = new();
    private int swingDuration = 1000;

    public override void _Ready()
    {
        var tree = GetTree();
        
        ball ??= GetNodeHelper.GetBall(tree);
        shadow ??= GetNodeHelper.GetShadow(tree);
        swingArea ??= GetNode<Area2D>("SwingArea");
        
        // signals to subscribe
        swingArea.BodyShapeEntered += OnBodyShapeEntered;
        swingArea.BodyShapeExited += OnBodyShapeExited;
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
    }

    private void ProcessSwinging(double delta)
    {
        if (IsSwinging)
        {
            if (isInRange && IsReachableHeight)
            {
                ApplyForce();
                StopSwing();
            }
            
            var swingDurationHasFinished = swingDurationTimer.ElapsedMilliseconds >= swingDuration/* * delta*/;
            if (swingDurationHasFinished)
            {
                StopSwing();
            }
        }
        else if (Input.IsActionJustPressed(InputMapAction.Swing))
        {
            GD.Print("Swinging...");
            swingDurationTimer.Restart();
        }
    }

    private void ApplyForce()
    {
        var heightForce = 15 * 1000;
        
        ball.ApplyForce(new(0, -heightForce/* / 1000*/));
        shadow.ApplyForce(new(0, -heightForce));
    }
    
    private void StopSwing()
    {
        GD.Print("Swing over...");
        
        swingDurationTimer.Reset();
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



/*private void LogMovement(Vector2 movementVector)
{
    GD.Print($"Movement Vector: {movementVector}");
    GD.Print($"Movement Vector: {Velocity}");
    GD.Print("----------------------------------");
}*/