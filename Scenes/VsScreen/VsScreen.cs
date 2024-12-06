using Godot;
using System;
using TennisSummerGJ2024.UtilityClasses.Shared;

public partial class VsScreen : Control
{
    private const string MainGamePath = "res://Scenes/GameRoom.tscn";

    [Export] public string OpponentName = "placeholder name";
    [Export] public string OpponentScenePath = "";
    
    public Label PlayerLabel => GetNode<Label>("PlayerName");
    public Label OpponentLabel => GetNode<Label>("OpponentName");

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed(InputMapAction.Interact))
        {
            var sceneNode = ResourceLoader.Load<PackedScene>(MainGamePath).Instantiate();
            var scene = (GameRoom)sceneNode;
            
            GetParent().AddChild(scene);
            
            scene.SpawnOpponent();
            
            QueueFree();
        }
    }

    public static void SpawnNewScreen()
    {
        
    }
}
