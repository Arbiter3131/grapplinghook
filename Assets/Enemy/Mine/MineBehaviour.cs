using UnityEngine;

public class MineBehaviour : EnemyBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // controller inherited from enemybehaviour as access to enemycontroller.

    Collider collider;

    ParticleSystem particleSystem;

    new void Start()
    {
        base.Start();
        collider = transform.GetComponent<Collider>();
        particleSystem = transform.Find("Explosion").GetComponent<ParticleSystem>();
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
        controller.CallPlaySound("explosion");
        particleSystem.Play();
        Destroy(transform.gameObject, 0.5f);
    }
}
