using Godot;
using System;
using System.Diagnostics;
using TennisSummerGJ2024.UtilityClasses.Extensions;
using TennisSummerGJ2024.UtilityClasses.Helpers;

public partial class Enemy : CharacterBody2D
{
    private EnemyState enemyState = 0;

    private Ball ball;
    private Shadow shadow;
    private Area2D swingArea;
    private ColorRect enemyRect;

    private int movementSpeed = 5000;
    private Stopwatch swingDurationTimer = new();

    private void FindNodes()
    {
        var tree = GetTree();

        ball = GetNodeHelper.GetBall(tree);
        shadow = GetNodeHelper.GetShadow(tree);
        swingArea = GetNode<Area2D>("SwingArea");
        enemyRect = GetNode<ColorRect>("EnemyRect");
    }
    
    public override void _Ready()
    {
        FindNodes();
        
        swingArea.BodyShapeEntered += OnSwingAreaBodyShapeEntered;
    }
    
    public override void _Process(double delta)
    {
        FindNodes();
    }

    public override void _PhysicsProcess(double delta)
    {
        switch (enemyState)
        {
            case EnemyState.Waiting:
                ProcessWaiting();
                break;
            case EnemyState.Repositioning:
                ProcessRepositioning(delta);
                break;
            case EnemyState.Swinging:
                ProcessSwinging();
                break;
        }
        
        MoveAndSlide();
    }

    private void ProcessWaiting()
    {
        // if not on the back of the court, go there
        
        if (shadow.LastHitByPlayer)
        {
            enemyState = EnemyState.Repositioning;
        }
    }

    private void ProcessRepositioning(double delta)
    {
        if (Math.Abs(Position.X - shadow.Position.X) < 10)
        {
            Velocity = Vector2.Zero;
            return;
        }
        
        var directionToBall = Position.DirectionTo(new (shadow.Position.X, Position.Y));
        Velocity = directionToBall * movementSpeed * (float)delta;
    }

    private void ProcessSwinging()
    {
        if (swingDurationTimer.ElapsedMilliseconds > 1500)
        {
            StopSwing();
        }
    }
    
    // signals
    private void OnSwingAreaBodyShapeEntered(Rid bodyRid, Node2D body, long bodyShapeIndex, long localShapeIndex)
    {
        if (body is not Shadow) return;

        if (enemyState != EnemyState.Swinging)
        {
            swingDurationTimer.Restart();
            
            enemyState = EnemyState.Swinging;
            Velocity = Vector2.Zero;
            enemyRect.Color = Colors.Red;

            if (shadow.IsReachableHeight)
            {
                ApplyForces();
                StopSwing();
            }
        }
    }

    private void ApplyForces()
    {
        shadow.LinearVelocity = new(0, 150);
        ball.LinearVelocity = new(0, 0);
    }
    
    private void StopSwing()
    {
        swingDurationTimer.Reset();
        
        enemyState = EnemyState.Waiting;
        enemyRect.Color = Colors.Orange;
    }
}

public enum EnemyState
{
    Waiting = 0,
    Repositioning = 1,
    Swinging = 2,
    
}
