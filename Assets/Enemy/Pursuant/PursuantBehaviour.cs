using UnityEngine;

public class PursuantBehaviour : EnemyBehaviour
{
    // This should have the behaviour of the enemy, the AI decisions, and should
    // for example, for the mine, this should be inherited as a minebehaviour
    // and keep track of the collider to detect if the player enters

    // This is the Pursuant, an enemy that, when activated, follows a direct path to the player, albeit slowly.
    // Similar to the Waxen's bullets, but can go through walls and turns slower.

    // This has been changed, it is now very fast and tracks the player better.
    // It is an enemy to try to avoid, it will die shortly after spotting the player.
    GameObject player;

    Vector3 playerPos;

    // Speed of the Pursuant.
    float speed = 0.5f;

    // how much we should track the player, aka how slow the projectile turns.
    float trackSpeed = 0.06f;

    // current direction and speed
    Vector3 moveVector = new Vector3(0f,0f,0f);

    // spotted, if the player has entered the Pursuant's range, so we dont end up just moving towards them across the map.
    bool spotted = false;

    // max spot range
    float maxRange = 20.0f;

    float timer;

    float deathTimer = 10.0f;

    Collider collider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    new void Start()
    {
        collider = transform.GetComponent<Collider>();
        player = GameObject.FindWithTag("Player");
        if(player == null)
        {
            UnityEngine.Debug.Log("Cannot find player!!");
        }
        playerPos = player.transform.position;
        moveVector = (playerPos - transform.position).normalized;
    }

    void FixedUpdate()
    {
        playerPos = player.transform.position;
        if(spotted)
        {
            SlowTrack();
            Move();
            TimedDeath();
        }
        else
        {
            CheckForPlayer();
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if(collision.transform.gameObject.tag != "Pursuant")
        {
            BaseStats otherStats = collision.transform.GetComponent<BaseStats>();
            if(otherStats != null)
            {
                otherStats.TakeDamage(3);
            }
        }
    }

    void CheckForPlayer()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, playerPos - transform.position, out hit, maxRange))
        {
            if(hit.transform.gameObject.tag == "Player")
            {
                spotted = true;
            }
            else
            {
                spotted = false;
            }
        }
    }

    // find the difference between the direction we are going and we should be going, then multiply that by an amount to
    // not instantly turn, then add to movevector.
    void SlowTrack()
    {
        Vector3 dir = (playerPos - transform.position).normalized;
        Vector3 trackVector = dir - moveVector;
        moveVector += trackVector * trackSpeed;
    }

    void Move()
    {
        transform.position = transform.position + moveVector * speed;
    }
    
    public override void Death()
    {
        Destroy(transform.gameObject);
    }

    void TimedDeath()
    {
        if(timer == null)
        {
            timer = Time.time;
        }
        if(Time.time > timer + deathTimer)
        {
            Death();
        }
    }
}
