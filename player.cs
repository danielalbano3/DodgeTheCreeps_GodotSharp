using Godot;
using System;

public partial class player : Area2D
{
  [Export]
  public int Speed { get; set; } = 400;

  public Vector2 Screensize;


  public override void _Ready()
  {
    Screensize = GetViewportRect().Size;
    Hide();
  }

  public override void _Process(double delta)
  {
    Vector2 velocity = Vector2.Zero;

    if (Input.IsActionPressed("right"))
    {
      velocity.X += 1;
    }
    if (Input.IsActionPressed("left"))
    {
      velocity.X -= 1;
    }
    if (Input.IsActionPressed("up"))
    {
      velocity.Y -= 1;
    }
    if (Input.IsActionPressed("down"))
    {
      velocity.Y += 1;
    }

    AnimatedSprite2D animatedSprite2D = GetNode<AnimatedSprite2D>("AS2D");

    if (velocity.Length() > 0)
    {
      velocity = velocity.Normalized() * Speed;
      animatedSprite2D.Play();
    }
    else
    {
      animatedSprite2D.Stop();
    }

    Position += velocity * (float)delta;
    Position = new Vector2(
      x: Mathf.Clamp(Position.X, 0, Screensize.X),
      y: Mathf.Clamp(Position.Y, 0, Screensize.Y)
    );

    if (velocity.X != 0)
    {
      animatedSprite2D.Animation = "walk";
      animatedSprite2D.FlipV = false;
      animatedSprite2D.FlipH = velocity.X < 0;
    }
    else if (velocity.Y != 0)
    {
      animatedSprite2D.Animation = "up";
      animatedSprite2D.FlipV = velocity.Y > 0;
    }

  }

}

