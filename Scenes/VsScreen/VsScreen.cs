using Godot;
using System;

public partial class VsScreen : Control
{
    public Label PlayerName => GetNode<Label>("PlayerName");
    public Label OpponentName => GetNode<Label>("OpponentName");
    
    
}
