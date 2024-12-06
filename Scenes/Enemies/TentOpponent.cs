using Godot;
using System;

public partial class TentOpponent : Enemy
{
    public override void GoToNextRound()
    {
        VsScreen.SpawnScreen(GetTree().CurrentScene, "Ben Ten (10 year old)", "res://Scenes/Enemies/KidOpponent.tscn");
    }

    public override void RestartRound()
    {
        VsScreen.SpawnScreen(GetTree().CurrentScene, "The Tent...?", "res://Scenes/Enemies/TentOpponent.tscn");
    }
}
