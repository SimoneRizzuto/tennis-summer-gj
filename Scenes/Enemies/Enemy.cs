using Godot;
using System;
using TennisSummerGJ2024.UtilityClasses.Helpers;

public partial class Enemy : CharacterBody2D
{
    private EnemyState enemyState = 0;

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

        switch (enemyState)
        {
            case EnemyState.Waiting:
                ProcessWaiting();
                break;
            case EnemyState.Repositioning:
                ProcessRepositioning();
                break;
            case EnemyState.Swinging:
                ProcessSwinging();
                break;
        }
        
    }

    private void ProcessWaiting()
    {
        if (shadow.LastHitByPlayer)
        {
            enemyState = EnemyState.Repositioning;
        }
    }

    private void ProcessRepositioning()
    {
        Position = new(shadow.Position.X, Position.Y);
    }

    private void ProcessSwinging()
    {
        
    }
}

public enum EnemyState
{
    Waiting = 0,
    Repositioning = 1,
    Swinging = 2,
    
}
