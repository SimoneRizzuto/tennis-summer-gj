using Godot;
using System;
using TennisSummerGJ2024.UtilityClasses.Helpers;

public partial class ScoreDisplay : Control
{
    private Label PlayerScore => GetNode<Label>("PlayerScore");
    private Label Separator => GetNode<Label>("|");
    private Label EnemyScore => GetNode<Label>("OpponentScore");
    
    public override void _Process(double delta)
    {
        var scoreManager = GetNodeHelper.GetScoreManager(GetTree());

        PlayerScore.Text = scoreManager.PlayerDisplayScore;
        EnemyScore.Text = scoreManager.OpponentDisplayScore;

        //Text = displayText;
    }
}
