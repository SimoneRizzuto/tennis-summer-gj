using Godot;
using System;
using TennisSummerGJ2024.Scenes.Player;
using TennisSummerGJ2024.UtilityClasses.Helpers;
using TennisSummerGJ2024.UtilityClasses.Shared;

public partial class Shadow : RigidBody2D
{
    private Ball Ball => GetNodeHelper.GetBall(GetTree());
    private Net Net => GetNodeHelper.GetNet(GetTree());
    private Player Player => GetNodeHelper.GetPlayer(GetTree());

    public float NetHeight => Net.GetNode<Sprite2D>("Net").Texture.GetHeight();
    public int PlayerHeight => 40; // get from player variable
    public int ArmHeight => 60; // get from player variable
    public bool IsReachableHeight => CollisionMask == HeightLevel.Shadow + HeightLevel.Net || CollisionMask == HeightLevel.Shadow + HeightLevel.Eye || CollisionMask == HeightLevel.Shadow + HeightLevel.Arm;

    public bool OnPlayerSide => Position.Y > GetNodeHelper.GetNet(GetTree())?.Position.Y;
    public bool PlayerBall = false; // is the player's ball
    public bool BouncedOnce = false;
    
    public override void _PhysicsProcess(double delta)
    {
        
        if (Ball == null || Net == null || Player == null)
        {
            return;
        }
        
        ProcessBallHeight();
        CorrectBallXToShadow();
    }

    private void ProcessBallHeight()
    {
        //GD.Print($"Ball Height: {ball.Height}, Net Height: {NetHeight}");
        
        if (Ball.Height <= NetHeight - 1)
        {
            CollisionMask = HeightLevel.Shadow + HeightLevel.Net;
        }
        else if (Ball.Height <= PlayerHeight)
        {
            CollisionMask = HeightLevel.Shadow + HeightLevel.Eye;
        }
        else if (Ball.Height <= ArmHeight)
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
        var y = Ball.LinearVelocity.Y;
        
        Ball.LinearVelocity = new(x, y);
    }
    
    public static void RespawnBall(SceneTree tree)
    {
        var oldShadowBall = GetNodeHelper.GetShadow(tree);
        oldShadowBall.QueueFree();
            
        var scene = ResourceLoader.Load<PackedScene>("res://Scenes/Ball/BallModule.tscn").Instantiate();

        var newShadowBall = (Node2D)scene;
        newShadowBall.Position = new(403, 393);
        
        tree.CurrentScene.AddChild(newShadowBall);
    }
}
