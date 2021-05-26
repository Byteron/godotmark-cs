using Godot;
using System.Collections.Generic;

public class Mover
{
    public Vector2 Position = new Vector2(5f, 5f);
    public Vector2 Velocity = Vector2.Zero;
    public Texture Texture = GD.Load<Texture>("res://icon.png");
}

public class GodotMark : Node2D
{
    List<Mover> _movers = new List<Mover>();
    [Export] int _instances = 1000;
    [Export] float _speed = 1000f;

    Label label;

    public override void _Ready()
    {
        label = GetNode<Label>("CanvasLayer/Label");
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("ui_accept"))
        {
            SpawnMovers();
        }
    }

    private void SpawnMovers()
    {
        for (int i = 0; i < _instances; i++)
        {
            var mover = new Mover();
            mover.Velocity = new Vector2((float)GD.RandRange(-1f, 1f), (float)GD.RandRange(-1f, 1f));
            _movers.Add(mover);
        }
    }

    public override void _Process(float delta)
    {
        foreach(var mover in _movers)
        {
            mover.Position += mover.Velocity * _speed * delta;

            if (mover.Position.x < 0 || mover.Position.x > 1024)
            {
                mover.Velocity.x = -mover.Velocity.x;
            }

            if (mover.Position.y < 0 || mover.Position.y > 600)
            {
                mover.Velocity.y = -mover.Velocity.y;
            }
        }

        label.Text = string.Format("FPS: {0}\nMovers: {1}", Engine.GetFramesPerSecond(), _movers.Count);

        Update();
    }

    public override void _Draw()
    {
        foreach(var mover in _movers)
        {
            DrawTexture(mover.Texture, mover.Position);
        }
    }
}
