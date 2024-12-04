using Godot;
using System;
using System.ComponentModel.Design.Serialization;
using TennisSummerGJ2024.UtilityClasses.Helpers;

public partial class ScoreManager : Node2D
{
    public bool ScoreHasBeenTallied;
    
    private Ball Ball => GetNodeHelper.GetBall(GetTree());
    private Shadow Shadow => GetNodeHelper.GetShadow(GetTree());

    private int playerScore = 0;
    private int opponentScore = 0;

    public override void _Process(double delta)
    {
        if (ScoreHasBeenTallied)
        {
            RespawnBall();
        }
    }

    public void RoundWin()
    {
        if (ScoreHasBeenTallied) return;
        
        if (Shadow.PlayerBall)
        {
            if (Shadow.OnPlayerSide)
            {
                opponentScore += 15;
            }
            else
            {
                playerScore += 15;
            }
        }
        else // Opponent's ball
        {
            if (Shadow.OnPlayerSide)
            {
                opponentScore += 15;
            }
            else
            {
                playerScore += 15;
            }
        }
        
        GD.Print($"Player: {playerScore}, Opponent: {opponentScore}");

        ScoreHasBeenTallied = true;
    }

    private void RespawnBall()
    {
        Shadow.RespawnBall(GetTree());
        ScoreHasBeenTallied = false;
    }
}