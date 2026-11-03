using UnityEngine;

public class EnemyController : BaseController
{
    // Enemy controller inherits from BaseController, this is made just in case theres
    // any behaviour that can be duplicated between all enemies.


    EnemyBehaviour behaviour;

    protected override void Start()
    {
        base.Start();
        behaviour = transform.GetComponent<EnemyBehaviour>();
    }

    // called by basestats when hp is depleted.
    // this should disable all the functions wi
    public override void DeathBehaviour()
    {
        
    }
}
