using Godot;
using System;

public partial class AgentOpponent : Enemy
{
	public override void GoToNextRound()
	{
		VsScreen.SpawnScreen(GetTree().CurrentScene, "Joe Baseball", "res://Scenes/Enemies/BaseballOpponent.tscn", "res://Assets/Sprites/BASEBALL Portrait.png", "res://Assets/Audio/Music - Baseballer Reveal.ogg");
	}

	public override void RestartRound()
	{
		VsScreen.SpawnScreen(GetTree().CurrentScene, "Agent 10", "res://Scenes/Enemies/AgentOpponent.tscn", "res://Assets/Sprites/Agent 10 Portrait.png", "res://Assets/Audio/Music - Agent 10.ogg");
	}
}
