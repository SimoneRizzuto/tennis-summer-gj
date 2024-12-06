using Godot;
using System;

public partial class TentOpponent : Enemy
{
    public override void GoToNextRound()
    {
        VsScreen.SpawnScreen(GetTree().CurrentScene, "Bitches be shoppin\'", "res://Scenes/Enemies/Enemy.tscn");
    }

    public override void RestartRound()
    {
        VsScreen.SpawnScreen(GetTree().CurrentScene, "The Tent...?", "res://Scenes/Enemies/TentOpponent.tscn");
    }
}
