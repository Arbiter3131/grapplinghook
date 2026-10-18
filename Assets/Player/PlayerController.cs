using UnityEngine;

public class PlayerController : BaseController
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    PlayerMovementHandler playerMovementHandler;

    protected override void Start()
    {
        base.Start();

        playerMovementHandler = transform.GetComponent<PlayerMovementHandler>();
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
}
