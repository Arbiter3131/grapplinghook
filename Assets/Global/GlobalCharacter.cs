using UnityEngine;

public class GlobalCharacter : MonoBehaviour
{
    // This should be inherited by both player and enemy, whatever needs to be stored between all characters will be here.

    protected int healthPoints = 10;
    protected GameObject parentPawn;
    protected Vector3 currentLocation;

    protected Rigidbody rigidBody;

    protected bool grounded;

    protected float height = 1.0f;


    void Start()
    {
        parentPawn = transform.gameObject;
        rigidBody = transform.GetComponent<Rigidbody>();
    }

    void Update()
    {
        currentLocation = transform.position;
    }

    private void CheckGrounded()
    {
        RaycastHit hit;
        if(Physics.Raycast(currentLocation, -Vector3.up, out hit, height))
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
