using Godot;
using System;

public partial class KidOpponent : Enemy
{
	public override void GoToNextRound()
	{
		VsScreen.SpawnScreen(GetTree().CurrentScene, "Agent 10", "res://Scenes/Enemies/AgentOpponent.tscn", "res://Assets/Sprites/Agent 10 Portrait.png", "res://Assets/Audio/Music - Agent 10.ogg");
	}

	public override void RestartRound()
	{
		VsScreen.SpawnScreen(GetTree().CurrentScene, "Ben Ten (10 year old)", "res://Scenes/Enemies/KidOpponent.tscn", "res://Assets/Sprites/Kid Portrait.png", "res://Assets/Audio/Music - 10YO.ogg");
	}
}
