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

    public float NetHeight => net.NetShape.GetRect().Size.Y;
    public int PlayerHeight => 40; // get from player variable
    public int ArmHeight => 60; // get from player variable
    
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
        if (ball.Height <= NetHeight - 1)
        {
            CollisionMask = HeightLevel.Shadow + HeightLevel.Net;
        }
        else if (ball.Height <= PlayerHeight)
        {
            CollisionMask = HeightLevel.Shadow + HeightLevel.Eye;
        }
        else if (ball.Height <= ArmHeight)
        {
            CollisionMask = HeightLevel.Shadow + HeightLevel.Arm;
        }
        else // too high to reach
        {
            CollisionMask = HeightLevel.Shadow + HeightLevel.Sky;
        }
    }
}
