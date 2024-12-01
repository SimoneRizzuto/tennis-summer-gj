using Godot;
using System;
using TennisSummerGJ2024.UtilityClasses.Helpers;
using TennisSummerGJ2024.UtilityClasses.Shared;

public partial class Shadow : RigidBody2D
{
    private Ball ball;
    
    public override void _Ready()
    {
        ball ??= GetNodeHelper.GetBall(GetTree());
    }

    public override void _Process(double delta)
    {
        ProcessBallHeight();
    }

    private void ProcessBallHeight()
    {
        var groundHeightThreshold = ball.DistanceToGround <= 35;
        if (groundHeightThreshold)
        {
            CollisionMask = HeightLevel.Shadow + HeightLevel.Ground + HeightLevel.Eye + HeightLevel.Sky; // 15
        }
        else
        {
            CollisionMask = HeightLevel.Shadow + HeightLevel.Eye + HeightLevel.Sky; // 13
        }
    }
}
