using UnityEngine;

public class PursuantStats : BaseStats
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        hitPoints = 3;
    }
}
