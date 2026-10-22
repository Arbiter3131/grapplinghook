using UnityEngine;

public class PlayerController : BaseController
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    PlayerMovementHandler playerMovementHandler;

    GrappleHandler grappleHandler;

    // set player max velocity in directions, should not change the values in basemovement anymore.
    float playerMaxV = 20f;
    float playerMaxVY = 20f;

    protected override void Start()
    {
        base.Start();
        playerMovementHandler = transform.GetComponent<PlayerMovementHandler>();
        grappleHandler = transform.GetComponent<GrappleHandler>();
        baseMovement.SetMaxVelocity(playerMaxV, playerMaxVY);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    public void MoveCamera(Vector2 v)
    {
        playerMovementHandler.CameraMovement(v);
    }

    public void PlayerMovement(Vector2 v)
    {
        playerMovementHandler.CurrentMoveInput(v);
    }

    public void CallPlayerJump()
    {
        playerMovementHandler.PlayerJump();
    }

    public Vector3 GetCameraDir()
    {
        return playerMovementHandler.GetCameraDirection();
    }

    public void AttemptGrapple(bool v)
    {
        grappleHandler.Grapple(v);
    }

    public void ToggleAirResist()
    {
        playerMovementHandler.ToggleAirResistance();
    }

    public void ToggleGravity()
    {
        playerMovementHandler.ToggleGravity();
    }
}
