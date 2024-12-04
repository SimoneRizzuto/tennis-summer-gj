using Godot;
using System;
using TennisSummerGJ2024.UtilityClasses.Helpers;

public partial class ScoreManager : Node2D
{
    public bool ScoreHasBeenTallied;
    
    private Ball ball;
    private Shadow shadow;

    private int playerScore = 0;
    private int opponentScore = 0;
    
    public override void _Ready()
    {
        FetchNodes();
    }
    
    private void FetchNodes()
    {
        ball = GetNodeHelper.GetBall(GetTree());
        shadow = GetNodeHelper.GetShadow(GetTree());
    }
    
    public void RoundWin()
    {
        if (shadow.OnPlayerSide)
        {
            if (shadow.BouncedOnce && shadow.LastHitByPlayer)
            {
                playerScore += 15;
            }
        }
        else
        {
            if (shadow.BouncedOnce && !shadow.LastHitByPlayer)
            {
                opponentScore += 15;
            }
        }
        
        GD.Print($"Player: {playerScore}, Opponent: {opponentScore}");

        ScoreHasBeenTallied = true;
    }
}