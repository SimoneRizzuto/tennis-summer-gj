using Godot;
using System;
using TennisSummerGJ2024.UtilityClasses.Helpers;

public partial class Ball : RigidBody2D
{
    public double CurrentHeight = 50;
    public float DistanceToGround => GlobalPosition.DistanceTo(shadow.GlobalPosition);

    private double heightDropSpeed = 2;
    private Shadow shadow;
    
    
    
    public override void _Ready()
    {
        shadow ??= GetNodeHelper.GetShadow(GetTree());
    }

    public override void _PhysicsProcess(double delta)
    {
        // DistanceToGround
        /* Use this value to change the collision layer of this, to collide with the Net.
         * May need to adapt the Net to be on that layuer.
         */
        
        //GlobalPosition = shadow.GlobalPosition - new Vector2(0, (float)CurrentHeight);



        
        
        GD.Print("Ball - GLOBAL POSITION: " + GlobalPosition);
        GD.Print("Ball - POSITION: " + Position);
        GD.Print("Distance To: " + DistanceToGround);
    }
}
