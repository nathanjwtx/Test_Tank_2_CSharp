using System;
using Godot;

public class TestTankEnemy : TestTankBase
{

    public Node Parent;
    public PathFollow2D Path;
    public KinematicBody2D Target;
    public Sprite EnemyTurret;
    [Export] public int DetectRadius;
    public bool CanShoot = true;

    public override void _Ready()
    {
        Parent = GetParent();
        GunTimer = (Timer) GetNode("Timer");
        GunTimer.Start();
        var circle = new CircleShape2D();
        circle.Radius = DetectRadius;
        var radius = (CollisionShape2D) GetNode("DetectRadius/CollisionShape2D");
        radius.Shape = circle;
        EnemyTurret = (Sprite) GetNode("Turret");
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
        if (Target != null)
        {
            Vector2 targetDir = (Target.GlobalPosition - GlobalPosition).Normalized();
            Vector2 currentDir = new Vector2(1, 0).Rotated(EnemyTurret.GlobalRotation);
            EnemyTurret.GlobalRotation = currentDir.LinearInterpolate(targetDir, 75 * delta).Angle();
            if (targetDir.Dot(currentDir) > 0.9)
            {
                if (CanShoot)
                {
                    GD.Print("fire");
                    Position2D muzzle = (Position2D) GetNode("Turret/Muzzle");
//                    ShootGun(muzzle);
                    GunTimer.Start();
                    CanShoot = false;
                }
            }
        }
    }

    public new void Control(float delta)
    {
        if (Parent.Name == "PathFollow2D")
        {
            Path = (PathFollow2D) GetParent();
            Path.SetOffset(Path.GetOffset() + Speed * delta);
            Position = new Vector2();
        }
    }

    private void _on_Timer_timeout()
    {
        GunTimer.Start();
        CanShoot = true;
    }
    
    private void _on_DetectRadius_body_entered(Godot.Object body)
    {
        if (body is KinematicBody2D player)
        {
            if (player.Name == "TestTankPlayer")
            {
                GunTimer.Start();
                Target = player;
            }
        }
    }
    
    private void _on_DetectRadius_body_exited(Godot.Object body)
    {
        if (body == Target)
        {
            Target = null;
        }
    }
}
