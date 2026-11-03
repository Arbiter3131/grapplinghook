using UnityEngine;

public class WaxenBehaviour : EnemyBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // controller inherited from enemybehaviour as access to enemycontroller.
    GameObject player;

    // link to a projectile prefab that will spawn when firing.
    public GameObject projectile;

    // how far we should spawn the projectile in front to avoid hitting itself immediately.
    // keeping the self-damage in the game because it could enable some funny scenarios where you guide their bullets
    // back into them.
    float spawnDist = 1.0f;

    // how long it takes the waxen to charge its attack when seeing the player.
    float chargeTime = 3.0f;

    // max range that we can spot the player.
    float maxRange = 50.0f;

    // are we currently charging an attack
    bool isCharging = false;

    // timer for charging
    float timer;

    Vector3 playerPos;

    new void Start()
    {
        base.Start();
        player = GameObject.FindWithTag("Player");
        if(player == null)
        {
            UnityEngine.Debug.Log("Cannot find player!!");
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        playerPos = player.transform.position;
        
        if(CheckForPlayer())
        {
            AttackBehaviour();
        }
    }


    bool CheckForPlayer()
    {
        if(CheckInRange())
        {
            return CheckIsSpotted();
        }
        return false;
    }

    bool CheckInRange()
    {
        if((transform.position - playerPos).magnitude < maxRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool CheckIsSpotted()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, playerPos - transform.position, out hit, maxRange))
        {
            if(hit.transform.gameObject.tag == "Player")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    void AttackBehaviour()
    {
        if(!isCharging)
        {
            timer = Time.time;
            isCharging = true;
        }
        if(Time.time - timer >= chargeTime)
        {
            Instantiate(projectile, transform.position + (playerPos - transform.position).normalized * spawnDist, Quaternion.identity);
            isCharging = false;
        }
    }

    public override void Death()
    {
        Destroy(transform.gameObject);
    }
}
