using Godot;
using System;

public class Controller : KinematicBody
{
    //constants
    //this gives a buffer zone for how close to the edge of the screen the mouse needs to be before the controller moves
    private const float SCREEN_EDGE_ZONE = 15;
    private const float SPEED = 30;

    //scene variables
    private Camera _camera = null;

    private Vector3 _motion = new Vector3();

    public override void _Ready()
    {
        _camera = GetNode<Camera>("Camera");
    }

    public override void _Process(float delta)
    {
        //update the movement of the camera if the mouse is on the edges of the screen
        UpdateMovement(delta);
        _motion = MoveAndSlide(_motion, Vector3.Up);
    }

    //this allows the controller to move if the mouse is being pressed up against the edges of the screen
    //works just like in starcraft or dota
    private void UpdateMovement(float delta)
    {
        var mousePosition = GetViewport().GetMousePosition();
        var viewportSize = GetViewport().Size;
        var direction = Vector3.Zero;

        //right side
        if (mousePosition.x >= viewportSize.x - SCREEN_EDGE_ZONE)
        {
            direction.x += 1f;
            direction.z -= 1f;
        }
        //left side
        if (mousePosition.x <= 0 + SCREEN_EDGE_ZONE)
        {
            direction.x -= 1f;
            direction.z += 1f;
        }
        //bottom side
        if (mousePosition.y >= viewportSize.y - SCREEN_EDGE_ZONE)
        {
            direction.x += 1f;
            direction.z += 1f;
        }
        //top side
        if (mousePosition.y <= 0 + SCREEN_EDGE_ZONE)
        {
            direction.x -= 1f;
            direction.z -= 1f;
        }
        //top left corner
        if (mousePosition.x <= 0 + SCREEN_EDGE_ZONE && mousePosition.y <= 0 + SCREEN_EDGE_ZONE) direction.x -= 1f;
        //bottom left corner
        if (mousePosition.x <= 0 + SCREEN_EDGE_ZONE && mousePosition.y >= viewportSize.y - SCREEN_EDGE_ZONE) direction.x += 1f;
        //top right corner
        if (mousePosition.x >= viewportSize.x - SCREEN_EDGE_ZONE && mousePosition.y <= 0 + SCREEN_EDGE_ZONE) direction.z -= 1f;
        //bottom right corner
        if (mousePosition.x >= viewportSize.x - SCREEN_EDGE_ZONE && mousePosition.y >= viewportSize.y - SCREEN_EDGE_ZONE) direction.z += 1f;

        if (direction != Vector3.Zero) direction = direction.Normalized();

        _motion.x = direction.x * SPEED;
		_motion.z = direction.z * SPEED;
    }
}