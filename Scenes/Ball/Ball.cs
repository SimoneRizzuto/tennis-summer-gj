using Godot;
using System;
using TennisSummerGJ2024.UtilityClasses.Helpers;

public partial class Ball : StaticBody2D
{
    // modify Mass, Friction or Bounce
    
    private Shadow shadow;
    
    
    public double CurrentHeight = 50;
    private double heightDropSpeed = 2;


    public override void _Ready()
    {
        shadow ??= GetNodeHelper.GetShadow(GetTree());
    }

    public override void _PhysicsProcess(double delta)
    {
        
        Position = shadow.Position - new Vector2(0, (float)CurrentHeight);
        
        //Position



        GD.Print("Ball - GLOBAL POSITION: " + GlobalPosition);
        GD.Print("Ball - POSITION: " + Position);

        /*var currentHeight = Position.Y;

        var newVec



        var heightVector = new Vector2(0, -50);

        Position -= heightVector * delta * heightDropSpeed;*/

        //height -= height * delta * heightDropSpeed;


    }

    /*public override void _IntegrateForces(PhysicsDirectBodyState2D state)
    {
        //LinearVelocity = new(0, -300);
    }*/
    
    
    /*;*/
}
