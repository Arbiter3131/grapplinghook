using UnityEngine;

public class WallrunHandler : MonoBehaviour
{
    // this will handle player wallrunning.
    private PlayerController controller;

    // layermask for solid walls, we should not be able to wallrun on enemies.
    LayerMask solidMask;

    // distance from the wall that we should stay at/check for wallrunning
    float wallDistance = 1.0f;

    // are we currently wallrunning?
    bool isWallRunning = false;
    
    // wallrun velocity.
    float wallRunVelocity;

    // direction we should wallrun.
    Vector3 wallRunDirection = new Vector3(0f,0f,0f);

    // direction to the wall that we are wallrunning on.
    Vector3 wallDirection;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = transform.GetComponent<PlayerController>();
        solidMask = LayerMask.GetMask("Grappleable","NonGrappleable");
        wallRunVelocity = controller.GetMaxVelocity();
    }

    void FixedUpdate()
    {
        if(isWallRunning)
        {
            CheckWallRun();
        }
    }

    // if we're wallrunning and we call this check, the player wants to jump off the wall.
    // cancel the wallrun then jump off, we don't need to call anything else so we return.
    // if we are checking for a wallrun, we check in the left and right directions.
    // if we find a wall to run on, then we get the normal and we take the cross product
    // of the vertical direction and the normal of the hit.
    // if the walls on the right, the transform.up is the first value
    // the walls on the left, the normal is the first value.
    // this gives us the direction to wallrun in.
    public void StartWallRun()
    {
        // if we are already wallrunning and the player presses space, get off the wall,
        // then add some vertical velocity.
        if(isWallRunning)
        {
            JumpOff();
            controller.ChangeVelocity(transform.up/2 * wallRunVelocity);
            isWallRunning = false;
            return;
        }
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.right, out hit, wallDistance, solidMask))
        {
            wallRunDirection = Vector3.Cross(transform.up, hit.normal);
            wallDirection = hit.point - transform.position;
            isWallRunning = true;
            WallRun();
        }
        else if(Physics.Raycast(transform.position, transform.right*-1, out hit, wallDistance, solidMask))
        {
            wallRunDirection = Vector3.Cross(hit.normal, transform.up);
            wallDirection = hit.point - transform.position;
            isWallRunning = true;
            WallRun();
        }
    }

    // constantly check if the wall that we are running on is still there.
    // this is robust enough that I don't have to code in a "cancel wallrun" for when
    // the player slashes.
    // this also works for the grapple, the player is pulled off the wall when a grapple
    // would effect the players movement.
    void CheckWallRun()
    {
        if(!Physics.Raycast(transform.position, wallDirection, wallDistance, solidMask))
        {
            JumpOff();
        }
    }

    // when ending wallrunning, toggle gravity and moveinput back on, then disable wallrunning.
    void JumpOff()
    {
        controller.ToggleGravity();
        controller.TogglePlayerMoveInput();
        isWallRunning = false;
    }

    // wallrunning physics.
    void WallRun()
    {
        controller.ToggleGravity();
        controller.TogglePlayerMoveInput();
        // UnityEngine.Debug.Log(wallRunDirection);
        controller.HardSetVelocity(wallRunDirection * wallRunVelocity);
    }
}
