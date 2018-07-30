using Godot;
using System;

public class TestMap2 : Node2D
{
    private Node2D _shooter;
    private Node2D _target;
    private TestBullet2 _bullet;
    private Position2D _muzzle;
    private Tween _tween;
    private Path2D _path;

    public override void _Ready()
    {
        _path = GetNode<Path2D>("Path2D");
    }

    public override void _Process(float delta)
    {
        
    }
    
    private void _on_Shooter_Shoot(Curve2D curve, PackedScene bullet)
    {
        var b = (TestBullet2) bullet.Instance();
        _path.Curve = curve;
        PathFollow2D follow = new PathFollow2D();
        follow.SetOffset(10);
        follow.AddChild(b);
        b.Position = new Vector2();
        _path.AddChild(follow);
        _path.Visible = true;
        
        follow.Show();
        GD.Print("shoot");
    }

    
}

