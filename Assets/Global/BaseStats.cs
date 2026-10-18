using UnityEngine;

public class BaseStats : MonoBehaviour
{
    // This should be inherited by both Player and Enemies, this will contain the functions to be called upon death, and store statistics.
    // This will store HP and damage, takedamage and dealdamage functions.

    protected int hitPoints = 1;

    protected int damageDealt = 0;

    protected bool invincible = false;

    void Update()
    {
        if(hitPoints <= 0)
        {
            DeathBehaviour();
        }
    }


    // to be implemented by inherited classes. This should determine if the enemy just simply dies, or in the case of the mine, the mine explodes shortly after death/contact.
    protected virtual void DeathBehaviour()
    {
        
    }

    public void TakeDamage(int damageTaken)
    {
        if(!invincible)
        {
            hitPoints -= damageTaken;
        }
    }


}
