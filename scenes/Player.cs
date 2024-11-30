using System;
using Godot;
using TennisSummerGJ2024.scenes.extensions;
using TennisSummerGJ2024.scenes.shared;

namespace TennisSummerGJ2024.scenes;
public partial class Player : CharacterBody2D
{
    private float moveSpeed = 10000f;
    
    public override void _PhysicsProcess(double delta)
    {
        ProcessMovement(delta);
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
    
    
    
    // debugging methods
    private void LogMovement(Vector2 movementVector)
    {
        Console.WriteLine($"Movement Vector: {movementVector}");
        Console.WriteLine($"Movement Vector: {Velocity}");
        Console.WriteLine("----------------------------------");
    }
}