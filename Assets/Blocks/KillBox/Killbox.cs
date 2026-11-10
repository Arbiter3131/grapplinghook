using UnityEngine;

public class Killbox : MonoBehaviour
{
    // kill anything that falls off the map.
    void OnTriggerEnter(Collider collision)
    {
        BaseStats otherStats = collision.transform.GetComponent<BaseStats>();
        if(otherStats != null)
        {
            otherStats.TakeDamage(99);
        }
    }
}
