using UnityEngine;

public class BaseMovement : MonoBehaviour
{
    // BaseMovement should keep track of velocities and acceleration, providing the functions to update those and should handle updating the position of the object its on.

    // height is the length of the raycast we should check to see if this character is standing on something. Default is 1.0f.
    CharacterController character;
    bool grounded;
    float height = 1.1f;
    Vector3 position;
    // maxVelocity should have a function to overwrite it.
    float maxVelocity = 0.5f;

    // current velocity that we should move this character by each fixedUpdate.
    Vector3 currVelocity = new Vector3(0f,0f,0f);

    // Layermask to detect objects we can stand on.
    LayerMask solidMask;
    
    void Awake()
    {
        solidMask = LayerMask.GetMask("Grappleable","NonGrappleable");
    }

    void Start()
    {
        // rBody = transform.GetComponent<Rigidbody>();
        character = transform.GetComponent<CharacterController>();
        grounded = false;
        position = transform.position;
    }

    void FixedUpdate()
    {
        position = transform.position;
        CheckGrounded();
        character.Move(currVelocity);
    }

    // check to make sure none of the values are going over our maximum velocity in each direction.
    // I clamped each value... this resulted in very weird movement glitches that ate up a lot of my time...
    // we are using charactercontroller now I guess.
    // Separating y axis velocity and forward/sideways velocity.
    Vector3 CheckVelocity(Vector3 check)
    {
        Vector3 resultVectorXZ = new Vector3(currVelocity.x + check.x, 0f, currVelocity.z + check.z);
        Vector3 resultVectorY = new Vector3(0f, currVelocity.y + check.y, 0f);

        if(resultVectorXZ.magnitude > maxVelocity)
        {
            resultVectorXZ = resultVectorXZ.normalized * maxVelocity;
        }
        if(resultVectorY.magnitude > maxVelocity)
        {
            resultVectorY = resultVectorY.normalized * maxVelocity;
        }
        return resultVectorXZ + resultVectorY;
    }

    // add to the rigidbodies current velocity, do the check to make sure it won't go over our max velocity.
    // we aren't using rigidbodies anymore, now using charactercontrollers.
    public void ApplyVelocity(Vector3 veloc)
    {
        // rBody.linearVelocity = CheckVelocity(veloc);
        currVelocity = CheckVelocity(veloc);
    }


    // use a raycast to check if we are currently on the ground. If we landed from being in the air, then set our y velocity to 0.
    void CheckGrounded()
    {
        RaycastHit hit;
        if(Physics.Raycast(position, -Vector3.up, out hit, height, solidMask))
        {
            grounded = true;
            if(currVelocity.y < 0f)
            {
                currVelocity.y = 0f;
            }
        }
        else
        {
            grounded = false;
        }
        // Debug.DrawRay(position, -Vector3.up * height, Color.yellow);
    }

    public Vector3 GetCurrentVelocity()
    {
        Vector3 returnVel = currVelocity;
        return returnVel;
    }

    // get function to just return grounded value.
    public bool GetGrounded()
    {
        return grounded;
    }

    public void SetMaxVelocity(float max)
    {
        maxVelocity = max;
    }

    public void SetHeight(float max)
    {
        height = max;
    }
}
