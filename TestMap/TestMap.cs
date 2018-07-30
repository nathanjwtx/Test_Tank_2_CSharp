using Godot;
using System;

public class TestMap : Node2D
{
    [Export] public PackedScene Bullet;
    
    private Path2D _path;
    private PathFollow2D _follow;
    private Curve2D _curve;
    private Sprite _bill;
    private Sprite _ben;
    private Bullet _bullet;
    private float _offset;
    // Member variables here, example:
    // private int a = 2;
    // private string b = "textvar";

    public override void _Ready()
    {
        _bullet = (Bullet) Bullet.Instance();
        _bill = GetNode<Sprite>("Bill");
        _ben  = GetNode<Sprite>("Ben");
        _path = GetNode<Path2D>("Path2D");
        _follow = new PathFollow2D();
        _follow.AddChild(_bullet);
        _path.Curve = CreatePath(_ben.GlobalPosition, _bill.GlobalPosition);
        _path.AddChild(_follow);
        _path.Visible = true;
        
    }

    public void Control(float delta)
    {
        _follow.SetOffset(_follow.GetOffset() + 50 * delta);
    }
    
    public override void _PhysicsProcess(float delta)
    {
        Control(delta);
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
//        GD.Print(_curve.GetPointCount());
        return _curve;
    }
}
