using UnityEngine;

public class PursuantBehaviour : EnemyBehaviour
{
    // This should have the behaviour of the enemy, the AI decisions, and should
    // for example, for the mine, this should be inherited as a minebehaviour
    // and keep track of the collider to detect if the player enters

    // This is the Pursuant, an enemy that, when activated, follows a direct path to the player, albeit slowly.
    // Similar to the Waxen's bullets, but can go through walls and turns slower.
    GameObject player;

    Vector3 playerPos;

    // Speed of the Pursuant.
    float speed = 0.1f;

    // how much we should track the player, aka how slow the projectile turns.
    float trackSpeed = 0.05f;

    // current direction and speed
    Vector3 moveVector = new Vector3(0f,0f,0f);

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
        SlowTrack();
        Move();
    }

    void OnTriggerEnter(Collider collision)
    {
        if(collision.transform.gameObject.tag != "Pursuant")
        {
            BaseStats otherStats = collision.transform.GetComponent<BaseStats>();
            if(otherStats != null)
            {
                otherStats.TakeDamage(1);
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
}
