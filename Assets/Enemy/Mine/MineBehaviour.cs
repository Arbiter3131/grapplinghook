using UnityEngine;

public class MineBehaviour : EnemyBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // controller inherited from enemybehaviour as access to enemycontroller.

    Collider collider;

    void Start()
    {
        base.Start();
        collider = transform.GetComponent<Collider>();
    }

    
    void OnTriggerEnter(Collider collision)
    {
        BaseStats otherStats = collision.transform.GetComponent<BaseStats>();
        if(otherStats != null)
        {
            otherStats.TakeDamage(1);
        }
        controller.DeathBehaviour();
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
