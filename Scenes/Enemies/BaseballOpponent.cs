using Godot;
using System;

public partial class BaseballOpponent : Enemy
{
	public override void GoToNextRound()
	{
		VsScreen.SpawnScreen(GetTree().CurrentScene, "The Tentacle of the Lake", "res://Scenes/Enemies/TentacleOpponent.tscn", "res://Assets/Sprites/Tentacle Portrait.png", "res://Assets/Audio/Music - Tenticle Reveal.ogg");
	}

	public override void RestartRound()
	{
		VsScreen.SpawnScreen(GetTree().CurrentScene, "Joe Baseball", "res://Scenes/Enemies/BaseballOpponent.tscn", "res://Assets/Sprites/BASEBALL Portrait.png", "res://Assets/Audio/Music - Baseballer Reveal.ogg");
	}
}
