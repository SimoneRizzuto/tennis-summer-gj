using Godot;
using System;
using TennisSummerGJ2024.UtilityClasses.Shared;

public partial class VsScreen : Control
{
    private const string VsScreenPath = "res://Scenes/VsScreen/VsScreen.tscn";
    private const string MainGamePath = "res://Scenes/GameRoom.tscn";

    [Export] public string OpponentName = "placeholder name";
    [Export] public string OpponentScenePath = "";
    [Export] public string OpponentPortraitPath = "";
    
    public Label PlayerLabel => GetNode<Label>("PlayerName");
    public Label OpponentLabel => GetNode<Label>("OpponentName");
    public Sprite2D OpponentPortrait => GetNode<Sprite2D>("OpponentPortrait");
    public AudioStreamPlayer2D Audio => GetNode<AudioStreamPlayer2D>("Audio");

    private bool playAudioOnce = false;
    
    public override void _Process(double delta)
    {
        if (!Audio.Playing && !playAudioOnce)
        {
            Audio.Play();
            playAudioOnce = true;
            GD.Print(Audio.Playing);
        }
        
        if (Input.IsActionJustPressed(InputMapAction.Interact))
        {
            var sceneNode = ResourceLoader.Load<PackedScene>(MainGamePath).Instantiate();
            var scene = (GameRoom)sceneNode;
            
            GetParent().AddChild(scene);
            
            scene.SpawnOpponent(OpponentScenePath);
            
            QueueFree();
        }
    }

    public static void SpawnScreen(Node nodeToSpawnOn, string opponentLabel, string opponentScenePath, string opponentTexturePath, string audioPath)
    {
        var scene = ResourceLoader.Load<PackedScene>(VsScreenPath).Instantiate();
        var vsScreen = (VsScreen)scene;
        
        vsScreen.OpponentLabel.Text = opponentLabel;
        vsScreen.OpponentScenePath = opponentScenePath;
        
        var texture = GD.Load<Texture2D>(opponentTexturePath);
        vsScreen.OpponentPortrait.Texture = texture;

        if (!string.IsNullOrEmpty(audioPath))
        {
            var audio = GD.Load<AudioStream>(audioPath);
            vsScreen.Audio.Stream = audio;
            vsScreen.Audio.VolumeDb = 1f;
        }
        
        nodeToSpawnOn.AddChild(scene);
    }
}
