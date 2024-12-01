using Godot;
using System;

public partial class Net : Node2D
{
    public ColorRect NetShape;
    public override void _Ready()
    {
        NetShape = GetNode<ColorRect>("ColorRect");
    }
}
