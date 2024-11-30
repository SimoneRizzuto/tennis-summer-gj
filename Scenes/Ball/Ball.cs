using Godot;
using System;
using TennisSummerGJ2024.UtilityClasses.Helpers;

public partial class Ball : StaticBody2D
{
    public double CurrentHeight = 50;
    private float DistanceToGround => GlobalPosition.DistanceTo(shadow.GlobalPosition);

    private double heightDropSpeed = 2;
    private Shadow shadow;
    
    
    
    public override void _Ready()
    {
        shadow ??= GetNodeHelper.GetShadow(GetTree());
    }

    public override void _PhysicsProcess(double delta)
    {
        GlobalPosition = shadow.GlobalPosition - new Vector2(0, (float)CurrentHeight);

        if (DistanceToGround > 5)
        {
            CurrentHeight -= 1;
        }
        
        GD.Print("Ball - GLOBAL POSITION: " + GlobalPosition);
        GD.Print("Ball - POSITION: " + Position);
        GD.Print("Distance To: " + DistanceToGround);
    }
}
