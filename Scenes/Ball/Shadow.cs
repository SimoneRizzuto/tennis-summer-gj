using Godot;
using System;
using TennisSummerGJ2024.UtilityClasses.Helpers;
using TennisSummerGJ2024.UtilityClasses.Shared;

public partial class Shadow : StaticBody2D
{
    private Ball ball;
    
    public override void _Ready()
    {
        ball ??= GetNodeHelper.GetBall(GetTree());
    }

    public override void _Process(double delta)
    {
        Position = ball.GlobalPosition - new Vector2(0, -20);
    }
}
