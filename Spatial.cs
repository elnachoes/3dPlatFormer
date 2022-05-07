using Godot;
using System;

public class Spatial : KinematicBody
{
	private const float GRAVITY = -0.98f;
	private const int SPEED = 10;
	private bool _isSelectedUnit;
	private Vector3 _motion = new Vector3();
	private Camera _camera = null;
	private Vector3 _destination = new Vector3();
	private float _movementTimeDelta = 0.0f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		AddToGroup("Player");
		_camera = GetNode<Camera>("Position3D/Camera");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(float delta)
	{
		_movementTimeDelta += delta * 0.4f;

		_motion = _motion.LinearInterpolate(_destination,_movementTimeDelta);
		ApplyGravity(delta);
		// UpdateMovement(delta);
		_motion = MoveAndSlide(_motion, Vector3.Up);
	}
	
	public override void _Input(InputEvent @event)
	{
		const float rayLength = 1000;

		if (@event is InputEventMouseButton eventMouseButton)
		{

		}
	}

	private void ClickMove(Vector3 newLocation)
	{
		_movementTimeDelta = 0.0f;
		_destination = newLocation;
	}

	private void ApplyGravity(float delta)
	{
		_motion.y += GRAVITY * SPEED * delta;
	}



	private void UpdateMovement(float delta)
	{
		var direction = Vector3.Zero;
		
		if (Input.IsActionPressed("foward") && !Input.IsActionPressed("backward")) direction.x -= 1f;
		if (Input.IsActionPressed("backward") && !Input.IsActionPressed("foward")) direction.x += 1f;
		if (Input.IsActionPressed("left") && !Input.IsActionPressed("right")) direction.z += 1f;
		if (Input.IsActionPressed("right") && !Input.IsActionPressed("left")) direction.z -= 1f;
		if (Input.IsActionPressed("jump") && IsOnFloor()) _motion.y += SPEED;
		if (direction != Vector3.Zero) direction = direction.Normalized();
		
		_motion.x  = direction.x * SPEED;
		_motion.z  = direction.z * SPEED;
		_motion.y += GRAVITY * SPEED * delta;
	}
	private void _on_Player_input_event(object camera, object @event, Vector3 position, Vector3 normal, int shape_idx)
	{
		// GD.Print(normal);
		// if (@event is InputEventMouseButton) _isSelectedUnit = true;
	}
}