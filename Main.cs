using Godot;
using System;
using System.Linq;
using TennisSummerGJ2024.UtilityClasses.Helpers;
using TennisSummerGJ2024.UtilityClasses.Shared;

public partial class Main : Node2D
{
    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed(InputMapAction.RespawnBall))
        {
            var childNode = GetChild(0);
            if (childNode.Name == "MainMenu")
            {
                return;
            }
            
            var tree = GetTree();
            
            var oldShadowBall = GetNodeHelper.GetShadow(tree);
            oldShadowBall.Free();
            
            var scene = ResourceLoader.Load<PackedScene>("res://Scenes/Ball/BallModule.tscn").Instantiate();

            var newShadowBall = (Node2D)scene;
            newShadowBall.Position = new(403, 393);
            
            childNode.AddChild(newShadowBall);
        }
    }

    /*private void SortNodesByGlobalPosition()
    {
        // Get all child nodes of the current node
        var children = GetChildren();

        // Filter out only Node2D-derived objects if needed (e.g., sprites, etc.)
        var node2DChildren = children.OfType<Node2D>().ToList();

        // Sort the nodes based on their GlobalPosition's Y value
        node2DChildren.Sort((a, b) => a.GlobalPosition.Y.CompareTo(b.GlobalPosition.Y));

        // Now the nodes are sorted by their global Y position
        // If you want to reorder them in the scene tree, you can do this:
        for (int i = 0; i < node2DChildren.Count; i++)
        {
            // Reorder nodes by moving them in the scene tree
            node2DChildren[i].GetParent().MoveChild(node2DChildren[i], i);
        }
    }*/
}
