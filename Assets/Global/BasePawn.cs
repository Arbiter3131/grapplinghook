using UnityEngine;
using Unity.Mathematics;

public class BasePawn : MonoBehaviour
{
    // This should be on both player and enemies, should be paired with an InputManager (either enemy or player).
    // InputManager should feed values to the methods on this script.
    // This serves as a definition for how the physics should work, including Gravity, air resistance, etc.

    // Max velocity is the maximum velocity achievable in any direction. This means you could go diagonally
    // with 5.00 velocity forwards and 5.00 velocity to the left. with a total of 10.00 velocity.
    protected float maxVelocity = 0.01f;
    protected float acceleration = 1.00f;

    // air resistance slowing your velocity in air.
    protected float airResistance = 0.25f;

    // friction slowing your velocity on the ground.
    protected float friction = 0.50f;
    protected bool grounded;
    // groundHeight, sets length of raycast to check for ground beneath.
    protected float groundHeight = 1.0f;

    // instantly added to your upwards velocity.
    protected float jumpAcceleration = 2.00f;
    // prevent 
    protected bool jumping = false;

    // gravity.
    protected float gravityAcceleration = 0.01f;

    // hp.
    protected int healthPoints = 10;

    protected GameObject parentPawn;
    protected Vector3 currentLocation;


    // storages for each directional velocity
    // sideVel + is left, - is right.
    [SerializeField] protected float sideVel = 0.0f;
    // forVel + is forwards, - is backwards.
    [SerializeField] protected float forVel = 0.0f;
    // vertVel + is up, - is down.
    [SerializeField] protected float vertVel = 0.0f;

    void Start()
    {
        parentPawn = transform.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        currentLocation = parentPawn.transform.position;
        parentPawn.transform.position = currentLocation + new Vector3(forVel, vertVel, sideVel);
        ForwardVelocity();
        SidewaysVelocity();
        VerticalVelocity();
    }

    private void ForwardVelocity()
    {
        
    }

    private void SidewaysVelocity()
    {
        
    }

    private void VerticalVelocity()
    {
        CheckGrounded();
        if (!grounded && math.abs(vertVel) < maxVelocity)
        {
            vertVel -= gravityAcceleration;
        }
        else if (grounded)
        {
            vertVel = 0.0f;
        }
    }

    private void CheckGrounded()
    {
        RaycastHit hit;
        if(Physics.Raycast(currentLocation, -Vector3.up, out hit, groundHeight))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
        // Debug.DrawRay(currentLocation, -Vector3.up * groundHeight, Color.yellow);
    }
}
