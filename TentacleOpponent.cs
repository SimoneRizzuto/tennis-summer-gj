using Godot;
using System;

public partial class TentacleOpponent : Enemy
{
	public override void RestartRound()
	{
		VsScreen.SpawnScreen(GetTree().CurrentScene, "The Tentacle of the Lake", "res://Scenes/Enemies/TentacleOpponent.tscn", "res://Assets/Sprites/Tentacle Portrait.png");
	}
}
