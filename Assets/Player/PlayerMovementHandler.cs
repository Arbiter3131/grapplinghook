using UnityEngine;

public class PlayerMovementHandler : MonoBehaviour
{
    // Total acceleration, I believe it would be best to limit the amount of functions calling ApplyVelocity at once.
    // sounds like a bad decision, so we will try to get a total acceleration value, then pass that to applyvelocity.
    // for example, the player is inputting forwards, so we add to the total accel vector3 that movement.
    // then we notice the player is in the air and affected by gravity, so we add the gravity accel downwards.
    // then we notice the player is affected by air resistance, so we want to slow down the sideways movement by the accel.
    // total all of them and apply that velocity.
    // we should then reset the acceleration so that we aren't infinitely adding onto it.
    Vector3 totalAcceleration = new Vector3(0,0,0);

    // How much we want a player to accelerate in a direction when input is detected.
    float acceleration = 0.1f;
    Vector2 playerInput = new Vector2(0,0);

    // Air Resistance. False by default. Should be disabled when wallrunning or slashing.
    bool airResist = false;
    float airResistVal = 0.1f;

    // Friction. True by default, we assume the player is on the ground in the beginning.
    bool friction = true;
    float frictionVal = 0.1f;

    // camera fields. Looksensitivity and a value to limiting the up/down angles you can look.
    float lookSens = 1.0f;
    float upDownLook = 0.0f;
    Camera camera;
    float lookLimit = 90f;

    // gravAccel is base acceleration of gravity for player, gravity is on by default
    // should be turned off for future wallrunning and slashing mechanics.
    bool gravity = true;
    float gravAccel = 0.5f;

    // list of stuff we need to keep track of.
    PlayerController controller;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camera = transform.GetComponentInChildren<Camera>();
        controller = transform.GetComponent<PlayerController>();
    }

    void FixedUpdate()
    {
        ApplyGravity();
        ApplyMovement();
        CallChangeVelocity();
        totalAcceleration = new Vector3(0,0,0);
    }

    // Passed the Vector2 from the input.
    // Rotates the player directly by the x input of the mouse movement * looksensitivity to control how fast the camera moves.
    // upDownLook tracks the vertical camera look, with a maximum and minimum of -90 to 90 so that the camera doesn't flip.
    // then the cameras local rotation is set to the value of upDownLook.
    public void CameraMovement(Vector2 v)
    {
        float leftRight = v.x * lookSens;
        transform.Rotate(Vector3.up * leftRight);

        upDownLook -= v.y * lookSens;
        upDownLook = Mathf.Clamp(upDownLook, -lookLimit, lookLimit);
        
        camera.transform.localRotation = Quaternion.Euler(upDownLook, 0f, 0f);
    }

    // Apply gravity to our velocity.
    void ApplyGravity()
    {
        if(gravity)
        {
            totalAcceleration.y -= gravAccel;
        }
    }

    // calls Changevelocity in the controller, which then applys the velocity to the BaseMovement script.
    void CallChangeVelocity()
    {
        controller.ChangeVelocity(totalAcceleration);
    }

    void ApplyMovement()
    {
        Vector3 movementVector = new Vector3(0,0,0);

        Vector3 sideMove = transform.right * playerInput.x * acceleration;
        Vector3 forwardMove = transform.forward * playerInput.y * acceleration;

        movementVector += sideMove + forwardMove;
        totalAcceleration += movementVector;
    }

    public void CurrentMoveInput(Vector2 v)
    {
        playerInput = v;
    }
}
