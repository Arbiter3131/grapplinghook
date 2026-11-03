using UnityEngine;

public class WaxenProjectile : EnemyBehaviour
{

    GameObject player;

    Vector3 playerPos;

    // Projectile speed.
    float speed = 0.2f;

    // how much we should track the player, aka how slow the projectile turns.
    float trackSpeed = 0.1f;

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
        if(collision.transform.gameObject.tag != "Bullet")
        {
            BaseStats otherStats = collision.transform.GetComponent<BaseStats>();
            if(otherStats != null)
            {
                otherStats.TakeDamage(1);
            }
            Death();
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
