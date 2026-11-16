using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private GameObject[] hitPointDisplay;

    [SerializeField] private GameObject slashIcon;

    [SerializeField] private GameObject slashCharging;

    float maxRechargeWidth = 100;

    float maxRechargeLength = 100;

    int currentHPDisplay = 2;

    public void UpdateHP(int damage)
    {
        for(int i = 0; i < damage; i++)
        {
            if(currentHPDisplay < 0)
            {
                return;
            }
            hitPointDisplay[currentHPDisplay].GetComponent<Image>().enabled = false;
            currentHPDisplay -= 1;
        }
    }

    public void UpdateFullCharge(bool show)
    {
        slashIcon.GetComponent<Image>().enabled = show;
    }

    public void UpdateSlashRecharge(float time)
    {
        slashCharging.GetComponent<RectTransform>().sizeDelta = new Vector2(maxRechargeWidth * time, maxRechargeLength * time);
    }
}
