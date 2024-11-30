using Godot;
using System;

public partial class Ball : RigidBody2D
{
    // modify Mass, Friction or Bounce
    
    
    private double height = 50;
    private double heightDropSpeed = 2; 

    public override void _PhysicsProcess(double delta)
    {
        height -= height * delta * heightDropSpeed;
        
        
    }

    /*public override void _IntegrateForces(PhysicsDirectBodyState2D state)
    {
        //LinearVelocity = new(0, -300);
    }*/
}
