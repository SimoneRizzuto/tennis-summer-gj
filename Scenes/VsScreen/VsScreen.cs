using Godot;
using System;
using TennisSummerGJ2024.UtilityClasses.Shared;

public partial class VsScreen : Control
{
    private const string MainGamePath = "res://Scenes/GameRoom.tscn";
    
    public Label PlayerName => GetNode<Label>("PlayerName");
    public Label OpponentName => GetNode<Label>("OpponentName");

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed(InputMapAction.Interact))
        {
            var scene = ResourceLoader.Load<PackedScene>(MainGamePath).Instantiate();
            
            GetParent().AddChild(scene);
            QueueFree();
        }
    }
}
