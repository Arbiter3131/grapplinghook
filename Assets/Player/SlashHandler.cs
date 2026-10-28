using UnityEngine;

public class SlashHandler : MonoBehaviour
{
    // this will handle player slash/dash, dealing damage should be handled when enemies are added.
    private PlayerController controller;

    // layermask for solid walls, we should ignore enemies when checking for obstructions.
    LayerMask solidMask;
    
    // the maximum distance the player should teleport when slashing.
    float slashDistance = 8.0f;

    // the velocity in the direction of the slash the player should be set to after the teleport.
    // this'll probably be set to the max velocity that the player can move.
    float slashVelocity;

    // the time in seconds that it takes for the slash to recharge after use.
    float rechargeTime = 5.0f;

    // maximum time the player can charge the slash, the longer the player holds it, the further they will go.
    float maxCharge = 2.0f;

    // if the slash is charged or not.
    bool isCharged = true;

    // tracking player input.
    bool pressed;

    // used to check the time the player charged the slash, and the time that the player released the slash
    // for recharging the slash.
    float timer;

    void Start()
    {
        controller = transform.GetComponent<PlayerController>();
        solidMask = LayerMask.GetMask("Grappleable","NonGrappleable");
        slashVelocity = controller.GetMaxVelocity();
    }

    void FixedUpdate()
    {
        if(!isCharged)
        {
            if(Time.time - timer > rechargeTime)
            {
                isCharged = true;
            }
        }
    }

    // take input from the player, set pressed to track it.
    public void SlashInput(bool isClicked)
    {
        if(isCharged)
        {
            if(isClicked)
            {
                ChargeSlash();
            }
            else
            {
                Slash();
            }
        }
    }

    // LMB is held, check if it's recharged or not, start the slash if its charged.
    void ChargeSlash()
    {
        timer = Time.time;
    }

    // if the player is currently grappling, then cancel the current grapple.
    // get the camera direction, and the distance we should teleport depending on how long the player held the charge timer.
    // if the player held for longer than 2 seconds, then use mathf.min to return the max charge value.
    // then check for obstructions, if there is an obstruction, then we will only teleport beside it.
    // then set the velocity of the player to the slashVelocity in the direction that the camera is facing.
    void Slash()
    {
        // UnityEngine.Debug.Log("Slashed value " + (Time.time - timer));
        controller.StopGrapple();
        Vector3 dir = controller.GetCameraDir();
        float distance = slashDistance * (Mathf.Min(maxCharge, Time.time - timer)/maxCharge);
        transform.position = CheckObstruction(dir, distance);
        controller.HardSetVelocity(dir * slashVelocity);
        timer = Time.time;
        isCharged = false;
    }

    // check for obstructions to make sure we don't teleport through walls.
    Vector3 CheckObstruction(Vector3 dir, float length)
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, dir, out hit, length, solidMask))
        {
            UnityEngine.Debug.Log(hit.point + hit.normal);
            return hit.point + hit.normal;
        }
        else
        {
            return transform.position + length * dir;
        }
    }

    // create a collider in between original position and new position after slash, damage all enemies
    // located in the collider.
    // will be implemented alongside the enemies.
    void DamageHitbox()
    {
        
    }
}
