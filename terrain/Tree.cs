using Godot;
using System;

public class Tree : StaticBody2D
{
    // Member variables here, example:
    // private int a = 2;
    // private string b = "textvar";

    public override void _Ready()
    {
        // Called every time the node is added to the scene.
        // Initialization here
        
    }
    
    private void _on_Area2D_body_entered(Godot.Object body)
    {
        if (body is KinematicBody2D tank)
        {
            if (tank.Name == "Target")
            {
                GD.Print("you hit tree");
            }
        }
    }
//    public override void _Process(float delta)
//    {
//        // Called every frame. Delta is time since last frame.
//        // Update game logic here.
//        
//    }
}



