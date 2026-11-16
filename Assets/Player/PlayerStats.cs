using UnityEngine;

public class PlayerStats : BaseStats
{
    PlayerController pController;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        hitPoints = 3;
        pController = transform.GetComponent<PlayerController>();
    }

    public override void TakeDamage(int damageTaken)
    {
        if(!dead)
        {
            pController.UpdateHPUI(damageTaken);
        }
        base.TakeDamage(damageTaken);
    }
}
