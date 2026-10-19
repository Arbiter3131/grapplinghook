using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    // I want this to be the main communication point between all scripts on a character

    // for example, when inputhandler detects input, it'll pass to here, which then the controller will call the specific methods.

    // this should not be directly used, this will just serve as an abstract, as in a guide to implement the specific enemy/player controllers.
    
    // probably call some gravity functions here.

    // ScriptModuleName scriptmodulename;

    // void Start()
    // find and assign scriptmodulenames
    // scriptmodulename = transform.getComponent<ScriptModuleName>();

    BaseStats baseStats;
    BaseMovement baseMovement;

    protected virtual void Start()
    {
        baseStats = transform.GetComponent<BaseStats>();
        baseMovement = transform.GetComponent<BaseMovement>();
    }

    // This would allow other modules such as a grappling hook script to call BaseMovement and modify the velocity.
    public virtual void ChangeVelocity(Vector3 input)
    {
        baseMovement.ApplyVelocity(input);
    }

    public virtual void ChangeStats(int stat)
    {
        baseStats.TakeDamage(stat);
    }

    public virtual bool GetGroundedVal()
    {
        return baseMovement.GetGrounded();
    }

    public virtual Vector3 GetCurrentVelocity()
    {
        return baseMovement.GetCurrentVelocity();
    }
}
