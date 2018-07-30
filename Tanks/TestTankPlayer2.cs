using System;
using System.Security.Cryptography.X509Certificates;
using Godot;

public class TestTankPlayer2 : TestTankBase
{
    private Node _parent;
    private Node2D _shooter;
    private Node2D _target;
    private Node2D _muzzle;
    
    public bool Fired = false;
    
    public override void _Ready()
    {
        base._Ready();
        _parent = GetParent();
        _muzzle = GetNode<Position2D>("Turret/Muzzle");
    }

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);
        Control(delta);
        MoveAndSlide(Velocity);
    }

    public new void Control(float delta)
    {
        const float speed = 120;
        const int rotationSpeed = 10;
        var rotDir = 0;
//        Turret = (Sprite) GetNode("Turret");
//        Turret.LookAt(GetGlobalMousePosition());
        if (Input.IsActionPressed("right"))
        {
            rotDir += 1;
        }

        if (Input.IsActionPressed("left"))
        {
            rotDir -= 1;
        }

        Rotation += rotationSpeed * rotDir * delta;

        // this line resets the vector and is v important. Needed because Velocity is declared at instance level
        Velocity = Vector2.Zero;
        if (Input.IsActionPressed("forward"))
        {
            Velocity = new Vector2(speed, 0).Rotated(Rotation);
        }

        if (Input.IsActionPressed("reverse"))
        {
            // ReSharper disable once PossibleLossOfFraction
            Velocity = new Vector2(-speed/2, 0).Rotated(Rotation);
        }

        if (Input.IsActionJustPressed("shoot"))
        {
//            _path.Curve = CreatePath(_muzzle.GlobalPosition, _target.GlobalPosition);
            GD.Print("click");
            Fired = true;
//            ShootGun(CreatePath(_muzzle.GlobalPosition, _target.GlobalPosition));
        }
    }
}

