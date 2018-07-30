using System;
using Godot;

public class TestTankEnemy : TestTankBase
{

    [Export] public int DetectRadius;

    private Node _parent;
    private PathFollow2D _follow;
    private KinematicBody2D _target;
    private Sprite _enemyTurret;
    private bool _canShoot = false;
    private Curve2D _curve;
    private Path2D _path;

    public override void _Ready()
    {
        _parent = GetParent();
        GunTimer = (Timer) GetNode("Timer");
        GunTimer.Start();
        var circle = new CircleShape2D();
        circle.Radius = DetectRadius;
        var radius = (CollisionShape2D) GetNode("DetectRadius/CollisionShape2D");
        radius.Shape = circle;
        _enemyTurret = (Sprite) GetNode("Turret");
    }

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);
        Control(delta);
        MoveAndSlide(Velocity);
    }

    public override void _Process(float delta)
    {
        base._Process(delta);
        if (_target != null)
        {
            if (_canShoot)
            {
                GD.Print("fire");
                ShootGun(CreatePath(_target.GlobalPosition, GlobalPosition));
                GunTimer.Start();
                _canShoot = false;
            }
        }
    }

    public new void Control(float delta)
    {
        
    }

    private Curve2D CreatePath(Vector2 shooterPos, Vector2 enemyPos)
    {
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
//            GD.Print(point);
            _curve.AddPoint(point, new Vector2(0, 0));
        }
        GD.Print(_curve.GetPointCount());
        return _curve;
    }
    
    #region Signals
    private void _on_Timer_timeout()
    {
        GunTimer.Start();
        _canShoot = true;
    }
    
    private void _on_DetectRadius_body_entered(Godot.Object body)
    {
//        var b = (KinematicBody2D) body;
//        GD.Print(b.Name);
        if (body is KinematicBody2D player)
        {
            if (player.Name == "Target")
            {
                GD.Print("you hit tank");
//                GunTimer.Start();
//                _target = player;
            }
        }
    }
    
    private void _on_DetectRadius_body_exited(Godot.Object body)
    {
        if (body == _target)
        {
            _target = null;
        }
    }
    #endregion
    
}
