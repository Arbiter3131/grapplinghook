using UnityEngine;

public class EnemyController : BaseController
{
    // Enemy controller inherits from BaseController, this is made just in case theres
    // any behaviour that can be duplicated between all enemies.
    GameObject cameraPos;

    EnemyBehaviour behaviour;

    protected override void Start()
    {
        base.Start();
        behaviour = transform.GetComponent<EnemyBehaviour>();
        cameraPos = GameObject.FindWithTag("MainCamera");
    }

    // called by basestats when hp is depleted.
    // this should disable the behaviour functions.
    public override void DeathBehaviour()
    {
        behaviour.Death();
    }

    // always look at the player, but without looking up or down.
    void Update()
    {
        Vector3 dir = cameraPos.transform.position - transform.position;
        dir.y = 0;
        transform.LookAt(transform.position + dir);
    }
}
