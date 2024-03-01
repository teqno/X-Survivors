using UnityEngine;
using UnityEngine.UI;

public class XPBar : MonoBehaviour
{
    public Image xpBarFill;
    public Player player;

    void Update()
    {
        xpBarFill.fillAmount = player.xp / (float) player.maxXp;    
    }
}
