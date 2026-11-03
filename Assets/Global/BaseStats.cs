using UnityEngine;

public class BaseStats : MonoBehaviour
{
    // This should be inherited by both Player and Enemies, this will contain the functions to be called upon death, and store statistics.
    // This will store HP, decided to keep the damage dealt in the slashhandler and the enemy behaviours.

    protected int hitPoints = 1;

    protected int damageDealt = 0;

    protected bool invincible = false;

    BaseController controller;

    bool dead = false;

    void Start()
    {
        controller = transform.GetComponent<BaseController>();
    }

    void Update()
    {
        
    }

    public void TakeDamage(int damageTaken)
    {
        if(!invincible)
        {
            hitPoints -= damageTaken;
        }
        if(hitPoints <= 0 && !dead)
        {
            dead = true;
            controller.DeathBehaviour();
        }
    }


}
