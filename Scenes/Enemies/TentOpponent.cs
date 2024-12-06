using Godot;
using System;

public partial class TentOpponent : Enemy
{
    public override void GoToNextRound()
    {
        VsScreen.SpawnScreen(GetTree().CurrentScene, "Ben Ten (10 year old)", "res://Scenes/Enemies/KidOpponent.tscn", "res://Assets/Sprites/Kid Portrait.png", "res://Assets/Audio/Music - 10YO.ogg");
    }

    public override void RestartRound()
    {
        VsScreen.SpawnScreen(GetTree().CurrentScene, "The Tent...?", "res://Scenes/Enemies/TentOpponent.tscn", "res://Assets/Sprites/Tent Portrait.png", "res://Assets/Audio/Music - Tent Reveal.ogg");
    }
}
