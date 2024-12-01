using Godot;
using System;
using TennisSummerGJ2024.Scenes.Player;
using TennisSummerGJ2024.UtilityClasses.Helpers;
using TennisSummerGJ2024.UtilityClasses.Shared;

public partial class Shadow : RigidBody2D
{
    private Ball ball;
    private Net net;
    private Player player;
    
    public override void _Ready()
    {
        ball ??= GetNodeHelper.GetBall(GetTree());
        net ??= GetNodeHelper.GetNet(GetTree());
        player ??= GetNodeHelper.GetPlayer(GetTree());
    }

    public override void _Process(double delta)
    {
        ProcessBallHeight();
    }

    private void ProcessBallHeight()
    {
        var netHeight = net.NetShape.GetRect().Size.Y;
        var playerHeight = 40; // get from player variable
        
        if (ball.DistanceToGround <= netHeight - 1)
        {
            CollisionMask = HeightLevel.Shadow + HeightLevel.Net;
        }
        else if (ball.DistanceToGround <= playerHeight)
        {
            CollisionMask = HeightLevel.Shadow + HeightLevel.Eye;
        }
        else if (ball.DistanceToGround <= 60)
        {
            CollisionMask = HeightLevel.Shadow + HeightLevel.Arm;
        }
        else
        {
            CollisionMask = HeightLevel.Shadow + HeightLevel.Sky;
        }
    }
}
