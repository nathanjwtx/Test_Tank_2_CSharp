using System;
using Godot;

public class TestTankPlayer2 : TestTankBase
{
    private Node _parent;
    private Curve2D _curve;
    private Path2D _path;
    private Node2D _shooter;
    private Node2D _target;
    
    public override void _Ready()
    {
        base._Ready();
        _parent = GetParent();
        _shooter = (Node2D) _parent.GetNode("Shooter");
        _target = (Node2D) _parent.GetNode("TargetTank");
        _path = (Path2D) _parent.GetNode("Path2D");
        _path.Curve = CreatePath(_shooter.GlobalPosition, _target.GlobalPosition);
    }

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);
        Control(delta);
//        MoveAndSlide(Velocity);
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
            GD.Print("click");
            ShootGun();
        }
    }
    
    private Curve2D CreatePath(Vector2 shooterPos, Vector2 enemyPos)
    {
        GD.Print(shooterPos);
        _curve = new Curve2D();
        Vector2 point = new Vector2();
        for (int i = 0; i < 11; i++)
        {
            var x = (Math.Pow((1 - (0.1 * i)), 2) * shooterPos[0]) 
                    + ((2 * (1 - (0.1 * i)) * i/10) * (Math.Abs(shooterPos[0] - enemyPos[0]) / 2))
                    + (Math.Pow((0.1 * i), 2) * enemyPos[0]);
            var y = (Math.Pow((1 - (0.1 * i)), 2) * shooterPos[1]) 
                    + ((2 * (1 - (0.1 * i)) * i/10) * 1)
                    + (Math.Pow((0.1 * i), 2) * enemyPos[1]);
            point.x = Convert.ToSingle(x);
            point.y = Convert.ToSingle(y);
            GD.Print(point);
            _curve.AddPoint(point, new Vector2(0, 0));
        }
        GD.Print(_curve.GetPointCount());
        return _curve;

    }

}

