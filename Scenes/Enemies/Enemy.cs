using Godot;
using System;
using TennisSummerGJ2024.UtilityClasses.Helpers;

public partial class Enemy : CharacterBody2D
{
    private EnemyState state = 0;

    private Ball ball;
    private Shadow shadow;

    private void FindNodes()
    {
        var tree = GetTree();

        ball = GetNodeHelper.GetBall(tree);
        shadow = GetNodeHelper.GetShadow(tree);
    }
    
    public override void _Process(double delta)
    {
        FindNodes();
        
        
    }
}

public enum EnemyState
{
    Idle = 0,
    Repositioning = 1,
    Swinging = 2,
    
}
