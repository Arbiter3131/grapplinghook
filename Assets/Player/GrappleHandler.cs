using UnityEngine;

public class GrappleHandler : MonoBehaviour
{
    // Maximum Length of the Grappling Hook (how far the player can grapple away from)
    float maxLength = 30f;

    // Current Length of the grappling hook.
    float length;

    // If we are currently grappled to something. False by default.
    bool isGrappled = false;
    // Where we are grappled to.
    Vector3 grapplePosition;

    // Speed we want to pull the player towards the grappling hook.
    // This may be kept or not, initial intention was for the grappling hook
    // to just be used to change directions and swing.
    // though the pulling towards is pretty fun so I might just keep it.
    // float pullStrength = 0.5f;

    // player controller
    PlayerController controller;
    LayerMask solidMask;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = transform.GetComponent<PlayerController>();
        solidMask = LayerMask.GetMask("Grappleable","NonGrappleable");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isGrappled)
        {
            GrapplePhysics();
        }
    }

    // Called on rightclick, if the call has the player pressing rightclick, then attempt grapple.
    // if its a release of the right mouse button, then remove the grapple.
    // we also toggle air resist so that the player doesn't lose speed while in the air with the grapple.
    // makes it feel a little better to use.
    public void Grapple(bool isClick)
    {
        if(isClick)
        {
            RaycastHit hit;
            Vector3 dir = controller.GetCameraDir();
            if(Physics.Raycast(transform.position, dir*maxLength, out hit, maxLength, solidMask))
            {
                // make sure its a grappleable surface, if not, then don't do anything.
                if(hit.transform.gameObject.layer == 3)
                {
                    // UnityEngine.Debug.Log(hit.transform.gameObject);
                    isGrappled = true;
                    grapplePosition = hit.point;
                    length = hit.distance;
                    controller.ToggleAirResist();
                }
            }
        }
        else
        {
            // release the current grapple when rightclick is released, also turn air resistance back on.
            // only toggle air resist if we were in a successful grapple.
            if(isGrappled)
            {
                controller.ToggleAirResist();
            }
            isGrappled = false;
        }
    }

    // method to cancel the current grapple, in the case that the player attempts to slash whilst grappling.
    public void CancelGrapple()
    {
        if(isGrappled)
        {
            controller.ToggleAirResist();
        }
        isGrappled = false;
    }

    // for now, just accelerate the player towards the grappled points until released.
    // might actually keep it that way, seems pretty fun. Original intention was for
    // the grappling hook to just provide a quick way for the player to rotate
    // and use it's force constraints to swing themselves.
    // but the pull towards the grapple point seems pretty fun as well.
    // will need more testing to determine.
    // This has been deprecated, if you want to test the old physics it has been left here.
    // void GrapplePhysics()
    // {
    //     Vector3 grappleVector = grapplePosition - transform.position;
    //     Debug.DrawRay(transform.position, grappleVector*maxLength, Color.red);
    //     if(grappleVector.magnitude > maxLength)
    //     {
    //         controller.ChangeVelocity(grappleVector.normalized * pullStrength * 3f);
    //     }
    //     else
    //     {
    //         controller.ChangeVelocity(grappleVector.normalized * pullStrength);
    //     }
        
    // }

    // get the current distance between the grapplePosition and the players current position.
    // get their current velocity.
    // project their current velocity onto the grapplevector to get how much they're moving away from the grapple point.
    // get the dot product between the grapple vector and the velocity they are moving away from the grapple point.
    // this allows us to get their current direction of movement, towards or away from the grapple point.
    // then if the dot product is < 0, it means that we are moving away from the grapple point.
    // if we are over the length at which we shot the grapple, cancel out the movement in the direction away from the grapple.
    // this gives us swinging physics.
    // Currently, we can still manage to get past the current length by swinging really fast in circles around objects
    // or just continuously moving away from the grapple point.
    // it doesn't serve much use in actual gameplay though, so I might just leave it in.
    void GrapplePhysics()
    {
        Vector3 grappleVector = grapplePosition - transform.position;
        Vector3 currVeloc = controller.GetCurrentVelocity();
        Vector3 velocAwayFromGrapple = Vector3.Project(currVeloc, grappleVector);
        float dotProduct = Vector3.Dot(grappleVector, velocAwayFromGrapple);
        Debug.DrawRay(transform.position, grappleVector*length, Color.red);
        if(grappleVector.magnitude > length && dotProduct < 0)
        {
            controller.ChangeVelocity(velocAwayFromGrapple * -1);
        }
    }
}
