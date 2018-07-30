using Godot;

public class TestTankBase : KinematicBody2D
{
    [Signal]
    delegate void Shoot();

    [Export] private PackedScene Bullet;
    [Export] public int Speed;

    public Timer GunTimer;
    public Vector2 Velocity;
    
    
    public override void _Ready()
    {
    }

    public override void _Process(float delta)
    {
           
    }

    public void Control(float delta)
    {
    }

    public override void _PhysicsProcess(float delta)
    {
        Control(delta);
        MoveAndSlide(Velocity);
    }

//    public void ShootGun(Position2D muzzle)
    public void ShootGun()
    {    
//        GD.Print("Fired");
//        var dir = new Vector2(1, 0).Rotated(muzzle.GlobalRotation);
//        EmitSignal("Shoot", Bullet, muzzle.GlobalPosition, dir);
        EmitSignal("Shoot");
    }
}