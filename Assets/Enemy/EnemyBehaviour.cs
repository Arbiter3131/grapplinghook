using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    // This should have the behaviour of the enemy, the AI decisions, and should
    // for example, for the mine, this should be inherited as a minebehaviour
    // and keep track of the collider to detect if the player enters
    // and then it should start a timer and explode
    protected EnemyController controller;



    protected void Start()
    {
        controller = transform.GetComponent<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
