using Godot;
using System;

public partial class Net : Node2D
{
    public Sprite2D NetSprite;
    public override void _Ready()
    {
        NetSprite = GetNode<Sprite2D>("Net");
    }

    public override void _Process(double delta)
    {
        NetSprite = GetNode<Sprite2D>("Net");
    }
}
