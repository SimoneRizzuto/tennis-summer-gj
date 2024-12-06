using Godot;
using System;
using static Godot.DisplayServer;

public partial class MainMenu : Control
{
    private const string MainGamePath = "res://Scenes/GameRoom.tscn";
    
    private Button beginButton;
    private Button fullscreenButton;
    private Button creditsButton;
    
    public override void _Ready()
    {
        WindowSetTitle("Quinten's Ten Games Of Tennis");
        
        beginButton = GetNode<Button>("BeginButton");
        fullscreenButton = GetNode<Button>("FullscreenButton");
        creditsButton = GetNode<Button>("CreditsButton");

        beginButton.Pressed += BeginIsPressed;
        fullscreenButton.Pressed += FullscreenIsPressed;
        creditsButton.Pressed += CreditsIsPressed;
    }
    
    private void BeginIsPressed()
    {
        VsScreen.SpawnScreen(GetParent(), "The Tent...?", "res://Scenes/Enemies/Enemy.tscn"); // change to Tent opponent
        QueueFree();
    }
    
    private void FullscreenIsPressed()
    {
        var currentMode = WindowGetMode();

        if (currentMode == WindowMode.Fullscreen)
        {
            WindowSetMode(WindowMode.Windowed);
        }
        else
        {
            WindowSetMode(WindowMode.Fullscreen);
        }
    }
    
    private void CreditsIsPressed()
    {
        
    }
}
