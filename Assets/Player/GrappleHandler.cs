using UnityEngine;

public class GrappleHandler : MonoBehaviour
{
    // Maximum Length of the Grappling Hook (how far the player can grapple away from)
    float maxLength = 15f;

    // If we are currently grappled to something. False by default.
    bool isGrappled = false;
    // Where we are grappled to.
    Vector3 grapplePosition;

    // Speed we want to pull the player towards the grappling hook.
    // This may be kept or not, initial intention was for the grappling hook
    // to just be used to change directions and swing.
    // though the pulling towards is pretty fun so I might just keep it.
    float pullStrength = 0.5f;

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
                    UnityEngine.Debug.Log(hit.transform.gameObject);
                    isGrappled = true;
                    grapplePosition = hit.point;
                    controller.ToggleAirResist();
                }
            }
            else
            {
                // not grappled on a miss (skill issue).
                isGrappled = false;
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

    // for now, just accelerate the player towards the grappled points until released.
    // might actually keep it that way, seems pretty fun. Original intention was for
    // the grappling hook to just provide a quick way for the player to rotate
    // and use it's force constraints to swing themselves.
    // but the pull towards the grapple point seems pretty fun as well.
    // will need more testing to determine.
    // decided to do a combination of the two, the pull strength is lower, but when beyond the max distance
    // the pull strength increases.
    void GrapplePhysics()
    {
        Vector3 grappleVector = grapplePosition - transform.position;
        Debug.DrawRay(transform.position, grappleVector*maxLength, Color.red);
        if(grappleVector.magnitude > maxLength)
        {
            controller.ChangeVelocity(grappleVector.normalized * pullStrength * 3f);
        }
        else
        {
            controller.ChangeVelocity(grappleVector.normalized * pullStrength);
        }
        
    }
}
