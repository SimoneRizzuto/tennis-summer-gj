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
    
    public override void _Ready()
    {
        ball = GetNodeHelper.GetBall(GetTree());
        net = GetNodeHelper.GetNet(GetTree());
        player = GetNodeHelper.GetPlayer(GetTree());
        
        BodyEntered += HandlePhysics;
    }

    public override void _Process(double delta)
    {
        ball = GetNodeHelper.GetBall(GetTree());
        net = GetNodeHelper.GetNet(GetTree());
        player = GetNodeHelper.GetPlayer(GetTree());
        
        if (ball == null || net == null || player == null)
        {
            return;
        }
        
        ProcessBallHeight();

        //GD.Print($"Shadow: {LinearVelocity}");
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
    
    private void HandlePhysics(Node node)
    {
        ball = GetNodeHelper.GetBall(GetTree());

        var x = LinearVelocity.X;
        var y = ball.LinearVelocity.Y;
        
        ball.LinearVelocity = new(x, y);
        
        GD.Print("Collision detected..");
    }

    private void ProcessBallXAxis()
    {
        /*
        
        // Get the current transform
        var currentTransform = ball.Transform;
        
        // Set the X value while preserving the Y value
        currentTransform.Origin.X = Position.X;
        
        // Apply the new transform
        ball.Transform = currentTransform;*/
        
        
        /*if (Math.Abs(ball.Position.X - Position.X) > 100)
        {
            var ballYVelocity = ball.LinearVelocity.Y;
            ball.LinearVelocity = new Vector2(LinearVelocity.X, ballYVelocity);
        }*/
        
        
        
        
        
        //var test = ball.Transform.Origin;
        //ball.Transform = new Transform2D(new(Position.X, 0), new(0, ball.), ball.Transform.Origin);
        //ball.LinearVelocity = new(LinearVelocity.X, ball.LinearVelocity.Y);
        //ball.Position = new Vector2(ball.Position.X, ball.Position.Y);
    }
}
