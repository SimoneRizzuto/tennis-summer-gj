using Godot;
using System;

public partial class GameRoom : Node2D
{
	public void SpawnOpponent(string opponentPath)
	{
		var enemyNode = ResourceLoader.Load<PackedScene>(opponentPath).Instantiate();

		var enemy = (Node2D)enemyNode;
		enemy.Position = new(401, 273);
        
		AddChild(enemy);
	}
}
