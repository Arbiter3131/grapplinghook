using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    // This should have the behaviour of the enemy, the AI decisions, and should
    // for example, for the mine, this should be inherited as a minebehaviour
    // and keep track of the collider to detect if the player enters
    protected EnemyController controller;



    protected void Start()
    {
        controller = transform.GetComponent<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Death()
    {
        
    }
}
