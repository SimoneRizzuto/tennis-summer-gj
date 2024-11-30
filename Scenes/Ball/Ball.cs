using Godot;
using System;

public partial class Ball : RigidBody2D
{
    // modify Mass, Friction or Bounce
    
    
    public double CurrentHeight = 50;
    private double heightDropSpeed = 2; 

    public override void _PhysicsProcess(double delta)
    {
        
        Position



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
