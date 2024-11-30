using Godot;
using System;
using TennisSummerGJ2024.UtilityClasses.Helpers;

public partial class Ball : StaticBody2D
{
    public double CurrentHeight = 50;
    
    private double heightDropSpeed = 2;
    private Shadow shadow;
    
    public override void _Ready()
    {
        shadow ??= GetNodeHelper.GetShadow(GetTree());
    }

    public override void _PhysicsProcess(double delta)
    {
        
        Position = shadow.Position - new Vector2(0, (float)CurrentHeight);
        
        GD.Print("Ball - GLOBAL POSITION: " + GlobalPosition);
        GD.Print("Ball - POSITION: " + Position);
    }
}
