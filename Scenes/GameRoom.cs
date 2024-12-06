using Godot;
using System;

public partial class GameRoom : Node2D
{
	public void SpawnOpponent()
	{
		var tree = GetTree();
		
		var enemyNode = ResourceLoader.Load<PackedScene>("res://Scenes/Enemies/Enemy.tscn").Instantiate();

		var enemy = (Node2D)enemyNode;
		enemy.Position = new(401, 273);
        
		tree.CurrentScene.AddChild(enemy);
	}
}
