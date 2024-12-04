using Godot;
using System;
using TennisSummerGJ2024.UtilityClasses.Helpers;
using TennisSummerGJ2024.UtilityClasses.Shared;

public partial class Ball : RigidBody2D
{
    public float Height => GlobalPosition.DistanceTo(shadow.GlobalPosition);
    
    private double heightDropSpeed = 2;
    private Shadow shadow;
    
    public override void _Ready()
    {
        shadow ??= GetNodeHelper.GetShadow(GetTree());
        
        BodyShapeEntered += OnBodyShapeEntered;
    }

    public override void _Process(double delta)
    {
        shadow = GetNodeHelper.GetShadow(GetTree());
        
        //GlobalPosition = new Vector2(shadow.GlobalPosition.X, GlobalPosition.Y);

        //GD.Print($"Ball: {LinearVelocity}");
    }
    
    // signals
    private void OnBodyShapeEntered(Rid bodyRid, Node body, long bodyShapeIndex, long localShapeIndex)
    {
        if (body is Shadow shadowNode)
        {
            if (shadowNode.BouncedOnce)
            {
                var scoreManager = GetNodeHelper.GetScoreManager(GetTree());
                if (scoreManager.ScoreHasBeenTallied) return;
                
                scoreManager.RoundWin();
            }
            else
            {
                shadowNode.BouncedOnce = true;
            }
        }
    }
}
