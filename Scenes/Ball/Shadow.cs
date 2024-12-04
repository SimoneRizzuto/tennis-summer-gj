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

    public float NetHeight => net.NetSprite.Texture.GetHeight();
    public int PlayerHeight => 40; // get from player variable
    public int ArmHeight => 60; // get from player variable
    public bool IsReachableHeight => CollisionMask == HeightLevel.Shadow + HeightLevel.Net || CollisionMask == HeightLevel.Shadow + HeightLevel.Eye || CollisionMask == HeightLevel.Shadow + HeightLevel.Arm;

    public bool OnPlayerSide => Position.Y < net.Position.Y;
    public bool LastHitByPlayer = false;
    public bool BouncedOnce = false;
    
    public override void _Ready()
    {
        ball = GetNodeHelper.GetBall(GetTree());
        net = GetNodeHelper.GetNet(GetTree());
        player = GetNodeHelper.GetPlayer(GetTree());
    }

    public override void _PhysicsProcess(double delta)
    {
        ball = GetNodeHelper.GetBall(GetTree());
        net = GetNodeHelper.GetNet(GetTree());
        player = GetNodeHelper.GetPlayer(GetTree());
        
        if (ball == null || net == null || player == null)
        {
            return;
        }
        
        ProcessBallHeight();
        CorrectBallXToShadow();
    }

    private void ProcessBallHeight()
    {
        //GD.Print($"Ball Height: {ball.Height}, Net Height: {NetHeight}");
        
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

    private void CorrectBallXToShadow()
    {
        var x = LinearVelocity.X;
        var y = ball.LinearVelocity.Y;
        
        ball.LinearVelocity = new(x, y);
    }
}
