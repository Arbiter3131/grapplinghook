using UnityEngine;

public class MineBehaviour : EnemyBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // controller inherited from enemybehaviour as access to enemycontroller.

    Collider collider;

    new void Start()
    {
        base.Start();
        collider = transform.GetComponent<Collider>();
    }

    
    void OnTriggerEnter(Collider collision)
    {
        BaseStats otherStats = collision.transform.GetComponent<BaseStats>();
        if(otherStats != null)
        {
            otherStats.TakeDamage(3);
        }
        Death();
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Death()
    {
        Destroy(transform.gameObject);
    }
}
