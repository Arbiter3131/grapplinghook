using UnityEngine;

public class PlayerController : BaseController
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    PlayerMovementHandler playerMovementHandler;

    GrappleHandler grappleHandler;

    SlashHandler slashHandler;

    WallrunHandler wallrunHandler;

    PlayerInput input;

    protected BaseMovement baseMovement;

    // set player max velocity in directions, should not change the values in basemovement anymore.
    float playerMaxV = 20f;
    float playerMaxVY = 20f;

    protected override void Start()
    {
        base.Start();
        playerMovementHandler = transform.GetComponent<PlayerMovementHandler>();
        grappleHandler = transform.GetComponent<GrappleHandler>();
        slashHandler = transform.GetComponent<SlashHandler>();
        wallrunHandler = transform.GetComponent<WallrunHandler>();
        input = transform.GetComponent<PlayerInput>();
        baseMovement = transform.GetComponent<BaseMovement>();
        baseMovement.SetMaxVelocity(playerMaxV, playerMaxVY);
    }

    public void MoveCamera(Vector2 v)
    {
        playerMovementHandler.CameraMovement(v);
    }

    public virtual Vector3 GetCurrentVelocity()
    {
        return baseMovement.GetCurrentVelocity();
    }

    public virtual void ChangeVelocity(Vector3 input)
    {
        baseMovement.ApplyVelocity(input);
    }

    public void UnfreezeRotation()
    {
        baseMovement.Unfreeze();
    }

    public virtual void HardSetVelocity(Vector3 input)
    {
        baseMovement.SetVelocity(input);
    }

    public virtual bool GetGroundedVal()
    {
        return baseMovement.GetGrounded();
    }

    public void PlayerMovement(Vector2 v)
    {
        playerMovementHandler.CurrentMoveInput(v);
    }

    // check if we're pressing space when grounded to jump.
    // if we are in the air, check to see if a wallrun is possible.
    public void CallPlayerJump()
    {
        if(GetGroundedVal())
        {
            playerMovementHandler.PlayerJump();
        }
        else
        {
            wallrunHandler.StartWallRun();
        }
    }

    public void TogglePlayerMoveInput()
    {
        playerMovementHandler.ToggleInput();
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

    public void AttemptSlash(bool v)
    {
        slashHandler.SlashInput(v);
    }

    public void StopGrapple()
    {
        grappleHandler.CancelGrapple();
    }

    public float GetMaxVelocity()
    {
        return playerMaxV;
    }

    public void ToggleMouseInput()
    {
        input.ToggleMouse();
    }

    public override void DeathBehaviour()
    {
        TogglePlayerMoveInput();
        StopGrapple();
        ToggleMouseInput();
        UnfreezeRotation();
        baseMovement.SetHeight(0.2f);
    }
}
