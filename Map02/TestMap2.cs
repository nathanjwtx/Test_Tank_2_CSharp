using Godot;
using System;

public class TestMap2 : Node2D
{
    private Node2D _shooter;
    private Node2D _target;
    private PackedScene _bullet;
    private Position2D _muzzle;
    private Tween _tween;

    public override void _Ready()
    {
//        _shooter = (Node2D) GetNode("Shooter");
//        _bullet = new PackedScene();
//        _muzzle = (Position2D) _shooter.GetNode("Turret/Muzzle");
//        _tween = (Tween) GetNode("Tween");
//        _target = (Node2D) GetNode("TargetTank");
//        _bullet.GlobalPosition = _muzzle.GlobalPosition;
//        _tween.InterpolateProperty(_bullet, "position", _muzzle.GlobalPosition,
//            _target.GlobalPosition, 3, Tween.TransitionType.Expo, Tween.EaseType.InOut);
//        _tween.Start();
    }

    public override void _Process(float delta)
    {
    }
    
    private void _on_Shooter_Shoot(Curve2D curve)
    {
        GD.Print("shoot");
    }
    
    private void _on_Tree_tree_entered()
    {
        GD.Print("entered");
    }
    
}




