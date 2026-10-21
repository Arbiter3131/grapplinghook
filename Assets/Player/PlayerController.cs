using UnityEngine;

public class PlayerController : BaseController
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    PlayerMovementHandler playerMovementHandler;

    // set player max velocity in directions, should not change the values in basemovement anymore.
    float playerMaxV = 0.5f;
    float playerMaxVY = 10f;

    protected override void Start()
    {
        base.Start();
        playerMovementHandler = transform.GetComponent<PlayerMovementHandler>();
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
}
