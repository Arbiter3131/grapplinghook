using UnityEngine;

public class BaseMovement : MonoBehaviour
{
    // BaseMovement should keep track of velocities and acceleration, providing the functions to update those and should handle updating the position of the object its on.

    // height is the length of the raycast we should check to see if this character is standing on something. Default is 1.0f.
    Rigidbody rBody;
    bool grounded;
    float height = 1.0f;
    Vector3 position;
    // maxVelocity should have a function to overwrite it.
    float maxVelocity = 5.0f;
    
    void Start()
    {
        rBody = transform.GetComponent<Rigidbody>();
        grounded = false;
        position = transform.position;
    }

    void FixedUpdate()
    {
        position = transform.position;
        ApplyVelocity(new Vector3(0,1,0));
    }

    Vector3 CheckVelocity(Vector3 check)
    {
        Vector3 currentVelocity = rBody.linearVelocity;
        Vector3 resultVelocity = new Vector3(0,0,0);
        if(currentVelocity.x + check.x > maxVelocity)
        {
            resultVelocity.x = maxVelocity;
        }
        else
        {
            resultVelocity.x = currentVelocity.x + check.x;
        }
        if(currentVelocity.y + check.y > maxVelocity)
        {
            resultVelocity.y = maxVelocity;
        }
        else
        {
            resultVelocity.y = currentVelocity.y + check.y;
        }
        if(currentVelocity.y + check.y > maxVelocity)
        {
            resultVelocity.y = maxVelocity;
        }
        else
        {
            resultVelocity.z = currentVelocity.z + check.z;
        }
        return resultVelocity;
    }

    // add to the rigidbodies current velocity.
    public void ApplyVelocity(Vector3 veloc)
    {
        rBody.linearVelocity = CheckVelocity(veloc);
    }

    void CheckGrounded()
    {
        RaycastHit hit;
        if(Physics.Raycast(position, -Vector3.up, out hit, height))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
        // Debug.DrawRay(currentLocation, -Vector3.up * groundheight, Color.yellow);
    }
}
