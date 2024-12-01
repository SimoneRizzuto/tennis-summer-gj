using Godot;
using System;
using TennisSummerGJ2024.UtilityClasses.Helpers;
using TennisSummerGJ2024.UtilityClasses.Shared;

public partial class Ball : RigidBody2D
{
    public float Height => GlobalPosition.DistanceTo(shadow.GlobalPosition);
    
    private double heightDropSpeed = 2;
    private Shadow shadow;
    
    public override void _Ready()
    {
        shadow ??= GetNodeHelper.GetShadow(GetTree());
    }

    public override void _Process(double delta)
    {
        shadow = GetNodeHelper.GetShadow(GetTree());
    }
}
