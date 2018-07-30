using Godot;
using System;

public class TestMap2 : Node2D
{
    private Node2D _shooter;
    private Node2D _target;
    private Node2D _bullet;
    private Position2D _muzzle;
    private Tween _tween;
    private Path2D _path;
    private PathFollow2D _follow;

    public override void _Ready()
    {
        _shooter = (Node2D) GetNode("Shooter");
//        _bullet = (Node2D) GetNode("Path2D/PathFollow2D/Bullet");
        _muzzle = (Position2D) _shooter.GetNode("Turret/Muzzle");
        _tween = (Tween) GetNode("Tween");
        _target = (Node2D) GetNode("TargetTank");
//        _bullet.GlobalPosition = _muzzle.GlobalPosition;
//        _tween.InterpolateProperty(_bullet, "position", _muzzle.GlobalPosition,
//            _target.GlobalPosition, 3, Tween.TransitionType.Expo, Tween.EaseType.InOut);
//        _tween.Start();
    }

    public override void _Process(float delta)
    {
//        _tween.Start();
//        _follow.SetOffset(10 + 50 * delta);
//        Position = new Vector2();
    }
    
    private void _on_Shooter_Shoot()
    {
        GD.Print("shoot");
    }
}
