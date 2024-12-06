using Godot;
using System;
using TennisSummerGJ2024.UtilityClasses.Helpers;

public partial class TentacleOpponent : Enemy
{
	public override void GoToNextRound()
	{
		var scene = ResourceLoader.Load<PackedScene>("res://Scenes/VictoryScreen/VictoryScreen.tscn").Instantiate();
		GetTree().CurrentScene.AddChild(scene);
		
		var gameRoom = GetNodeHelper.GetGameRoom(GetTree());
		gameRoom.QueueFree();
	}

	public override void RestartRound()
	{
		VsScreen.SpawnScreen(GetTree().CurrentScene, "The Tentacle of the Lake", "res://Scenes/Enemies/TentacleOpponent.tscn", "res://Assets/Sprites/Tentacle Portrait.png", "res://Assets/Audio/Music - Tenticle Reveal.ogg");
	}
}
