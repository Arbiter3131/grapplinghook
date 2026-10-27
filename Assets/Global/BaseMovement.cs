using UnityEngine;

public class BaseMovement : MonoBehaviour
{
    // BaseMovement should keep track of velocities and acceleration, providing the functions to update those and should handle updating the position of the object its on.

    // height is the length of the raycast we should check to see if this character is standing on something. Default is 1.0f.
    Rigidbody character;
    bool grounded;
    float height = 1.1f;
    Vector3 position;
    // maxVelocity should have a function to overwrite it.
    // maxVelocityY adds to the maximum velocity of the y value, so that the player and maybe certain enemies
    // can move up and down faster.
    float maxVelocity = 1f;
    float maxVelocityY = 5f;

    // stopVelocity, when should we decide to set the players velocity to 0 (prevent weird sliding)

    // current velocity that we should move this character by each fixedUpdate.
    // There was a bunch of issues between character controllers and rigidbodies, I finally fixed
    // the weird sliding issue with the Rigidbody so i swapped back to using that.
    // the advantage of a rigidbody over the charactercontroller, is that the rigidbody
    // has a built in velocity.
    // what this means, is that I can directly set the velocity of the rigidbody in a direction.
    // and I don't have to do my own calculations/checks to ensure that velocity isn't preserved if the player
    // runs into a wall. (Player would retain velocity in the walls direction, if moving away you would need to "slow down" in the walls direction)
    // essentially, at the moment I'm using UnityEngine physics for just collision detection and velocity at this point.
    // Vector3 currVelocity = new Vector3(0f,0f,0f);

    // Layermask to detect objects we can stand on.
    LayerMask solidMask;
    
    void Awake()
    {
        solidMask = LayerMask.GetMask("Grappleable","NonGrappleable");
    }

    void Start()
    {
        // rBody = transform.GetComponent<Rigidbody>();
        character = transform.GetComponent<Rigidbody>();
        grounded = false;
        position = transform.position;
    }

    void FixedUpdate()
    {
        position = transform.position;
        CheckGrounded();
        if(character.linearVelocity.magnitude < 0.05f)
        {
            character.linearVelocity = Vector3.zero;
        }
    }

    // check to make sure none of the values are going over our maximum velocity in each direction.
    // I clamped each value... this resulted in very weird movement glitches that ate up a lot of my time...
    // we are using charactercontroller now I guess.
    // Separating y axis velocity and forward/sideways velocity.
    Vector3 CheckVelocity(Vector3 check)
    {
        Vector3 resultVectorXZ = new Vector3(character.linearVelocity.x + check.x, 0f, character.linearVelocity.z + check.z);
        Vector3 resultVectorY = new Vector3(0f, character.linearVelocity.y + check.y, 0f);

        if(resultVectorXZ.magnitude > maxVelocity)
        {
            resultVectorXZ = resultVectorXZ.normalized * maxVelocity;
        }
        if(resultVectorY.magnitude > maxVelocityY)
        {
            resultVectorY = resultVectorY.normalized * maxVelocityY;
        }
        return resultVectorXZ + resultVectorY;
    }

    // add to the rigidbodies current velocity, do the check to make sure it won't go over our max velocity.
    public void ApplyVelocity(Vector3 veloc)
    {
        character.linearVelocity = CheckVelocity(veloc);
    }

    // set the velocity directly, should only be used by the Slash.
    public void SetVelocity(Vector3 veloc)
    {
        character.linearVelocity = veloc;
    }


    // use a raycast to check if we are currently on the ground. If we landed from being in the air, then set our y velocity to 0.
    void CheckGrounded()
    {
        RaycastHit hit;
        if(Physics.Raycast(position, -Vector3.up, out hit, height, solidMask))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
        // Debug.DrawRay(position, -Vector3.up * height, Color.yellow);
    }

    public Vector3 GetCurrentVelocity()
    {
        Vector3 returnVel = character.linearVelocity;
        return returnVel;
    }

    // get function to just return grounded value.
    public bool GetGrounded()
    {
        return grounded;
    }

    public void SetMaxVelocity(float max, float maxY=1.0f)
    {
        maxVelocity = max;
        maxVelocityY = maxY;
    }

    public void SetHeight(float max)
    {
        height = max;
    }
}
