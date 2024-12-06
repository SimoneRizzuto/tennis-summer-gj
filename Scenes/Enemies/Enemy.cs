using Godot;
using System;
using System.Diagnostics;
using TennisSummerGJ2024.UtilityClasses.Extensions;
using TennisSummerGJ2024.UtilityClasses.Helpers;

public partial class Enemy : CharacterBody2D
{
    private EnemyState enemyState = 0;

    private Ball Ball => GetNodeHelper.GetBall(GetTree());
    private Shadow Shadow => GetNodeHelper.GetShadow(GetTree());
    private ScoreManager ScoreManager => GetNodeHelper.GetScoreManager(GetTree());
    
    private Area2D swingArea;

    private int movementSpeed = 5000;
    private Stopwatch swingDurationTimer = new();

    private void FindNodes()
    {
        swingArea = GetNode<Area2D>("SwingArea");
    }
    
    public override void _Ready()
    {
        FindNodes();
        
        swingArea.BodyShapeEntered += OnSwingAreaBodyShapeEntered;
    }
    
    public override void _Process(double delta)
    {
        if (ScoreManager.Winner.HasValue)
        {
            HandleRoundWin();
        }
    }

    private void HandleRoundWin()
    {
        if (ScoreManager.Winner == Person.Player)
        {
            GoToNextRound();
        }
        else
        {
            RestartRound();
        }
        
        var gameRoom = GetNodeHelper.GetGameRoom(GetTree());
        gameRoom.QueueFree();
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
        
        if (Shadow.PlayerBall)
        {
            enemyState = EnemyState.Repositioning;
        }
    }

    private void ProcessRepositioning(double delta)
    {
        if (Name == "TentOpponent") return;
        
        if (Math.Abs(Position.X - Shadow.Position.X) < 10)
        {
            Velocity = Vector2.Zero;
            return;
        }
        
        var directionToBall = Position.DirectionTo(new (Shadow.Position.X, Position.Y));
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
        if (body is not global::Shadow) return;

        if (enemyState != EnemyState.Swinging)
        {
            swingDurationTimer.Restart();
            
            enemyState = EnemyState.Swinging;
            Velocity = Vector2.Zero;

            if (Shadow.IsReachableHeight)
            {
                ApplyForces();
                
                Shadow.BouncedOnce = false;
                
                StopSwing();
            }
        }
    }

    private void ApplyForces()
    {
        Shadow.LinearVelocity = new(0, 150);
        Ball.LinearVelocity = new(0, 0);
    }
    
    private void StopSwing()
    {
        swingDurationTimer.Reset();
        
        enemyState = EnemyState.Waiting;
    }
    
    public virtual void GoToNextRound() { }
    public virtual void RestartRound() { }
}

public enum EnemyState
{
    Waiting = 0,
    Repositioning = 1,
    Swinging = 2,
    
}
