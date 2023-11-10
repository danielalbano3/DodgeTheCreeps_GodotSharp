using Godot;
using System;
using System.Collections.Generic;

public partial class Main : Node
{
	[Export]
	public PackedScene MobScene { get; set; }

	private int _score;

	public void game_over()
	{
		GetNode<Timer>("MobTimer").Stop();
		GetNode<Timer>("ScoreTimer").Stop();

		GetNode<HUD>("HUD").ShowGameOver();
	}

	public void NewGame()
	{
		GetTree().CallGroup("mobs", Node.MethodName.QueueFree);

		_score = 0;
		Player player = GetNode<Player>("Player");
		Marker2D startPosition = GetNode<Marker2D>("StartPosition");
		player.Start(startPosition.Position);
		GetNode<Timer>("StartTimer").Start();

		HUD hud = GetNode<HUD>("HUD");
		hud.UpdateScore(_score);
		hud.ShowMessage("Get Ready!");
	}

	private void _on_score_timer_timeout()
	{
		_score++;
		GetNode<HUD>("HUD").UpdateScore(_score);
	}

	private void _on_start_timer_timeout()
	{
		GetNode<Timer>("MobTimer").Start();
		GetNode<Timer>("ScoreTimer").Start();
	}

	private void _on_mob_timer_timeout()
	{
		Mob mob = MobScene.Instantiate<Mob>();

		PathFollow2D mobSpawnLocation = GetNode<PathFollow2D>("MobPath/MobSpawnLocation");
		mobSpawnLocation.ProgressRatio = GD.Randf();

		float direction = mobSpawnLocation.Rotation + Mathf.Pi / 2;

		mob.Position = mobSpawnLocation.Position;
	
		direction += (float)GD.RandRange(-Mathf.Pi / 4, Mathf.Pi / 4);
		mob.Rotation = direction;

		Vector2 velocity = new Vector2((float)GD.RandRange(150.0, 250.0), 0);
		mob.LinearVelocity = velocity.Rotated(direction);

		AddChild(mob);	
	}

	public override void _Ready()
	{
		
	}

}
