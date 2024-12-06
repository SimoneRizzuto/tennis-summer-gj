using Godot;
using System;
using System.ComponentModel.Design.Serialization;
using TennisSummerGJ2024.UtilityClasses.Helpers;

public partial class ScoreManager : Node2D
{
    public string PlayerDisplayScore => DisplayScore(playerScore);
    public string OpponentDisplayScore => DisplayScore(opponentScore);
    public bool ScoreHasBeenTallied;

    public Person? Winner; 
    
    private Ball Ball => GetNodeHelper.GetBall(GetTree());
    private Shadow Shadow => GetNodeHelper.GetShadow(GetTree());

    private int playerScore = 0;
    private int opponentScore = 0;
    
    public override void _Process(double delta)
    {
        if (playerScore == 5)
        {
            GD.Print("GAME - Player Wins");
            return;
        }
        
        if (opponentScore == 5)
        {
            GD.Print("GAME - Opponent Wins");
            return;
        }
        
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
                IncrementScore(Person.Opponent);
            }
            else
            {
                IncrementScore(Person.Player);
            }
        }
        else // Opponent's ball
        {
            if (Shadow.OnPlayerSide)
            {
                IncrementScore(Person.Opponent);
            }
            else
            {
                IncrementScore(Person.Player);
            }
        }
        
        GD.Print($"Player: {PlayerDisplayScore}, Opponent: {OpponentDisplayScore}");
        
        ScoreHasBeenTallied = true;
    }

    private void IncrementScore(Person person)
    {
        switch (person)
        {
            case Person.Player:
            {
                if (opponentScore == 4)
                {
                    opponentScore--;
                }
                else
                {
                    playerScore++;
                }
                
                if (playerScore > 4)
                {
                    Winner = Person.Player;
                }

                break;
            }
            case Person.Opponent:
            {
                if (playerScore == 4)
                {
                    playerScore--;
                }
                else
                {
                    opponentScore++;
                }

                if (opponentScore > 4)
                {
                    Winner = Person.Opponent;
                }

                break;
            }
        }
    }

    private string DisplayScore(int score)
    {
        switch (score)
        {
            case 0: return "0";
            case 1: return "15";
            case 2: return "30";
            case 3: return "40";
            case 4: return "A";
            case 5: return "G";
            default: return "";
        }
    }

    private void RespawnBall()
    {
        Shadow.RespawnBall(GetTree());
        ScoreHasBeenTallied = false;
    }
}

public enum Person
{
    Player,
    Opponent,
}